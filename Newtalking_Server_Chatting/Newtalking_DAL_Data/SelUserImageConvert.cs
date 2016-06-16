using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Newtalking_DAL_Data
{
    static public class SelUserImageConvert
    {
        static public UserImage ConvertToClass(byte[] data)
        {
            UserImage userImage = new UserImage();

            userImage.Uid = BitConverter.ToInt32(data, 2);
            userImage.User_id = BitConverter.ToInt32(data, 6);
            string temp = Encoding.Default.GetString(data, 10, 255);

            foreach (char c in temp)
            {
                if (c == '\0')
                    break;
                userImage.File_name += c;
            }

            return userImage;
        }
    }
}
