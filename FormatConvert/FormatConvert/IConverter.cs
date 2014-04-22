using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

//Marek Mihalech

namespace FormatConvert
{
    interface IConverter
    {
        void StartApp(string directory, bool overWrite);
        void StartApp(List<string> listOfFiles, bool overWrite);

        BitmapSource LoadImage(string fileNameAndPath);
        BitmapSource SaveImage(string fileNameAndPath, bool overWrite);

        BitmapSource ConvertImagesToFormat(BitmapSource image, FileTypes outputFormat);
        BitmapSource ResizeImage(BitmapSource image, int newWidth, int newHeight, bool overZoom);
        
        void WriteToLog(string fileName, string info);



    }
}
