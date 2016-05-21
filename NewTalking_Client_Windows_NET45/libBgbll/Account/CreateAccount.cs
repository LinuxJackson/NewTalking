using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libNet.Data;
using Model;
using DataConverter;

namespace libBgbll.Account
{
    public static class CreateAccount
    {
        public static async Task<bool> Create(LoginData loginData, libData.FuncReceiveData callBackFunc)
        {
            lock (libData.CallBackFuncLib.ArrUidCallBack)
            {
                libData.CallBackFuncLib.ArrUidCallBack.Add(libData.CallBackNum.CallBackIndex, callBackFunc);
            }
            return await Sender.Send(AccountRequestConverter.ConvertToBytes(loginData));
        }
    }
}
