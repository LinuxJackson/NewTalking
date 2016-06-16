using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Newtalking_DAL_Data
{
    static public class MessageDataConvert
    {
        static public byte[] ConvertToBytes(MessageData data)
        {
            byte[] bResult = new byte[1452];

            short type = 1;
            BitConverter.GetBytes(type).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.User_id).CopyTo(bResult, 2);
            BitConverter.GetBytes(data.Receiver_id).CopyTo(bResult, 6);
            BitConverter.GetBytes(data.Time.Ticks).CopyTo(bResult, 10);
            Encoding.Default.GetBytes(data.Message).CopyTo(bResult, 18);

            return bResult;
        }

        static public MessageData ConvertToClass(byte[] bReceived)
        {
            MessageData dataResult = new MessageData();

            dataResult.User_id = BitConverter.ToInt32(bReceived, 2);
            dataResult.Receiver_id = BitConverter.ToInt32(bReceived, 6);
            long timeTick = BitConverter.ToInt64(bReceived, 10);
            dataResult.Time = new DateTime(timeTick);
            string msgTemp = Encoding.Default.GetString(bReceived, 18, 1434);

            foreach (char c in msgTemp)
            {
                if (c == '\0')
                    break;
                dataResult.Message += c;
            }

            return dataResult;
        }
    }
}
