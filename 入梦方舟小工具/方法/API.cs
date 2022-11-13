using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace 入梦方舟小工具.方法
{
    class API
    {
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        public static extern int SetCursorPos(int x, int y);
        public  enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }


        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        private static extern void keybd_event(
byte bVk, //虚拟键值
byte bScan,// 一般为0
int dwFlags, //这里是整数类型 0 为按下，2为释放
int dwExtraInfo //这里是整数类型 一般情况下设成为0
);
        public static void 键盘_单机(byte keyy, int dwFlags)//键代码,0 为按下，2为释放
        {

            keybd_event(keyy, 0, dwFlags, 0);

        }
        public static string 网页_访问(string webpath)
        {
            WebClient client = new WebClient();
            client.Proxy = null;
            return Encoding.UTF8.GetString(client.DownloadData(webpath));
        }
        [DllImport("user32.dll", EntryPoint = "IsWindowVisible")]
        public static extern bool 窗口_是否可见(IntPtr hWnd);

        public static bool 进程_是否存在(string newName)
        {
            string programName = newName.Replace(".exe", "");
            return Process.GetProcessesByName(programName).Length > 0 ? true : false;
        }
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        public static extern int 窗口_置状态(IntPtr hwnd, int nCmdShow);
        [DllImport("User32.dll", EntryPoint = "ShowWindowAsync")]
        private static extern bool 窗口_隐藏显示(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll", EntryPoint = "FindWindowA")]
        public static extern IntPtr FindWindowA(string IpClassName, string IpWindowName);
        [DllImport("User32.dll", EntryPoint = "PostMessageA")]
        public static extern int PostMessageA(IntPtr hWnd, int Msg, int wParam, int lParam);
        public static void 键盘_消息(IntPtr hWnd, int Msg, byte keyy)//句柄,按键功能,键盘按键
        {

            if (Msg == 1)                        //输入字符(大写)
            {
                PostMessageA(hWnd, 258, keyy, 0);
            }
            else if (Msg == 2)                     //输入字符(小写)
            {
                PostMessageA(hWnd, 260, keyy, 0);
            }
            else if (Msg == 3)
            {
                PostMessageA(hWnd, 260, keyy, 0);//3=按下
            }
            else if (Msg == 4)
            {
                PostMessageA(hWnd, 261, keyy, 0);//4=放开
            }
            else if (Msg == 5)
            {
                PostMessageA(hWnd, 260, keyy, 0);//5=单击
                //程序_延时(10);
                PostMessageA(hWnd, 261, keyy, 0);

            }
        }
        #region 进程_进程_句柄取进程路径X64
        [Flags]
        enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000,
            ReadControl = 0x00020000,
            PROCESS_QUERY_LIMITED_INFORMATION = 0x1000
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool QueryFullProcessImageName(IntPtr hprocess, int dwFlags, StringBuilder lpExeName, out int size);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hHandle);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out uint processId);
        private static Process GetProcessByHandle(IntPtr hwnd)
        {
            try
            {
                uint processID;
                GetWindowThreadProcessId(hwnd, out processID);
                return Process.GetProcessById((int)processID);
            }
            catch { return null; }
        }

        private static string GetExecutablePathAboveVista(int ProcessId)
        {
            var buffer = new StringBuilder(1024);
            IntPtr hprocess = OpenProcess(ProcessAccessFlags.PROCESS_QUERY_LIMITED_INFORMATION,
                                          false, ProcessId);
            if (hprocess != IntPtr.Zero)
            {
                try
                {
                    int size = buffer.Capacity;
                    if (QueryFullProcessImageName(hprocess, 0, buffer, out size))
                    {
                        return buffer.ToString();
                    }
                }
                finally
                {
                    CloseHandle(hprocess);
                }
            }
            return null;
        }

        public static string GetWindowPath(IntPtr hwind)
        {
            try
            {
                Process currentProcess = GetProcessByHandle(hwind);

                if (Environment.OSVersion.Version.Major >= 6)
                {
                    string newMethReturn = GetExecutablePathAboveVista(currentProcess.Id);
                    if (!string.IsNullOrWhiteSpace(newMethReturn))
                        return newMethReturn;
                }


                if (currentProcess != null)

                    return currentProcess.MainModule.FileName;
                else
                    return null;
            }
            catch
            { return null; }
        }
        public static string 文本_取左边内容(string str, string s)
        {
            string temp = str.Substring(0, str.IndexOf(s));
            return temp;
        }
        #endregion
        /// <summary>
        /// 鼠标_消息
        /// </summary>
        /// <param name = "HWD" > 句柄 </ param >
        /// < param name="键"> 1 #左键   2 #右键   3 #中键  </param>
        /// <param name = "控制" > 1 #单击   2 #双击   3 #按下  4 #放开</param>
        public static void 鼠标_消息(IntPtr HWD, int 键, int 控制)
        {
            if (键 == 1)
            {
                if (控制 == 1)
                {
                    PostMessageA(HWD, 513, 1, 0);//左键按下
                    //程序_延时(10);
                    PostMessageA(HWD, 514, 0, 0);//左键放开
                }
                else if (控制 == 2)
                {
                    PostMessageA(HWD, 513, 1, 0);
                    //程序_延时(10);
                    PostMessageA(HWD, 514, 0, 0);
                    //程序_延时(10);
                    PostMessageA(HWD, 515, 0, 0);

                }
                else if (控制 == 3)
                {
                    PostMessageA(HWD, 513, 1, 0);
                }
                else if (控制 == 4)
                {
                    PostMessageA(HWD, 514, 0, 0);
                }

            }
            if (键 == 2)
            {
                if (控制 == 1)
                {
                    PostMessageA(HWD, 516, 2, 0);
                    //程序_延时(10);
                    PostMessageA(HWD, 517, 2, 0);
                }
                else if (控制 == 2)
                {
                    PostMessageA(HWD, 516, 2, 0);
                    //程序_延时(10);
                    PostMessageA(HWD, 517, 2, 0);
                    //程序_延时(10);
                    PostMessageA(HWD, 518, 0, 0);
                }
                else if (控制 == 3)
                {
                    PostMessageA(HWD, 516, 2, 0);
                }
                else if (控制 == 4)
                {
                    PostMessageA(HWD, 517, 2, 0);
                }

            }
            if (键 == 3)
            {
                if (控制 == 1)
                {
                    PostMessageA(HWD, 519, 16, 0);
                    //程序_延时(10);
                    PostMessageA(HWD, 520, 0, 0);
                }
                else if (控制 == 2)
                {
                    PostMessageA(HWD, 519, 16, 0);
                    // 程序_延时(10);
                    PostMessageA(HWD, 520, 0, 0);
                    // 程序_延时(10);
                    PostMessageA(HWD, 521, 0, 0);

                }
                else if (控制 == 3)
                {
                    PostMessageA(HWD, 519, 16, 0);
                }
                else if (控制 == 4)
                {
                    PostMessageA(HWD, 520, 0, 0);
                }


            }
        }
        public static string 进程_名取路径(string prossName)
        {
            string path = null;
            Process[] ps = Process.GetProcesses();
            foreach (Process p in ps)
            {
                if (p.ProcessName == prossName)
                {
                    path = p.MainModule.FileName.ToString();
                    return path;
                }
            }
            return path;
        }
        [DllImport("user32.dll")]
        public static extern void mouse_event(MouseEventFlag flags, int dx, int dy, int data, int extraInfo);
        /// <summary>
        /// 鼠标_单机
        /// </summary>
        /// <param name = "mes" > 1 = 单机 = 3鼠标右键单击；4鼠标右键弹起</param>
        public static void 鼠标_单击(int mes)
        {
            if (mes == 1)
            {
                mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, 0);
                mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, 0);
            }

            if (mes == 3)
            {
                mouse_event(MouseEventFlag.RightDown, 0, 0, 0, 0);

            }
            if (mes == 4)
            {

                mouse_event(MouseEventFlag.RightUp, 0, 0, 0, 0);
            }
        }
        public static void 窗口_激活显示(IntPtr hwnd)
        {
            ShowWindowAsync(hwnd, 1);//显示
            窗口_置焦点(hwnd);//当到最前端
        }
        [DllImport("User32.dll", EntryPoint = "SetForegroundWindow")]
        private static extern bool 窗口_置焦点(IntPtr hWnd);
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        public static void 文本_投递(IntPtr 句柄, string 文本内容)
        {
            byte[] b = Encoding.GetEncoding("gb2312").GetBytes(文本内容);
            for (int i = 0; i < b.Length; i++)
            {
                PostMessageA(句柄, 258, b[i], 0);
            }
        }

        [DllImport("user32.dll", EntryPoint = "FindWindowExA")]
        public static extern int 窗口_取句柄(int hWndChild, int a, string name1, string name12);
       
        


        public static void 进程_结束(string name)
        {

            Process[] ps = Process.GetProcessesByName(name);
            if (ps.Length > 0)
            {
                foreach (Process p in ps)
                    p.Kill();
            }

        }

        /// <summary>
        /// 文件_复制
        /// </summary>
        /// <param name="name1">被复制文件名</param>
        /// <param name="name2">复制到文件名</param>
        /// <param name="fugai">0=覆盖，非0不覆盖</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "CopyFileA")]
        public static extern bool 文件_复制(string name1, string name2, int fugai);
        public static bool 文本_是否存在(string test, string test1)
        {
            if (test.IndexOf(test1) != -1)
            {
                return (true);
            }
            else return (false);
        }
        public static void 目录_创建(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            di.Create();
        }

        [DllImport("kernel32")]
        //                        读配置文件方法的6个参数：所在的分区（section）、键值、     初始缺省值、     StringBuilder、   参数长度上限、配置文件路径
        private static extern int GetPrivateProfileString(string section, string key, string deVal, StringBuilder retVal,
int size, string filePath);

        [DllImport("kernel32")]
        //                            写配置文件方法的4个参数：所在的分区（section）、  键值、     参数值、        配置文件路径
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        public static void 写配置项(string path, string section, string key, string value)
        {
            //写配置项参数

            string strPath = Environment.CurrentDirectory + path;//"\\bin\\RMbinconfig.ini";
            WritePrivateProfileString(section, key, value, strPath);
        }
        public static string 读配置项(string path, string section, string key)
        {
            StringBuilder sb = new StringBuilder(255);
            string strPath = Environment.CurrentDirectory + path;//"\\bin\\RMbinconfig.ini"
            //最好初始缺省值设置为非空，因为如果配置文件不存在，取不到值，程序也不会报错
            GetPrivateProfileString(section, key, null, sb, 255, strPath);
            return sb.ToString();

        }
        [DllImport("kernel32.dll", EntryPoint = "IsDebuggerPresent")]
        public static extern bool 程序_是否被调试();
        [DllImport("user32.dll")]
        public static extern int GetCursorPos(ref Point lpPoint);  //获取鼠标坐标，该坐标是光标所在的屏幕坐标位置
        [DllImport("user32.dll")]
        public static extern int WindowFromPoint(int xPoint, int yPoint);  //指定坐标处窗体句柄
        [DllImport("user32.dll")]
        public static extern int GetWindowText(int hwnd, StringBuilder lpString, int nMaxCount);//获取窗体标题名称
        [DllImport("shell32.dll", EntryPoint = "ShellExecuteA")]
        private static extern int ShellExecuteA(int hwd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);
        public static void 网页_打开指定网址(string Path)
        {
            ShellExecuteA(0, "open", Path, "", "", 1);
        }
        [DllImport("user32.dll")]
        public static extern int GetClassName(int hwnd, StringBuilder lpstring, int nMaxCount); //获取窗体类名称      
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(int hwnd, int wMsg, int wParam, string lParam);
        [DllImport("user32.dll")]
        public static extern int MessageBoxTimeoutA(IntPtr hwnd, string lptex, string lpCaption, int uType, int wlange, int dwTimeout);
        public static void 信息框(string txt, string txt1, int dwTimeout)
        {
            MessageBoxTimeoutA(Process.GetCurrentProcess().MainWindowHandle, txt, txt1, 0, 0, dwTimeout);
        }
    }
}
