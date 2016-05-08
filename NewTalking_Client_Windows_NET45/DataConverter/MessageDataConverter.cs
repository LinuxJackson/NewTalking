using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace DataConverter
{
    public static class MessageDataConverter
    {
        public static MessageData ConvertToClass(byte[] data)
        {
            MessageData dataResult = new MessageData();

            dataResult.User_id = BitConverter.ToInt32(data, 2);
            dataResult.Receiver_id = BitConverter.ToInt32(data, 6);
            long timeTick = BitConverter.ToInt64(data, 10);
            dataResult.Time = new DateTime(timeTick);
            string msgTemp = Encoding.Default.GetString(data, 18, 1434);

            return dataResult;
        }

        public static byte[] ConvertToBytes(MessageData data)
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
    }
}
