using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Newtalking_DAL_Data
{
    static public class RefreshRequestConvert
    {
        static public RefreshRequest ConvertToClass(byte[] data)
        {
            RefreshRequest rr = new RefreshRequest();
            rr.Uid = BitConverter.ToInt32(data, 2);
            rr.User_id = BitConverter.ToInt32(data, 6);

            return rr;
        }

        static public byte[] ConvertToBytes_End(Int32 uid)
        {
            byte[] bResult = new byte[10];

            BitConverter.GetBytes((short)2).CopyTo(bResult, 0);
            BitConverter.GetBytes(uid).CopyTo(bResult, 2);
            BitConverter.GetBytes((Int32)0).CopyTo(bResult, 6);

            return bResult;
        }
    }
}
