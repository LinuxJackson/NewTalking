﻿#pragma checksum "..\..\Login.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4459267DAD9F5EB76B075FB04601ED49"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using NewTalking;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace NewTalking {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal NewTalking.MainWindow wdLogin;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblBtnLogin;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUser_id;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUser_pwd;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblNewTalking;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblState;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblFillUser_id;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblFillUser_pwd;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblVersion;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/NewTalking;component/login.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Login.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.wdLogin = ((NewTalking.MainWindow)(target));
            
            #line 8 "..\..\Login.xaml"
            this.wdLogin.Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lblBtnLogin = ((System.Windows.Controls.Label)(target));
            
            #line 10 "..\..\Login.xaml"
            this.lblBtnLogin.MouseEnter += new System.Windows.Input.MouseEventHandler(this.lblBtn_MouseEnter);
            
            #line default
            #line hidden
            
            #line 10 "..\..\Login.xaml"
            this.lblBtnLogin.MouseLeave += new System.Windows.Input.MouseEventHandler(this.lblBtn_MouseLeave);
            
            #line default
            #line hidden
            
            #line 10 "..\..\Login.xaml"
            this.lblBtnLogin.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.lblBtnLogin_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtUser_id = ((System.Windows.Controls.TextBox)(target));
            
            #line 11 "..\..\Login.xaml"
            this.txtUser_id.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtUserKeyDown);
            
            #line default
            #line hidden
            
            #line 11 "..\..\Login.xaml"
            this.txtUser_id.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtUser_id_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtUser_pwd = ((System.Windows.Controls.TextBox)(target));
            
            #line 12 "..\..\Login.xaml"
            this.txtUser_pwd.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtUserKeyDown);
            
            #line default
            #line hidden
            
            #line 12 "..\..\Login.xaml"
            this.txtUser_pwd.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtUser_pwd_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lblNewTalking = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.lblState = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.lblFillUser_id = ((System.Windows.Controls.Label)(target));
            
            #line 15 "..\..\Login.xaml"
            this.lblFillUser_id.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.lblFillUser_id_MouseDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.lblFillUser_pwd = ((System.Windows.Controls.Label)(target));
            
            #line 16 "..\..\Login.xaml"
            this.lblFillUser_pwd.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.lblFillUser_pwd_MouseDown);
            
            #line default
            #line hidden
            return;
            case 9:
            this.lblVersion = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

