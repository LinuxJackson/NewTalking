using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Controls;

namespace NewTalking.Anim.LoginWindow
{
    internal static class StartingAnim
    {
        static Label lblFillUser_id;
        static Label lblFillUser_pwd;
        static Label btnLogin;
        static Label lblVersion;
        static Label lblSignup;
        static Image igIcon;

        internal static void Show(Label lblWelcome, Label btnLogin, TextBox txtUser_id,
            TextBox txtUser_pwd, Label lblFillUser_id, Label lblFillUser_pwd, Label lblVersion, Label lblSignup, Image igIcon)
        {
            lblFillUser_id.Opacity = 0;
            lblFillUser_pwd.Opacity = 0;
            btnLogin.Opacity = 0;
            lblVersion.Opacity = 0;
            lblSignup.Opacity = 0;
            igIcon.Opacity = 0;

            StartingAnim.btnLogin = btnLogin;
            StartingAnim.lblFillUser_id = lblFillUser_id;
            StartingAnim.lblFillUser_pwd = lblFillUser_pwd;
            StartingAnim.lblVersion = lblVersion;
            StartingAnim.lblSignup = lblSignup;
            StartingAnim.igIcon = igIcon;

            ThicknessAnimation tka = new ThicknessAnimation();
            tka.AccelerationRatio = 0.2;
            tka.DecelerationRatio = 0.8;

            tka.From = new System.Windows.Thickness(
                lblWelcome.Margin.Left,
                lblWelcome.Margin.Top - 100,
                lblWelcome.Margin.Right,
                lblWelcome.Margin.Bottom
                );

            tka.To = new System.Windows.Thickness(
                lblWelcome.Margin.Left,
                lblWelcome.Margin.Top,
                lblWelcome.Margin.Right,
                lblWelcome.Margin.Bottom
                );

            tka.Duration = TimeSpan.FromSeconds(2);
            lblWelcome.BeginAnimation(Label.MarginProperty, tka);

            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = TimeSpan.FromSeconds(2);

            lblWelcome.BeginAnimation(Label.OpacityProperty, da);
            //lblFillUser_id.BeginAnimation(Label.OpacityProperty, da);
            //lblFillUser_pwd.BeginAnimation(Label.OpacityProperty, da);
            //btnLogin.BeginAnimation(Label.OpacityProperty, da);
            da.Duration = TimeSpan.FromSeconds(0.8);

            txtUser_id.BeginAnimation(TextBox.OpacityProperty, da);
            txtUser_pwd.BeginAnimation(TextBox.OpacityProperty, da);

            tka.Duration = TimeSpan.FromSeconds(0.8);
            tka.From = new System.Windows.Thickness(
                txtUser_id.Margin.Left,
                txtUser_id.Margin.Top + 200,
                txtUser_id.Margin.Right,
                txtUser_id.Margin.Bottom
                );

            tka.To = new System.Windows.Thickness(
                txtUser_id.Margin.Left,
                txtUser_id.Margin.Top,
                txtUser_id.Margin.Right,
                txtUser_id.Margin.Bottom
                );

            txtUser_id.BeginAnimation(TextBox.MarginProperty, tka);

            tka.From = new System.Windows.Thickness(
                txtUser_pwd.Margin.Left,
                txtUser_pwd.Margin.Top + 400,
                txtUser_pwd.Margin.Right,
                txtUser_pwd.Margin.Bottom
                );

            tka.To = new System.Windows.Thickness(
                txtUser_pwd.Margin.Left,
                txtUser_pwd.Margin.Top,
                txtUser_pwd.Margin.Right,
                txtUser_pwd.Margin.Bottom
                );

            tka.Completed += Tka_Completed;
            txtUser_pwd.BeginAnimation(TextBox.MarginProperty, tka);
        }

        private static void Tka_Completed(object sender, EventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = TimeSpan.FromSeconds(1.3);
            lblFillUser_id.BeginAnimation(Label.OpacityProperty, da);
            lblFillUser_pwd.BeginAnimation(Label.OpacityProperty, da);
            btnLogin.BeginAnimation(Label.OpacityProperty, da);
            lblVersion.BeginAnimation(Label.OpacityProperty, da);
            lblSignup.BeginAnimation(Label.OpacityProperty, da);
            igIcon.BeginAnimation(Label.OpacityProperty, da);
        }
    }
}
