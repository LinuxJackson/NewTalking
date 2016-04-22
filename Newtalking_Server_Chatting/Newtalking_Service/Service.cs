﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Data;
using System.Net;
using System.Collections;
using Newtalking_DAL_Server;
using Newtalking_BLL_Server;
using System.Net.Sockets;

namespace Newtalking_Service
{
    public class Service
    {
        static public void ActiveService()
        {
            Thread tdService = new Thread(delegate ()
            {
                GetClient getClient = new GetClient();
                while (true)
                {
                    TcpClient tcpClient = getClient.Get();

                    Thread tdClientService = new Thread(delegate ()
                    {
                        TcpClient tcpUser = tcpClient;

                        try
                        {
                            Data_BLL bll = new Data_BLL();
                            while (true)
                            {
                                Receiver receiver = new Receiver();
                                bll.Analysis(receiver.ReceiveData(tcpUser));
                            }
                        }
                        catch
                        {
                            //[未升级] 从已登录用户中删除
                            lock (Data.Data.ArrOnlineUsers)
                            {
                                List<OnlineUserProperties> arrTemp = new List<OnlineUserProperties>();
                                for (int i = 0; i < Data.Data.ArrOnlineUsers.Count; i++)
                                {
                                    OnlineUserProperties onlineUser = (OnlineUserProperties)Data.Data.ArrOnlineUsers[i];
                                    if (onlineUser.Client != tcpUser)
                                        arrTemp.Add(Data.Data.ArrOnlineUsers[i]);
                                }
                                Data.Data.ArrOnlineUsers = arrTemp;
                            }
                        }
                    });
                    tdClientService.Start();
                    ThreadPool.ArrServiceThreads.Add(tdClientService);
                }
            });
            tdService.Start();
        }
    }
}
