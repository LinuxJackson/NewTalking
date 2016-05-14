using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libNet.Server;
using System.Threading.Tasks;

namespace libBgbll.Server
{
    static public class ConnectServer
    {
        static async public Task<bool> ConnectAsync()
        {
            return await Connection.ConnectAsync();
        }
    }
}
