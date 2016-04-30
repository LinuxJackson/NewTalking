using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Newtalking_DAL_Data
{
    public class MessageDataConvert
    {
        public byte[] ConvertToBytes(MessageData data)
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

        public MessageData ConvertToClass(byte[] bReceived)
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

    public class LoginDataConvert
    {
        public byte[] ConvertToBytes(bool boolean, int uid)
        {
            byte[] bResult = new byte[6];
            BitConverter.GetBytes(boolean).CopyTo(bResult, 0);
            BitConverter.GetBytes(uid).CopyTo(bResult, 2);

            return bResult;
        }

        public LoginData ConvertToClass(byte[] data)
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

    public class AccountRequestConvert
    {
        public LoginData ConvertToClass(byte[] data)
        {
            LoginData dataResult = new LoginData();
            dataResult.Uid = BitConverter.ToInt32(data, 2);
            dataResult.User_password = BitConverter.ToString(data, 10, 16);

            return dataResult;
        }

        public byte[] ConvertToBytes(LoginData data)
        {
            byte[] bResult = new byte[26];

            short type = 3;
            BitConverter.GetBytes(type).CopyTo(bResult, 0);
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
    public class AccountInfoConvet
    {
        public byte[] ConvertToBytes(AccountInfo data)
        {
            //4+2+4+24
            byte[] bResult = new byte[46];

            BitConverter.GetBytes(data.MessageType).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.Uid).CopyTo(bResult, 2);
            BitConverter.GetBytes(data.User_id).CopyTo(bResult, 6);
            BitConverter.GetBytes(data.Sex).CopyTo(bResult, 10);
            BitConverter.GetBytes(data.Birthday.Ticks).CopyTo(bResult, 12);
            Encoding.Default.GetBytes(data.Phone).CopyTo(bResult, 20);

            return bResult;
        }

        public AccountInfo ConvertToClass(byte[] data)
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
    }

    public class FileRequestConvert
    {
        public FileRequest ConvertToClass_Send(byte[] data)
        {
            FileRequest fileRequest = new FileRequest();
            fileRequest.Uid = BitConverter.ToInt32(data, 2);
            fileRequest.User_id = BitConverter.ToInt32(data, 6);
            fileRequest.FileName = BitConverter.ToString(data, 10);

            return fileRequest;
        }

        public ReceiveFileRequest ConvertToData_Receive(byte[] data)
        {
            ReceiveFileRequest receiveFile = new ReceiveFileRequest();
            receiveFile.User_id = BitConverter.ToInt32(data, 0);
            receiveFile.File_name_length = BitConverter.ToInt16(data, 4);
            receiveFile.File_name = BitConverter.ToString(data, 6, receiveFile.File_name_length);
            return receiveFile;
        }
    }

    public class RefreshRequestConvert
    {
        public RefreshRequest ConvertToClass(byte[] data)
        {
            RefreshRequest rr = new RefreshRequest();
            rr.Uid = BitConverter.ToInt32(data, 2);
            rr.User_id = BitConverter.ToInt32(data, 6);

            return rr;
        }
    }

    public class SelectAccountConvert
    {
        public SelectAccount ConvertToClass(byte[] data)
        {
            SelectAccount sel = new SelectAccount();
            sel.Uid = BitConverter.ToInt32(data, 2);
            sel.Sel_info = Encoding.Default.GetString(data, 6, 30);

            return sel;
        }
    }

    public class FollowingConvert
    {
        public FollowingData ConvertToClass(byte[] data)
        {
            FollowingData followingData = new FollowingData();
            followingData.Uid = BitConverter.ToInt32(data, 2);
            followingData.User_id = BitConverter.ToInt32(data, 6);
            followingData.Following_id = BitConverter.ToInt32(data, 10);

            return followingData;
        }

        public byte[] ConvertToBytes(FollowingData data, bool isSucceed)
        {
            byte[] bResult = new byte[6];

            BitConverter.GetBytes(data.User_id).CopyTo(bResult, 0);
            BitConverter.GetBytes(isSucceed).CopyTo(bResult, 4);

            return bResult;
        }
    }
}
