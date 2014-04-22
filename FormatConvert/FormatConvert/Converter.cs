using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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




        public void ProcessAllImages()
        {
            if(mLoadingFromDirectory)
            {

            }
            else //loading from list
            {

            }
        }

        public BitmapSource LoadImage(string filename)
        {
            throw new NotImplementedException();
        }

        public BitmapSource ConvertImageToFormat(BitmapSource image)
        {
            throw new NotImplementedException();

        }

        public BitmapSource ResizeImage(BitmapSource image)
        {
            throw new NotImplementedException();

        }

        public void SaveImage(string fileNameAndPath)
        {

        }

        public void WriteToLog(string fileName, string info)
        {

        }
    }
}
