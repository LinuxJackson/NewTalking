using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using File_DAL;
using Newtalking_DAL_Server;
using Newtalking_DAL_Data;
using Model;

namespace Newtalking_BLL_Server.File
{
    internal class ReceiveFile
    {
        ReceiveFileRequest rfr = new ReceiveFileRequest();
        System.Net.Sockets.TcpClient remoteClient;

        internal ReceiveFile(DataPackage data)
        {
            rfr = FileRequestConvert.ConvertToData_Receive(data.Data);
            remoteClient = data.Client;
        }

        internal bool Receive()
        {
            Newtalking_DAL_Server.ReceiveFile rece = new Newtalking_DAL_Server.ReceiveFile(remoteClient);
            FileCheck fileCheck = new FileCheck();
            string[] strs = fileCheck.CheckCreateUserDir(rfr.User_id);
            WriteFile writer = new WriteFile(strs[0]);
            try {
                byte[] data;
                do
                {
                    data = rece.Receive();
                    writer.Write(data);
                } while (data.Length == 1024);
                writer.fileStream.Close();
                return true;
            }
            catch
            {
                writer.Delete();
                return false;
            }
        }

    }
}
