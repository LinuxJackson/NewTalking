using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtalking_DAL_Server;
using Newtalking_DAL_Data;
using System.Net;

namespace Newtalking_BLL_Server.UserInfo
{
    internal class ReadUserInfo
    {
        AccountInfo accountInfo = new AccountInfo();
        DataPackage dataSend = new DataPackage();

        internal ReadUserInfo(DataPackage data)
        {
            accountInfo = AccountInfoConvet.ConvertToClass(data.Data);
            SQLService sql = new SQLService();
            accountInfo = sql.AccountInfoReader(accountInfo.User_id);
            dataSend.Client = data.Client;
            dataSend.Data = AccountInfoConvet.ConvertToBytes(accountInfo);
        }

        internal AccountInfo Response()
        {
            Sender sender = new Sender();
            sender.SendMessage(dataSend);
            return accountInfo;
        }
    }
}