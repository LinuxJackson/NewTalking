using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Newtalking_DAL_Data
{
    static public class SelectAccountConvert
    {
        static public SelectAccount ConvertToClass(byte[] data)
        {
            SelectAccount sel = new SelectAccount();
            sel.Uid = BitConverter.ToInt32(data, 2);
            sel.Sel_info = Encoding.Default.GetString(data, 6, 30);

            return sel;
        }
    }
}
