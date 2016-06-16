using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace File_DAL
{
    public static class GetFileInfo
    {
        public static int GetLength(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            return (int)fileInfo.Length;
        }
    }
}
