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
        AddFollowingData add;
        AddFollowingConvert converter = new AddFollowingConvert();
        TcpClient client;

        public AddFollowing(DataPackage data)
        {
            add = converter.ConvertToClass(data.Data);
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
                dpk.Data = converter.ConvertToBytes(add, sql.AddFollowing(add));

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
