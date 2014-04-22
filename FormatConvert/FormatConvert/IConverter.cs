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
        /// <summary>
        /// Searching in directory for image files (JPEG, PNG, Tiff, GIF, BMP)
        /// </summary>
        /// <param name="directory">directory where to search for imgs</param>
        /// <param name="overWrite">bool var, defining if overwriting is enabled</param>
        /// <param name="skipErrors">bool var, if true, application will skip batch errors such as: some files are not images etc..., if false stop</param>
        /// <param name="outputFormat"></param>
        void StartApp(string directory, bool overWrite, bool skipErrors, FileTypes outputFormat);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listOfFiles">list files to convert</param>
        /// <param name="overWrite">bool var, defining if overwriting is enabled</param>
        /// <param name="skipErrors">bool var, if true, application will skip batch errors such as: some files are not images etc..., if false stop</param>
        /// <param name="outputFormat"></param>
        void StartApp(List<string> listOfFiles, bool overWrite, bool skipErrors, FileTypes outputFormat);

        /// <summary>
        /// load image and return bitmapsource
        /// </summary>
        /// <param name="fileNameAndPath">absolute path to image with name and suffix</param>
        /// <returns>loaded image in BitmapSource</returns>
        BitmapSource LoadImage(string fileNameAndPath);


        /// <summary>
        /// converting method
        /// </summary>
        /// <param name="image">loaded BitmapSource image to convert</param>
        /// <param name="outputFormat"></param>
        /// <param name="quality">if jpeg, compression quality, default for jpeg will be 100, else should be -1</param>
        /// <returns></returns>
        BitmapSource ConvertImageToFormat(BitmapSource image, FileTypes outputFormat, int quality);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">loaded BitmapSource image to resizing</param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="overZoom">if true, ratio > 1 will be used</param>
        /// <returns></returns>
        BitmapSource ResizeImage(BitmapSource image, int newWidth, int newHeight, bool overZoom);

        /// <summary>
        /// method for saving bitmapsource
        /// </summary>
        /// <param name="fileNameAndPath"></param>
        /// <param name="overWrite"></param>
        void SaveImage(string fileNameAndPath, bool overWrite);


        /// <summary>
        /// method for xml log file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="info"></param>
        void WriteToLog(string fileName, string info);



    }
}
