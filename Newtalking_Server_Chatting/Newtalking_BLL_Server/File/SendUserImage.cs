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

namespace Newtalking_BLL_Server.File
{
    internal class SendUserImage
    {
        DataPackage dataPack;
        FileStream fsSend;
        byte[] bPackBegin = new byte[4];
        string path;

        internal SendUserImage(DataPackage dataPackTemp)
        {
            //修改为数据库查询后传入
            dataPack = dataPackTemp;
            Buffer.BlockCopy(dataPackTemp.Data, 2, bPackBegin, 0, 4);

            FileCheck fileCheck = new FileCheck();
            FileRequest fr = FileRequestConvert.ConvertToClass_Send(dataPackTemp.Data);

            SQLService sql = new SQLService();
            string strFileName = sql.SelUserImageName(fr.User_id);
            path = fileCheck.SelUserImage(fr.User_id, strFileName);
        }

        internal bool Send()
        {
            if (path == FileFlags.FileExistsFailedFlag)
                return false;
            else
                fsSend = new FileStream(path, FileMode.Open, FileAccess.Read);
            Newtalking_DAL_Server.SendFile sender = new Newtalking_DAL_Server.SendFile(dataPack.Client, fsSend, bPackBegin);
            return sender.Send();
        }
    }
}
