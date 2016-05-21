using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace NewTalking.Anim.LoginWindow
{
    static internal class SignupAnim
    {
        static internal void TurnToSignup(Label lblSignup, Label lblLogin, TextBox txtUser_id, Label lblReturn, Label lblUser_pwd)
        {
            txtUser_id.IsEnabled = false;
            lblLogin.IsEnabled = false;
            lblReturn.IsEnabled = true;
            lblUser_pwd.Content = "Your New Password";

            //Margin="191,182,0,0"
            ThicknessAnimation tka = new ThicknessAnimation();
            tka.From = lblSignup.Margin;
            tka.To = new System.Windows.Thickness(230, 182, 0, 0);
            tka.Duration = TimeSpan.FromSeconds(0.5);
            tka.AccelerationRatio = 0.2;
            tka.DecelerationRatio = 0.8;
            lblSignup.BeginAnimation(Label.MarginProperty, tka);

            DoubleAnimation da = new DoubleAnimation();
            da.From = lblLogin.Opacity;
            da.To = 0;
            da.Duration = TimeSpan.FromSeconds(0.4);
            lblLogin.BeginAnimation(Label.OpacityProperty, da);

            da.From = lblReturn.Opacity;
            da.To = 1;
            lblReturn.BeginAnimation(Label.OpacityProperty, da);
        }

        static internal void TurnToLogin(Label lblSignup, Label lblLogin, TextBox txtUser_id, Label lblReturn, Label lblUser_pwd)
        {
            txtUser_id.IsEnabled = true;
            lblLogin.IsEnabled = true;
            lblReturn.IsEnabled = false;
            lblUser_pwd.Content = "Your Password";

            //Margin="191,182,0,0"
            ThicknessAnimation tka = new ThicknessAnimation();
            tka.From = lblSignup.Margin;
            tka.To = new System.Windows.Thickness(191, 182, 0, 0);
            tka.Duration = TimeSpan.FromSeconds(0.5);
            tka.AccelerationRatio = 0.2;
            tka.DecelerationRatio = 0.8;
            lblSignup.BeginAnimation(Label.MarginProperty, tka);

            DoubleAnimation da = new DoubleAnimation();
            da.From = lblLogin.Opacity;
            da.To = 1;
            da.Duration = TimeSpan.FromSeconds(0.4);
            lblLogin.BeginAnimation(Label.OpacityProperty, da);

            da.From = lblReturn.Opacity;
            da.To = 0;
            lblReturn.BeginAnimation(Label.OpacityProperty, da);
        }
    }
}
