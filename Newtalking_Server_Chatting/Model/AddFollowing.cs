using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class FollowingData
    {
        private int uid;
        private int user_id;
        private int following_id;

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

        public int Following_id
        {
            get
            {
                return following_id;
            }

            set
            {
                following_id = value;
            }
        }
    }
}
