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

        //private void btnClick_Click(object sender, EventArgs e)
        //{
        //    this.richTextBox1.Clear();
        //    btnClick.Enabled = false;
        //    AsyncMethodCaller caller = new AsyncMethodCaller(TestMethod);
        //    IAsyncResult result = caller.BeginInvoke(GetResult, null);

        //    //// 捕捉调用线程的同步上下文派生对象
        //    //sc= SynchronizationContext.Current;
        //}

        //#region 使用APM实现异步编程
        //// 同步方法
        //private string TestMethod()
        //{
        //    // 模拟做一些耗时的操作
        //    // 实际项目中可能是读取一个大文件或者从远程服务器中获取数据等。
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Thread.Sleep(200);
        //    }

        //    return "点击我按钮事件完成";
        //}

        //// 回调方法
        //private void GetResult(IAsyncResult result)
        //{
        //    AsyncMethodCaller caller = (AsyncMethodCaller)((AsyncResult)result).AsyncDelegate;
        //    // 调用EndInvoke去等待异步调用完成并且获得返回值
        //    // 如果异步调用尚未完成，则 EndInvoke 会一直阻止调用线程，直到异步调用完成
        //    string resultvalue = caller.EndInvoke(result);
        //    //sc.Post(ShowState,resultvalue);
        //    richTextBox1.Invoke(showStateCallback, resultvalue);
        //}

        //// 显示结果到richTextBox
        //private void ShowState(object result)
        //{
        //    richTextBox1.Text = result.ToString();
        //    btnClick.Enabled = true;
        //}

        //// 显示结果到richTextBox
        ////private void ShowState(string result)
        ////{
        ////    richTextBox1.Text = result;
        ////    btnClick.Enabled = true;
        ////}
        //#endregion
        /// <summary>  
        /// 回调方法  
        /// </summary>  
        /// <param name="result"></param>  
        /// 
        //private void CallBack(IAsyncResult result)
        //{
        //    string AsyncResult = string.Empty;
        //    if (result.IsCompleted)
        //    {
        //        Func<string, string> delegageFunc = ((result as AsyncResult).AsyncDelegate) as Func<string, string>;
        //        if (delegageFunc != null)
        //        {
        //            AsyncResult = delegageFunc.EndInvoke(result);
        //            Console.WriteLine(AsyncResult);
        //        }
        //    }

        //}

        //private byte[] ReceiveDataCallBack(IAsyncResult result)
        //{
        //    return new byte[233];

        //}

        ///////////////////////////////////////////////////////////////////////////
        //public async Task MyMethodAsync()
        //{
        //    Task<int> longRunningTask = LongRunningOperationAsync();
        //    // independent work which doesn't need the result of LongRunningOperationAsync can be done here

        //    //and now we call await on the task 
        //    int result = await longRunningTask;
        //    //use the result 
        //    Console.WriteLine(result);
        //}

        //public async Task<int> LongRunningOperationAsync() // assume we return an int from this long running operation 
        //{
        //    await Task.Delay(1000); //1 seconds delay
        //    return 1;
        //}

        ////////////////////////////////////////////////////////////////////////////////////////
        //private async void button1_Click(object sender, EventArgs e)
        //{
        //    // Call the method that runs asynchronously.
        //    string result = await WaitAsynchronouslyAsync();

        //    // Call the method that runs synchronously.
        //    //string result = await WaitSynchronously ();

        //    // Display the result.
        //    textBox1.Text += result;
        //}

        //// The following method runs asynchronously. The UI thread is not
        //// blocked during the delay. You can move or resize the Form1 window 
        //// while Task.Delay is running.
        //public async Task<string> WaitAsynchronouslyAsync()
        //{
        //    await Task.Delay(10000);
        //    return "Finished";
        //}

        //// The following method runs synchronously, despite the use of async.
        //// You cannot move or resize the Form1 window while Thread.Sleep
        //// is running because the UI thread is blocked.
        //public async Task<string> WaitSynchronously()
        //{
        //    // Add a using directive for System.Threading.
        //    Thread.Sleep(10000);
        //    return "Finished";
        //}

        ////////////////////////////////////////////////////////////////////////////////

        //public delegate void MyDelegate(Label myControl, string myArg2);

        //private void Button_Click(object sender, EventArgs e)
        //{
        //    object[] myArray = new object[2];

        //    myArray[0] = new Label();
        //    myArray[1] = "Enter a Value";
        //    myTextBox.BeginInvoke(new MyDelegate(DelegateMethod), myArray);
        //}

        //public void DelegateMethod(Label myControl, string myCaption)
        //{
        //    myControl.Location = new Point(16, 16);
        //    myControl.Size = new Size(80, 25);
        //    myControl.Text = myCaption;
        //    this.Controls.Add(myControl);
        //}

        private async void Connect()
        {
            try
            {
                Task<bool> conn = ConnectServer.Connect();
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

        }
    }
}
