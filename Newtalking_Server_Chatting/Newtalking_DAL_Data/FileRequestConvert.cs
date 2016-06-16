using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Newtalking_DAL_Data
{
    static public class FileRequestConvert
    {
        static public FileRequest ConvertToClass_Send(byte[] data)
        {
            FileRequest fileRequest = new FileRequest();
            fileRequest.Uid = BitConverter.ToInt32(data, 2);
            fileRequest.User_id = BitConverter.ToInt32(data, 6);

            string temp = Encoding.Default.GetString(data, 10, 255);
            foreach (char c in temp)
            {
                if (c == '\0')
                    break;
                fileRequest.FileName += c;
            }

            fileRequest.FileKey = Encoding.Default.GetString(data, 265, 16);
            return fileRequest;
        }

        static public ReceiveFileRequest ConvertToData_Receive(byte[] data)
        {
            ReceiveFileRequest receiveFile = new ReceiveFileRequest();
            receiveFile.User_id = BitConverter.ToInt32(data, 2);
            receiveFile.File_length = BitConverter.ToInt16(data, 6);
            receiveFile.File_name_length = BitConverter.ToInt16(data, 10);
            receiveFile.File_name = Encoding.Default.GetString(data, 12, receiveFile.File_name_length);
            return receiveFile;
        }
    }
}
