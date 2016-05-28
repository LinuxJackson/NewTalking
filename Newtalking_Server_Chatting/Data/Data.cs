using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Data
{
    public class Data
    {
        static private Dictionary<int, OnlineUserProperties> arrOnlineUsers = new Dictionary<int, OnlineUserProperties>();

        public static Dictionary<int, OnlineUserProperties> ArrOnlineUsers
        {
            get { return Data.arrOnlineUsers; }
            set { Data.arrOnlineUsers = value; }
        }
    }
}
