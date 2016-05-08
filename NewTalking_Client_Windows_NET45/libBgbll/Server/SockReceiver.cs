using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libNet.Data;
using System.Threading.Tasks;

namespace libBgbll.Server
{
    public static class SockReceiver
    {
        public static async Task<byte[]> Receive()
        {
            try
            {
                return await Receiver.Receive();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
