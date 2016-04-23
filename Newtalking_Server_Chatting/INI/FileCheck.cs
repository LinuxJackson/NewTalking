using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Server_Properties;

namespace INI
{
    static public class FileCheck
    {
        static public void CheckCreate()
        {
            if (!Directory.Exists(Property.IniDir))
                Directory.CreateDirectory(Property.IniDir);
            if (!File.Exists(Property.IniDir + "MySqlPWD.txt"))
            {
                FileStream fs1 = new FileStream(Property.IniDir + "MySqlPWD.txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine("");//开始写入值
                sw.Close();
                fs1.Close();
            }
        }

        static public void ChangeSQLKey(string key)
        {
            try
            {
                File.WriteAllText(Property.IniDir + "MySqlPWD.txt", key);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        static public string ReadSQLKey()
        {
            try
            {
                return File.ReadAllText(Property.IniDir + "MySqlPWD.txt");
            }
            catch(Exception ex)
            {
                return "";
            }
        }
    }
}
