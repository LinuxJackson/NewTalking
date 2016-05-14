using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Collections;

namespace libData
{
    public delegate void FuncReceiveMessage(MessageData data);
    public delegate void FuncReceiveData(byte[] data);

    public static class CallBackFuncLib
    {
        static private FuncReceiveMessage receiveMessage;
        static private Dictionary<int, FuncReceiveData> arrUidCallBack = new Dictionary<int, FuncReceiveData>();

        public static FuncReceiveMessage ReceiveMessage
        {
            get
            {
                return receiveMessage;
            }

            set
            {
                receiveMessage = value;
            }
        }

        public static Dictionary<int, FuncReceiveData> ArrUidCallBack
        {
            get
            {
                return arrUidCallBack;
            }

            set
            {
                arrUidCallBack = value;
            }
        }
    }
}
