using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace libNet.Data
{
    static public class Receiver
    {
        const int BufferSize = 1452;
        static public async Task<byte[]> Receive()
        {
            try
            {
                NetworkStream streamToClient = libFlags.Server.Connection.GetStream();
                byte[] buffer = new byte[BufferSize];
                int bytesRead = await streamToClient.ReadAsync(buffer, 0, BufferSize);
                int correctSize = 0;
                while (buffer[correctSize] != 0)
                {
                    correctSize++;
                }
                byte[] correctBuffer = new byte[correctSize];
                Buffer.BlockCopy(buffer, 0, correctBuffer, 0, correctSize);

                return buffer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
