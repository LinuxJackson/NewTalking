using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class FileRequest
    {
        private int uid;
        private int user_id;
        private string fileName;
        private string fileKey;

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
        
        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                fileName = value;
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

        public string FileKey
        {
            get
            {
                return fileKey;
            }

            set
            {
                fileKey = value;
            }
        }
    }
}
