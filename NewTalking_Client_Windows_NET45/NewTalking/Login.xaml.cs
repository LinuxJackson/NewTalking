﻿using System;
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
using NewTalking.Anim.LoginWindow;

namespace NewTalking
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 

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
                LabelOpacity.AutoTimesShow(this.lblState, 1000, 1.5, true, "Connecting...");
                Task<bool> conn = ConnectServer.ConnectAsync();
                bool IsConnected = await conn;
                if (IsConnected)
                {
                    LabelOpacity.AutoTimesShow(this.lblState, 3, 0.7, true, "Connected");
                    while (true)
                    {
                        byte[] rece = await ReceiveData.Receive();
                        await Task.Run(() => { byte[] data = rece; MainBLL.Analysis(data); });
                    }
                }
                else
                    LabelOpacity.AutoTimesShow(this.lblState, 3, 0.7, true, "Connection Failed");
            }
            catch
            {
                LabelOpacity.AutoTimesShow(this.lblState, 3, 0.7, true, "Connection Failed");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUser_id.Focus();
            Connect();
        }

        private void lblBtnLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Login_Form();
            lblBtn_MouseDown(sender, e);
        }

        private delegate void ChangeTextFunc(Label lbl, string text);
        private void ChangeText(Label lbl, string text)
        {
            LabelOpacity.AutoTimesShow(lbl, 3, 0.5, false, "User password is not correct!");
        }

        private void Login_Form()
        {
            if (CheckBlanks())
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
                         this.lblState.Dispatcher.BeginInvoke(new ChangeTextFunc(ChangeText), lblState, "User password is not correct!");
                     }
                     else
                     {
                         this.lblState.Dispatcher.BeginInvoke(new ChangeTextFunc(ChangeText), lblState, "User password is not correct!");
                         this.lblBtnLogin.IsEnabled = true;
                     }
                 };
                Login.UserLogin(func, LoginData);
            }
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

        private void lblFillUser_pwd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUser_pwd.Focus();
        }

        private void lblFillUser_id_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUser_id.Focus();
        }

        private void txtUser_id_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtUser_id.Text != "")
                lblFillUser_id.Visibility = Visibility.Hidden;
            else
                lblFillUser_id.Visibility = Visibility.Visible;
        }

        private void txtUser_pwd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtUser_id.Text != "")
                lblFillUser_pwd.Visibility = Visibility.Hidden;
            else
                lblFillUser_pwd.Visibility = Visibility.Visible;
        }

        private void txtUserKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (CheckBlanks())
                    Login_Form();
            }
        }

        private bool CheckBlanks()
        {
            int a;
            if (!Int32.TryParse(txtUser_id.Text, out a))
            {
                LabelOpacity.AutoTimesShow(lblState, 2, 0.5, false, "User id is not correct!");
                return false;
            }
            if (txtUser_pwd.Text == "" || txtUser_pwd.Text.Length > 16)
            {
                LabelOpacity.AutoTimesShow(lblState, 2, 0.5, false, "User password is not correct!");
                return false;
            }
            return true;
        }
    }
}
