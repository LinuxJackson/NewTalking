using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using libFlags;
using System.Threading.Tasks;

namespace libNet.Server
{
    public static class Connection
    {
        static public async Task<bool> ConnectAsync()
        {
            try
            {
                TcpClient tcp = new TcpClient();
                await tcp.ConnectAsync(ServerInfo.IP, ServerInfo.Port);
                libFlags.Server.Connection = tcp;

                return true;
            }
            catch
            {
                libFlags.Server.Connection = null;
                return false;
            }
        }

        static public bool DisConnect()
        {
            try
            {
                if (libFlags.Server.Connection != null)
                    libFlags.Server.Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
