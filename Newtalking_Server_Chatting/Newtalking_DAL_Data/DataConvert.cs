using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Newtalking_DAL_Data
{
    static public class MessageDataConvert
    {
        static public byte[] ConvertToBytes(MessageData data)
        {
            byte[] bResult = new byte[1452];

            short type = 1;
            BitConverter.GetBytes(type).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.User_id).CopyTo(bResult, 2);
            BitConverter.GetBytes(data.Receiver_id).CopyTo(bResult, 6);
            BitConverter.GetBytes(data.Time.Ticks).CopyTo(bResult, 10);
            Encoding.Default.GetBytes(data.Message).CopyTo(bResult, 18);

            return bResult;
        }

        static public MessageData ConvertToClass(byte[] bReceived)
        {
            MessageData dataResult = new MessageData();

            dataResult.User_id = BitConverter.ToInt32(bReceived, 2);
            dataResult.Receiver_id = BitConverter.ToInt32(bReceived, 6);
            long timeTick = BitConverter.ToInt64(bReceived, 10);
            dataResult.Time = new DateTime(timeTick);
            string msgTemp = Encoding.Default.GetString(bReceived, 18, 1434);

            foreach (char c in msgTemp)
            {
                if (c == '\0')
                    break;
                dataResult.Message += c;
            }

            return dataResult;
        }
    }

    static public class LoginDataConvert
    {
        static public byte[] ConvertToBytes(bool boolean, int uid)
        {
            byte[] bResult = new byte[8];
            BitConverter.GetBytes((short)2).CopyTo(bResult, 0);
            BitConverter.GetBytes(uid).CopyTo(bResult, 2);
            BitConverter.GetBytes(boolean).CopyTo(bResult, 6);

            return bResult;
        }

        static public LoginData ConvertToClass(byte[] data)
        {
            LoginData dataResult = new LoginData();
            dataResult.Uid = BitConverter.ToInt32(data, 2);
            dataResult.User_id = BitConverter.ToInt32(data, 6);

            string pwd = Encoding.Default.GetString(data, 10, 16);
            int i = 0;
            while(pwd[i]!='\0')
            {
                dataResult.User_password += pwd[i];
                i++;
            }

            return dataResult;
        }
    }

    static public class AccountRequestConvert
    {
        static public LoginData ConvertToClass(byte[] data)
        {
            LoginData dataResult = new LoginData();
            dataResult.Uid = BitConverter.ToInt32(data, 2);
            string sTemp = Encoding.Default.GetString(data, 10, 16);
            foreach (char c in sTemp)
            {
                if (c == '\0')
                    break;
                dataResult.User_password += c;
            }
            return dataResult;
        }

        static public byte[] ConvertToBytes(LoginData data)
        {
            byte[] bResult = new byte[26];
            
            BitConverter.GetBytes((short)2).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.Uid).CopyTo(bResult, 2);
            BitConverter.GetBytes(data.User_id).CopyTo(bResult, 6);
            Encoding.Default.GetBytes(data.User_password).CopyTo(bResult, 10);
            
            return bResult;
        }
    }
    //    CREATE DATABASE NewTalking;
    //CREATE TABLE users(user_id INTEGER PRIMARY KEY AUTO_INCREMENT, user_name VARCHAR(30) NOT NULL, user_password VARCHAR(20) NOT NULL);
    //CREATE TABLE users_information(user_id INTEGER, user_sex CHAR(2), user_birthday INTEGER, user_phone INTEGER, FOREIGN KEY(user_id) REFERENCES users(user_id));

    //INSERT INTO users(user_name, user_password) VALUES('大明', 123456);
    //    INSERT INTO users(user_name, user_password) VALUES('小明', 123456);

    //    INSERT INTO users_information VALUES(1, '男', 946659661, 10086);
    //    INSERT INTO users_information VALUES(2, '男', 946659662, 10010);

    //    SELECT* FROM users INNER JOIN users_information ON users.user_id = users_information.user_id;
    static public class AccountInfoConvet
    {
        static public byte[] ConvertToBytes(AccountInfo data)
        {
            //4+2+4+24
            byte[] bResult = new byte[48];

            BitConverter.GetBytes((short)2).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.Uid).CopyTo(bResult, 2);
            BitConverter.GetBytes(data.User_id).CopyTo(bResult, 6);
            BitConverter.GetBytes(data.Sex).CopyTo(bResult, 10);
            BitConverter.GetBytes(data.Birthday.Ticks).CopyTo(bResult, 12);
            Encoding.Default.GetBytes(data.Phone).CopyTo(bResult, 20);

            return bResult;
        }

        static public AccountInfo ConvertToClass(byte[] data)
        {
            AccountInfo dataResult = new AccountInfo();

            dataResult.User_id = BitConverter.ToInt32(data, 2);
            dataResult.Sex = BitConverter.ToInt16(data, 6);
            dataResult.Birthday = new DateTime(BitConverter.ToInt64(data, 12));
            string tempPhone = Encoding.Default.GetString(data, 20, 24);

            foreach(char c in tempPhone)
            {
                if (c == '\0')
                    break;
                dataResult.Phone += c;
            }

            return dataResult;
        }

        static public byte[] ConvertToBytes_Re(bool boolean, Int32 uid)
        {
            byte[] bResult = new byte[8];

            BitConverter.GetBytes((short)2).CopyTo(bResult, 0);
            BitConverter.GetBytes(uid).CopyTo(bResult, 2);
            BitConverter.GetBytes(boolean).CopyTo(bResult, 6);

            return bResult;
        }
    }

    static public class FileRequestConvert
    {
        static public FileRequest ConvertToClass_Send(byte[] data)
        {
            FileRequest fileRequest = new FileRequest();
            fileRequest.Uid = BitConverter.ToInt32(data, 2);
            fileRequest.User_id = BitConverter.ToInt32(data, 6);

            string temp = Encoding.Default.GetString(data, 10, 255);
            foreach(char c in temp)
            {
                if (c == '\0')
                    break;
                fileRequest.FileName += c;
            }

            fileRequest.FileKey = Encoding.Default.GetString(data, 265, 16);
            return fileRequest;
        }

        static public ReceiveFileRequest ConvertToData_Receive(byte[] data)
        {
            ReceiveFileRequest receiveFile = new ReceiveFileRequest();
            receiveFile.User_id = BitConverter.ToInt32(data, 2);
            receiveFile.File_name_length = BitConverter.ToInt16(data, 6);
            receiveFile.File_name =Encoding.Default.GetString(data, 8, receiveFile.File_name_length);
            return receiveFile;
        }
    }

    static public class RefreshRequestConvert
    {
        static public RefreshRequest ConvertToClass(byte[] data)
        {
            RefreshRequest rr = new RefreshRequest();
            rr.Uid = BitConverter.ToInt32(data, 2);
            rr.User_id = BitConverter.ToInt32(data, 6);

            return rr;
        }

        static public byte[] ConvertToBytes_End(Int32 uid)
        {
            byte[] bResult = new byte[10];

            BitConverter.GetBytes((short)2).CopyTo(bResult, 0);
            BitConverter.GetBytes(uid).CopyTo(bResult, 2);
            BitConverter.GetBytes((Int32)0).CopyTo(bResult, 6);

            return bResult;
        }
    }

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

    static public class FollowingConvert
    {
        static public FollowingData ConvertToClass(byte[] data)
        {
            FollowingData followingData = new FollowingData();
            followingData.Uid = BitConverter.ToInt32(data, 2);
            followingData.User_id = BitConverter.ToInt32(data, 6);
            followingData.Following_id = BitConverter.ToInt32(data, 10);

            return followingData;
        }

        static public byte[] ConvertToBytes(FollowingData data, bool isSucceed)
        {
            byte[] bResult = new byte[6];

            BitConverter.GetBytes(data.User_id).CopyTo(bResult, 0);
            BitConverter.GetBytes(isSucceed).CopyTo(bResult, 4);

            return bResult;
        }
    }

    static public class UserImageConvert
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
