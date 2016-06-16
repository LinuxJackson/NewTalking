using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Newtalking_DAL_Data
{
    static public class AccountInfoConvet
    {
        static public byte[] ConvertToBytes(AccountInfo data)
        {
            //4+2+4+24
            byte[] bResult = new byte[48];

            BitConverter.GetBytes((short)2).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.Uid).CopyTo(bResult, 2);
            BitConverter.GetBytes(data.User_id).CopyTo(bResult, 6);
            BitConverter.GetBytes(data.Sex).CopyTo(bResult, 10);
            BitConverter.GetBytes(data.Birthday.Ticks).CopyTo(bResult, 12);
            Encoding.Default.GetBytes(data.Phone).CopyTo(bResult, 20);

            return bResult;
        }

        static public AccountInfo ConvertToClass(byte[] data)
        {
            AccountInfo dataResult = new AccountInfo();

            dataResult.User_id = BitConverter.ToInt32(data, 2);
            dataResult.Sex = BitConverter.ToInt16(data, 6);
            dataResult.Birthday = new DateTime(BitConverter.ToInt64(data, 12));
            string tempPhone = Encoding.Default.GetString(data, 20, 24);

            foreach (char c in tempPhone)
            {
                if (c == '\0')
                    break;
                dataResult.Phone += c;
            }

            return dataResult;
        }

        static public byte[] ConvertToBytes_Re(bool boolean, Int32 uid)
        {
            byte[] bResult = new byte[8];

            BitConverter.GetBytes((short)2).CopyTo(bResult, 0);
            BitConverter.GetBytes(uid).CopyTo(bResult, 2);
            BitConverter.GetBytes(boolean).CopyTo(bResult, 6);

            return bResult;
        }
    }
}
