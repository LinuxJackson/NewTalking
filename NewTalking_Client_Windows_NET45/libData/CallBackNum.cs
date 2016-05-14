using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libData
{
    static public class CallBackNum
    {
        private static int callBackIndex = 0;

        public static int CallBackIndex
        {
            get
            {
                return callBackIndex;
            }

            set
            {
                callBackIndex = value;
            }
        }
    }
}
