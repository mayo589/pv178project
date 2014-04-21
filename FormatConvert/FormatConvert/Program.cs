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
            Stream imageStreamSource = new FileStream("source/Penguins.jpg", FileMode.Open, FileAccess.Read, FileShare.Read);
            JpegBitmapDecoder decoder = new JpegBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource bitmapSource = decoder.Frames[0];

            // Draw the Image
            /*Image myImage = new Image();
            myImage.Source = bitmapSource;
            myImage.Stretch = Stretch.None;
            myImage.Margin = new Thickness(20);

            */

            


           /* int width = 128;
            int height = 128;
            int stride = width;
            byte[] pixels = new byte[height * stride];
            */
            // Define the image palette
           // BitmapPalette myPalette = BitmapPalettes.Halftone256;

            // Creates a new empty image with the pre-defined palette

            /*BitmapSource image = BitmapSource.Create(
                width,
                height,
                96,
                96,
                PixelFormats.Indexed8,
                myPalette,
                pixels,
                stride);*/

            FileStream stream = new FileStream("output/newww.png", FileMode.Create);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            //TextBlock myTextBlock = new TextBlock();
            //myTextBlock.Text = "Codec Author is: " + encoder.CodecInfo.Author.ToString();
            //JpegBitmapEncoder en = new JpegBitmapEncoder(); en.Quality....... na kompresiu
            var target = new TransformedBitmap(
                            bitmapSource,
                            new ScaleTransform(
                               100 / bitmapSource.Width * 96 / bitmapSource.DpiX,
                                100/ bitmapSource.Height * 96 / bitmapSource.DpiY,
                                0, 0

                         ));
            //bitmapSource.Height = 100;

            encoder.Interlace = PngInterlaceOption.On;
            encoder.Frames.Add(BitmapFrame.Create(target));
            encoder.Save(stream);




            

        }
    }
}
