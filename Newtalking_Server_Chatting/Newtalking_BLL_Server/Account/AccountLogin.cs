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
    internal class AccountLogin
    {
        internal LoginData loginData = new LoginData();
        bool isLogined = false;
        System.Net.Sockets.TcpClient client;
        int user_id;

        internal AccountLogin(DataPackage data)
        {
            loginData = LoginDataConvert.ConvertToClass(data.Data);
            client = data.Client;
            user_id = loginData.User_id;
        }

        internal bool Login()
        {
            SQLService sql = new SQLService();
            isLogined = sql.Login(loginData);
            return isLogined;
        }

        internal bool Respect()
        {
            Sender sender = new Sender();

            DataPackage data = new DataPackage
            {
                Client = client,
                Data = LoginDataConvert.ConvertToBytes(isLogined, loginData.Uid)
            };

            return sender.SendMessage(data);
        }

        internal void AddToOnlineUserList()
        {
            lock (Data.Data.ArrOnlineUsers)
            {
                try
                {
                    if (!Data.Data.ArrOnlineUsers.ContainsKey(user_id))
                    {
                        OnlineUserProperties onlineUser = new OnlineUserProperties();
                        onlineUser.User_id = user_id;
                        onlineUser.Clients.Add(client);

                        Data.Data.ArrOnlineUsers.Add(user_id, onlineUser);
                    }
                    else
                    {
                        OnlineUserProperties user = Data.Data.ArrOnlineUsers[user_id];
                        user.Clients.Add(client);
                    }
                }
                catch
                {

                }
            }
        }
    }

}
