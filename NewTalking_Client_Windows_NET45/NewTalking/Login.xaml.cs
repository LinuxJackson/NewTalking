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
using NewTalking.Anim.LoginWindow;
using System.Windows.Threading;
using libBgbll.Account;

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
            //image2.Source = new BitmapImage(new Uri("/images/2.jpg", UriKind.Relative));
            InitializeComponent();
            igIcon.Source = new BitmapImage(new Uri(@"\Resources\Images\U@[A(UJCLE08$~N82{287%L.png", UriKind.Relative));
        }
        
        private bool IsConnected_flag = false;

        DispatcherTimer tmrReconnect = new DispatcherTimer();
        private async void Connect()
        {
            reconnectShowEnabledFlag = true;
            lblState.MouseDown -= reconnect_MouseDown;
            tmrReconnect.IsEnabled = false;
            reconnectTime = 20;
            try
            {
                LabelOpacity.AutoTimesShow(this.lblState, 1000, 1.5, "Connecting...");
                Task<bool> conn = ConnectServer.ConnectAsync();
                bool IsConnected = await conn;
                if (IsConnected)
                {
                    LabelOpacity.AutoTimesShow(this.lblState, 3, 0.7, "Connected");
                    IsConnected_flag = true;
                    while (true)
                    {
                        byte[] rece = await ReceiveData.Receive();
                        await Task.Run(() => { byte[] data = rece; MainBLL.Analysis(data); });
                    }
                }
                else
                {
                    lblState.MouseDown += reconnect_MouseDown;
                    tmrReconnect.IsEnabled = true;
                    IsConnected_flag = false;
                    LabelOpacity.AutoTimesShow(this.lblState, 2, 0.7, "Connection Failed");
                }
            }
            catch
            {
                lblState.MouseDown += reconnect_MouseDown;
                tmrReconnect.IsEnabled = true;
                LabelOpacity.AutoTimesShow(this.lblState, 2, 0.7, "Connection Failed");
                IsConnected_flag = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tmrReconnect.Tick += TmrReconnect_Tick;
            tmrReconnect.Interval = TimeSpan.FromSeconds(1);

            StartingAnim.Show(lblNewTalking, lblBtnLogin, txtUser_id, txtUser_pwd, lblFillUser_id, lblFillUser_pwd,lblVersion, lblBtnSignup, igIcon);
            txtUser_id.Focus();
            Connect();
        }

        bool reconnectShowEnabledFlag = true;
        int reconnectTime = 0;
        private void TmrReconnect_Tick(object sender, EventArgs e)
        {
            reconnectTime--;
            if (reconnectTime < 15)
                if (reconnectShowEnabledFlag)
                    lblState.Content = reconnectTime + " seconds or [Click Here] to reconnect";
            if (reconnectTime == 0)
                Connect();
        }

        private void reconnect_MouseDown(object sender,MouseButtonEventArgs e)
        {
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
            LabelOpacity.AutoTimesShow(lbl, 3, 0.5, text);
        }

        private void Login_Form()
        {
            reconnectShowEnabledFlag = false;
            LabelOpacity.AutoTimesShow(this.lblState, 3, 0.7, "Logining...");
            if (CheckBlanks())
            {
                if (IsConnected_flag)
                {
                    lblBtnLogin.IsEnabled = false;
                    txtUser_id.IsEnabled = false;
                    txtUser_pwd.IsEnabled = false;

                    LoginData loginData = new LoginData();
                    loginData.User_id = Int32.Parse(txtUser_id.Text);
                    loginData.User_password = txtUser_pwd.Text;

                    loginData.Uid = CallBackNum.CallBackIndex = CallBackNum.CallBackIndex + 1;

                    FuncReceiveData func = delegate (byte[] data)
                     {
                         LoginData_Re loginData_re = LoginDataConverter.ConvertToClass(data);
                         if (loginData_re.IsLogined)
                         {
                             this.lblState.Dispatcher.BeginInvoke(new ChangeTextFunc(ChangeText), lblState, "User password is not correct!");
                         }
                         else
                         {
                             this.lblState.Dispatcher.BeginInvoke(new ChangeTextFunc(ChangeText), lblState, "User password is not correct!");
                             this.lblBtnLogin.IsEnabled = true;
                             txtUser_id.IsEnabled = true;
                             txtUser_pwd.IsEnabled = true;
                         }
                     };
                    Login.UserLogin(func, loginData);
                }
                else
                {
                    LabelOpacity.AutoTimesShow(lblState, 2, 0.5, "Connection Failed", isReverse: false);
                }
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
            if (txtUser_pwd.Text != "")
                lblFillUser_pwd.Visibility = Visibility.Hidden;
            else
                lblFillUser_pwd.Visibility = Visibility.Visible;
        }

        private void txtUserKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (isOnSigningUp)
                    CreateAccount_Form();
                else
                    Login_Form();
            }
        }

        private bool CheckBlanks()
        {
            int a;
            if (!Int32.TryParse(txtUser_id.Text, out a))
            {
                txtUser_id.Focus();
                txtUser_id.SelectAll();
                LabelOpacity.AutoTimesShow(lblState, 2, 0.5, "User id is not correct!", isReverse: false);
                return false;
            }
            if (txtUser_pwd.Text == "" || txtUser_pwd.Text.Length > 16)
            {
                txtUser_pwd.Focus();
                txtUser_pwd.SelectAll();
                LabelOpacity.AutoTimesShow(lblState, 2, 0.5, "Password can't be null or more than 16", isReverse: false);
                return false;
            }
            return true;
        }

        bool isOnSigningUp = false;

        private void lblBtnSignup_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(!isOnSigningUp)
            {
                SignupAnim.TurnToSignup(lblBtnSignup, lblBtnLogin, txtUser_id, lblReturnToLogin, lblFillUser_pwd);
                isOnSigningUp = true;
                txtUser_pwd.Focus();
            }
            else
            {
                CreateAccount_Form();
            }
        }

        private void lblReturnToLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isOnSigningUp = false;
            SignupAnim.TurnToLogin(lblBtnSignup, lblBtnLogin, txtUser_id, lblReturnToLogin, lblFillUser_pwd);
            txtUser_id.Focus();
        }

        private delegate void TextBoxTextChangeFunc(TextBox txt, string strText);
        private void TextBoxTextChange(TextBox txt, string strText)
        {
            txt.Text = strText;
        }

        private delegate void TextBoxEnableFunc(TextBox txt);

        private void TextBoxEnabled(TextBox txt)
        {
            txt.IsEnabled = true;
        }

        private async void CreateAccount_Form()
        {
            reconnectShowEnabledFlag = false;

            if (txtUser_pwd.Text == "" || txtUser_pwd.Text.Length > 16)
            {
                LabelOpacity.AutoTimesShow(lblState, 2, 0.5, "Password can't be null or more than 16", isReverse: false);
                return;
            }
            if (IsConnected_flag)
            {
                lblBtnSignup.IsEnabled = false;
                txtUser_pwd.IsEnabled = false;

                LoginData loginData = new LoginData();
                LabelOpacity.AutoTimesShow(this.lblState, 3, 0.7, "Signing Up...");
                loginData.User_password = txtUser_pwd.Text;
                loginData.Uid = CallBackNum.CallBackIndex = CallBackNum.CallBackIndex + 1;

                FuncReceiveData func = delegate(byte[] data)
                {
                    LoginData loginData_re = AccountRequestConverter.ConvertToClass(data);
                    if (loginData_re.User_id == 0)
                    {
                        this.lblState.Dispatcher.BeginInvoke(new ChangeTextFunc(ChangeText), lblState, "Couldn't sign up!");
                    }
                    else
                    {
                        this.lblState.Dispatcher.BeginInvoke(new ChangeTextFunc(ChangeText), lblState, "Your new account: " + loginData_re.User_id);
                        txtUser_id.Dispatcher.BeginInvoke(new TextBoxTextChangeFunc(TextBoxTextChange), txtUser_id, loginData_re.User_id.ToString());
                        txtUser_pwd.Dispatcher.BeginInvoke(new TextBoxEnableFunc(TextBoxEnabled), txtUser_pwd);
                    }
                };

                bool isCreated = await CreateAccount.Create(loginData, func);
            }
            else
            {
                LabelOpacity.AutoTimesShow(lblState, 2, 0.5, "Connection Failed", isReverse: false);
            }
        }
    }
}
