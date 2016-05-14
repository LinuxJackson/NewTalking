using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtalking_BLL_Server.File;
using Newtalking_DAL_Server;
using Model;
using Newtalking_DAL_Data;
using System.Net.Sockets;
using File_DAL;

namespace Newtalking_BLL_Server.Account
{
    internal class UploadUserImage
    {
        UserImage userImage;
        TcpClient client;

        internal UploadUserImage(DataPackage data)
        {
            userImage = UserImageConvert.ConvertToClass(data.Data);
            client = data.Client;
        }

        internal bool Receive()
        {
            Newtalking_DAL_Server.ReceiveFile rece = new Newtalking_DAL_Server.ReceiveFile(client);
            string[] strs = FileCheck.CheckCreateUserDir(userImage.User_id);

            WriteFile writer = new WriteFile(strs[1] + userImage.File_name);
            try
            {
                byte[] data;
                do
                {
                    data = rece.Receive();
                    writer.Write(data);
                } while (data.Length == 1024);
                writer.fileStream.Close();

                SQLService sql = new SQLService();
                sql.ChangeUser_Image(userImage.User_id, userImage.File_name);
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
