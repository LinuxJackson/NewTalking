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
        int file_length;

        public ReceiveFile(TcpClient tcpClient, WriteFile writeFile, int file_length)
        {
            //端口2002
            remoteClient = tcpClient;
            writer = writeFile;
            this.file_length = file_length;
        }

        public bool Receive()
        {
            try
            {
                byte[] buffer = new byte[file_length];

                NetworkStream stream = remoteClient.GetStream();
                int readLength = 0;
                while (readLength < file_length)
                {
                    int NowRead = stream.Read(buffer, readLength, file_length - readLength);
                    if (NowRead == 0)
                    {
                        writer.Write(buffer);
                        break;
                    }
                    readLength += NowRead;
                }

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
