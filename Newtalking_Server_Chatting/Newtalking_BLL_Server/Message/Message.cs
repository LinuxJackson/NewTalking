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
    internal class Message
    {
        byte[] bData;
        MessageData msgData = new MessageData();

        internal Message(byte[] data, int user_id)
        {
            msgData = MessageDataConvert.ConvertToClass(data);
            bData = data;
            msgData.User_id = user_id;
        }

        internal void Send()
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

                    Sender sender = new Sender();
                    bool isSent = false;

                    foreach (System.Net.Sockets.TcpClient client in user.Clients)
                    {
                        DataPackage data = new DataPackage
                        {
                            Client = client,
                            Data = bData
                        };

                        System.Threading.Thread tdSend = new System.Threading.Thread(delegate ()
                        {
                            try
                            {
                                sender.SendMessage(data);
                                isSent = true;
                            }
                            catch
                            {

                            }
                        });

                        tdSend.Start();
                    }

                    if (isSent)
                        return;
                    else
                    {
                        //lock (Data.Data.ArrSendingMessages)
                        //{
                        //    Data.Data.ArrSendingMessages.Add(msgData);
                        //}
                        Thread td = new Thread(delegate ()
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