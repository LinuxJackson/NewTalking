using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace libNet.Data
{
    static public class Sender
    {
        static public async Task<bool> Send(byte[] data)
        {
            try
            {
                NetworkStream streamToServer = libFlags.Server.Connection.GetStream();
                await streamToServer.WriteAsync(data, 0, data.Length);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
