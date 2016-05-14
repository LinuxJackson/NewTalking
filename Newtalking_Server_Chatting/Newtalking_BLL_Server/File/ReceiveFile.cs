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
            string[] strs = FileCheck.CheckCreateUserDir(rfr.User_id);
            WriteFile writer = new WriteFile(strs[0] + rfr.File_name);
            try {
                byte[] data;
                do
                {
                    data = rece.Receive();
                    writer.Write(data);
                } while (data.Length == 1024);
                writer.fileStream.Close();

                string key = "";
                Random random = new Random();
                for (int i = 0; i < 16; i++)
                    key += random.Next(0, 10).ToString();

                SQLService sql = new SQLService();
                sql.UpLoadFile(rfr.User_id, rfr.File_name, key);
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
