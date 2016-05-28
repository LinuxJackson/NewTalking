using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtalking_DAL_Server;
using System.IO;
using File_DAL;
using Newtalking_DAL_Data;
using Flags;

namespace Newtalking_BLL_Server.Account
{
    internal class SelUserImage
    {
        DataPackage dataPack;
        FileStream fsSend;
        string path;

        internal SelUserImage(DataPackage dataPackTemp)
        {
            //修改为数据库查询后传入

            UserImage userImage = UserImageConvert.ConvertToClass(dataPackTemp.Data);
            SQLService sql = new SQLService();
            string strFileName = sql.SelUserImageName(userImage.User_id);
            path = FileCheck.SelUserImage(userImage.User_id, strFileName);
        }

        internal bool Send()
        {
            if (path == FileFlags.FileExistsFailedFlag)
                return false;
            else
                fsSend = new FileStream(path, FileMode.Open, FileAccess.Read);
            Newtalking_DAL_Server.SendFile sender = new Newtalking_DAL_Server.SendFile(dataPack.Client, fsSend);
            return sender.Send();
        }
    }
}
