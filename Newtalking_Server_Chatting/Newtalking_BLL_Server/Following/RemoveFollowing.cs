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
    internal class RemoveFollowing
    {
        TcpClient client;
        FollowingData followingData;

        internal RemoveFollowing(DataPackage data)
        {
            client = data.Client;
            followingData = FollowingConvert.ConvertToClass(data.Data);
        }

        internal bool Response()
        {
            try
            {
                SQLService sql = new SQLService();
                bool isSucceed = sql.RemoveFollowing(followingData);

                Sender sender = new Sender();
                DataPackage dpk = new DataPackage();
                dpk.Client = client;
                dpk.Data = FollowingConvert.ConvertToBytes(followingData, isSucceed);
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
