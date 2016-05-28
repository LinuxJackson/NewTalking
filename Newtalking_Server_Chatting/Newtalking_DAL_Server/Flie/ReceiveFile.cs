using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Model;
using Newtalking_DAL_Server;
using File_DAL;

namespace Newtalking_DAL_Server
{
    public class ReceiveFile
    {
        const int BufferSize = 1024;
        TcpClient remoteClient;
        Receiver rece;
        WriteFile writer;

        public ReceiveFile(TcpClient tcpClient, WriteFile writeFile)
        {
            //端口2002
            remoteClient = tcpClient;
            writer = writeFile;
        }

        public bool Receive()
        {
            try
            {
                int bytesRead = 0;
                do
                {
                    NetworkStream streamToClient = remoteClient.GetStream();
                    byte[] buffer = new byte[BufferSize];
                    bytesRead = streamToClient.Read(buffer, 0, BufferSize);
                    writer.Write(buffer);
                } while (bytesRead == BufferSize);
                return true;
            }
            catch
            {
                writer.Delete();
                return false;
            }
            finally
            {
                writer.fileStream.Close();
            }
        }
    }
}
