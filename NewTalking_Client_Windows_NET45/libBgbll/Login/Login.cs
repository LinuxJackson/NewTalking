using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using libFlags;
using System.Threading;
using libData;
using libNet.Data;
using DataConverter;

namespace libBgbll.Login
{
    public static class Login
    {
        public static void UserLogin(libData.FuncReceiveData func, LoginData logData)
        {
            Thread td = new Thread(delegate ()
            {
                lock (CallBackFuncLib.ArrUidCallBack)
                {
                    CallBackFuncLib.ArrUidCallBack.Add(logData.Uid, func);
                }
                Sender.Send(LoginDataConvert.ConvertToBytes(logData));
            });
            td.Start();
        }
    }
}
