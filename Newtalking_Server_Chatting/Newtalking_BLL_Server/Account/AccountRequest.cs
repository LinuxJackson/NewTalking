using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtalking_DAL_Data;
using Newtalking_DAL_Server;
using Data;
using File_DAL;

namespace Newtalking_BLL_Server.Account
{
    internal class AccountRequest
    {
        DataPackage dataResponse = new DataPackage();

        internal AccountRequest(DataPackage data)
        {
            SQLService sql = new SQLService();
            dataResponse.Client = data.Client;

            LoginData loginData = AccountRequestConvert.ConvertToClass(data.Data);
            loginData.User_id = sql.AccountRequest(loginData.User_password);

            dataResponse.Data = AccountRequestConvert.ConvertToBytes(loginData);
            FileCheck.CheckCreateUserDir(loginData.User_id);
        }

        internal void Response()
        {
            Sender sender = new Sender();
            sender.SendMessage(dataResponse);
        }
    }
}
