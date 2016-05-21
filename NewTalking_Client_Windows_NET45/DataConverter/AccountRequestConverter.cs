using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace DataConverter
{
    static public class AccountRequestConverter
    {
        static public LoginData ConvertToClass(byte[] data)
        {
            LoginData loginData = new LoginData();

            loginData.Uid = BitConverter.ToInt32(data, 2);
            loginData.User_id = BitConverter.ToInt32(data, 6);

            return loginData;
        }

        static public byte[] ConvertToBytes(LoginData data)
        {
            byte[] bResult = new byte[24];

            BitConverter.GetBytes((short)3).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.Uid).CopyTo(bResult, 2);
            BitConverter.GetBytes(data.User_id).CopyTo(bResult, 6);
            Encoding.Default.GetBytes(data.User_password).CopyTo(bResult, 10);
            return bResult;
        }
    }
}
