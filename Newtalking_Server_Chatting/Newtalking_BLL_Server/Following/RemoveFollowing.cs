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
    public class RemoveFollowing
    {
        TcpClient client;
        FollowingData followingData;
        FollowingConvert converter = new FollowingConvert();

        public RemoveFollowing(DataPackage data)
        {
            client = data.Client;
            followingData = converter.ConvertToClass(data.Data);
        }

        public bool Response()
        {
            try
            {
                SQLService sql = new SQLService();
                bool isSucceed = sql.RemoveFollowing(followingData);

                Sender sender = new Sender();
                DataPackage dpk = new DataPackage();
                dpk.Client = client;
                dpk.Data = converter.ConvertToBytes(followingData, isSucceed);
                sender.SendMessage(dpk);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
