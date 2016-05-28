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
            string[] strs = FileCheck.CheckCreateUserDir(rfr.User_id);
            try {
                Newtalking_DAL_Server.ReceiveFile rece = new Newtalking_DAL_Server.ReceiveFile(remoteClient, new WriteFile(strs[0] + rfr.File_name));
                if (!rece.Receive())
                    return false;

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
                return false;
            }
        }

    }
}
