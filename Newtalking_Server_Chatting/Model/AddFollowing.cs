using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class AddFollowingData
    {
        private int uid;
        private int user_id;
        private int add_id;

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

        public int User_id
        {
            get
            {
                return user_id;
            }

            set
            {
                user_id = value;
            }
        }

        public int Add_id
        {
            get
            {
                return add_id;
            }

            set
            {
                add_id = value;
            }
        }
    }
}
