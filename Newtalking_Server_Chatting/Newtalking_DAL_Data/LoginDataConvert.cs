using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Newtalking_DAL_Data
{
    static public class LoginDataConvert
    {
        static public byte[] ConvertToBytes(bool boolean, int uid)
        {
            byte[] bResult = new byte[8];
            BitConverter.GetBytes((short)2).CopyTo(bResult, 0);
            BitConverter.GetBytes(uid).CopyTo(bResult, 2);
            BitConverter.GetBytes(boolean).CopyTo(bResult, 6);

            return bResult;
        }

        static public LoginData ConvertToClass(byte[] data)
        {
            LoginData dataResult = new LoginData();
            dataResult.Uid = BitConverter.ToInt32(data, 2);
            dataResult.User_id = BitConverter.ToInt32(data, 6);

            string pwd = Encoding.Default.GetString(data, 10, 16);
            int i = 0;
            while (pwd[i] != '\0')
            {
                dataResult.User_password += pwd[i];
                i++;
            }

            return dataResult;
        }
    }

    static public class AccountRequestConvert
    {
        static public LoginData ConvertToClass(byte[] data)
        {
            LoginData dataResult = new LoginData();
            dataResult.Uid = BitConverter.ToInt32(data, 2);
            string sTemp = Encoding.Default.GetString(data, 10, 16);
            foreach (char c in sTemp)
            {
                if (c == '\0')
                    break;
                dataResult.User_password += c;
            }
            return dataResult;
        }

        static public byte[] ConvertToBytes(LoginData data)
        {
            byte[] bResult = new byte[26];

            BitConverter.GetBytes((short)2).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.Uid).CopyTo(bResult, 2);
            BitConverter.GetBytes(data.User_id).CopyTo(bResult, 6);
            Encoding.Default.GetBytes(data.User_password).CopyTo(bResult, 10);

            return bResult;
        }
    }
}
