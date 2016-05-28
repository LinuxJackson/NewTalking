﻿using System;
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
            try
            {
                string[] strs = FileCheck.CheckCreateUserDir(userImage.User_id);
                Newtalking_DAL_Server.ReceiveFile rece = new Newtalking_DAL_Server.ReceiveFile(client, new WriteFile(strs[1] + userImage.File_name));
                if (!rece.Receive())
                    return false;

                SQLService sql = new SQLService();
                sql.ChangeUser_Image(userImage.User_id, userImage.File_name);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
