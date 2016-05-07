using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtalking_DAL_Data;
using Newtalking_DAL_Server;

namespace Newtalking_BLL_Server.Account
{
    internal class SearchAccount
    {
        System.Net.Sockets.TcpClient client;
        SelectAccount selAcc;

        internal SearchAccount(DataPackage dpk)
        {
            selAcc = SelectAccountConvert.ConvertToClass(dpk.Data);
            client = dpk.Client;
        }

        internal bool Response()
        {
            try
            {
                SQLService sql = new SQLService();
                System.Collections.ArrayList arr = sql.SearchAccount(selAcc);

                foreach (object obj in arr)
                {
                    Sender sender = new Sender();
                    DataPackage dpk = new DataPackage();
                    AccountInfoConvet.ConvertToBytes((AccountInfo)obj);
                    dpk.Client = client;
                    sender.SendMessage(dpk);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
