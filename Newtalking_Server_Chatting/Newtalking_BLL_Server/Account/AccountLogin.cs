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
        OnlineUserProperties onlineUser = new OnlineUserProperties();

        internal AccountLogin(DataPackage data)
        {
            loginData = LoginDataConvert.ConvertToClass(data.Data);
            onlineUser.Client = data.Client;
            onlineUser.User_id = loginData.User_id;
        }

        internal bool Login()
        {
            SQLService sql = new SQLService();
            isLogined = sql.Login(loginData);
            return isLogined;
        }

        internal bool Respect()
        {
            DataPackage data = new DataPackage();
            data.Client = onlineUser.Client;
            data.Data = LoginDataConvert.ConvertToBytes(isLogined, loginData.Uid);
            Sender sender = new Sender();

            //[未升级] 发送所有消息

            //ArrayList arrTemp = new ArrayList();
            //lock (Data.Data.ArrSendingMessages)
            //{
            //    ArrayList arrNew = Data.Data.ArrSendingMessages;
            //    Data.Data.ArrSendingMessages.Clear();
            //    for (int i = 0; i < arrNew.Count; i++)
            //    {
            //        MessageData msg = (MessageData)arrNew[i];
            //        if (msg.Receiver_id == data.User_id)
            //            arrTemp.Add(msg);
            //        else
            //            Data.Data.ArrSendingMessages.Add(msg);
            //    }
            //}
            //for (int i = 0; i < arrTemp.Count; i++)
            //{
            //    MessageData msg = (MessageData)arrTemp[i];
            //    Newtalking_DAL_Data.MessageDataConvert convert = new Newtalking_DAL_Data.MessageDataConvert();
            //    Message msgSend = new Message(convert.ConvertToBytes(msg));
            //    msgSend.Send();
            //})

            return sender.SendMessage(data);
        }

        internal void AddToOnlineUserList()
        {
            lock (Data.Data.ArrOnlineUsers)
            {
                try
                {
                    if (!Data.Data.ArrOnlineUsers.ContainsKey(onlineUser.User_id))
                        Data.Data.ArrOnlineUsers.Add(onlineUser.User_id, onlineUser);
                }
                catch
                {

                }
            }
        }
    }

}
