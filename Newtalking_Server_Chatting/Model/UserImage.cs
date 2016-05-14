using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UserImage
    {
        private int uid;
        private int user_id;
        private string file_name = "";

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

        public string File_name
        {
            get
            {
                return file_name;
            }

            set
            {
                file_name = value;
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
    }
}
