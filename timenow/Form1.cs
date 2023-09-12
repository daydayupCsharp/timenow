using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static timenow.test1;
using Newtonsoft.Json;
using System.Threading;

namespace timenow
{
    public partial class test1 : Form
    {
        //第二个客户端的代码

        const int WM_COPYDATA = 0x004A;

        /// <summary>
        /// 重新DefWndProc方法，处理接收到的windows消息
        /// </summary>
        /// <param name = "m" ></ param >
        /// 
        public class response
        {
            public string ID { get; set; }
            public string Pack { get; set; }
            public string PDX { get; set; }
        }
        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {

            switch (m.Msg)
            {
                case WM_COPYDATA:
                    COPYDATASTRUCT myStr_pack = new COPYDATASTRUCT();
                    Type myType_pack = myStr_pack.GetType();
                    myStr_pack = (COPYDATASTRUCT)m.GetLParam(myType_pack);
                    //textBox1.Text = myStr.lpData;
                    response re = new response();
                    re = JsonConvert.DeserializeObject<response>(myStr_pack.lpData);

                    textBox1.AppendText(re.Pack);
                    textBox2.AppendText(re.PDX);


                    break;

                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;//若发送较小数据可用此字段传输
            public int cbData;//发送数据的大小（以字节为单位）
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;//发送数据
        }
        public test1()
        {
            InitializeComponent();
        }



        public string ReceiveMessage(ref System.Windows.Forms.Message m)
        {

            COPYDATASTRUCT cds = (COPYDATASTRUCT)m.GetLParam(typeof(COPYDATASTRUCT));
            textBox1.Text = cds.lpData;
            return cds.lpData; //接收只接收了lpData，如果需要其它消息，自己定义。
        }
        private void button1_Click(object sender, EventArgs e)
        {

            //textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmssfff")+Directory.GetCurrentDirectory();
        }
    //    //第一个客户端的代码

    //    /// <summary>
    //    /// 发送消息
    //    /// </summary>
    //    /// <param name="hWnd">发送窗口句柄</param>
    //    /// <param name="msg">发送的消息类型</param>
    //    /// <param name="wParam">附加消息,根据msg参数区分使用</param>
    //    /// <param name="lParam">发送数据</param>
    //    /// <returns></returns>
    //    [DllImport("User32.dll", EntryPoint = "SendMessage")]
    //    private static extern int SendMessage(int hWnd, int msg, int wParam, ref COPYDATASTRUCT lParam);

    //    /// <summary>
    //    /// 查找窗口句柄
    //    /// </summary>
    //    /// <param name="lpClassName">类名</param>
    //    /// <param name="lpWindowName">窗口标题</param>
    //    /// <returns></returns>
    //    [DllImport("User32.dll", EntryPoint = "FindWindow")]
    //    private static extern int FindWindow(string lpClassName, string lpWindowName);

    //    //当一个应用程序传递数据给另一个应用程序时发送此消息
    //    const int WM_COPYDATA = 0x004A;



    //    public struct COPYDATASTRUCT
    //    {
    //        public IntPtr dwData;//若发送较小数据可用此字段传输
    //        public int cbData;//发送数据的大小（以字节为单位）
    //        [MarshalAs(UnmanagedType.LPStr)]
    //        public string lpData;//发送数据
    //    }
    //    public class request
    //    {
    //        public string ID { get; set; }
    //        public string Pack { get; set; }
    //        public string PDX { get; set; }
    //    }
    //    private void button2_Click(object sender, EventArgs e)
    //    {

    //        Process p = Process.Start(@"D:\VS new\timenow\timenow\bin\Debug\timenow.exe");
    //        p.WaitForInputIdle();
    //        Thread.Sleep(100);
    //        request re = new request();
    //        re.ID = DateTime.Now.ToString("yyyyMMddhhmmss");
    //        re.Pack = textUrl.Text;
    //        re.PDX = textSend.Text;
    //        string s = JsonConvert.SerializeObject(re);
    //        int WINDOW_HANDLER = FindWindow(null, @"接收窗口");
    //        if (WINDOW_HANDLER != 0)
    //        {

    //            byte[] sarr = System.Text.Encoding.Default.GetBytes(s);
    //            int len = sarr.Length;
    //            COPYDATASTRUCT cds;
    //            cds.dwData = (IntPtr)100;
    //            cds.lpData = s;
    //            cds.cbData = len + 1;
    //            SendMessage(WINDOW_HANDLER, WM_COPYDATA, 0, ref cds);

    //        }
    //    }
    }
}
