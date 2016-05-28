using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace Data
{
    public class OnlineUserProperties
    {
        private List<TcpClient> clients = new List<TcpClient>();
        private int user_id;

        public List<TcpClient> Clients
        {
            get
            {
                return clients;
            }

            set
            {
                clients = value;
            }
        }

        public int User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }
    }
}
