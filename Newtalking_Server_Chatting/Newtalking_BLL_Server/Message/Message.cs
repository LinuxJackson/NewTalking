using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using Newtalking_DAL_Data;
using Newtalking_DAL_Server;
using System.Net;
using System.Collections;
using System.Threading;

namespace Newtalking_BLL_Server.Message
{
    public class Message
    {
        byte[] bData;
        MessageData msgData = new MessageData();
        MessageDataConvert convert = new MessageDataConvert();

        public Message(byte[] data, int user_id)
        {
            msgData = convert.ConvertToClass(data);
            bData = data;
            msgData.User_id = user_id;
        }

        public void Send()
        {
            ArrayList arrOnlineUsers = new ArrayList();
            lock (Data.Data.ArrOnlineUsers)
            {
                if (!Data.Data.ArrOnlineUsers.ContainsKey(msgData.Receiver_id))
                {
                    SQLService sql = new SQLService();
                    sql.InsertIntoOverMessages(msgData);
                }
                else
                {
                    Data.OnlineUserProperties user = Data.Data.ArrOnlineUsers[msgData.Receiver_id];

                    DataPackage dataPackage = new DataPackage();
                    dataPackage.Client = user.Client;
                    dataPackage.Data = bData;
                    Sender sender = new Sender();
                    if (sender.SendMessage(dataPackage))
                        return;
                    else
                    {
                        //lock (Data.Data.ArrSendingMessages)
                        //{
                        //    Data.Data.ArrSendingMessages.Add(msgData);
                        //}
                        Thread td = new Thread(delegate()
                        {
                            SQLService sql = new SQLService();
                            sql.InsertIntoOverMessages(msgData);
                        });
                        td.Start();
                    }
                }
            }
            //bool isFoundOnline = false;

            ////[未升级] 数据库处理
            //for (int i = 0; i < arrOnlineUsers.Count; i++)
            //{
            //    Data.OnlineUserProperties user = (Data.OnlineUserProperties)arrOnlineUsers[i];
            //    if (msgData.Receiver_id == user.User_id)
            //    {
            //        break;
            //    }
            //}
            //if (!isFoundOnline)
            //{
            //    SQLService sql = new SQLService();
            //    sql.InsertIntoOverMessages(msgData);
            //}

            
                //lock (Data.Data.ArrSendingMessages)
                //{
                //    Data.Data.ArrSendingMessages.Add(msgData);
                //}
        }
    }
}