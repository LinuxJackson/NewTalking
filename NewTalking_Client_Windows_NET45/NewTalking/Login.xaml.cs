using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using libBgbll.Server;
using Model;
using libBgbll;
using System.Threading.Tasks;
using NewTalking.Data;
using libBgbll.Login;
using libData;
using DataConverter;

namespace NewTalking
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Connect()
        {
            try
            {
                Task<bool> conn = ConnectServer.ConnectAsync();
                bool IsConnected = await conn;
                if (IsConnected)
                {
                    lblState.Content = "连接成功";
                    while (true)
                    {
                        byte[] rece = await ReceiveData.Receive();
                        await Task.Run(() => { byte[] data = rece; MainBLL.Analysis(data); });
                    }
                }
                else
                    lblState.Content = "连接异常";
            }
            catch
            {
                this.lblState.Content = "连接异常";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Connect();
        }

        private void lblBtnLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Login_Form();
            lblBtn_MouseDown(sender, e);
        }

        private void Login_Form()
        {
            lblBtnLogin.IsEnabled = false;
            LoginData LoginData = new LoginData();
            LoginData.User_id = Int32.Parse(txtUser_id.Text);
            LoginData.User_password = txtUser_pwd.Text;

            LoginData.Uid = CallBackNum.CallBackIndex = CallBackNum.CallBackIndex + 1;

            FuncReceiveData func = delegate (byte[] data)
             {
                 LoginData_Re loginData = LoginDataConvert.ConvertToClass(data);
                 if (loginData.IsLogined)
                 {
                     this.lblState.Content = "Welcome!";
                 }
                 else
                 {
                     this.lblState.Content = "Failed!";
                     this.lblBtnLogin.IsEnabled = true;
                 }
             };
            Login.UserLogin(func, LoginData);
        }

        private void lblBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void lblBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void lblBtn_MouseDown(object sender, MouseEventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.Foreground = new SolidColorBrush(Colors.LightBlue);
        }
    }
}
