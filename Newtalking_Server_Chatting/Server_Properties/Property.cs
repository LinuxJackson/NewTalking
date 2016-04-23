using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Server_Properties
{
    public class Property
    {
        static string iniDir = Environment.CurrentDirectory + "\\ServerINI\\";
        static IPAddress ip = IPAddress.Parse("0.0.0.0");
        static int port = 2001;
        static string sqlKey;

        public static string IniDir
        {
            get
            {
                return iniDir;
            }

            set
            {
                iniDir = value;
            }
        }

        public static IPAddress Ip
        {
            get
            {
                return ip;
            }

            set
            {
                ip = value;
            }
        }

        public static int Port
        {
            get
            {
                return port;
            }

            set
            {
                port = value;
            }
        }

        public static string SqlKey
        {
            get
            {
                return sqlKey;
            }

            set
            {
                sqlKey = value;
            }
        }
    }
}
