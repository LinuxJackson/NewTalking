using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using MySQLDriverCS;
using System.Data.Common;
using System.Data;
using System.Collections;
using Flags;

namespace Newtalking_DAL_Server
{
    //防注入修改：参数化查询
    //using (SqlCommand Cmd = Conn.CreateCommand())
    //            {
    //                Cmd.CommandText = "select * from TB_Users where passport=@UN and password=@PWD";
    //                Cmd.Parameters.Add(new SqlParameter("UN", Passport));
    //                Cmd.Parameters.Add(new SqlParameter("PWD", Password));

    public class SQLService
    {
        string sql = "";
        MySQLConnection con = new MySQLConnection(new MySQLConnectionString("localhost", "NewTalking", "root", Server_Properties.Property.SqlKey).AsString);

        public bool Login(LoginData data)
        {
            try
            {
                con.Open();
                sql = "SELECT user_password from users where user_id = " + data.User_id + "";
                MySQLCommand com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                    if (reader["user_password"].ToString() == data.User_password)
                        return true;
                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public int AccountRequest(string pwd)
        {
            try {
                con.Open();
                sql = "INSERT INTO users(user_password) VALUES(" + pwd + ")";
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                sql = "SELECT * FROM users WHERE user_id = LAST_INSERT_ID()";
                com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReader();
                int user_id = 0;
                if(reader.Read())
                {
                    user_id = Int32.Parse(reader["user_id"].ToString());
                } 
                sql = "INSERT INTO users_information VALUES(null,null,null,null)";
                com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return user_id;
            }
            catch { return 0; }
            finally
            {
                con.Close();
            }
        }

        public AccountInfo AccountInfoReader(int user_id)
        {
            try {
                con.Open();
                sql = "SELECT* FROM users_information WHERE user_id = " + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReader();
                if(reader.Read())
                {
                    AccountInfo accountInfo = new AccountInfo();
                    accountInfo.Sex = short.Parse(reader["user_sex"].ToString());
                    accountInfo.Birthday = DateTime.Parse(reader["user_birthday"].ToString());
                    accountInfo.Phone = reader["uesr_phome"].ToString();
                    return accountInfo;
                }
                return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public bool AccountInfoEditor(AccountInfo accountInfo)
        {
            try
            {
                con.Open();
                sql = "UPDATE user_sex = " + accountInfo.Sex + ", user_birthday = '" + accountInfo.Birthday.ToString("yyyy-MM-dd") + "', user_phone = " + accountInfo.Phone + " FROM users_information WHERE user_id = " + accountInfo.User_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }

        }

        public bool CheckOnlineUser(int user_id)
        {
            try
            {
                con.Open();
                sql = "SELECT * FROM user_online WHERE user_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                MySQLDataReader reader = com.ExecuteReaderEx();
                if (reader.Read())
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool InsertOnlineUser(int user_id)
        {
            try
            {
                con.Open();
                string sql = "INSERT INTO user_online(user_id) VALUES(" + user_id + ")";
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool DelOnlineUser(int user_id)
        {
            try
            {
                con.Open();
                string sql = "DELETE user_online WHERE user_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool InsertIntoOverMessages(MessageData msg)
        {
            try
            {
                con.Open();
                sql = "INSERT INTO over_messages(sender_id,receiver_id,time,message) VALUES(" + msg.User_id + "," + msg.Receiver_id + ",'" + msg.Time.ToString("yy-MM-dd hh:mm:ss") + "'," + msg.Message.Trim() + ")";
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public List<MessageData> SelOverMessages(int user_id)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                sql = "SELECT * FROM over_messages WHERE receiver_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                MySQLDataAdapter adp = new MySQLDataAdapter(com);
                adp.Fill(ds);

                List<MessageData> messages = new List<MessageData>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MessageData msg = new MessageData();
                    msg.User_id = (int)dr["sender_id"];
                    msg.Receiver_id = (int)dr["receiver_id"];
                    msg.Time = DateTime.Parse(dr["time"].ToString());
                    msg.Message = dr["message"].ToString();
                    messages.Add(msg);
                }

                sql = "DELETE FROM over_messages WHERE receiver_id=" + user_id;
                com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();

                return messages;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public ArrayList SearchAccount(SelectAccount selAccount)
        {
            try
            {
                //AccountInfo accountInfo = new AccountInfo();
                //accountInfo.Sex = short.Parse(reader["user_sex"].ToString());
                //accountInfo.Birthday = DateTime.Parse(reader["user_birthday"].ToString());
                //accountInfo.Phone = reader["uesr_phome"].ToString();
                //return accountInfo;
                con.Open();
                sql = "INNER JOIN users_information ON users_information.user_name LIKE '%" + selAccount.Sel_info + "%'";
                MySQLCommand com = new MySQLCommand(sql, con);
                MySQLDataAdapter adp = new MySQLDataAdapter(com);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                ArrayList arr = new ArrayList();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    AccountInfo accountInfo = new AccountInfo();
                    accountInfo.User_id = Int32.Parse(dr["user_id"].ToString());
                    accountInfo.User_name = dr["user_name"].ToString();
                    accountInfo.Sex = short.Parse(dr["user_sex"].ToString());
                    accountInfo.Birthday = DateTime.Parse(dr["user_birthday"].ToString());
                    accountInfo.Phone = dr["uesr_phome"].ToString();
                    arr.Add(accountInfo);
                }

                try
                {
                    sql = "SELECT * FROM users INNER JOIN users_information ON users.user_id = " + Int32.Parse(selAccount.Sel_info);
                    com = new MySQLCommand(sql, con);
                    DbDataReader reader = com.ExecuteReader();
                    if(reader.Read())
                    {
                        AccountInfo accountInfo = new AccountInfo();
                        accountInfo.User_id = Int32.Parse(reader["user_id"].ToString());
                        accountInfo.User_name = reader["user_name"].ToString();
                        accountInfo.Sex = short.Parse(reader["user_sex"].ToString());
                        accountInfo.Birthday = DateTime.Parse(reader["user_birthday"].ToString());
                        accountInfo.Phone = reader["uesr_phome"].ToString();
                        arr.Add(accountInfo);
                    }
                }
                catch
                {

                }

                return arr;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public bool AddFollowing(FollowingData data)
        {
            try
            {
                con.Open();

                sql = "INSERT INTO following_list(user_id,follow_id) VALUES(" + data.User_id + "," + data.Following_id + ")";
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();

                sql = "SELECT * FROM black_list WHERE user_id=" + data.User_id + " and black_id=" + data.Following_id;
                com = new MySQLCommand(sql, con);
                MySQLDataReader reader = com.ExecuteReaderEx();
                if(reader.Read())
                {
                    sql = "DELETE FROM black_list WHERE user_id=" + data.User_id + " and black_id=" + data.Following_id;
                    com = new MySQLCommand(sql, con);
                    com.ExecuteNonQuery();
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool RemoveFollowing(FollowingData data)
        {
            try
            {
                con.Open();

                sql = "SELECT * FROM following_list where user_id=" + data.User_id + " and follow_id=" + data.Following_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                MySQLDataReader reader = com.ExecuteReaderEx();
                if(reader.Read())
                {
                    sql = "DELETE FROM following_list where user_id=" + data.User_id + " and follow_id=" + data.Following_id;
                    com = new MySQLCommand(sql, con);
                    com.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public string SelUserImageName(int user_id)
        {
            try
            {
                con.Open();
                sql = "SELECT * FROM user_images WHERE user_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                    return reader["file_name"].ToString();
                else
                    return FileFlags.FileExistsFailedFlag;
            }
            catch
            {
                return FileFlags.FileExistsFailedFlag;
            }
            finally
            {
                con.Close();
            }
        }

        public bool InsertUserImage(int user_id,string strFileName)
        {
            try
            {
                con.Open();
                sql = "INSERT INTO user_images(user_id, file_name) VALUES(" + user_id + ", " + strFileName + ")";
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool ChangeUser_Image(int user_id, string strFileName)
        {
            try
            {
                con.Open();

                sql = "SELECT * FROM user_images WHERE user_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    sql = "DELETE FROM uesr_images WHERE file_name=" + strFileName;
                    com = new MySQLCommand(sql, con);
                    com.ExecuteNonQuery();
                }

                sql = "INSERT INTO user_images(user_id, file_name) VALUES(" + user_id + ", " + strFileName + ")";
                com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool UpLoadFile(int user_id, string strFileName, string strFileKey)
        {
            try
            {
                con.Open();

                sql = "SELECT * FROM files WHERE file_name=" + strFileName + " AND user_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);

                DbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                    return true;
                else
                {
                    sql = "INSERT INTO files(user_id, file_name, file_key) VALUES(" + user_id + ", " + strFileName + ", " + strFileKey + ")";
                    com = new MySQLCommand(sql, con);
                    com.ExecuteNonQuery();
                }
                
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool CheckFileKey(int user_id,string strFileName,string strFileKey)
        {
            try
            {
                con.Open();

                sql = "SELECT * FROM files WHERE file_name=" + strFileName + " AND user_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);

                DbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                    if (reader["file_key"].ToString() == strFileKey)
                        return true;
                    else
                        return false;
                else
                    return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        //public bool DelOverMessages(int bool)

//         --插入用户
//INSERT INTO users(user_name, user_password) VALUES(?, ?);

//--搜索最大值
//SELECT LAST_INSERT_ID();

//--插入用户资料
//INSERT INTO users_information VALUES(?, ?, ?, ?);

//--修改全部信息
//UPDATE user_sex = ?, user_birthday = ?, user_phone = ? FROM users_information WHERE user_id = ?;

//--修改密码
//UPDATE user_password FROM users WHERE user_id = ?;

//--查找密码
//SELECT user_password FROM users WHERE user_id = ?;

//--查找全部信息
//SELECT* FROM users_information WHERE user_id = ?;

//--查找用户名
//SELECT user_name FROM users WHERE user_id = ?;
    }
}
