using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace NewTalking.Anim.LoginWindow
{
    internal static class LabelOpacity
    {
        static Label lbl;
        static int times;
        static double time;
        static bool isReverse;
        static string strText;

        internal static void AutoTimesShow(Label lbl, int times, double time, string strText, bool isReverse = true)
        {
            LabelOpacity.lbl = lbl;
            LabelOpacity.times = times;
            LabelOpacity.time = time;
            LabelOpacity.isReverse = isReverse;
            LabelOpacity.strText = strText;
            DoubleAnimation da = new DoubleAnimation();
            da.From = lbl.Opacity;
            da.To = 0;
            da.Duration = TimeSpan.FromSeconds(0.5);
            da.Completed += Da_Completed;
            lbl.BeginAnimation(Label.OpacityProperty, da);
        }

        private static void Da_Completed(object sender, EventArgs e)
        {
            lbl.Content = strText;
            DoubleAnimation da = new DoubleAnimation();
            da.From = lbl.Opacity;
            da.To = 1;
            da.Duration = TimeSpan.FromSeconds(time);
            da.RepeatBehavior = new RepeatBehavior(times);
            da.AutoReverse = true;
            if (isReverse)
                da.Completed += daReverse_Completed;
            lbl.BeginAnimation(Label.OpacityProperty, da);
        }

        private static void daReverse_Completed(object sender, EventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = lbl.Opacity;
            da.To = 0.6;
            da.Duration = TimeSpan.FromSeconds(1);
            lbl.BeginAnimation(Label.OpacityProperty, da);
        }
    }
}
