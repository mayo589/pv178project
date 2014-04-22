using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;

//Marek Mihalech

namespace FormatConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            // Open a Stream and decode a JPEG image
            /*Stream imageStreamSource = new FileStream("source/chnagedims.jpg", FileMode.Open, FileAccess.Read, FileShare.Read);
            JpegBitmapDecoder decoder = new JpegBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource bitmapSource = decoder.Frames[0];

        
            Console.WriteLine(bitmapSource.Width); Console.WriteLine(bitmapSource.Height);
            
            int width = 1500;
            int height = 2000;

            double ratioW = width / bitmapSource.Width;
            double ratioH = height / bitmapSource.Height;

            double ratio = ratioW < ratioH ? ratioW : ratioH;
            Console.WriteLine(ratio);
            Console.WriteLine(bitmapSource.DpiX);
            Console.WriteLine(bitmapSource.DpiY);


            FileStream stream = new FileStream("output/chnagedims.png", FileMode.Create);
            PngBitmapEncoder encoder = new PngBitmapEncoder();

            var target = new TransformedBitmap(
                            bitmapSource,
                            new ScaleTransform(
                             (ratio * bitmapSource.Width) / bitmapSource.Width * bitmapSource.DpiX / bitmapSource.DpiX,
                             (ratio * bitmapSource.Height) / bitmapSource.Height * bitmapSource.DpiY / bitmapSource.DpiY,
                                0, 0

                         ));            
            encoder.Interlace = PngInterlaceOption.On;
            encoder.Frames.Add(BitmapFrame.Create(target));
            encoder.Save(stream);

            
            Console.ReadKey();*/



            bool canEnd = false;
            Console.WriteLine(">>>>Welcome in Image Converter!<<<<");

            while (!canEnd)
            {
                /*bool canOverwrite = false;
                bool canSkipErrors = true;*/
                Converter myConv = null;

                bool enteredInvalid;
                /*do
                {
                    Console.WriteLine(">>> Please, select output format. \n1->JPEG\n2->PNG\n3->Tiff\n4->GIF\n5->BMP");
                    string outputFormat = Console.ReadLine();
                    enteredInvalid = false;
                    switch (outputFormat)
                    {
                        case "1":
                            break;
                        case "2":
                            break;
                        case "3":
                            break;
                        case "4":
                            break;
                        case "5":
                            break;
                        default:
                            Console.WriteLine("!!!Sorry, enter only numbers between <1,5>");
                            enteredInvalid = true;
                            break;
                    }
                } while (enteredInvalid);*/


                do
                {
                    Console.WriteLine(">>> Please, select 1 or 2,\n1-> add all files from some directory,\n2-> add files manually");
                    string oneOrTwo = Console.ReadLine();
                    enteredInvalid = false;
                    switch (oneOrTwo)
                    {
                        case "1":
                            Console.WriteLine("Enter directory: ");
                            string dir = Console.ReadLine();
                            myConv = new Converter(dir);
                            break;
                        case "2":
                            Console.WriteLine("Enter names of files with suffix, separated by space: ");
                            string files = Console.ReadLine();
                            List<string> listOfFiles = new List<string>(files.Split(' '));
                            myConv = new Converter(listOfFiles);
                            break;
                        default:
                            Console.WriteLine("!!!That wasn't 1 or 2 .. :D");
                            enteredInvalid = true;
                            break;
                    }
                } while (enteredInvalid);
                //mam vytvorenu instanciu s directory alebo names


                Console.WriteLine("Type 'yes', if you would like to edit settings like overwriting and skipping errors\nDefault is :skipErrors = true, overwrite files = false");
                string stringYes = Console.ReadLine();
                if(stringYes == "yes")
                {
                    bool enteredInvalidYesOrNo = false;
                    do
                    {
                        Console.WriteLine("Type 'yes' or 'no', if you would like to overwrite your files");
                        string yesOrNo = Console.ReadLine();
                        enteredInvalidYesOrNo = false;
                        switch(yesOrNo)
                        {
                            case "yes":
                                myConv.OverWrite = true;
                                Console.WriteLine("Overwriting files is set to true");
                                break;
                             case "no":
                                myConv.OverWrite = false;
                                Console.WriteLine("Overwriting files is set to false");
                                break;
                             default:
                                 Console.WriteLine("!!!That wasn't yes or no .. :D");
                                 enteredInvalidYesOrNo = true;
                                 break;
                        }
                    }while(enteredInvalidYesOrNo);


                    do
                    {
                        Console.WriteLine("Type 'yes' or 'no', if you would like to skip errors");
                        string yesOrNo = Console.ReadLine();
                        enteredInvalidYesOrNo = false;
                        switch (yesOrNo)
                        {
                            case "yes":
                                myConv.SkipErrors = true;
                                Console.WriteLine("Skipping errors is set to true");
                                break;
                            case "no":
                                myConv.SkipErrors = false;
                                Console.WriteLine("Skipping errors is set to false");
                                break;
                            default:
                                Console.WriteLine("!!!That wasn't yes or no .. :D");
                                enteredInvalidYesOrNo = true;
                                break;
                        }
                    } while (enteredInvalidYesOrNo);


                }
                //mam vytvorenu instanciu s directory alebo names && mam nastavene skiperrors a overwrite
                

                do
                {
                    Console.WriteLine("Would you like to convert format, resize images, or both?\n1->only convert\n2->only resize\n3->convert and resize");
                    string oneTwoThree =  Console.ReadLine();
                    enteredInvalid = false;
                    switch (oneTwoThree)
                    {

                        case "1":
                            myConv.ActualAction = Action.Convert;
                            loadSettings(myConv, Action.Convert);
                            break;
                        case "2":
                            myConv.ActualAction = Action.Resize;
                            loadSettings(myConv, Action.Resize);
                            break;
                        case "3":
                            myConv.ActualAction = Action.Both;
                            loadSettings(myConv, Action.Both);
                            break;
                        default:
                            Console.WriteLine("!!!That wasn't 1/2/3 .. :D");
                            enteredInvalid = true;
                            break;
                    }
                }while(enteredInvalid);
                //mam vytvorenu instanciu s directory alebo names && mam nastavene skiperrors a overwrite && nastavenu akciu a vsetko loadnute

                myConv.ProcessAllImages();



            }
        }

        private static void loadSettings(Converter converter, Action action)
        {
            if(action == Action.Convert) //potrebujem format, ak jpeg tak aj kompresiu
            {
                loadFormat(converter);
            }
            else if(action == Action.Resize)
            {
                loadZoomWH(converter);
            }
            else
            {
                loadFormat(converter);
                loadZoomWH(converter);
            }
        }

        private static void loadZoomWH(Converter conv)
        {
            bool enteredInvalid;
            do
            {
                enteredInvalid = false;
                Console.WriteLine(">>> Please, select, if you want to oversize images or not. Type 'yes' or 'no'");
                string yOrNo = Console.ReadLine();
                switch (yOrNo)
                {
                    case "yes":
                        conv.OverZoom = true;
                        break;
                    case "no":
                        conv.OverZoom = false;
                        break;
                    default:
                        Console.WriteLine("!!! That wasn't yes or no");
                        break;
                }
            } while (enteredInvalid);


            Console.WriteLine(">>> Please enter new width and height as two numbers separated by space!!!");
            do
            {
                enteredInvalid = false;
                string twoNumbers = Console.ReadLine();
                string[] numbers = twoNumbers.Split(' ');
                int width;
                int height;

                bool result = Int32.TryParse(numbers[0], out width);
                if (true == result)
                {
                    conv.NewWidth = width;
                }
                else
                {
                    Console.WriteLine("Entered wrong width");
                    conv.NewWidth = -1;
                    enteredInvalid = true;
                }

                bool result2 = Int32.TryParse(numbers[1], out height);
                if (true == result)
                {
                    conv.NewHeight = height;
                }
                else
                {
                    Console.WriteLine("Entered wrong height");
                    conv.NewHeight = -1;
                    enteredInvalid = true;
                }


            } while (enteredInvalid);
        }

        private static void loadFormat(Converter conv)
        {
            bool enteredInvalid;
            do
            {
                Console.WriteLine(">>> Please, select output format. \n1->JPEG\n2->PNG\n3->Tiff\n4->GIF\n5->BMP");
                string oFormat = Console.ReadLine();
                enteredInvalid = false;
                switch (oFormat)
                {
                    case "1":
                        conv.OutputFormat = FileTypes.Jpeg;
                        break;
                    case "2":
                        conv.OutputFormat = FileTypes.Png;
                        break;
                    case "3":
                        conv.OutputFormat = FileTypes.Tiff;
                        break;
                    case "4":
                        conv.OutputFormat = FileTypes.Gif;
                        break;
                    case "5":
                        conv.OutputFormat = FileTypes.Bmp;
                        break;
                    default:
                        Console.WriteLine("!!!Sorry, enter only numbers between <1,5>");
                        enteredInvalid = true;
                        conv.OutputFormat = FileTypes.Invalid;
                        break;
                }
            } while (enteredInvalid);

            if(conv.OutputFormat == FileTypes.Jpeg)
            {
                Console.WriteLine(">>> You entered JPEG as format, please specify compression (0-100%)");
                do
                {
                    string compressionString = Console.ReadLine();
                    int number;
                    bool result = Int32.TryParse(compressionString, out number);
                    if (true == result && number <= 100 && number >= 0)
                    {
                        conv.JpegCompression = number;
                    }
                    else
                    {
                        Console.WriteLine("Entered wrong number");
                        conv.JpegCompression = -1;
                        enteredInvalid = true;
                    }
                } while (enteredInvalid);

            }
        }

    }
}
