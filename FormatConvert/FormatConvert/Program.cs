using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Drawing.Imaging.EncoderParameter;


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




            int width = 128;
            int height = 128;
            int stride = width;
            byte[] pixels = new byte[height * stride];

            // Define the image palette
            BitmapPalette myPalette = BitmapPalettes.Halftone256;

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

            FileStream stream = new FileStream("output/new.png", FileMode.Create);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            //TextBlock myTextBlock = new TextBlock();
            //myTextBlock.Text = "Codec Author is: " + encoder.CodecInfo.Author.ToString();
            encoder.Interlace = PngInterlaceOption.On;
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.Save(stream);

            EncoderParameters myEncoderParameters = new EncoderParameters(1);


        }
    }
}
