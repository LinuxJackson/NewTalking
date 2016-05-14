using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libNet.Data;
using System.Threading.Tasks;

namespace libBgbll.Server
{
    public static class ReceiveData
    {
        public static async Task<byte[]> Receive()
        {
            return await Receiver.Receive();
        }
    }
}
