using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormatConvert
{
    interface IConverter
    {
        void ConvertImagesToFormat(string directory, FileTypes type);
        void ConvertImagesToFormat(List<string> listOfFiles, FileTypes type);
        

    }
}
