using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace libFlags
{
    public static class Server
    {
        private static TcpClient connection;

        public static TcpClient Connection
        {
            get
            {
                return connection;
            }

            set
            {
                connection = value;
            }
        }
    }
}
