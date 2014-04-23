using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;

namespace FormatConvert
{
    class Converter : IConverter
    {
        
        private string mDirectory;
        private List<string> mListOfFiles = new List<string>();
        private bool mLoadingFromDirectory;
        private bool mSkipErrors = true;
        private bool mOverWrite = false;
        private Action mActualAction;

        private FileTypes mOutputFormat;
        private int mJpegCompression = 100;

        private bool mOverZoom = false;
        private int mNewWidth;
        private int mNewHeight;

        private int mDirInt = 0;
        private bool mCreatedOutputDir = false;

        public Action ActualAction
        {
            get { return mActualAction; }
            set { mActualAction = value; }
        }

        public bool SkipErrors
        {
            get { return mSkipErrors; }
            set { mSkipErrors = value; }
        }

        public bool OverWrite
        {
            get { return mOverWrite; }
            set { mOverWrite = value; }
        }

        public bool OverZoom
        {
            get { return mOverZoom; }
            set { mOverZoom = value; }
        }

        public int NewWidth
        {
            get { return mNewWidth; }
            set { mNewWidth = value; }
        }

        public int NewHeight
        {
            get { return mNewHeight; }
            set { mNewHeight = value; }
        }

        public int JpegCompression
        {
            get { return mJpegCompression; }
            set { mJpegCompression = value; }
        }

        public FileTypes OutputFormat
        {
            get { return mOutputFormat; }
            set { mOutputFormat = value; }
        }

        

        public Converter(string directory)
        {
            mDirectory = directory;
            mLoadingFromDirectory = true;
        }

        public Converter(List<string> listOfFiles)
        {
            mListOfFiles = listOfFiles;
            mLoadingFromDirectory = false;
        }




        public void ProcessAllImages() //presunut do program cs? pre lepisu pracu s kniznicou
        {
            if(mLoadingFromDirectory)
            {
                string[] filePaths = Directory.GetFiles(@mDirectory);
                for (int i = 0; i < filePaths.Length; i++ )
                {
                    BitmapSource loadedImage = LoadImage(filePaths[i]);
                    if(loadedImage == null && mSkipErrors == false)
                    {
                        Console.WriteLine("Stopping program, skip errors is set to false!");
                        return;
                    }
                    else if (loadedImage == null && mSkipErrors == true)
                    {
                        Console.WriteLine("Program is going on, skiperros is set on true");
                        continue;
                    }

                    BitmapEncoder processedImage = null;

                    bool result = processAction(loadedImage, out processedImage, filePaths[i]);
                    if (result)
                    {
                        SaveImage(processedImage, filePaths[i]);
                    }
                    Console.WriteLine("Processing completed : {0}%", ((i * 100) / filePaths.Length));
                }
            }
            else //loading from list
            {
                for(int i = 0; i < mListOfFiles.Count; i++)
                {
                    BitmapSource loadedImage = LoadImage(mListOfFiles[i]);
                    if (loadedImage == null && mSkipErrors == false)
                    {
                        Console.WriteLine("Stopping program, skip errors is set to false!");
                        return;
                    }
                    else if (loadedImage == null && mSkipErrors == true)
                    {
                        Console.WriteLine("Program is going on, skiperros is set on true");
                        continue;
                    }
                    BitmapEncoder encoderImage = null;

                    bool result = processAction(loadedImage, out encoderImage, mListOfFiles[i]);
                    if (result)
                    {
                        string combined = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(mListOfFiles[i]));
                        SaveImage(encoderImage, combined);
                    }
                    Console.WriteLine("Processing completed : {0}%", ((i * 100) / mListOfFiles.Count));
                }
            }

            Console.WriteLine("Completed!!!!!");
        }

