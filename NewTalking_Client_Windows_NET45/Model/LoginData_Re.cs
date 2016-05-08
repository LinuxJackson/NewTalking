using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class LoginData_Re
    {
        private int uid;
        private bool isLogined;

        public int Uid
        {
            get
            {
                return uid;
            }

            set
            {
                uid = value;
            }
        }

        public bool IsLogined
        {
            get
            {
                return isLogined;
            }

            set
            {
                isLogined = value;
            }
        }
    }
}
