using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

//Marek Mihalech

namespace FormatConvert
{
    public interface IConverter
    {
        FileTypes OutputFormat { get; set; }
        bool SkipErrors { get; set; }
        bool OverWrite { get; set; }
        //string Directory { get; set; }
        //List<string> ListOfFiles { get; set; }
        //bool LoadingFromDirectory { get; set; }
        Action ActualAction { get; set; }
        int JpegCompression { get; set; }
        bool OverZoom { get; set; }
        int NewWidth { get; set; }
        int NewHeight { get; set; }


        void ProcessAllImages();


        BitmapSource LoadImage(string filename);


        //BitmapSource ConvertImageToFormat(BitmapSource image);


        //BitmapSource ResizeImage(BitmapSource image);


        //void SaveImage(string fileNameAndPath);


        void WriteToLog(string fileName, string info);



    }
}
