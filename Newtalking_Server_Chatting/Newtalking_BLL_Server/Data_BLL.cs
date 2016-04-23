using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Threading;
using System.Collections;

namespace Newtalking_BLL_Server
{
    public class Data_BLL
    {
        public void Analysis(DataPackage data)
        {
            //性能待优化
            Thread tdAnalysis = new Thread(delegate () {

            short type;
            byte[] bType = new byte[2];

            bType[0] = data.Data[0];
            bType[1] = data.Data[1];
            type = BitConverter.ToInt16(bType, 0);

                switch (type)
                {
                    case 1:            //信息[测试成功]
                        Message message = new Message(data.Data);
                        message.Send();
                        break;
                    case 2:            //登陆[测试成功]
                        AccountLogin accountLogin = new AccountLogin(data);
                        if (accountLogin.Login())
                        {
                            accountLogin.AddToOnlineUserList();
                        }
                        accountLogin.Respect();
                        break;
                    case 3:         //账号申请[未测试]
                        AccountRequest accountRequest = new AccountRequest(data);
                        accountRequest.Response();
                        break;
                    case 4:        //获取账户信息[未测试]
                        ReadUserInfo readUserInfo = new ReadUserInfo(data);
                        readUserInfo.Response();
                        break;
                    case 5:        //修改账户信息[未测试]
                        EditAccountInfo editUserInfo = new EditAccountInfo(data);
                        editUserInfo.Response();
                        break;
                    case 6:        //请求文件[未测试] --机制待修改[重要]
                        SendFile sendFile = new SendFile(data);
                        sendFile.Send();
                        break;
                    case 7:        //接收文件[未测试] --机制待修改[重要]
                                   //开辟新线程[待修改]
                        Newtalking_BLL_Server.File.ReceiveFile receFile = new File.ReceiveFile(data);
                        receFile.Receive();
                        break;
                    case 8:         //用户头像申请[未测试]
                        SendUserImage sendUserImage = new SendUserImage(data);
                        sendUserImage.Send();
                        break;
                    case 9:         //消息刷新申请[未测试]
                        MessageFresh msgFresh = new MessageFresh(data);
                        msgFresh.Response();
                        break;
                    case 10:        //搜索用户

                        break;
                    case 11:        //添加关注
                        break;
                    case 12:        //撤销关注
                        break;
                    case 13:        //屏蔽
                        break;
                    case 14:        //图片消息[待定]
                        break;
                }
            });
            tdAnalysis.Start();
        }
    }
}
