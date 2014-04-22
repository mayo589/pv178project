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
            Stream imageStreamSource = new FileStream("source/chnagedims.jpg", FileMode.Open, FileAccess.Read, FileShare.Read);
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

            
            Console.ReadKey();

        }
    }
}
