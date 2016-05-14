using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using libData;

namespace libBgbll
{
    static public class MainBLL
    {
        static public void Analysis(byte[] data)
        {
            Thread td = new Thread(delegate ()
            {
                short type = BitConverter.ToInt16(data, 0);

                switch (type)
                {
                    case 1:           //接收到消息
                        //CallBackFuncLib.ReceiveMessage()
                        break;
                    case 2:         //缺少default判定
                        int uid = BitConverter.ToInt32(data, 2);
                        FuncReceiveData func = null;

                        lock (CallBackFuncLib.ArrUidCallBack)
                        {
                            if (CallBackFuncLib.ArrUidCallBack.ContainsKey(uid))
                            {
                                func = CallBackFuncLib.ArrUidCallBack[uid];
                                CallBackFuncLib.ArrUidCallBack.Remove(uid);
                            }
                        }
                        func(data);
                        break;
                }
            });

            td.Start();
        }
    }
}