        private bool processAction(BitmapSource loadedImage, out BitmapEncoder encoderImage, string filename)
        {
            if (mActualAction == Action.Convert)
            {
                encoderImage = MakeEncoder();
                encoderImage.Frames.Add(BitmapFrame.Create(loadedImage));

            }
            else if (mActualAction == Action.Resize)
            {

                //encoderImage = ResizeImage(loadedImage);
                string suffix = Path.GetExtension(filename).ToLower();
                switch (suffix)
                {
                        case ".bmp":
                            encoderImage = new BmpBitmapEncoder();
                            break;
                        case ".gif":
                            encoderImage = new GifBitmapEncoder();
                            break;
                        case ".jpeg":
                            encoderImage = new JpegBitmapEncoder();
                            break;
                        case ".png":
                            encoderImage = new PngBitmapEncoder();
                            break;
                        case ".tiff":
                            encoderImage = new TiffBitmapEncoder();
                            break;
                        default: // illegal suffix
                            Console.WriteLine("Illegal image suffix... ");
                            encoderImage = null;
                            return false;
                }   

                TransformedBitmap trbm = ResizeImage(loadedImage);
                if (trbm != null)
                {
                    encoderImage.Frames.Add(BitmapFrame.Create(trbm));
                }
                else
                {
                    return false;
                }
                //encoderImage = new 
            }
            else //BOTH
            {

                encoderImage = MakeEncoder();
                TransformedBitmap trbm = ResizeImage(loadedImage);
                if (trbm != null)
                {
                    encoderImage.Frames.Add(BitmapFrame.Create(trbm));
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public BitmapSource LoadImage(string filename)
        {
            Stream imageStreamSource = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            //need to export suffix from filename
            //filename.EndsWith(".jpeg", StringComparison.Ordinal);
            string suffix = Path.GetExtension(filename).ToLower();

            BitmapDecoder decoder = null;

            try
            {
                switch (suffix)
                {

                    case ".bmp":
                        decoder = new BmpBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                        break;
                    case ".gif":
                        decoder = new GifBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                        break;
                    case ".jpeg":
                        decoder = new JpegBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                        break;
                    case ".png":
                        decoder = new PngBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                        break;
                    case ".tiff":
                        decoder = new TiffBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                        break;
                    default: // illegal suffix
                        Console.WriteLine("Illegal image suffix... ");
                        return null;
                }
            }catch(FileFormatException)
            {
                Console.WriteLine("File is not image... ");
                return null;
            }
            //JpegBitmapDecoder decoder = new JpegBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource bitmapSource = decoder.Frames[0];
            return bitmapSource;
        }

        public BitmapEncoder MakeEncoder()
        {
            BitmapEncoder encoder = null;
            switch(mOutputFormat)
            {
                case FileTypes.Bmp:
                    encoder = new BmpBitmapEncoder();
                    break;
                case FileTypes.Gif:
                    encoder = new GifBitmapEncoder();
                    break;
                case FileTypes.Jpeg:
                    encoder = new JpegBitmapEncoder();
                    break;
                case FileTypes.Png:
                    encoder = new PngBitmapEncoder();
                    break;
                case FileTypes.Tiff:
                    encoder = new TiffBitmapEncoder();
                    break;
                default:
                    encoder = null; // never?
                    return encoder;
            }
            return encoder;
        }

        public TransformedBitmap ResizeImage(BitmapSource image)
        {

            double ratioW = mNewWidth / image.Width;
            double ratioH = mNewHeight / image.Height;

            double ratio = ratioW < ratioH ? ratioW : ratioH;
            TransformedBitmap target = null; ;

            if (mOverZoom)
            {
                 target = new TransformedBitmap(
                                image,
                                new ScaleTransform(
                                 (ratio * image.Width) / image.Width * image.DpiX / image.DpiX,
                                 (ratio * image.Height) / image.Height * image.DpiY / image.DpiY,
                                    0, 0

                             ));
            }
            else if(!mOverZoom && ratio > 1)
            {
                Console.WriteLine("Cannot make bigger image, overZoom is set to false, skipping");
                return null;
            }
            return target;
        }

        public void SaveImage(BitmapEncoder encoder, string fileNameAndPath)
        { // nezabudnut na jpeg kompresiu

            

            if (encoder.GetType() == typeof(JpegBitmapEncoder))
            {
                JpegBitmapEncoder newEncoder = (JpegBitmapEncoder) encoder;
                newEncoder.QualityLevel = mJpegCompression;
                FileStream stream = getStream(fileNameAndPath);
                newEncoder.Save(stream);
            }
            else
            {
                FileStream stream = getStream(fileNameAndPath);
                encoder.Save(stream);
            }

           // FileStream stream = new FileStream("output/chnagedims.png", FileMode.Create);
           // encoder.Save(stream);
        }

        private FileStream getStream(string fileNameAndPath)
        {//moverwrite = false -> dam do novej zlozky output, moverwrite = true, prepisem
            if(mOverWrite) 
            {
                string changedSuffix = Path.ChangeExtension(fileNameAndPath, "." + FileTypeToString(mOutputFormat));
                FileStream stream = new FileStream(changedSuffix, FileMode.Create);
                return stream;
            }
            else
            {
                //bool successful = false;
                int i = 0;
                while (mCreatedOutputDir == false) 
                {
                    if (!Directory.Exists("output-" + i))
                    {
                        DirectoryInfo di = Directory.CreateDirectory("output-" + i);
                        mCreatedOutputDir = true;
                        mDirInt = i;
                    }
                    else
                    {
                        i++;
                    }
                }

                string name = Path.GetFileNameWithoutExtension(fileNameAndPath);
                FileStream stream = new FileStream("output-" + mDirInt + "/" + name + "." + FileTypeToString(mOutputFormat), FileMode.Create);
                return stream;

            }
        }

        private string FileTypeToString(FileTypes type)
        {
            switch(type)
            {
                case FileTypes.Bmp:
                    return "bmp";
                case FileTypes.Gif:
                    return "gif";
                case FileTypes.Jpeg:
                    return "jpeg";
                case FileTypes.Png:
                    return "png";
                case FileTypes.Tiff:
                    return "tiff";
                default:
                    return "";
            }
        }

        public void WriteToLog(string fileName, string info)
        {

        }
    }
}
