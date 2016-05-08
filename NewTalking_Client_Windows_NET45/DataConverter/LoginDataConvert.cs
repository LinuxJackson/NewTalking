using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace DataConverter
{
    static public class LoginDataConvert
    {
        static public LoginData_Re ConvertToClass(byte[] data)
        {
            LoginData_Re logRE = new LoginData_Re();
            logRE.Uid = BitConverter.ToInt32(data, 0);
            logRE.IsLogined = BitConverter.ToBoolean(data, 4);

            return logRE;
        }

        static public byte[] ConvertToBytes(LoginData data)
        {
            byte[] bResult = new byte[24];

            BitConverter.GetBytes(data.Uid).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.User_id).CopyTo(bResult, 4);
            Encoding.Default.GetBytes(data.User_password).CopyTo(bResult, 8);
            return bResult;
        }
    }
}
