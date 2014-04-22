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
        public FileTypes OutputFormat;
        public bool SkipErrors;
        public bool OverWrite;
        public string Directory;
        public List<string> ListOfFiles;
        public bool LoadingFromDirectory;
        public Action ActualAction;
        public int JpegCompression;
        public bool OverZoom;
        public int NewWidth;
        public int NewHeight;


        public void ProcessAllImages();

        /// <summary>
        /// load image and return bitmapsource
        /// </summary>
        /// <param name="fileNameAndPath">absolute path to image with name and suffix</param>
        /// <returns>loaded image in BitmapSource</returns>
        public BitmapSource LoadImage(string fileNameAndPath);



        /// <summary>
        /// converting method
        /// </summary>
        /// <param name="image">loaded BitmapSource image to convert</param>
        /// <param name="outputFormat"></param>
        /// <param name="quality">if jpeg, compression quality, default for jpeg will be 100, else should be -1</param>
        /// <returns></returns>
        public BitmapSource ConvertImageToFormat(BitmapSource image);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">loaded BitmapSource image to resizing</param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="overZoom">if true, ratio > 1 will be used</param>
        /// <returns></returns>
        public BitmapSource ResizeImage(BitmapSource image);

        /// <summary>
        /// method for saving bitmapsource
        /// </summary>
        /// <param name="fileNameAndPath"></param>
        /// <param name="overWrite"></param>
        public void SaveImage(string fileNameAndPath);


        /// <summary>
        /// method for xml log file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="info"></param>
        public void WriteToLog(string fileName, string info);



    }
}
