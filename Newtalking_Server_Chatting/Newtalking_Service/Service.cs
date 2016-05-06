using System;
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
using INI;
using Server_Properties;

namespace Newtalking_Service
{
    public class Service
    {
        static public void ActiveService()
        {
            Thread tdService = new Thread(delegate ()
            {
                INI.FileCheck.CheckCreate();
                try
                {
                    Property.SqlKey = INI.FileCheck.ReadSQLKey().Trim();
                }
                catch
                {
                    Property.SqlKey = "";
                }
                
                GetClient getClient = new GetClient();
                while (true)
                {
                    TcpClient tcpClient = getClient.Get();

                    Thread tdClientService = new Thread(delegate ()
                    {
                        TcpClient tcpUser = tcpClient;
                        Data_BLL bll = new Data_BLL();

                        try
                        {
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
                                if (bll.isLogined)
                                    Data.Data.ArrOnlineUsers.Remove(bll.logined_ID);
                            }
                        }
                    });
                    tdClientService.Start();
                    //ThreadPool.ArrServiceThreads.Add(tdClientService);
                }
            });
            tdService.Start();
        }
    }
}
