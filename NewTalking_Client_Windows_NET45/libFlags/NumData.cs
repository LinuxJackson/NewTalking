using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libFlags
{
    static public class NumData
    {
        private static int uid_CallBackIndex = 0;

        public static int Uid_CallBackIndex
        {
            get
            {
                return uid_CallBackIndex;
            }

            set
            {
                uid_CallBackIndex = value;
            }
        }
    }
}
