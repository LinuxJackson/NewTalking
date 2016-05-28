using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Model;

namespace Newtalking_DAL_Server
{
    public class SendFile
    {
        TcpClient tcpTarget;
        const int BufferSize = 1024;
        FileStream fsSend;

        public SendFile(TcpClient tcp, FileStream fsTemp)
        {
            tcpTarget = tcp;
            fsSend = fsTemp;
        }

        public bool Send()
        {
            try {
                byte[] data = new byte[BufferSize];
                Sender sender = new Sender();
                int op = 0;
                int readCount = 0;
                do
                {
                    readCount = fsSend.Read(data, op, BufferSize);
                    DataPackage dpk = new DataPackage();
                    dpk.Client = tcpTarget;
                    dpk.Data = data;
                    sender.SendMessage(dpk);
                } while (readCount == BufferSize);
                fsSend.Close();
                return true; }
            catch
            {
                return false;
            }
        }
    }
}
