using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtalking_DAL_Data;
using Newtalking_DAL_Server;
using System.Net;
using Newtalking_DAL_Data;

namespace Newtalking_BLL_Server.UserInfo
{
    internal class EditAccountInfo
    {
        AccountInfo accountInfo = new AccountInfo();
        DataPackage dataSend = new DataPackage();

        internal EditAccountInfo(DataPackage data)
        {
            accountInfo = AccountInfoConvet.ConvertToClass(data.Data);
            dataSend.Client = data.Client;
        }

        internal void Response()
        {
            SQLService sql = new SQLService();
            bool isSucceed = sql.AccountInfoEditor(accountInfo);
            dataSend.Data = AccountInfoConvet.ConvertToBytes_Re(isSucceed, accountInfo.Uid);
            Sender sender = new Sender();
            sender.SendMessage(dataSend);
        }
    }
}
