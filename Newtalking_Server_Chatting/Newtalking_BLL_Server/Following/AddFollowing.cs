using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtalking_DAL_Data;
using Newtalking_DAL_Server;
using Model;
using System.Net.Sockets;

namespace Newtalking_BLL_Server.Following
{
    public class AddFollowing
    {
        FollowingData followingData;
        TcpClient client;

        public AddFollowing(DataPackage data)
        {
            followingData = FollowingConvert.ConvertToClass(data.Data);
            client = data.Client;
        }

        public bool Response()
        {
            try
            {
                SQLService sql = new SQLService();

                Sender sender = new Sender();
                DataPackage dpk = new DataPackage();
                dpk.Client = client;
                dpk.Data = FollowingConvert.ConvertToBytes(followingData, sql.AddFollowing(followingData));

                return sender.SendMessage(dpk);
                //bool isSucceed = sql.AddFollowing(add);
            }
            catch
            {
                return false;
            }
        }
    }
}
