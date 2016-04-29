using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Model;

namespace Newtalking_DAL_Server
{
    public class Sender
    {
        public bool SendMessage(DataPackage dp)
        {
            if (dp.Client == null) return false;
            try{
                NetworkStream streamToServer = dp.Client.GetStream();
                streamToServer.Write(dp.Data, 0, dp.Data.Length);
                return true;
            } catch {
                return false;
            }
        }
    }
}
