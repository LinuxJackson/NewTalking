using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace libNet.Data
{
    static public class Sender
    {
        static public bool Send(byte[] data)
        {
            try
            {
                NetworkStream streamToServer = libFlags.Server.Connection.GetStream();
                streamToServer.Write(data, 0, data.Length);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
