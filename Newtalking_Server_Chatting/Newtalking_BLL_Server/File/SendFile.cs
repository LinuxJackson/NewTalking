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
    internal class SendFile
    {
        DataPackage dataPack;
        FileStream fsSend;
        byte[] bPackBegin = new byte[4];
        string path;
        bool isAccess = true;

        internal SendFile(DataPackage dataPackTemp)
        {
            dataPack = dataPackTemp;
            Buffer.BlockCopy(dataPackTemp.Data, 2, bPackBegin, 0, 4);

            FileCheck fileCheck = new FileCheck();
            FileRequest fr = FileRequestConvert.ConvertToClass_Send(dataPackTemp.Data);
            path = fileCheck.SelUserFileDir(fr.User_id, fr.FileName);

            SQLService sql = new SQLService();
            if (sql.CheckFileKey(fr.User_id, fr.FileName, fr.FileKey))
                isAccess = false;
        }

        internal bool Send()
        {
            if (isAccess)
            {
                if (path == FileFlags.FileExistsFailedFlag)
                    return false;
                else
                    fsSend = new FileStream(path, FileMode.Open, FileAccess.Read);
                Newtalking_DAL_Server.SendFile sender = new Newtalking_DAL_Server.SendFile(dataPack.Client, fsSend, bPackBegin);
                return sender.Send();
            }
            return false;
        }
    }
}
