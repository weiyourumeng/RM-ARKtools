using KeyHook;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Speech.Synthesis;
using 入梦方舟小工具.方法;
using Microsoft.Win32;

namespace 入梦方舟小工具
{
    partial class 入梦方舟小工具 : Form
    {
        #region  API方法 
        API aPI = new API();
        private static  IntPtr 句柄 = API.FindWindowA("UnrealWindow", "ARK: Survival Evolved");
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseUp = true;//鼠标左右键被按下
            Cursor = Cursors.Cross; //改变鼠标样式为十字架

        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseUp = false;//鼠标左右键被弹起
            Cursor = Cursors.Default;//改变鼠标样式为默认
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {//当鼠标移动时发生
            if (isMouseUp)//左键是否被按下
            {
                API.GetCursorPos(ref pi); //获取鼠标坐标值
                X编辑框1.Text = pi.X.ToString(); //label1显示鼠标坐标值的x值与y值
                Y编辑框1.Text = pi.Y.ToString();
                API.写配置项(功能配置路径, "Set", "XBOX", X编辑框1.Text);
                API.写配置项(功能配置路径, "Set", "YBOX", Y编辑框1.Text);

            }

        }
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseUp = true;//鼠标左右键被按下
            Cursor = Cursors.Cross; //改变鼠标样式为十字架

        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseUp = false;//鼠标左右键被弹起
            Cursor = Cursors.Default;//改变鼠标样式为默认
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {//当鼠标移动时发生
            if (isMouseUp)//左键是否被按下
            {
                API.GetCursorPos(ref pi); //获取鼠标坐标值
                X编辑框2.Text = pi.X.ToString(); //label1显示鼠标坐标值的x值与y值
                Y编辑框2.Text = pi.Y.ToString();
                API.写配置项(功能配置路径, "Set", "XBOX2", X编辑框2.Text);
                API.写配置项(功能配置路径, "Set", "YBOX2", Y编辑框2.Text);

            }

        }
        bool isMouseUp = false;//判断鼠标左右键是否被按下状态
        Point pi = new Point();//鼠标的坐标，这个Point要引用using System.Drawing;比较懒就用这个吧。
        StringBuilder name = new StringBuilder(256);//接受窗体标题和类名称
        [DllImport("user32.dll")]
        private static extern int MessageBoxTimeoutA(IntPtr hwnd, string lptex, string lpCaption, int uType, int wlange, int dwTimeout);
        public void 信息框(string txt, string txt1, int dwTimeout)
        {
            MessageBoxTimeoutA(this.Handle, txt, txt1, 0, 0, dwTimeout);
        }

        KeyboardHook k_hook = new KeyboardHook();//实例化键盘钩子
        Form2 buy = new Form2();
        public string 功能配置路径 = "\\RM\\RuMengARK.ini";
        public string 游戏路径 = "\\RM\\RuMengARKPath.ini";

        public static int Wi = Screen.PrimaryScreen.Bounds.Height;//1080
        public static int He = Screen.PrimaryScreen.Bounds.Width;//1920   
        private static void 鼠标_移动(int x, int y)
        {
            if (He == 1920 && Wi == 1080)
            {
                API.SetCursorPos(x, y);
            }
            else if (He == 2048 && Wi == 1152)
            {
                API.SetCursorPos(Convert.ToInt32(x * 1.066666666666), Convert.ToInt32(y * 1.066666666666));
            }
            else if (He == 2560 && Wi == 1440)
            {
                API.SetCursorPos(Convert.ToInt32(x * 1.3333333), Convert.ToInt32(y * 1.3333333));
            }
            else if (He == 4096 && Wi == 2160)
            {
                API.SetCursorPos(Convert.ToInt32(x * 2.13333333), Convert.ToInt32(y * 2));
            }
            else if (He == 3840 && Wi == 2160)
            {
                API.SetCursorPos(Convert.ToInt32(x * 2), Convert.ToInt32(y * 2));
            }
            else if (He == 1600 && Wi == 900)
            {
                API.SetCursorPos(Convert.ToInt32(x * 0.83333333), Convert.ToInt32(y * 0.83333333));
            }
            else if (He == 1680 && Wi == 1050)
            {
                API.SetCursorPos(Convert.ToInt32(x * 0.875), Convert.ToInt32(y * 0.97222222));
            }
            else if (He == 1440 && Wi == 900)
            {
                API.SetCursorPos(Convert.ToInt32(x * 0.75), Convert.ToInt32(y * 0.83333333));
            }
            else if (He == 1920 && Wi == 1200)
            {
                API.SetCursorPos(Convert.ToInt32(x), Convert.ToInt32(y * 1.11111111));
            }
            else if (He == 1400 && Wi == 875)
            {
                API.SetCursorPos(Convert.ToInt32(x * 0.72916666), Convert.ToInt32(y * 0.81018518));
            }
            else if (He == 1536 && Wi == 864)
            {
                API.SetCursorPos(Convert.ToInt32(x * 0.8), Convert.ToInt32(y * 0.8));
            }
            else if (He == 3440 && Wi == 1440)
            {
                API.SetCursorPos(Convert.ToInt32(x * 1.79166666), Convert.ToInt32(y * 1.3333333));
            }
            else API.SetCursorPos(x, y);
        }

        public static void 程序_延时(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();//转让控制权
            }
        }
        private static bool IsDragging = false; //用于指示当前是不是在拖拽状态
        private Point StartPoint = new Point(0, 0); //记录鼠标按下去的坐标, new是为了拿到空间, 两个0无所谓的
                                                    //记录动了多少距离,然后给窗体Location赋值,要设置Location,必须用一个Point结构体,不能直接给Location的X,Y赋值
        private Point OffsetPoint = new Point(0, 0);
        private void 窗口移动_鼠标按下(object sender, MouseEventArgs e)
        {   //如果按下去的按钮不是左键就return,节省运算资源
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            // 按下鼠标后,进入拖动状态:
            IsDragging = true;
            //保存刚按下时的鼠标坐标
            StartPoint.X = e.X;
            StartPoint.Y = e.Y;
        }
        private void 窗口移动_鼠标移动(object sender, MouseEventArgs e)
        {//鼠标移动时调用,检测到IsDragging为真时
            if (IsDragging == true)
            {   //用当前坐标减去起始坐标得到偏移量Offset
                OffsetPoint.X = e.X - StartPoint.X;
                OffsetPoint.Y = e.Y - StartPoint.Y;
                // 将Offset转化为屏幕坐标赋值给Location,设置Form在屏幕中的位置,如果不作PointToScreen转换,你自己看看效果就好
                Location = PointToScreen(OffsetPoint);
            }
        }
        private void 窗口移动_鼠标左键弹起(object sender, MouseEventArgs e)
        {   //左键抬起时,及时把拖动判定设置为false,否则,你也可以试试效果
            IsDragging = false;
        }
        private void 常规_离开(object sender, EventArgs e)
        {
            关闭.BackgroundImage = Properties.Resources.X常规;
        }
        private void 常规_点燃(object sender, EventArgs e)
        {
            关闭.BackgroundImage = Properties.Resources.X点燃;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (句柄 != (IntPtr)0)
            {
                if (API.窗口_是否可见(句柄))
                    API.窗口_置状态(句柄, 0);
                else API.窗口_置状态(句柄, 1);
                API.窗口_激活显示(this.Handle);
            }
        }
        #endregion
        #region 代码折叠
        private bool leftloop = false;//连点器标志位
        private bool wloop = true;//W标志位
        private bool Eloop = false;//E标志位
        private bool Floop = false;//F标志位
        private bool Sloop = false;//自动孵化标志位
        private bool Zloop = false;//自动下载
        private bool Xloop = false;//自动上传       
        private bool Cloop = false;//自动C
        private bool Rloop = false;//自动右键
        private bool Oloop = false;//自动o键
        private bool HPloop = false;//自动抽血器
        private bool wuloop = false;//自动抽鞭子
        private bool 开启传服判断 = false;//开始传服
        private bool diubaoloop = false;//自动丢包
        private bool jifugongju = false;//挤服工具
        private void inthook(object sender, KeyEventArgs e)//消息循环
        {
            if (F12显示隐藏.Checked == true)
            {
                if (e.KeyCode == Keys.F12)
                {
                    显示();
                }
            }
            if (句柄 != (IntPtr)0)
            {
                if (e.KeyCode.ToString() == 鼠标左键编辑框.Text.ToUpper())//连点器
                {
                    if (延迟时间.Text != "")
                    {
                        if (leftloop)
                        {
                            leftloop = false;
                            if (checkBox1.Checked == true)
                            {
                                buy.label1.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";
                            }
                            label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";
                        }
                        else
                        {
                            leftloop = true;
                            if (checkBox1.Checked == true)
                            {
                                buy.label1.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器开启";
                            }
                            label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器开启";
                            new Thread(workLeftMouseClickLoop).Start();
                        }
                    }
                    else
                    {
                        延迟时间.Text = "100";
                        API.写配置项(功能配置路径, "Set", "Nei", "100");
                        if (leftloop)
                        {
                            leftloop = false;
                            if (checkBox1.Checked == true)
                            {
                                buy.label1.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";
                            }
                            label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";
                        }
                        else
                        {
                            leftloop = true;
                            if (checkBox1.Checked == true)
                            {
                                buy.label1.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器开启";
                            }
                            label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器开启";
                            new Thread(workLeftMouseClickLoop).Start();
                        }
                    }
                }
                if (e.KeyCode.ToString() == 自动前进编辑框.Text.ToUpper())
                {
                    if (方舟未运行.Text != "方舟未运行" || 方舟未运行.Text != "方舟已运行(隐藏)")
                    {
                        if (激活窗口.Checked == true)
                        {
                            API.窗口_激活显示(句柄);
                        }
                        前进();
                    }
                    else
                    {
                        信息框(方舟未运行.Text, "信息:", 5000);
                    }
                }
                if (ALTZ.Checked == true)
                {
                    if (e.KeyData == (Keys.B | Keys.Alt))
                    {
                        if (方舟未运行.Text != "方舟未运行" || 方舟未运行.Text != "方舟已运行(隐藏)")
                        {
                            if (checkBox8.Checked == true || checkBox9.Checked == true || checkBox10.Checked == true || checkBox11.Checked == true || checkBox12.Checked == true || checkBox13.Checked == true || checkBox14.Checked == true || checkBox15.Checked == true)
                            {
                                diubaoloop停止();
                                if (激活窗口.Checked == true)
                                {
                                    API.窗口_激活显示(句柄);
                                }
                                if (diubaoloop)
                                {
                                    diubaoloop = false;
                                    if (checkBox1.Checked == true)
                                    {
                                        buy.label1.Text = "一键清包停止";
                                    }
                                }
                                else
                                {
                                    diubaoloop = true;
                                    if (checkBox1.Checked == true)
                                    {
                                        buy.label1.Text = "一键清包开始";
                                    }
                                    new Thread(diubaozloop).Start();
                                    //传送服务器();
                                }

                            }
                            else
                            {
                                信息框("丢弃资源未选中", "信息:", 5000);
                            }
                        }
                        else
                        {
                            信息框(方舟未运行.Text, "信息:", 5000);
                        }

                    }
                }
                if (checkBox17.Checked == true)
                {
                    if (e.KeyData == (Keys.D6 | Keys.Alt))
                    {
                        if (方舟未运行.Text != "方舟未运行" || 方舟未运行.Text != "方舟已运行(隐藏)")
                        {
                            挤服停止();
                            if (激活窗口.Checked == true)
                            {
                                API.窗口_激活显示(句柄);
                            }
                            if (jifugongju)
                            {
                                jifugongju = false;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "挤服工具停止";
                                }
                            }
                            else
                            {
                                jifugongju = true;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "挤服工具开始";
                                }
                                new Thread(挤服工具).Start();
                                //传送服务器();
                            }

                        }
                        else
                        {
                            信息框(方舟未运行.Text, "信息:", 5000);
                        }

                    }
                }
                if (ALT9传服.Checked == true)
                {
                    if (e.KeyData == (Keys.D9 | Keys.Alt))
                    {
                        if (方舟未运行.Text != "方舟未运行" || 方舟未运行.Text != "方舟已运行(隐藏)")
                        {
                            传服停止();
                            if (激活窗口.Checked == true)
                            {
                                API.窗口_激活显示(句柄);
                            }
                            if (开启传服判断)
                            {
                                开启传服判断 = false;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "传送服务器停止";
                                }
                            }
                            else
                            {
                                开启传服判断 = true;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "传送服务器开始";
                                }
                                new Thread(传送服务器).Start();
                                //传送服务器();
                            }

                        }
                        else
                        {
                            信息框(方舟未运行.Text, "信息:", 5000);
                        }

                    }
                }
                if (ALTR自动右键.Checked == true)
                {
                    if (e.KeyData == (Keys.R | Keys.Alt))
                    {
                        if (方舟未运行.Text != "方舟未运行" || 方舟未运行.Text != "方舟已运行(隐藏)")
                        {
                            Rloop停止();
                            if (激活窗口.Checked == true)
                            {
                                API.窗口_激活显示(句柄);
                            }
                            if (Rloop)
                            {
                                Rloop = false;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "ALT+R已关闭";
                                }
                                ALTR自动右键.Text = "ALT+R已关闭";
                            }
                            else
                            {
                                Rloop = true;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "ALT+R已开启";
                                }
                                ALTR自动右键.Text = "ALT+R已开启";
                                new Thread(workRLoop).Start();

                            }

                        }
                        else
                        {
                            信息框(方舟未运行.Text, "信息:", 5000);
                        }
                    }

                }         
                if (ALTC自动C.Checked == true)
                {
                    if (e.KeyData == (Keys.C | Keys.Alt))
                    {

                        if (方舟未运行.Text != "方舟未运行" || 方舟未运行.Text != "方舟已运行(隐藏)")
                        {
                            Cloop停止();
                            if (激活窗口.Checked == true)
                            {
                                API.窗口_激活显示(句柄);
                            }
                            if (Cloop)
                            {
                                Cloop = false;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "自动C键已关闭";
                                }
                                ALTC自动C.Text = "自动C键已关闭";
                            }
                            else
                            {
                                Cloop = true;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "自动C键已开启";
                                }
                                ALTC自动C.Text = "自动C键已开启";
                                new Thread(workCLoop).Start();

                            }
                        }
                        else
                        {
                            信息框(方舟未运行.Text, "信息:", 5000);
                        }
                    }
                }
                if (选择框F9.Checked == true)
                {
                    if (e.KeyCode == Keys.F9)
                    {

                        if (方舟未运行.Text != "方舟未运行" || 方舟未运行.Text != "方舟已运行(隐藏)")
                        {
                            Floop停止();
                            if (激活窗口.Checked == true)
                            {
                                API.窗口_激活显示(句柄);
                            }
                            if (Floop)
                            {
                                Floop = false;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "F9自动F关闭";
                                }
                                选择框F9.Text = "F9自动F关闭";
                            }
                            else
                            {
                                Floop = true;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "F9自动F开启";
                                }
                                选择框F9.Text = "F9自动F开启";
                                new Thread(workFLoop).Start();
                            }
                        }
                        else
                        {
                            信息框(方舟未运行.Text, "信息:", 5000);
                        }
                    }
                }
                if (选择框F10.Checked == true)
                {
                    if (e.KeyCode == Keys.F10)
                    {

                        if (方舟未运行.Text != "方舟未运行" || 方舟未运行.Text != "方舟已运行(隐藏)")
                        {
                            Eloop停止();
                            if (激活窗口.Checked == true)
                            {
                                API.窗口_激活显示(句柄);
                            }
                            if (Eloop)
                            {
                                Eloop = false;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "F9自动E关闭";
                                }
                                选择框F10.Text = "F10自动E关闭";
                            }
                            else
                            {
                                Eloop = true;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "F9自动E开启";
                                }
                                选择框F10.Text = "F10自动E开启";
                                new Thread(workELoop).Start();
                            }
                        }
                        else
                        {
                            信息框(方舟未运行.Text, "信息:", 5000);
                        }
                    }
                }
                if (自动孵化.Checked == true)
                {
                    if (e.KeyData == (Keys.S | Keys.Alt))
                    {
                        if (方舟未运行.Text != "方舟未运行" || 方舟未运行.Text != "方舟已运行(隐藏)")
                        {
                            Sloop停止();
                            Console.WriteLine("自动孵化收到消息");
                            if (Sloop)
                            {
                                Sloop = false;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "自动冻龙已关闭";
                                }
                                自动孵化.Text = "ALT+S自动冻龙已关闭";
                            }
                            else
                            {
                                Sloop = true;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "自动冻龙已开启";
                                }

                                自动孵化.Text = "ALT+S自动冻龙已开启";

                                new Thread(workSloop).Start();

                            }
                        }
                        else
                        {
                            信息框(方舟未运行.Text, "信息:", 5000);
                        }
                    }

                }
                if (下载贡品.Checked == true)
                {
                    if (e.KeyData == (Keys.Z | Keys.Alt))
                    {
                        if (方舟未运行.Text != "方舟未运行" || 方舟未运行.Text != "方舟已运行(隐藏)")
                        {
                            Zloop停止();
                            Console.WriteLine("收到下载消息");
                            if (Zloop)
                            {
                                Zloop = false;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "下载贡品关闭";
                                }
                                下载贡品.Text = "ALT+Z下载贡品已关闭";
                            }
                            else
                            {
                                Zloop = true;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "下载贡品开启";
                                }
                                下载贡品.Text = "ALT+Z下载贡品已开启";
                                new Thread(workZLoop).Start();

                            }
                        }
                        else
                        {
                            信息框(方舟未运行.Text, "信息:", 5000);
                        }
                    }
                }
                if (上传贡品.Checked == true)
                {
                    if (e.KeyData == (Keys.X | Keys.Alt))
                    {
                        if (方舟未运行.Text != "方舟未运行" || 方舟未运行.Text != "方舟已运行(隐藏)")
                        {
                            Xloop停止();
                            Console.WriteLine("收到上传消息");
                            if (Xloop)
                            {
                                Xloop = false;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "上传贡品关闭";
                                }
                                上传贡品.Text = "ALT+X上传贡品已关闭";
                            }
                            else
                            {
                                Xloop = true;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "上传贡品品开启";
                                }
                                上传贡品.Text = "ALT+X上传贡品已开启";
                                new Thread(workXLoop).Start();
                            }
                        }
                        else
                        {
                            信息框(方舟未运行.Text, "信息:", 5000);
                        }
                    }

                }
                if (转移收取.Checked == true)
                {
                    if (e.KeyData == (Keys.D2 | Keys.Alt))
                    {
                        if (激活窗口.Checked == true)
                        {
                            API.窗口_激活显示(句柄);
                        }
                        Console.WriteLine("转移收取");
                        if (checkBox1.Checked == true)
                        {
                            buy.label1.Text = "放下物品栏";
                        }
                        new Thread(shoucaishouqu).Start();
                    }
                }
                if (转移放下.Checked == true)
                {
                    if (e.KeyData == (Keys.D1 | Keys.Alt))
                    {
                        if (激活窗口.Checked == true)
                        {
                            API.窗口_激活显示(句柄);
                        }
                        if (checkBox1.Checked == true)
                        {
                            buy.label1.Text = "收取物品栏";
                        }
                        Console.WriteLine("转移放下");
                        new Thread(shoucaifangxia).Start();
                     
                    }
                }
                if (ALTQ刷新背包.Checked == true)
                {
                    if (e.KeyData == (Keys.Q | Keys.Alt))
                    {
                        if (激活窗口.Checked == true)
                        {
                            API.窗口_激活显示(句柄);
                        }
                        Console.WriteLine("刷新背包");
                        if (checkBox1.Checked == true)
                        {
                            buy.label1.Text = "刷新物品栏";
                        }
                        new Thread(shoucaiLoop).Start();

                    }
                }
                if (ALT0分解种子.Checked == true)
                {
                    if (e.KeyData == (Keys.D0 | Keys.Alt))
                    {
                        if (方舟未运行.Text != "方舟未运行" || 方舟未运行.Text != "方舟已运行(隐藏)")
                        {
                            自动1判断停止();
                            if (激活窗口.Checked == true)
                            {
                                API.窗口_激活显示(句柄);
                            }

                            if (自动1判断 == 1)
                            {
                                自动1判断 = 2;
                                Console.WriteLine("分解种子");
                                ALT0分解种子.Text = "ALT+0分解种子已开启";
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "分解种子已开启";
                                }
                                程序_延时(1000);
                                new Thread(循环禽龙).Start();
                                //循环禽龙();
                            }
                            else
                            {
                                自动1判断 = 1;
                                ALT0分解种子.Text = "ALT+0分解种子已关闭";
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "分解种子已关闭";
                                }
                            }
                        }
                        else
                        {
                            信息框(方舟未运行.Text, "信息:", 5000);
                        }
                    }

                }
                if (ALT3自动O.Checked == true)
                {
                    if (e.KeyData == (Keys.D3 | Keys.Alt))
                    {
                        if (方舟未运行.Text != "方舟未运行" || 方舟未运行.Text != "方舟已运行(隐藏)")
                        {
                            Oloop停止();
                            if (激活窗口.Checked == true)
                            {
                                API.窗口_激活显示(句柄);
                            }
                            Console.WriteLine("自动O");
                            if (Oloop)
                            {
                                Oloop = false;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "自动O键已关闭";
                                }
                                ALT3自动O.Text = "ALT+3自动O键已关闭";
                            }
                            else
                            {
                                Oloop = true;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "自动O键已开启";
                                }
                                ALT3自动O.Text = "ALT+3自动O键已开启";
                                new Thread(workOLoop).Start();
                            }
                        }
                        else
                        {
                            信息框(方舟未运行.Text, "信息:", 5000);
                        }
                    }

                }
                if (ALT4抽血器.Checked == true)
                {
                    if (e.KeyData == (Keys.D4 | Keys.Alt))
                    {
                        if (方舟未运行.Text != "方舟未运行" || 方舟未运行.Text != "方舟已运行(隐藏)")
                        {
                            HPloop停止();
                            if (激活窗口.Checked == true)
                            {
                                API.窗口_激活显示(句柄);
                            }
                            Console.WriteLine("抽血器");
                            if (HPloop)
                            {
                                HPloop = false;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "抽血器已关闭";
                                }
                            }
                            else
                            {
                                HPloop = true;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "抽血器已开启";
                                }
                                new Thread(开始抽血器).Start();
                            }


                        }
                        else
                        {
                            信息框(方舟未运行.Text, "信息:", 5000);
                        }

                    }

                }
                if (F11停止全部.Checked == true)
                {
                    if (e.KeyCode == Keys.F11)
                    {
                        停止();
                    }
                }
                if (ALT5抽鞭子.Checked == true)
                {
                    if (e.KeyData == (Keys.D5 | Keys.Alt))
                    {
                        if (方舟未运行.Text != "方舟未运行" || 方舟未运行.Text != "方舟已运行(隐藏)")
                        {
                            wuloop停止();
                            if (激活窗口.Checked == true)
                            {
                                API.窗口_激活显示(句柄);
                            }
                            Console.WriteLine("ALT5抽鞭子");
                            if (wuloop)
                            {
                                wuloop = false;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "抽鞭子已关闭";
                                }
                                ALT5抽鞭子.Text = "ALT+5抽鞭子已关闭";
                            }
                            else
                            {
                                wuloop = true;
                                if (checkBox1.Checked == true)
                                {
                                    buy.label1.Text = "抽鞭子已开启";
                                }
                                ALT5抽鞭子.Text = "ALT+5抽鞭子已开启";
                                new Thread(开始抽鞭子).Start();
                            }
                        }
                        else
                        {
                            信息框(方舟未运行.Text, "信息:", 5000);
                        }

                    }

                }

            }
        }
        private void 交换坐标()
        {
            string X坐标 = X编辑框1.Text;
            string Y坐标 = Y编辑框1.Text;
            string X坐标1 = X编辑框2.Text;
            string Y坐标1 = Y编辑框2.Text;
            string 下载床1 = 下载角色.Text;
            string 下载床2 = 下载角色2.Text;
            string 服务器1 = 服务器名称.Text;
            string 服务器2 = 服务器名称1.Text;
            X编辑框1.Text = X坐标1;
            Y编辑框1.Text = Y坐标1;
            X编辑框2.Text = X坐标;
            Y编辑框2.Text = Y坐标;
            服务器名称.Text = 服务器2;
            服务器名称1.Text = 服务器1;
            下载角色.Text = 下载床2;
            下载角色2.Text = 下载床1;
        }
        private void PlayAsync(string txt)
        {
            SpeechSynthesizer speechSyn = new SpeechSynthesizer();
            var currentSpokenPrompt = speechSyn.GetCurrentlySpokenPrompt();
            if (currentSpokenPrompt != null)
            {
                speechSyn.SpeakAsyncCancel(currentSpokenPrompt);
            }
            speechSyn.SpeakAsync(txt);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            交换坐标();
        }//交换坐标
        private void 传送服务器()
        {
            while (开启传服判断)
            {
                Thread.Sleep(3000);
                API.键盘_消息(句柄, 5, (byte)Keys.F);
                Thread.Sleep(int.Parse(F后延时.Text));
                鼠标_移动(952, 647);
                Thread.Sleep(100);
                API.鼠标_单击(1);
                Thread.Sleep(int.Parse(服务器等待延时.Text));
                鼠标_移动(1288, 138);
                Thread.Sleep(100);
                API.鼠标_单击(1);
                Thread.Sleep(100);
                API.文本_投递(句柄, 服务器名称.Text);
                Thread.Sleep(int.Parse(服务器等待延时.Text));
                鼠标_移动(854, 243);
                Thread.Sleep(50);
                API.鼠标_单击(1);
                Thread.Sleep(100);
                鼠标_移动(952, 937);
                Thread.Sleep(100);
                API.鼠标_单击(1);
                Thread.Sleep(int.Parse(传送角色等待.Text));

                Console.WriteLine("开始下载");
                if (下载角色.Text != "")
                {
                    鼠标_移动(314, 977);
                    Thread.Sleep(100);
                    API.鼠标_单击(1);
                    API.文本_投递(句柄, 下载角色.Text);
                    Thread.Sleep(1000);
                }
                鼠标_移动(int.Parse(X编辑框1.Text), int.Parse(Y编辑框1.Text));
                Thread.Sleep(1500);
                API.鼠标_单击(1);
                Thread.Sleep(100);
                鼠标_移动(626, 977);
                Thread.Sleep(100);
                API.鼠标_单击(1);
                Thread.Sleep(int.Parse(传送角色等待1.Text));

                if (checkBox1.Checked == true)
                {
                    buy.label1.Text = "下载角色完成";
                }
                开启传服判断 = false;
                交换坐标();
                Console.WriteLine("下载角色完成");
            }

        }
        private void F后延时1_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(F后延时1.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                F后延时1.Text = "1000";
                return;
            }
            API.写配置项(功能配置路径, "Set", "Fhouyanshi1", F后延时1.Text);

        }

        private void F后延时_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(F后延时.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                F后延时.Text = "1000";
                return;
            }
            API.写配置项(功能配置路径, "Set", "Fhouyanshi", F后延时.Text);

        }

        private void 服务器等待延时_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(服务器等待延时.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                服务器等待延时.Text = "5000";
                return;
            }
            API.写配置项(功能配置路径, "Set", "severtimer", 服务器等待延时.Text);

        }

        private void 服务器名称_TextChanged(object sender, EventArgs e)
        {

            API.写配置项(功能配置路径, "Set", "severname", 服务器名称.Text);

        }

        private void 服务器搜索等待_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(服务器搜索等待.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                服务器搜索等待.Text = "5000";
                return;
            }
            API.写配置项(功能配置路径, "Set", "severdaey", 服务器搜索等待.Text);

        }

        private void 传送角色等待_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(传送角色等待.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                传送角色等待.Text = "25000";
                return;
            }
            API.写配置项(功能配置路径, "Set", "namedaey", 传送角色等待.Text);

        }

        private void 下载角色_TextChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "downname", 下载角色.Text);
        }

        private void X编辑框1_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(X编辑框1.Text, out _))
            {
                信息框("只能输入正负阿拉伯数字！", "信息:", 5000);
                X编辑框1.Text = "";
                return;
            }
            API.写配置项(功能配置路径, "Set", "XBOX", X编辑框1.Text);
        }

        private void Y编辑框1_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(Y编辑框1.Text, out _))
            {
                信息框("只能输入正负阿拉伯数字！", "信息:", 5000);
                Y编辑框1.Text = "";
                return;
            }
            API.写配置项(功能配置路径, "Set", "YBOX", Y编辑框1.Text);
        }

        private void X编辑框2_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(X编辑框2.Text, out _))
            {
                信息框("只能输入正负阿拉伯数字！", "信息:", 5000);
                X编辑框2.Text = "";
                return;
            }
            API.写配置项(功能配置路径, "Set", "XBOX2", X编辑框2.Text);
        }

        private void Y编辑框2_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(Y编辑框2.Text, out _))
            {
                信息框("只能输入正负阿拉伯数字！", "信息:", 5000);
                Y编辑框2.Text = "";
                return;
            }
            API.写配置项(功能配置路径, "Set", "YBOX2", Y编辑框2.Text);
        }

        private void 传送角色等待1_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(传送角色等待1.Text, out _))
            {
                信息框("只能输入正负阿拉伯数字！", "信息:", 5000);
                传送角色等待1.Text = "8000";
                return;
            }
            API.写配置项(功能配置路径, "Set", "namedaey1", 传送角色等待1.Text);
        }

        private void 服务器名称1_TextChanged(object sender, EventArgs e)
        {

            API.写配置项(功能配置路径, "Set", "sever1", 服务器名称1.Text);

        }

        private void 下载角色2_TextChanged(object sender, EventArgs e)
        {

            API.写配置项(功能配置路径, "Set", "downname2", 下载角色2.Text);
        }
        private void ALT9传服_CheckedChanged(object sender, EventArgs e)
        {
            if (ALT9传服.Checked == true)
            {
                API.写配置项(功能配置路径, "Hook", "ALT9", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", "ALT9", "0");
            }
        }
        private void 最小化_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void 最小化_点燃(object sender, EventArgs e)
        {
            最小化.BackgroundImage = Properties.Resources.最小化点燃;
        }

        private void 最小化_MouseLeave(object sender, EventArgs e)
        {
            最小化.BackgroundImage = Properties.Resources.最小化常规;
        }
        string zhuangtai;
        private void 更新_Click(object sender, EventArgs e)
        {
            if (severnumber == "")
            {
                API.信息框("无法链接服务器......", "信息:", 5000);
            }
            else
            {
                if (File.Exists("update.exe") == false)
                {
                    API.信息框("update.exe不存在,请在官网下载更新!", "信息:", 5000);
                    Process.Start("http://www.bianshengruanjian.com");
                }
                else
                {
                    if (notifyIcon1.Visible == true)
                    {
                        notifyIcon1.Visible = false;
                    }
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = "update.exe";
                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = true;
                    Process.Start(psi);
                    程序_延时(10);
                    Environment.Exit(0);
                }
            }
           
   
        }
 
        private void 传服停止()
        {
            if (jifugongju == true)
            {
                jifugongju = false;//停止挤服                           
            }
            if (diubaoloop == true)
            {
                diubaoloop = false;//E标志位              
            }
            if (Eloop == true)
            {
                Eloop = false;//E标志位              
                选择框F10.Text = "F10自动E关闭";
                f11停止全部提示();
            }
            if (Floop == true)
            {
                Floop = false;//F标志位
                选择框F9.Text = "F9自动F关闭";
                f11停止全部提示();
            }
            if (label7.Text.IndexOf("前进开启") != -1)
            {
                Console.WriteLine("前进F11关闭");
                wloop = true;//W标志位
                API.键盘_消息(句柄, 4, (byte)Keys.W);
                label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";
                f11停止全部提示();
            }
            if (leftloop == true)
            {
                leftloop = false;//连点器标志位  
                label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";
                f11停止全部提示();
            }
            if (Sloop == true)
            {
                Sloop = false;//自动孵化标志位
                自动孵化.Text = "ALT+S自动冻龙已关闭";
                f11停止全部提示();
            }

            if (Xloop == true)
            {
                Xloop = false;//自动上传
                上传贡品.Text = "ALT+X上传贡品已关闭";
                f11停止全部提示();
            }
            if (Zloop == true)
            {
                Zloop = false;//自动下载
                下载贡品.Text = "ALT+Z下载贡品已关闭";
                f11停止全部提示();
            }
            if (Cloop == true)
            {
                Cloop = false;//自动C
                ALTC自动C.Text = "自动C键已关闭";
                f11停止全部提示();
            }
            if (Rloop == true)
            {
                Rloop = false;//自动右键
                ALTR自动右键.Text = "自动右键已关闭";
                f11停止全部提示();
            }
            if (Oloop == true)
            {
                Oloop = false;//自动o键
                ALT3自动O.Text = "ALT+3自动O键已关闭";
                f11停止全部提示();
            }
            if (HPloop == true)
            {
                HPloop = false;//自动抽血器                
                f11停止全部提示();
            }

            if (自动1判断 != 1)
            {
                自动1判断 = 1;
                f11停止全部提示();
            }
            if (wuloop == true)
            {
                wuloop = false;
                ALT5抽鞭子.Text = "ALT+5抽鞭子已关闭";
                f11停止全部提示();
            }


            f11停止全部提示();
        }
        private void 停止全部()
        {
           
            pictureBox4.Image = null;
            计时吃东西时钟.Stop();
            计时吃饭.Checked = false;
            计时喝水时钟.Stop();
            计时喝水.Checked = false;
            检测黑包.Checked = false;
            检测黑包时钟.Stop();
            识图自动吃喝.Checked = false;
            识图自动时钟.Stop();
            if (开启传服判断 == true)
            {
                开启传服判断 = false;

            }
   
            if (jifugongju == true)
            {
                jifugongju = false;//停止挤服                           
            }
            if (diubaoloop == true)
            {
                diubaoloop = false;//E标志位              
            }
            if (Eloop == true)
            {
                Eloop = false;//E标志位              
                选择框F10.Text = "F10自动E关闭";
                f11停止全部提示();
            }
            if (Floop == true)
            {
                Floop = false;//F标志位
                选择框F9.Text = "F9自动F关闭";
                f11停止全部提示();
            }
            if (label7.Text.IndexOf("前进开启") != -1)
            {
                Console.WriteLine("前进F11关闭");
                wloop = true;//W标志位
                API.键盘_消息(句柄, 4, (byte)Keys.W);
                label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";
                f11停止全部提示();
            }
            if (leftloop == true)
            {
                leftloop = false;//连点器标志位  
                label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";
                f11停止全部提示();
            }
            if (Sloop == true)
            {
                Sloop = false;//自动孵化标志位
                自动孵化.Text = "ALT+S自动冻龙已关闭";
                f11停止全部提示();
            }

            if (Xloop == true)
            {
                Xloop = false;//自动上传
                上传贡品.Text = "ALT+X上传贡品已关闭";
                f11停止全部提示();
            }
            if (Zloop == true)
            {
                Zloop = false;//自动下载
                下载贡品.Text = "ALT+Z下载贡品已关闭";
                f11停止全部提示();
            }
            if (Cloop == true)
            {
                Cloop = false;//自动C
                ALTC自动C.Text = "自动C键已关闭";
                f11停止全部提示();
            }
            if (Rloop == true)
            {
                Rloop = false;//自动右键
                ALTR自动右键.Text = "自动右键已关闭";
                f11停止全部提示();
            }
            if (Oloop == true)
            {
                Oloop = false;//自动o键
                ALT3自动O.Text = "ALT+3自动O键已关闭";
                f11停止全部提示();
            }
            if (HPloop == true)
            {
                HPloop = false;//自动抽血器                
                f11停止全部提示();
            }

            if (自动1判断 != 1)
            {
                自动1判断 = 1;
                f11停止全部提示();
            }
            if (wuloop == true)
            {
                wuloop = false;
                ALT5抽鞭子.Text = "ALT+5抽鞭子已关闭";
                f11停止全部提示();
            }

        }
        private void 停止()
        {
            停止全部();
            f11停止全部提示();
        }
        private void 挤服停止()
        {
            if (wuloop == true)
            {
                wuloop = false;
                ALT5抽鞭子.Text = "ALT+5抽鞭子已关闭";
                f11停止全部提示();
            }

            if (开启传服判断 == true)
            {
                开启传服判断 = false;

            }
            if (diubaoloop == true)
            {
                diubaoloop = false;//E标志位              
            }
            if (Eloop == true)
            {
                Eloop = false;//E标志位              
                选择框F10.Text = "F10自动E关闭";

            }
            if (Floop == true)
            {
                Floop = false;//F标志位
                选择框F9.Text = "F9自动F关闭";

            }
            if (label7.Text.IndexOf("前进开启") != -1)
            {
                Console.WriteLine("前进F11关闭");
                wloop = true;//W标志位
                API.键盘_消息(句柄, 4, (byte)Keys.W);
                label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";

            }
            if (leftloop == true)
            {
                leftloop = false;//连点器标志位  
                label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";

            }
            if (Sloop == true)
            {
                Sloop = false;//自动孵化标志位
                自动孵化.Text = "ALT+S自动冻龙已关闭";

            }

            if (Xloop == true)
            {
                Xloop = false;//自动上传
                上传贡品.Text = "ALT+X上传贡品已关闭";

            }
            if (Zloop == true)
            {
                Zloop = false;//自动下载
                下载贡品.Text = "ALT+Z下载贡品已关闭";

            }
            if (Cloop == true)
            {
                Cloop = false;//自动C
                ALTC自动C.Text = "自动C键已关闭";

            }
            if (Rloop == true)
            {
                Rloop = false;//自动右键
                ALTR自动右键.Text = "自动右键已关闭";

            }
            if (Oloop == true)
            {
                Oloop = false;//自动o键
                ALT3自动O.Text = "ALT+3自动O键已关闭";

            }
            if (HPloop == true)
            {
                HPloop = false;//自动抽血器                

            }

            if (自动1判断 != 1)
            {
                自动1判断 = 1;

            }

        }
        private void wuloop停止()
        {
            if (jifugongju == true)
            {
                jifugongju = false;//停止挤服                           
            }
            if (开启传服判断 == true)
            {
                开启传服判断 = false;

            }
            if (diubaoloop == true)
            {
                diubaoloop = false;//E标志位              
            }
            if (Eloop == true)
            {
                Eloop = false;//E标志位              
                选择框F10.Text = "F10自动E关闭";

            }
            if (Floop == true)
            {
                Floop = false;//F标志位
                选择框F9.Text = "F9自动F关闭";

            }
            if (label7.Text.IndexOf("前进开启") != -1)
            {
                Console.WriteLine("前进F11关闭");
                wloop = true;//W标志位
                API.键盘_消息(句柄, 4, (byte)Keys.W);
                label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";

            }
            if (leftloop == true)
            {
                leftloop = false;//连点器标志位  
                label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";

            }
            if (Sloop == true)
            {
                Sloop = false;//自动孵化标志位
                自动孵化.Text = "ALT+S自动冻龙已关闭";

            }

            if (Xloop == true)
            {
                Xloop = false;//自动上传
                上传贡品.Text = "ALT+X上传贡品已关闭";

            }
            if (Zloop == true)
            {
                Zloop = false;//自动下载
                下载贡品.Text = "ALT+Z下载贡品已关闭";

            }
            if (Cloop == true)
            {
                Cloop = false;//自动C
                ALTC自动C.Text = "自动C键已关闭";

            }
            if (Rloop == true)
            {
                Rloop = false;//自动右键
                ALTR自动右键.Text = "自动右键已关闭";

            }
            if (Oloop == true)
            {
                Oloop = false;//自动o键
                ALT3自动O.Text = "ALT+3自动O键已关闭";

            }
            if (HPloop == true)
            {
                HPloop = false;//自动抽血器                

            }

            if (自动1判断 != 1)
            {
                自动1判断 = 1;

            }

        }
        private void diubaoloop停止1()
        {
            if (jifugongju == true)
            {
                jifugongju = false;//停止挤服                           
            }
            if (开启传服判断 == true)
            {
                开启传服判断 = false;

            }
            if (Eloop == true)
            {
                Eloop = false;//E标志位              
                选择框F10.Text = "F10自动E关闭";
                f11停止全部提示();
            }
            if (Floop == true)
            {
                Floop = false;//F标志位
                选择框F9.Text = "F9自动F关闭";
                f11停止全部提示();
            }
            if (label7.Text.IndexOf("前进开启") != -1)
            {
                Console.WriteLine("前进F11关闭");
                wloop = true;//W标志位
                API.键盘_消息(句柄, 4, (byte)Keys.W);
                label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";
                f11停止全部提示();
            }

            if (Sloop == true)
            {
                Sloop = false;//自动孵化标志位
                自动孵化.Text = "ALT+S自动冻龙已关闭";
                f11停止全部提示();
            }

            if (Xloop == true)
            {
                Xloop = false;//自动上传
                上传贡品.Text = "ALT+X上传贡品已关闭";
                f11停止全部提示();
            }
            if (Zloop == true)
            {
                Zloop = false;//自动下载
                下载贡品.Text = "ALT+Z下载贡品已关闭";
                f11停止全部提示();
            }
            if (Cloop == true)
            {
                Cloop = false;//自动C
                ALTC自动C.Text = "自动C键已关闭";
                f11停止全部提示();
            }
            if (Rloop == true)
            {
                Rloop = false;//自动右键
                ALTR自动右键.Text = "自动右键已关闭";
                f11停止全部提示();
            }
            if (Oloop == true)
            {
                Oloop = false;//自动o键
                ALT3自动O.Text = "ALT+3自动O键已关闭";
                f11停止全部提示();
            }
            if (HPloop == true)
            {
                HPloop = false;//自动抽血器                
                f11停止全部提示();
            }

            if (自动1判断 != 1)
            {
                自动1判断 = 1;
                f11停止全部提示();
            }
            if (wuloop == true)
            {
                wuloop = false;
                ALT5抽鞭子.Text = "ALT+5抽鞭子已关闭";
                f11停止全部提示();
            }


            f11停止全部提示();
        }
        private void diubaoloop停止()
        {
            if (jifugongju == true)
            {
                jifugongju = false;//停止挤服                           
            }
            if (开启传服判断 == true)
            {
                开启传服判断 = false;

            }
            if (Eloop == true)
            {
                Eloop = false;//E标志位              
                选择框F10.Text = "F10自动E关闭";
                f11停止全部提示();
            }
            if (Floop == true)
            {
                Floop = false;//F标志位
                选择框F9.Text = "F9自动F关闭";
                f11停止全部提示();
            }
            if (label7.Text.IndexOf("前进开启") != -1)
            {
                Console.WriteLine("前进F11关闭");
                wloop = true;//W标志位
                API.键盘_消息(句柄, 4, (byte)Keys.W);
                label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";
                f11停止全部提示();
            }
            if (leftloop == true)
            {
                leftloop = false;//连点器标志位  
                label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";
                f11停止全部提示();
            }
            if (Sloop == true)
            {
                Sloop = false;//自动孵化标志位
                自动孵化.Text = "ALT+S自动冻龙已关闭";
                f11停止全部提示();
            }

            if (Xloop == true)
            {
                Xloop = false;//自动上传
                上传贡品.Text = "ALT+X上传贡品已关闭";
                f11停止全部提示();
            }
            if (Zloop == true)
            {
                Zloop = false;//自动下载
                下载贡品.Text = "ALT+Z下载贡品已关闭";
                f11停止全部提示();
            }
            if (Cloop == true)
            {
                Cloop = false;//自动C
                ALTC自动C.Text = "自动C键已关闭";
                f11停止全部提示();
            }
            if (Rloop == true)
            {
                Rloop = false;//自动右键
                ALTR自动右键.Text = "自动右键已关闭";
                f11停止全部提示();
            }
            if (Oloop == true)
            {
                Oloop = false;//自动o键
                ALT3自动O.Text = "ALT+3自动O键已关闭";
                f11停止全部提示();
            }
            if (HPloop == true)
            {
                HPloop = false;//自动抽血器                
                f11停止全部提示();
            }

            if (自动1判断 != 1)
            {
                自动1判断 = 1;
                f11停止全部提示();
            }
            if (wuloop == true)
            {
                wuloop = false;
                ALT5抽鞭子.Text = "ALT+5抽鞭子已关闭";
                f11停止全部提示();
            }


            f11停止全部提示();
        }
        private void HPloop停止()
        {
            if (jifugongju == true)
            {
                jifugongju = false;//停止挤服                           
            }
            if (开启传服判断 == true)
            {
                开启传服判断 = false;

            }
            if (diubaoloop == true)
            {
                diubaoloop = false;//E标志位              
            }
            if (Eloop == true)
            {
                Eloop = false;//E标志位              
                选择框F10.Text = "F10自动E关闭";

            }
            if (Floop == true)
            {
                Floop = false;//F标志位
                选择框F9.Text = "F9自动F关闭";

            }
            if (label7.Text.IndexOf("前进开启") != -1)
            {
                Console.WriteLine("前进F11关闭");
                wloop = true;//W标志位
                API.键盘_消息(句柄, 4, (byte)Keys.W);
                label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";

            }
            if (leftloop == true)
            {
                leftloop = false;//连点器标志位  
                label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";

            }
            if (Sloop == true)
            {
                Sloop = false;//自动孵化标志位
                自动孵化.Text = "ALT+S自动冻龙已关闭";

            }

            if (Xloop == true)
            {
                Xloop = false;//自动上传
                上传贡品.Text = "ALT+X上传贡品已关闭";

            }
            if (Zloop == true)
            {
                Zloop = false;//自动下载
                下载贡品.Text = "ALT+Z下载贡品已关闭";

            }
            if (Cloop == true)
            {
                Cloop = false;//自动C
                ALTC自动C.Text = "自动C键已关闭";

            }
            if (Rloop == true)
            {
                Rloop = false;//自动右键
                ALTR自动右键.Text = "自动右键已关闭";

            }
            if (Oloop == true)
            {
                Oloop = false;//自动o键
                ALT3自动O.Text = "ALT+3自动O键已关闭";

            }


            if (自动1判断 != 1)
            {
                自动1判断 = 1;

            }
            if (wuloop == true)
            {
                wuloop = false;
                ALT5抽鞭子.Text = "ALT+5抽鞭子已关闭";

            }
        }
        private void Oloop停止()
        {
            if (jifugongju == true)
            {
                jifugongju = false;//停止挤服                           
            }
            if (开启传服判断 == true)
            {
                开启传服判断 = false;

            }
            if (diubaoloop == true)
            {
                diubaoloop = false;//E标志位              
            }
            if (Eloop == true)
            {
                Eloop = false;//E标志位              
                选择框F10.Text = "F10自动E关闭";

            }
            if (Floop == true)
            {
                Floop = false;//F标志位
                选择框F9.Text = "F9自动F关闭";

            }
            if (label7.Text.IndexOf("前进开启") != -1)
            {
                Console.WriteLine("前进F11关闭");
                wloop = true;//W标志位
                API.键盘_消息(句柄, 4, (byte)Keys.W);
                label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";

            }
            if (leftloop == true)
            {
                leftloop = false;//连点器标志位  
                label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";

            }
            if (Sloop == true)
            {
                Sloop = false;//自动孵化标志位
                自动孵化.Text = "ALT+S自动冻龙已关闭";

            }

            if (Xloop == true)
            {
                Xloop = false;//自动上传
                上传贡品.Text = "ALT+X上传贡品已关闭";

            }
            if (Zloop == true)
            {
                Zloop = false;//自动下载
                下载贡品.Text = "ALT+Z下载贡品已关闭";

            }
            if (Cloop == true)
            {
                Cloop = false;//自动C
                ALTC自动C.Text = "自动C键已关闭";

            }
            if (Rloop == true)
            {
                Rloop = false;//自动右键
                ALTR自动右键.Text = "自动右键已关闭";

            }

            if (HPloop == true)
            {
                HPloop = false;//自动抽血器                

            }

            if (自动1判断 != 1)
            {
                自动1判断 = 1;

            }
            if (wuloop == true)
            {
                wuloop = false;
                ALT5抽鞭子.Text = "ALT+5抽鞭子已关闭";

            }
        }
        private void 自动1判断停止()
        {
            if (jifugongju == true)
            {
                jifugongju = false;//停止挤服                           
            }
            if (开启传服判断 == true)
            {
                开启传服判断 = false;

            }
            if (diubaoloop == true)
            {
                diubaoloop = false;//E标志位              
            }
            if (Eloop == true)
            {
                Eloop = false;//E标志位              
                选择框F10.Text = "F10自动E关闭";

            }
            if (Floop == true)
            {
                Floop = false;//F标志位
                选择框F9.Text = "F9自动F关闭";

            }
            if (label7.Text.IndexOf("前进开启") != -1)
            {
                Console.WriteLine("前进F11关闭");
                wloop = true;//W标志位
                API.键盘_消息(句柄, 4, (byte)Keys.W);
                label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";

            }
            if (leftloop == true)
            {
                leftloop = false;//连点器标志位  
                label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";

            }
            if (Sloop == true)
            {
                Sloop = false;//自动孵化标志位
                自动孵化.Text = "ALT+S自动冻龙已关闭";

            }

            if (Xloop == true)
            {
                Xloop = false;//自动上传
                上传贡品.Text = "ALT+X上传贡品已关闭";

            }
            if (Zloop == true)
            {
                Zloop = false;//自动下载
                下载贡品.Text = "ALT+Z下载贡品已关闭";

            }
            if (Cloop == true)
            {
                Cloop = false;//自动C
                ALTC自动C.Text = "自动C键已关闭";

            }
            if (Rloop == true)
            {
                Rloop = false;//自动右键
                ALTR自动右键.Text = "自动右键已关闭";

            }
            if (Oloop == true)
            {
                Oloop = false;//自动o键
                ALT3自动O.Text = "ALT+3自动O键已关闭";

            }
            if (HPloop == true)
            {
                HPloop = false;//自动抽血器                

            }

            if (wuloop == true)
            {
                wuloop = false;
                ALT5抽鞭子.Text = "ALT+5抽鞭子已关闭";

            }
        }
        private void Zloop停止()
        {
            if (jifugongju == true)
            {
                jifugongju = false;//停止挤服                           
            }
            if (开启传服判断 == true)
            {
                开启传服判断 = false;

            }
            if (diubaoloop == true)
            {
                diubaoloop = false;//E标志位              
            }
            if (Eloop == true)
            {
                Eloop = false;//E标志位              
                选择框F10.Text = "F10自动E关闭";

            }
            if (Floop == true)
            {
                Floop = false;//F标志位
                选择框F9.Text = "F9自动F关闭";

            }
            if (label7.Text.IndexOf("前进开启") != -1)
            {
                Console.WriteLine("前进F11关闭");
                wloop = true;//W标志位
                API.键盘_消息(句柄, 4, (byte)Keys.W);
                label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";

            }
            if (leftloop == true)
            {
                leftloop = false;//连点器标志位  
                label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";

            }
            if (Sloop == true)
            {
                Sloop = false;//自动孵化标志位
                自动孵化.Text = "ALT+S自动冻龙已关闭";

            }

            if (Xloop == true)
            {
                Xloop = false;//自动上传
                上传贡品.Text = "ALT+X上传贡品已关闭";

            }

            if (Cloop == true)
            {
                Cloop = false;//自动C
                ALTC自动C.Text = "自动C键已关闭";

            }
            if (Rloop == true)
            {
                Rloop = false;//自动右键
                ALTR自动右键.Text = "自动右键已关闭";

            }
            if (Oloop == true)
            {
                Oloop = false;//自动o键
                ALT3自动O.Text = "ALT+3自动O键已关闭";

            }
            if (HPloop == true)
            {
                HPloop = false;//自动抽血器                

            }

            if (自动1判断 != 1)
            {
                自动1判断 = 1;

            }
            if (wuloop == true)
            {
                wuloop = false;
                ALT5抽鞭子.Text = "ALT+5抽鞭子已关闭";

            }
        }
        private void Xloop停止()
        {
            if (jifugongju == true)
            {
                jifugongju = false;//停止挤服                           
            }
            if (开启传服判断 == true)
            {
                开启传服判断 = false;

            }
            if (diubaoloop == true)
            {
                diubaoloop = false;//E标志位              
            }
            if (Eloop == true)
            {
                Eloop = false;//E标志位              
                选择框F10.Text = "F10自动E关闭";

            }
            if (Floop == true)
            {
                Floop = false;//F标志位
                选择框F9.Text = "F9自动F关闭";

            }
            if (label7.Text.IndexOf("前进开启") != -1)
            {
                Console.WriteLine("前进F11关闭");
                wloop = true;//W标志位
                API.键盘_消息(句柄, 4, (byte)Keys.W);
                label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";

            }
            if (leftloop == true)
            {
                leftloop = false;//连点器标志位  
                label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";

            }
            if (Sloop == true)
            {
                Sloop = false;//自动孵化标志位
                自动孵化.Text = "ALT+S自动冻龙已关闭";

            }

            if (Zloop == true)
            {
                Zloop = false;//自动下载
                下载贡品.Text = "ALT+Z下载贡品已关闭";

            }
            if (Cloop == true)
            {
                Cloop = false;//自动C
                ALTC自动C.Text = "自动C键已关闭";

            }
            if (Rloop == true)
            {
                Rloop = false;//自动右键
                ALTR自动右键.Text = "自动右键已关闭";

            }
            if (Oloop == true)
            {
                Oloop = false;//自动o键
                ALT3自动O.Text = "ALT+3自动O键已关闭";

            }
            if (HPloop == true)
            {
                HPloop = false;//自动抽血器                

            }

            if (自动1判断 != 1)
            {
                自动1判断 = 1;

            }
            if (wuloop == true)
            {
                wuloop = false;
                ALT5抽鞭子.Text = "ALT+5抽鞭子已关闭";

            }
        }
        private void Sloop停止()
        {
            if (jifugongju == true)
            {
                jifugongju = false;//停止挤服                           
            }
            if (开启传服判断 == true)
            {
                开启传服判断 = false;

            }
            if (diubaoloop == true)
            {
                diubaoloop = false;//E标志位              
            }
            if (Eloop == true)
            {
                Eloop = false;//E标志位              
                选择框F10.Text = "F10自动E关闭";

            }
            if (Floop == true)
            {
                Floop = false;//F标志位
                选择框F9.Text = "F9自动F关闭";

            }
            if (label7.Text.IndexOf("前进开启") != -1)
            {
                Console.WriteLine("前进F11关闭");
                wloop = true;//W标志位
                API.键盘_消息(句柄, 4, (byte)Keys.W);
                label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";

            }
            if (leftloop == true)
            {
                leftloop = false;//连点器标志位  
                label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";

            }


            if (Xloop == true)
            {
                Xloop = false;//自动上传
                上传贡品.Text = "ALT+X上传贡品已关闭";

            }
            if (Zloop == true)
            {
                Zloop = false;//自动下载
                下载贡品.Text = "ALT+Z下载贡品已关闭";

            }
            if (Cloop == true)
            {
                Cloop = false;//自动C
                ALTC自动C.Text = "自动C键已关闭";

            }
            if (Rloop == true)
            {
                Rloop = false;//自动右键
                ALTR自动右键.Text = "自动右键已关闭";

            }
            if (Oloop == true)
            {
                Oloop = false;//自动o键
                ALT3自动O.Text = "ALT+3自动O键已关闭";

            }
            if (HPloop == true)
            {
                HPloop = false;//自动抽血器                

            }

            if (自动1判断 != 1)
            {
                自动1判断 = 1;

            }
            if (wuloop == true)
            {
                wuloop = false;
                ALT5抽鞭子.Text = "ALT+5抽鞭子已关闭";

            }
        }
        private void Eloop停止()
        {
            if (jifugongju == true)
            {
                jifugongju = false;//停止挤服                           
            }
            if (开启传服判断 == true)
            {
                开启传服判断 = false;

            }
            if (diubaoloop == true)
            {
                diubaoloop = false;//E标志位              
            }

            if (Floop == true)
            {
                Floop = false;//F标志位
                选择框F9.Text = "F9自动F关闭";

            }
            if (label7.Text.IndexOf("前进开启") != -1)
            {
                Console.WriteLine("前进F11关闭");
                wloop = true;//W标志位
                API.键盘_消息(句柄, 4, (byte)Keys.W);
                label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";

            }
            if (leftloop == true)
            {
                leftloop = false;//连点器标志位  
                label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";

            }
            if (Sloop == true)
            {
                Sloop = false;//自动孵化标志位
                自动孵化.Text = "ALT+S自动冻龙已关闭";

            }

            if (Xloop == true)
            {
                Xloop = false;//自动上传
                上传贡品.Text = "ALT+X上传贡品已关闭";

            }
            if (Zloop == true)
            {
                Zloop = false;//自动下载
                下载贡品.Text = "ALT+Z下载贡品已关闭";

            }
            if (Cloop == true)
            {
                Cloop = false;//自动C
                ALTC自动C.Text = "自动C键已关闭";

            }
            if (Rloop == true)
            {
                Rloop = false;//自动右键
                ALTR自动右键.Text = "自动右键已关闭";

            }
            if (Oloop == true)
            {
                Oloop = false;//自动o键
                ALT3自动O.Text = "ALT+3自动O键已关闭";

            }
            if (HPloop == true)
            {
                HPloop = false;//自动抽血器                

            }

            if (自动1判断 != 1)
            {
                自动1判断 = 1;

            }
            if (wuloop == true)
            {
                wuloop = false;
                ALT5抽鞭子.Text = "ALT+5抽鞭子已关闭";

            }
        }
        private void Floop停止()
        {
            if (jifugongju == true)
            {
                jifugongju = false;//停止挤服                           
            }
            if (开启传服判断 == true)
            {
                开启传服判断 = false;

            }
            if (diubaoloop == true)
            {
                diubaoloop = false;//E标志位              
            }
            if (Eloop == true)
            {
                Eloop = false;//E标志位              
                选择框F10.Text = "F10自动E关闭";

            }

            if (label7.Text.IndexOf("前进开启") != -1)
            {
                Console.WriteLine("前进F11关闭");
                wloop = true;//W标志位
                API.键盘_消息(句柄, 4, (byte)Keys.W);
                label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";

            }
            if (leftloop == true)
            {
                leftloop = false;//连点器标志位  
                label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";

            }
            if (Sloop == true)
            {
                Sloop = false;//自动孵化标志位
                自动孵化.Text = "ALT+S自动冻龙已关闭";

            }

            if (Xloop == true)
            {
                Xloop = false;//自动上传
                上传贡品.Text = "ALT+X上传贡品已关闭";

            }
            if (Zloop == true)
            {
                Zloop = false;//自动下载
                下载贡品.Text = "ALT+Z下载贡品已关闭";

            }
            if (Cloop == true)
            {
                Cloop = false;//自动C
                ALTC自动C.Text = "自动C键已关闭";

            }
            if (Rloop == true)
            {
                Rloop = false;//自动右键
                ALTR自动右键.Text = "自动右键已关闭";

            }
            if (Oloop == true)
            {
                Oloop = false;//自动o键
                ALT3自动O.Text = "ALT+3自动O键已关闭";

            }
            if (HPloop == true)
            {
                HPloop = false;//自动抽血器                

            }

            if (自动1判断 != 1)
            {
                自动1判断 = 1;

            }
            if (wuloop == true)
            {
                wuloop = false;
                ALT5抽鞭子.Text = "ALT+5抽鞭子已关闭";

            }
        }
        private void Cloop停止()
        {
            if (jifugongju == true)
            {
                jifugongju = false;//停止挤服                           
            }
            if (开启传服判断 == true)
            {
                开启传服判断 = false;

            }
            if (diubaoloop == true)
            {
                diubaoloop = false;//E标志位              
            }
            if (Eloop == true)
            {
                Eloop = false;//E标志位              
                选择框F10.Text = "F10自动E关闭";

            }
            if (Floop == true)
            {
                Floop = false;//F标志位
                选择框F9.Text = "F9自动F关闭";

            }
            if (label7.Text.IndexOf("前进开启") != -1)
            {
                Console.WriteLine("前进F11关闭");
                wloop = true;//W标志位
                API.键盘_消息(句柄, 4, (byte)Keys.W);
                label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";

            }
            if (leftloop == true)
            {
                leftloop = false;//连点器标志位  
                label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";

            }
            if (Sloop == true)
            {
                Sloop = false;//自动孵化标志位
                自动孵化.Text = "ALT+S自动冻龙已关闭";

            }

            if (Xloop == true)
            {
                Xloop = false;//自动上传
                上传贡品.Text = "ALT+X上传贡品已关闭";

            }
            if (Zloop == true)
            {
                Zloop = false;//自动下载
                下载贡品.Text = "ALT+Z下载贡品已关闭";

            }

            if (Rloop == true)
            {
                Rloop = false;//自动右键
                ALTR自动右键.Text = "自动右键已关闭";

            }
            if (Oloop == true)
            {
                Oloop = false;//自动o键
                ALT3自动O.Text = "ALT+3自动O键已关闭";

            }
            if (HPloop == true)
            {
                HPloop = false;//自动抽血器                

            }

            if (自动1判断 != 1)
            {
                自动1判断 = 1;

            }
            if (wuloop == true)
            {
                wuloop = false;
                ALT5抽鞭子.Text = "ALT+5抽鞭子已关闭";

            }
        }
        private void Rloop停止()
        {
            if (jifugongju == true)
            {
                jifugongju = false;//停止挤服                           
            }
            if (开启传服判断 == true)
            {
                开启传服判断 = false;

            }
            if (diubaoloop == true)
            {
                diubaoloop = false;//E标志位              
            }
            if (Eloop == true)
            {
                Eloop = false;//E标志位              
                选择框F10.Text = "F10自动E关闭";

            }
            if (Floop == true)
            {
                Floop = false;//F标志位
                选择框F9.Text = "F9自动F关闭";

            }
            if (label7.Text.IndexOf("前进开启") != -1)
            {
                Console.WriteLine("前进F11关闭");
                wloop = true;//W标志位
                API.键盘_消息(句柄, 4, (byte)Keys.W);
                label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";

            }
            if (leftloop == true)
            {
                leftloop = false;//连点器标志位  
                label2.Text = 鼠标左键编辑框.SelectedItem.ToString() + "连点器关闭";

            }
            if (Sloop == true)
            {
                Sloop = false;//自动孵化标志位
                自动孵化.Text = "ALT+S自动冻龙已关闭";

            }

            if (Xloop == true)
            {
                Xloop = false;//自动上传
                上传贡品.Text = "ALT+X上传贡品已关闭";

            }
            if (Zloop == true)
            {
                Zloop = false;//自动下载
                下载贡品.Text = "ALT+Z下载贡品已关闭";

            }
            if (Cloop == true)
            {
                Cloop = false;//自动C
                ALTC自动C.Text = "自动C键已关闭";

            }

            if (Oloop == true)
            {
                Oloop = false;//自动o键
                ALT3自动O.Text = "ALT+3自动O键已关闭";

            }
            if (HPloop == true)
            {
                HPloop = false;//自动抽血器                

            }

            if (自动1判断 != 1)
            {
                自动1判断 = 1;

            }
            if (wuloop == true)
            {
                wuloop = false;
                ALT5抽鞭子.Text = "ALT+5抽鞭子已关闭";

            }
        }

        private void 开始抽鞭子()//自动抽鞭子   
        {
            for (int i = int.Parse(textBox20.Text); i > 0; i--)
            {
                Console.WriteLine("自动抽鞭子还有" + i + "次");
                label47.Text = "自动抽鞭子还有" + i + "次";
                if (wuloop == false)
                {
                    Console.WriteLine("跳出循环");
                    break;
                }
                if (checkBox4.Checked == true)
                    API.鼠标_消息(句柄, 1, 1);
                else API.鼠标_单击(1);
                if (wuloop == false)
                {
                    Console.WriteLine("跳出循环");
                    break;
                }
                Thread.Sleep(int.Parse(textBox19.Text) * 1000);//延迟鞭子抽的时间
            }
            if (checkBox1.Checked == true)
            {
                buy.label1.Text = "抽鞭子已关闭";
            }
        }
        private void diubaozloop()//自动丢包
        {
            while (diubaoloop)
            {
                if (checkBox7.Checked == true)
                {
                    if (checkBox6.Checked == true)
                    {
                        API.键盘_消息(句柄, 5, (byte)Keys.F);
                    }
                    else
                    {
                        API.键盘_消息(句柄, 5, (byte)Keys.I);
                    }
                    Thread.Sleep(1000);
                }
                if (diubaoloop == false)
                {
                    break;
                }
                if (checkBox8.Checked == true)
                {
                    初始化搜索栏();
                    API.文本_投递(句柄, textBox17.Text);
                    Thread.Sleep(int.Parse(textBox39.Text));
                    if (checkBox21.Checked == true)
                    {
                        英文一键丢();
                    }
                    else
                    {
                        for (int i = 0; i < int.Parse(textBox21.Text); i++)
                        {
                            if (diubaoloop == false)
                            {
                                break;
                            }
                            循环丢弃();
                        }
                    }
                }
                if (checkBox9.Checked == true)
                {
                    初始化搜索栏();
                    API.文本_投递(句柄, textBox23.Text);
                    Thread.Sleep(int.Parse(textBox39.Text));
                    if (checkBox21.Checked == true)
                    {
                        英文一键丢();
                    }
                    else
                    {
                        for (int i = 0; i < int.Parse(textBox22.Text); i++)
                        {
                            if (diubaoloop == false)
                            {
                                break;
                            }
                            循环丢弃();
                        }
                    }
                }
                if (checkBox11.Checked == true)
                {
                    初始化搜索栏();
                    API.文本_投递(句柄, textBox25.Text);
                    Thread.Sleep(int.Parse(textBox39.Text));
                    if (checkBox21.Checked == true)
                    {
                        英文一键丢();
                    }
                    else
                    {
                        for (int i = 0; i < int.Parse(textBox24.Text); i++)
                        {
                            if (diubaoloop == false)
                            {
                                break;
                            }
                            循环丢弃();
                        }
                    }
                }
                if (checkBox10.Checked == true)
                {
                    初始化搜索栏();
                    API.文本_投递(句柄, textBox27.Text);
                    Thread.Sleep(int.Parse(textBox39.Text));
                    if (checkBox21.Checked == true)
                    {
                        英文一键丢();
                    }
                    else
                    {
                        for (int i = 0; i < int.Parse(textBox26.Text); i++)
                        {
                            if (diubaoloop == false)
                            {
                                break;
                            }
                            循环丢弃();
                        }
                    }
                }
                if (checkBox15.Checked == true)
                {
                    初始化搜索栏();
                    API.文本_投递(句柄, textBox29.Text);
                    Thread.Sleep(int.Parse(textBox39.Text));
                    if (checkBox21.Checked == true)
                    {
                        英文一键丢();
                    }
                    else
                    {
                        for (int i = 0; i < int.Parse(textBox28.Text); i++)
                        {
                            if (diubaoloop == false)
                            {
                                break;
                            }
                            循环丢弃();
                        }
                    }
                }
                if (checkBox14.Checked == true)
                {
                    初始化搜索栏();
                    API.文本_投递(句柄, textBox31.Text);
                    Thread.Sleep(int.Parse(textBox39.Text));
                    if (checkBox21.Checked == true)
                    {
                        英文一键丢();
                    }
                    else
                    {
                        for (int i = 0; i < int.Parse(textBox30.Text); i++)
                        {
                            if (diubaoloop == false)
                            {
                                break;
                            }
                            循环丢弃();
                        }
                    }
                }
                if (checkBox13.Checked == true)
                {
                    初始化搜索栏();
                    API.文本_投递(句柄, textBox33.Text);
                    Thread.Sleep(int.Parse(textBox39.Text));
                    if (checkBox21.Checked == true)
                    {
                        英文一键丢();
                    }
                    else
                    {
                        for (int i = 0; i < int.Parse(textBox32.Text); i++)
                        {
                            if (diubaoloop == false)
                            {
                                break;
                            }
                            循环丢弃();
                        }
                    }
                }
                if (checkBox12.Checked == true)
                {
                    初始化搜索栏();
                    API.文本_投递(句柄, textBox35.Text);
                    Thread.Sleep(int.Parse(textBox39.Text));
                    if (checkBox21.Checked == true)
                    {
                        英文一键丢();
                    }
                    else
                    {
                        for (int i = 0; i < int.Parse(textBox34.Text); i++)
                        {
                            if (diubaoloop == false)
                            {
                                break;
                            }
                            循环丢弃();
                        }
                    }
                }
                Thread.Sleep(100);
                diubaoloop = false;
                if (checkBox1.Checked == true)
                {
                    buy.label1.Text = "一键清包停止";
                }
                if (checkBox7.Checked == true)
                {
                    Thread.Sleep(500);
                    API.键盘_消息(句柄, 5, (byte)Keys.Escape);
                }
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            鼠标_移动(1287, 180);
            Thread.Sleep(2000);
            API.mouse_event(API.MouseEventFlag.Move, 250, 0, 0, 0);//核心分解
        }
        private void 英文一键丢()
        {
            if (checkBox6.Checked == true)
            {
                //API.mouse_event(API.MouseEventFlag.Move, 250, 0, 0, 0);//核心分解
                鼠标_移动(1476, 185);
                Thread.Sleep(int.Parse(textBox38.Text));
                //API.鼠标_单击(1);
                API.鼠标_单击(1);

            }
            else
            {
                鼠标_移动(409, 187);
                Thread.Sleep(int.Parse(textBox38.Text));
                //API.鼠标_单击(1);
                API.鼠标_单击(1);

            }

        }
        private void 初始化搜索栏()
        {
            if (checkBox6.Checked == true)
            {
                Thread.Sleep(10);
                鼠标_移动(1287, 180);
                Thread.Sleep(10);
                API.鼠标_消息(句柄, 1, 2);

            }
            else
            {
                Thread.Sleep(10);
                鼠标_移动(144, 180);
                Thread.Sleep(10);
                API.鼠标_消息(句柄, 1, 2);

            }
            Thread.Sleep(10);

        }
        private void 循环丢弃()
        {
            if (checkBox6.Checked == true)
            {
                鼠标_移动(1290, 277);
                Thread.Sleep(10);
                API.鼠标_单击(1);
                Thread.Sleep(10);
                API.键盘_消息(句柄, 5, (byte)Keys.O);
                Thread.Sleep(10);
                鼠标_移动(1390, 277);
                Thread.Sleep(10);
                API.键盘_消息(句柄, 5, (byte)Keys.O);
            }
            else
            {
                鼠标_移动(162, 277);
                Thread.Sleep(10);
                API.鼠标_单击(1);
                Thread.Sleep(10);
                API.键盘_消息(句柄, 5, (byte)Keys.O);
                Thread.Sleep(10);
                鼠标_移动(254, 277);
                Thread.Sleep(10);
                API.键盘_消息(句柄, 5, (byte)Keys.O);
            }

        }
        private void shoucaiLoop()
        {
            if (自动收菜1.Checked == true)
            {
                API.键盘_消息(句柄, 5, (byte)Keys.F);
                程序_延时(int.Parse(收菜编辑框2.Text));
            }
            程序_延时(30);
            鼠标_移动(1427, 185);
            程序_延时(int.Parse(自动收菜编辑框.Text));
            API.鼠标_单击(1);
            程序_延时(30);
            鼠标_移动(352, 184);
            程序_延时(int.Parse(转移放下编辑框.Text));
            API.鼠标_单击(1);
            if (全部转移背包.Checked == true)
            {
                程序_延时(int.Parse(收菜编辑框1.Text));
            }
            API.键盘_消息(句柄, 5, (byte)Keys.Escape);
        }
        private void shoucaishouqu()
        {
            if (自动收菜1.Checked == true)
            {
                API.键盘_消息(句柄, 5, (byte)Keys.F);
                程序_延时(int.Parse(收菜编辑框2.Text));
            }
            程序_延时(30);
            鼠标_移动(352, 184);
            程序_延时(int.Parse(自动收菜编辑框.Text));
            API.鼠标_单击(1);
            if (全部转移背包.Checked == true)
            {
                程序_延时(int.Parse(收菜编辑框1.Text));
                API.键盘_消息(句柄, 5, (byte)Keys.Escape);
            }
        }
        private void shoucaifangxia()
        {
            if (自动收菜1.Checked == true)
            {
                API.键盘_消息(句柄, 5, (byte)Keys.F);
                程序_延时(int.Parse(收菜编辑框2.Text));
            }
            程序_延时(30);
            鼠标_移动(1427, 185);
            程序_延时(int.Parse(转移放下编辑框.Text));
            API.鼠标_单击(1);
            if (全部转移背包.Checked == true)
            {
                程序_延时(int.Parse(收菜编辑框1.Text));
                API.键盘_消息(句柄, 5, (byte)Keys.Escape);
            }
        }
        private void workOLoop()//自动o键   
        {
            while (Oloop)
            {
                Console.WriteLine("自动o键");
                API.键盘_消息(句柄, 5, (byte)Keys.O);
                Thread.Sleep(int.Parse(textBox7.Text));
            }

        }
        private void 循环禽龙()//循环禽龙   
        {
            for (int i = 0; i < int.Parse(textBox6.Text); i++)
            {
                if (自动收菜1.Checked == true)
                {
                    API.键盘_消息(句柄, 5, (byte)Keys.F);
                    程序_延时(int.Parse(收菜编辑框2.Text));
                }
                if (自动1判断 == 1)
                {
                    Console.WriteLine("跳出循环");
                    break;
                }
                鼠标_移动(1427, 185);
                程序_延时(int.Parse(收菜编辑框1.Text));
                if (自动1判断 == 1)
                {
                    Console.WriteLine("跳出循环");
                    break;
                }
                API.鼠标_单击(1);
                程序_延时(int.Parse(收菜编辑框1.Text));
                鼠标_移动(352, 184);
                if (自动1判断 == 1)
                {
                    Console.WriteLine("跳出循环");
                    break;
                }
                程序_延时(int.Parse(收菜编辑框1.Text));
                API.鼠标_单击(1);
                if (自动1判断 == 1)
                {
                    Console.WriteLine("跳出循环");
                    break;
                }
                程序_延时(int.Parse(收菜编辑框1.Text));
                if (自动1判断 == 1)
                {
                    Console.WriteLine("跳出循环");
                    break;
                }
                API.键盘_消息(句柄, 5, (byte)Keys.Escape);
                程序_延时(int.Parse(收菜编辑框1.Text));
                API.键盘_消息(句柄, 3, (byte)Keys.E);
                if (自动1判断 == 1)
                {
                    Console.WriteLine("跳出循环");
                    break;
                }
                程序_延时(int.Parse(收菜编辑框1.Text));
                if (自动1判断 == 1)
                {
                    Console.WriteLine("跳出循环");
                    break;
                }
                鼠标_移动(960, 540);
                程序_延时(200);
                API.mouse_event(API.MouseEventFlag.Move, int.Parse(textBox8.Text), int.Parse(textBox9.Text), 0, 0);//核心分解   
                程序_延时(100);
                if (自动1判断 == 1)
                {
                    Console.WriteLine("跳出循环");
                    break;
                }
                程序_延时(int.Parse(收菜编辑框2.Text));
                if (自动1判断 == 1)
                {
                    Console.WriteLine("跳出循环");
                    break;
                }
                API.键盘_消息(句柄, 4, (byte)Keys.E);
                程序_延时(int.Parse(收菜编辑框2.Text));

            }
            ALT0分解种子.Text = "ALT+0分解种子已关闭";
        }
        private void workCLoop()//自动C   
        {
            while (Cloop)
            {
                Console.WriteLine("自动C");
                API.键盘_消息(句柄, 5, (byte)Keys.C);
                Thread.Sleep(int.Parse(textBox2.Text));
            }

        }
        private void workRLoop()//自动右键   
        {
            while (Rloop)
            {
                Console.WriteLine("自动右键");
                if (checkBox4.Checked == true)
                    API.鼠标_消息(句柄, 2, 1);
                else
                {
                    API.mouse_event(API.MouseEventFlag.RightDown, 0, 0, 0, 0);
                    API.mouse_event(API.MouseEventFlag.RightUp, 0, 0, 0, 0);
                }               
                Thread.Sleep(int.Parse(textBox5.Text));
            }
        }
        private void 初始化配置常量()
        {
            if (He == 1920 && Wi == 1080)
            {
                textBox41.Text = "1705";
                textBox40.Text = "1000";
                API.写配置项(功能配置路径, "Set", "textBox41", "1705");
                API.写配置项(功能配置路径, "Set", "textBox40", "1000");
            }
            else if (He == 2048 && Wi == 1152)
            {
                textBox41.Text = "2280";
                textBox40.Text = "1350";
                API.写配置项(功能配置路径, "Set", "textBox41", "2280");
                API.写配置项(功能配置路径, "Set", "textBox40", "1350");
            }
            else if (He == 2560 && Wi == 1440)
            {
                textBox41.Text = "2274";
                textBox40.Text = "1350";
                API.写配置项(功能配置路径, "Set", "textBox41", "2280");
                API.写配置项(功能配置路径, "Set", "textBox40", "1350");
            }
            width = Screen.PrimaryScreen.Bounds.Width / 2 - buy1.Width / 2;
            height = Screen.PrimaryScreen.Bounds.Height / 2 - buy1.Height / 2;
            API.写配置项(功能配置路径, "Set", "textBox73", width.ToString());
            API.写配置项(功能配置路径, "Set", "textBox70", height.ToString());
            textBox73.Text = width.ToString();
            textBox70.Text = height.ToString();
            API.写配置项(功能配置路径, "Set", "textBox60", "20");
            API.写配置项(功能配置路径, "Set", "textBox65", "20");
            textBox60.Text = "40";
            textBox65.Text = "40";
            buy1.Visible = false;
            checkBox16.Checked = false;


            API.写配置项(功能配置路径, "Set", "60FPS", "0");
            限制60FPS.Checked = false;
            鼠标左键编辑框.SelectedIndex = 0;
            自动前进编辑框.SelectedIndex = 1;
            API.写配置项(游戏路径, "Set", "Path", "");//初始化方舟运行目录

            API.写配置项(功能配置路径, "Set", "houtai", "0");//前台模式当前
            API.写配置项(功能配置路径, "Set", "moveleft", "0");
            API.写配置项(功能配置路径, "Set", "run", "1");
            API.写配置项(功能配置路径, "Hook", "F11", "1");
            API.写配置项(功能配置路径, "Set", "checkBox6", "1");
            API.写配置项(功能配置路径, "Set", "checkBox7", "1");
            API.写配置项(功能配置路径, "Set", "checkBox21", "1");
            API.写配置项(功能配置路径, "Set", "checkBox8", "1");
            API.写配置项(功能配置路径, "Set", "checkBox9", "1");
            API.写配置项(功能配置路径, "Set", "checkBox10","1");
            API.写配置项(功能配置路径, "Set", "checkBox11", "1");
            API.写配置项(功能配置路径, "Set", "Opacity", "1");
            API.写配置项(功能配置路径, "Set", " Top", "0");
            API.写配置项(功能配置路径, "Set", "textBox69", "定时时间到了");
            textBox69.Text = "定时时间到了";
            TopMost = false;
            checkBox2.Checked = false;
            延迟时间.Text = "100";
            冰冻时间.Text = "14";
            冰冻间隔.Text = "16";
            textBox3.Text = "5";
            textBox4.Text = "5";
            textBox1.Text = "100";
            自动收菜编辑框.Text = "100";
            转移放下编辑框.Text = "100";
            收菜编辑框1.Text = "200";
            收菜编辑框2.Text = "1000";
            textBox2.Text = "1000";
            textBox5.Text = "1000";
            textBox7.Text = "10";
            textBox6.Text = "10";
            textBox8.Text = "150";
            textBox9.Text = "-150";
            textBox10.Text = "2000";
            textBox11.Text = "2500";
            血量值.Text = "11";
            textBox12.Text = "7";
            textBox13.Text = "250";
            textBox14.Text = "280";
            textBox16.Text = "350";
            textBox15.Text = "280";
            textBox18.Text = "3";
            textBox19.Text = "600";
            textBox20.Text = "40";
            API.写配置项(功能配置路径, "Set", "Nei", "100");
            API.写配置项(功能配置路径, "Set", "bingdong", "14");
            API.写配置项(功能配置路径, "Set", "jiange", "16");
            API.写配置项(功能配置路径, "Set", "textBox3", "5");
            API.写配置项(功能配置路径, "Set", "textBox4", "5");
            API.写配置项(功能配置路径, "Set", "textBox1", "100");
            API.写配置项(功能配置路径, "Set", "zidonshoucai", "100");
            API.写配置项(功能配置路径, "Set", "zhuanyifangxia", "100");
            API.写配置项(功能配置路径, "Set", "shoucaitxt", "200");
            API.写配置项(功能配置路径, "Set", "shoucaitxt2", "1000");
            API.写配置项(功能配置路径, "Set", "textBox2", "1000");
            API.写配置项(功能配置路径, "Set", "textBox5", "1000");
            API.写配置项(功能配置路径, "Set", "textBox7", "10");
            API.写配置项(功能配置路径, "Set", "textBox6", "10");
            API.写配置项(功能配置路径, "Set", "textBox8", "150");
            API.写配置项(功能配置路径, "Set", "textBox9", "-150");
            API.写配置项(功能配置路径, "Set", "textBox10", "2000");
            API.写配置项(功能配置路径, "Set", "textBox11", "2500");
            API.写配置项(功能配置路径, "Set", "hp1", "11");
            API.写配置项(功能配置路径, "Set", "textBox12", "7");
            API.写配置项(功能配置路径, "Set", "textBox13", "250");
            API.写配置项(功能配置路径, "Set", "textBox14", "280");
            API.写配置项(功能配置路径, "Set", "textBox16", "350");
            API.写配置项(功能配置路径, "Set", "textBox15", "280");
            API.写配置项(功能配置路径, "Set", "textBox18", "3");
            API.写配置项(功能配置路径, "Set", "textBox19", "600");
            API.写配置项(功能配置路径, "Set", "textBox20", "40");         
            API.写配置项(功能配置路径, "Set", "allesc", "1");
            API.写配置项(功能配置路径, "Set", "allf", "1");
            API.写配置项(功能配置路径, "Set", "HP", "1");
            API.写配置项(功能配置路径, "Set", "move", "1");
       
            API.写配置项(功能配置路径, "Set", "buyitems", "0.7");
            API.写配置项(功能配置路径, "Set", "radioButton6", "1");
            API.写配置项(功能配置路径, "Set", "radioButton4", "1");
            API.写配置项(功能配置路径, "Set", "jihuo", "1");
            API.写配置项(功能配置路径, "Set", "Fhouyanshi1", "1000");
            API.写配置项(功能配置路径, "Set", "Fhouyanshi", "1000");
            API.写配置项(功能配置路径, "Set", "severtimer", "5000");
            API.写配置项(功能配置路径, "Set", "severname", "");
            API.写配置项(功能配置路径, "Set", "severdaey", "5000");
            API.写配置项(功能配置路径, "Set", "namedaey", "25000");
            API.写配置项(功能配置路径, "Set", "downname", "");
            API.写配置项(功能配置路径, "Set", "XBOX", "");
            API.写配置项(功能配置路径, "Set", "YBOX", "");
            API.写配置项(功能配置路径, "Set", "XBOX2", "");
            API.写配置项(功能配置路径, "Set", "YBOX2", "");
            API.写配置项(功能配置路径, "Set", "namedaey1", "8000");
            API.写配置项(功能配置路径, "Set", "sever1", "");
            API.写配置项(功能配置路径, "Set", "downname2", "");
            F后延时1.Text = "1000";
            F后延时.Text = "1000";
            服务器等待延时.Text = "5000";
            服务器名称.Text = "";
            服务器搜索等待.Text = "5000";
            传送角色等待.Text = "25000";
            下载角色.Text = "";
            X编辑框1.Text = "";
            X编辑框2.Text = "";
            Y编辑框1.Text = "";
            Y编辑框2.Text = "";
            传送角色等待1.Text = "8000";
            服务器名称1.Text = "";
            下载角色2.Text = "";
            API.写配置项(功能配置路径, "Set", "textBox66", "10");
            API.写配置项(功能配置路径, "Set", "textBox67", "941");
            API.写配置项(功能配置路径, "Set", "textBox68", "766");
            textBox66.Text = "10"; 
            textBox67.Text = "941"; 
            textBox68.Text = "766";  
            API.写配置项(功能配置路径, "Set", "textBox17", "B");
            API.写配置项(功能配置路径, "Set", "textBox23", "I");
            API.写配置项(功能配置路径, "Set", "textBox27", "C");
            API.写配置项(功能配置路径, "Set", "textBox25", "O");
            API.写配置项(功能配置路径, "Set", "textBox33", "");
            API.写配置项(功能配置路径, "Set", "textBox29", "");
            API.写配置项(功能配置路径, "Set", "textBox35", "");
            API.写配置项(功能配置路径, "Set", "textBox31", "");
            textBox23.Text = "I"; textBox33.Text = "";
            textBox29.Text = ""; textBox17.Text = "B";
            textBox27.Text = "C"; textBox25.Text = "O";
            textBox35.Text = ""; textBox31.Text = "";
            API.写配置项(功能配置路径, "Set", "textBox21", "10");
            API.写配置项(功能配置路径, "Set", "textBox24", "10");
            API.写配置项(功能配置路径, "Set", "textBox26", "10");
            API.写配置项(功能配置路径, "Set", "textBox22", "10");
            API.写配置项(功能配置路径, "Set", "textBox32", "10");
            API.写配置项(功能配置路径, "Set", "textBox34", "10");
            API.写配置项(功能配置路径, "Set", "textBox28", "10");
            API.写配置项(功能配置路径, "Set", "textBox30", "10");
            API.写配置项(功能配置路径, "Set", "textBox36", "10");
            API.写配置项(功能配置路径, "Set", "textBox38", "10");
            textBox21.Text = "10"; textBox34.Text = "10";
            textBox24.Text = "10"; textBox28.Text = "10";
            textBox26.Text = "10"; textBox30.Text = "10";
            textBox22.Text = "10"; 
            textBox32.Text = "10";       
            textBox43.Text = "51";
            textBox42.Text = "115";//水的R
            textBox44.Text = "161";//水的G
            textBox46.Text = "181";//水的B
            textBox58.Text = "217";
            textBox45.Text = "100";           
            textBox47.Text = "127";            
            textBox50.Text = "193";
            textBox52.Text = "228";
            textBox48.Text = "181";
            textBox59.Text = "133";
            textBox56.Text = "0";
            textBox57.Text = "0";
            textBox55.Text = "8";
            textBox54.Text = "107";
            textBox38.Text = "500";
     
            textBox39.Text = "100";
            API.写配置项(功能配置路径, "Set", "textBox38", "500");

            API.写配置项(功能配置路径, "Set", "textBox43", "51");

            API.写配置项(功能配置路径, "Set", "textBox42", "134");
            API.写配置项(功能配置路径, "Set", "textBox58", "217");
            API.写配置项(功能配置路径, "Set", "textBox45", "100");
            API.写配置项(功能配置路径, "Set", "textBox44", "175");
            API.写配置项(功能配置路径, "Set", "textBox47", "127");
            API.写配置项(功能配置路径, "Set", "textBox46", "193");    
            API.写配置项(功能配置路径, "Set", "textBox50", "193");
            API.写配置项(功能配置路径, "Set", "textBox52", "228");       
            API.写配置项(功能配置路径, "Set", "textBox48", "181");     
            API.写配置项(功能配置路径, "Set", "textBox59", "133");
            API.写配置项(功能配置路径, "Set", "textBox56", "0");
            API.写配置项(功能配置路径, "Set", "textBox57", "0");
            API.写配置项(功能配置路径, "Set", "textBox55", "8");
            API.写配置项(功能配置路径, "Set", "textBox54", "107");
            API.写配置项(功能配置路径, "Set", "textBox61", "5");
            API.写配置项(功能配置路径, "Set", "textBox62", "8");
            API.写配置项(功能配置路径, "Set", "textBox63", "19");
            API.写配置项(功能配置路径, "Set", "textBox64", "1792");
            API.写配置项(功能配置路径, "Set", "textBox37", "1530");
            API.写配置项(功能配置路径, "Set", "textBox36", "187");
            API.写配置项(功能配置路径, "Set", "textBox39", "100");
            API.写配置项(功能配置路径, "Set", "textBox60", "20");
            API.写配置项(功能配置路径, "Set", "textBox65", "20");
            API.写配置项(功能配置路径, "Set", "textBox53", "97");
            API.写配置项(功能配置路径, "Set", "textBox51", "111");
            API.写配置项(功能配置路径, "Set", "textBox49", "171");
            textBox49.Text = "97";
            textBox51.Text = "111";
            textBox53.Text = "171";
            if (Flag != true)//关闭定时器
            {
                定时关机时钟.Stop();
                Flag = true;
                labl关机.Visible = false;
                label54.Visible = false;
                button4.Text = "启动";
                flagNum = 0;
            }
            // textBox60.Text = "5";
            //API.写配置项(功能配置路径, "Set", "textBox60", "5");
        }
        private void 初始化常量_Click(object sender, EventArgs e)
        {
            停止();
            初始化配置常量();           
            信息框("初始化成功!", "信息:", 5000);
        }
        private void 配置初始化()
        {       
            if (Directory.Exists(Environment.CurrentDirectory + "\\RM") == false)
            {
                API.目录_创建(Environment.CurrentDirectory + "\\RM");
                if (File.Exists(Environment.CurrentDirectory + "\\RM\\RuMengARKPath.ini") == false)
                {
                    File.WriteAllText(Environment.CurrentDirectory + "\\RM\\RuMengARKPath.ini", null, Encoding.Default);
                }
                if (File.Exists(Environment.CurrentDirectory + "\\RM\\RuMengARK.ini") == false)
                {
                    File.WriteAllText(Environment.CurrentDirectory + "\\RM\\RuMengARK.ini", null, Encoding.Default);
                    程序_延时(10);                    
                    初始化配置常量();
                }
               
            }
            else
            {
                if (File.Exists(Environment.CurrentDirectory + "\\RM\\RuMengARKPath.ini") == false)
                {
                    File.WriteAllText(Environment.CurrentDirectory + "\\RM\\RuMengARKPath.ini", null, Encoding.Default);
                }
                if (File.Exists(Environment.CurrentDirectory + "\\RM\\RuMengARK.ini") == false)
                {
                    File.WriteAllText(Environment.CurrentDirectory + "\\RM\\RuMengARK.ini", null, Encoding.Default);
                    程序_延时(10);                   
                    初始化配置常量();
                }
                API.写配置项(功能配置路径, "Set", "Hwd", this.Handle.ToString());
                API.写配置项(功能配置路径, "Set", "Ver", ver);
             
                if (API.读配置项(功能配置路径, "Set", "houtai") == "1")
                {
                    checkBox4.Checked = true;
                    checkBox4.Text = "后台模式";  
                }
              else
                {
                    checkBox4.Checked = false;
                    checkBox4.Text = "前台模式";
                }
                
               if (API.读配置项(功能配置路径, "Set", "60FPS") == "1")
                {
                    限制60FPS.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "checkBox16") == "1")
                {
                    checkBox16.Checked = true;                   
                }
                if (API.读配置项(功能配置路径, "Set", "Opacity") == "1")
                {
                    checkBox1.Checked = true;
                    提示透明度();
                }
                if (checkBox16.Checked == true)
                {
                    buy1.Visible = true;
                }
                else
                {
                    buy1.Visible = false;

                }          
                if (checkBox1.Checked == true)
                {
                    buy.Visible = true;
                }
                else
                {
                    buy.Visible = false;
                }
                if (API.读配置项(功能配置路径, "Set", "jihuo") == "1")
                {
                    激活窗口.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Hook", "F9") == "1")
                {
                    选择框F9.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Hook", "F10") == "1")
                {
                    选择框F10.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Hook", "F11") == "1")
                {
                    F11停止全部.Checked = true;
                }
                
                if (API.读配置项(功能配置路径, "Set", "Top") == "1")
                {
                    checkBox2.Checked = true;
                    置顶();
                }
                if (API.读配置项(功能配置路径, "Hook", "Home") == "1")
                {
                    F12显示隐藏.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "left") == "1")
                {
                    向左偏移一格.Checked = true;
                }

                if (API.读配置项(功能配置路径, "Hook", "ALTQ") == "1")
                {
                    ALTQ刷新背包.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Hook", "alts") == "1")
                {
                    自动孵化.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Hook", "altz") == "1")
                {
                    下载贡品.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Hook", "altx") == "1")
                {
                    上传贡品.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "allesc") == "1")
                {
                    全部转移背包.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "allf") == "1")
                {
                    自动收菜1.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Hook", "alt1") == "1")
                {
                    转移收取.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Hook", "alt2") == "1")
                {
                    转移放下.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Hook", "ALTC") == "1")
                {
                    ALTC自动C.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Hook", "ALTR") == "1")
                {
                    ALTR自动右键.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Hook", "ALT3") == "1")
                {
                    ALT3自动O.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Hook", "ALT0") == "1")
                {
                    ALT0分解种子.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Hook", "ALT4") == "1")
                {
                    ALT4抽血器.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "HP") == "1")
                {
                    血瓶.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "move") == "1")
                {
                    鼠标.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Hook", "ALT5") == "1")
                {
                    ALT5抽鞭子.Checked = true;

                }
                if (API.读配置项(功能配置路径, "Hook", "ALT6") == "1")
                {
                    checkBox17.Checked = true;

                }
                if (API.读配置项(功能配置路径, "Hook", "ALT9") == "1")
                {
                    ALT9传服.Checked = true;

                }
                if (API.读配置项(功能配置路径, "Set", "radioButton1") == "1")
                {
                    radioButton1.Checked = true;
                    if (radioButton1.Checked == true)
                    {
                        zhuangtai = radioButton1.Text;
                    }
                }
                if (API.读配置项(功能配置路径, "Set", "radioButton2") == "1")
                {
                    radioButton2.Checked = true;
                    if (radioButton2.Checked == true)
                    {
                        zhuangtai = radioButton2.Text;
                    }
                }
                if (API.读配置项(功能配置路径, "Set", "radioButton3") == "1")
                {
                    radioButton3.Checked = true;
                    if (radioButton3.Checked == true)
                    {
                        zhuangtai = radioButton3.Text;
                    }
                }
                if (API.读配置项(功能配置路径, "Set", "radioButton6") == "1")
                {
                    radioButton6.Checked = true;
                    if (radioButton6.Checked == true)
                    {
                        zhuangtai = radioButton6.Text;
                    }
                }
                if (API.读配置项(功能配置路径, "Set", "radioButton7") == "1")
                {
                    radioButton7.Checked = true;
                    if (radioButton7.Checked == true)
                    {
                        zhuangtai = radioButton7.Text;
                    }
                }
                if (API.读配置项(功能配置路径, "Set", "radioButton4") == "1")
                {
                    radioButton4.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "radioButton5") == "1")
                {
                    radioButton5.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "ALTZ") == "1")
                {
                    ALTZ.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "checkBox6") == "1")
                {
                    checkBox22.Text = "龙物品栏";
                    checkBox22.Checked = true;
                    checkBox6.Text = "龙物品栏";
                    checkBox6.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "checkBox7") == "1")
                {
                    checkBox7.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "checkBox8") == "1")
                {
                    checkBox8.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "checkBox9") == "1")
                {
                    checkBox9.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "checkBox10") == "1")
                {
                    checkBox10.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "checkBox11") == "1")
                {
                    checkBox11.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "checkBox12") == "1")
                {
                    checkBox12.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "checkBox13") == "1")
                {
                    checkBox13.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "checkBox14") == "1")
                {
                    checkBox14.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "checkBox15") == "1")
                {
                    checkBox15.Checked = true;
                }
            
                if (API.读配置项(功能配置路径, "Set", "checkBox19") == "1")
                {
                    checkBox19.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "checkBox18") == "1")
                {
                    checkBox18.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "checkBox20") == "1")
                {
                    checkBox20.Checked = true;
                }
                if (API.读配置项(功能配置路径, "Set", "checkBox21") == "1")
                {
                    checkBox21.Checked = true;
                }
                
               

                if (API.读配置项(功能配置路径, "Set", "buyitems") == "0.1")
                {
                    提示透明度选择框.SelectedIndex = 0;
                }
                else if (API.读配置项(功能配置路径, "Set", "buyitems") == "0.2")
                {
                    提示透明度选择框.SelectedIndex = 1;
                }
                else if (API.读配置项(功能配置路径, "Set", "buyitems") == "0.3")
                {
                    提示透明度选择框.SelectedIndex = 2;
                }
                else if (API.读配置项(功能配置路径, "Set", "buyitems") == "0.4")
                {
                    提示透明度选择框.SelectedIndex = 3;
                }
                else if (API.读配置项(功能配置路径, "Set", "buyitems") == "0.5")
                {
                    提示透明度选择框.SelectedIndex = 4;
                }
                else if (API.读配置项(功能配置路径, "Set", "buyitems") == "0.6")
                {
                    提示透明度选择框.SelectedIndex = 5;
                }
                else if (API.读配置项(功能配置路径, "Set", "buyitems") == "0.7")
                {
                    提示透明度选择框.SelectedIndex = 6;
                }
                else if (API.读配置项(功能配置路径, "Set", "buyitems") == "0.8")
                {
                    提示透明度选择框.SelectedIndex = 7;
                }
                else if (API.读配置项(功能配置路径, "Set", "buyitems") == "0.9")
                {
                    提示透明度选择框.SelectedIndex = 8;
                }
                else if (API.读配置项(功能配置路径, "Set", "buyitems") == "1")
                {
                    提示透明度选择框.SelectedIndex = 9;
                }
                textBox73.Text = API.读配置项(功能配置路径, "Set", "textBox73");
                textBox70.Text = API.读配置项(功能配置路径, "Set", "textBox70");
                buy1.Location = new Point(int.Parse(textBox73.Text), int.Parse(textBox70.Text));

                textBox69.Text = API.读配置项(功能配置路径, "Set", "textBox69");
                textBox66.Text = API.读配置项(功能配置路径, "Set", "textBox66");
                textBox67.Text = API.读配置项(功能配置路径, "Set", "textBox67");
                textBox68.Text = API.读配置项(功能配置路径, "Set", "textBox68");
                
                延迟时间.Text = API.读配置项(功能配置路径, "Set", "Nei");
                冰冻时间.Text = API.读配置项(功能配置路径, "Set", "bingdong");
                冰冻间隔.Text = API.读配置项(功能配置路径, "Set", "jiange");
                textBox3.Text = API.读配置项(功能配置路径, "Set", "textBox3");
                textBox4.Text = API.读配置项(功能配置路径, "Set", "textBox4");
                textBox1.Text = API.读配置项(功能配置路径, "Set", "textBox1");
                自动收菜编辑框.Text = API.读配置项(功能配置路径, "Set", "zidonshoucai");
                转移放下编辑框.Text = API.读配置项(功能配置路径, "Set", "zhuanyifangxia");
                收菜编辑框1.Text = API.读配置项(功能配置路径, "Set", "shoucaitxt");
                收菜编辑框2.Text = API.读配置项(功能配置路径, "Set", "shoucaitxt2");
                textBox2.Text = API.读配置项(功能配置路径, "Set", "textBox2");
                textBox5.Text = API.读配置项(功能配置路径, "Set", "textBox5");
                textBox7.Text = API.读配置项(功能配置路径, "Set", "textBox7");
                textBox6.Text = API.读配置项(功能配置路径, "Set", "textBox6");
                textBox8.Text = API.读配置项(功能配置路径, "Set", "textBox8");
                textBox9.Text = API.读配置项(功能配置路径, "Set", "textBox9");
                textBox10.Text = API.读配置项(功能配置路径, "Set", "textBox10");
                textBox11.Text = API.读配置项(功能配置路径, "Set", "textBox11");
                血量值.Text = API.读配置项(功能配置路径, "Set", "hp1");
                textBox12.Text = API.读配置项(功能配置路径, "Set", "textBox12");
                textBox13.Text = API.读配置项(功能配置路径, "Set", "textBox13");
                textBox14.Text = API.读配置项(功能配置路径, "Set", "textBox14");
                textBox16.Text = API.读配置项(功能配置路径, "Set", "textBox16");
                textBox15.Text = API.读配置项(功能配置路径, "Set", "textBox15");
                textBox18.Text = API.读配置项(功能配置路径, "Set", "textBox18");
                textBox19.Text = API.读配置项(功能配置路径, "Set", "textBox19");
                textBox20.Text = API.读配置项(功能配置路径, "Set", "textBox20");

                F后延时1.Text = API.读配置项(功能配置路径, "Set", "Fhouyanshi1");
                F后延时.Text = API.读配置项(功能配置路径, "Set", "Fhouyanshi");
                服务器等待延时.Text = API.读配置项(功能配置路径, "Set", "severtimer");
                服务器名称.Text = API.读配置项(功能配置路径, "Set", "severname");
                服务器搜索等待.Text = API.读配置项(功能配置路径, "Set", "severdaey");
                传送角色等待.Text = API.读配置项(功能配置路径, "Set", "namedaey");
                下载角色.Text = API.读配置项(功能配置路径, "Set", "downname");
                X编辑框1.Text = API.读配置项(功能配置路径, "Set", "XBOX");
                X编辑框2.Text = API.读配置项(功能配置路径, "Set", "XBOX2");
                Y编辑框1.Text = API.读配置项(功能配置路径, "Set", "YBOX");
                Y编辑框2.Text = API.读配置项(功能配置路径, "Set", "YBOX2");
                传送角色等待1.Text = API.读配置项(功能配置路径, "Set", "namedaey1");
                服务器名称1.Text = API.读配置项(功能配置路径, "Set", "sever1");
                下载角色2.Text = API.读配置项(功能配置路径, "Set", "downname2");
                textBox23.Text = API.读配置项(功能配置路径, "Set", "textBox23");
                textBox17.Text = API.读配置项(功能配置路径, "Set", "textBox17");
                textBox27.Text = API.读配置项(功能配置路径, "Set", "textBox27");
                textBox25.Text = API.读配置项(功能配置路径, "Set", "textBox25");
                textBox35.Text = API.读配置项(功能配置路径, "Set", "textBox35");
                textBox33.Text = API.读配置项(功能配置路径, "Set", "textBox33");
                textBox29.Text = API.读配置项(功能配置路径, "Set", "textBox29");
                textBox31.Text = API.读配置项(功能配置路径, "Set", "textBox31");

                textBox21.Text = API.读配置项(功能配置路径, "Set", "textBox21");
                textBox24.Text = API.读配置项(功能配置路径, "Set", "textBox24");
                textBox26.Text = API.读配置项(功能配置路径, "Set", "textBox26");
                textBox22.Text = API.读配置项(功能配置路径, "Set", "textBox22");
                textBox32.Text = API.读配置项(功能配置路径, "Set", "textBox32");
                textBox34.Text = API.读配置项(功能配置路径, "Set", "textBox34");
                textBox28.Text = API.读配置项(功能配置路径, "Set", "textBo28");
                textBox30.Text = API.读配置项(功能配置路径, "Set", "textBox30");


                textBox41.Text = API.读配置项(功能配置路径, "Set", "textBox41");
                textBox43.Text = API.读配置项(功能配置路径, "Set", "textBox43");
                textBox40.Text = API.读配置项(功能配置路径, "Set", "textBox40");
                textBox42.Text = API.读配置项(功能配置路径, "Set", "textBox42");
                textBox58.Text = API.读配置项(功能配置路径, "Set", "textBox58");
                textBox45.Text = API.读配置项(功能配置路径, "Set", "textBox45");
                textBox44.Text = API.读配置项(功能配置路径, "Set", "textBox44");
                textBox47.Text = API.读配置项(功能配置路径, "Set", "textBox47");
                textBox46.Text = API.读配置项(功能配置路径, "Set", "textBox46");
                textBox53.Text = API.读配置项(功能配置路径, "Set", "textBox53");
                textBox50.Text = API.读配置项(功能配置路径, "Set", "textBox50");
                textBox52.Text = API.读配置项(功能配置路径, "Set", "textBox52");
                textBox51.Text = API.读配置项(功能配置路径, "Set", "textBox51");
                textBox48.Text = API.读配置项(功能配置路径, "Set", "textBox48");
                textBox49.Text = API.读配置项(功能配置路径, "Set", "textBox49");
                textBox59.Text = API.读配置项(功能配置路径, "Set", "textBox59");
                textBox56.Text = API.读配置项(功能配置路径, "Set", "textBox56");
                textBox57.Text = API.读配置项(功能配置路径, "Set", "textBox57");
                textBox55.Text = API.读配置项(功能配置路径, "Set", "textBox55");
                textBox54.Text = API.读配置项(功能配置路径, "Set", "textBox54");
               // textBox60.Text = API.读配置项(功能配置路径, "Set", "textBox60");
                textBox61.Text = API.读配置项(功能配置路径, "Set", "textBox61");
                textBox62.Text = API.读配置项(功能配置路径, "Set", "textBox62");
                textBox63.Text = API.读配置项(功能配置路径, "Set", "textBox63");
                textBox64.Text = API.读配置项(功能配置路径, "Set", "textBox64");
                textBox37.Text = API.读配置项(功能配置路径, "Set", "textBox37");
                textBox36.Text = API.读配置项(功能配置路径, "Set", "textBox36");
                textBox38.Text = API.读配置项(功能配置路径, "Set", "textBox38");
                textBox39.Text = API.读配置项(功能配置路径, "Set", "textBox39");
                textBox60.Text= API.读配置项(功能配置路径, "Set", "textBox60");
                textBox65.Text= API.读配置项(功能配置路径, "Set", "textBox65");
                
                
            }

        }
        private void 初始化()
        {
            if (File.Exists("update.exe") == false)
            {
                API.信息框("update.exe不存在,请在官网下载更新!", "信息:", 5000);
                Process.Start("http://www.bianshengruanjian.com");
            }
            label96.Text = null;
            label95.Text = null;
            //CPU序列号.Text = t.CreateCode().Substring(0, 25);
            label82.Text = "方舟ID:" + 句柄;
            label63.Text = He.ToString() + "x" + Wi.ToString();
            label94.Text = label63.Text;
 
            k_hook.KeyDownEvent += new KeyEventHandler(inthook);//添加键盘事件
            k_hook.Start();//安装键盘钩子    
            Console.WriteLine("句柄" + 句柄);
            label15.Text = "当前版本: " + ver;
            label128.Text = label15.Text;
            try
            {
              
                label127.Text = API.网页_访问("");//最新更新内容
                label16.Text = "最新版本: " + severnumber;
               
                if (ver != severnumber)
                {
                    更新.Visible = true;
                }
            }
            catch
            {
                label127.Text = null;
                label16.Text = "无服务器连接" ;
                更新.Visible = true;
            }
            label31.Text = "计时喝水≈:" + int.Parse(textBox10.Text) / 60 + "分钟";
            label32.Text = "计时吃饭≈:" + int.Parse(textBox11.Text) / 60 + "分钟";
            comboBox6.Text = "1";
            comboBox5.Text = "0";
            comboBox4.Text = "0";
            comboBox1.Text = DateTime.Now.Hour.ToString();
            comboBox2.Text = DateTime.Now.Minute.ToString();
            comboBox3.Text = DateTime.Now.Second.ToString();
        }
        private void 挤服工具()//挤服工具   
        {
            while (jifugongju)
            {
                鼠标_移动(int.Parse(textBox68.Text), int.Parse(textBox67.Text));
                Thread.Sleep(50);
                API.鼠标_单击(1);
                Thread.Sleep(int.Parse(textBox66.Text) * 1000);
                //鼠标_移动(958, 616);
                //Thread.Sleep(50);
                //API.鼠标_单击(1);
                //Thread.Sleep(int.Parse(textBox66.Text) * 1000);//延迟挤服工具时间
            }
        }
        private void workXLoop()//X键循环   
        {
            while (Xloop)
            {
                Console.WriteLine("X上传贡品");
                if (向左偏移一格.Checked == true)
                {
                    鼠标_移动(150, 280);
                }
                else
                {
                    鼠标_移动(250, 280);
                }
                API.鼠标_单击(1);
                Thread.Sleep(50);
                API.键盘_消息(句柄, 5, (byte)Keys.T);
                Thread.Sleep(int.Parse(textBox3.Text) * 1000);
            }
        }
        private void workZLoop()//Z键循环  
        {
            while (Zloop)
            {
                Console.WriteLine("Z下载贡品");
                鼠标_移动(1291, 277);
                API.鼠标_单击(1);
                Thread.Sleep(100);
                API.键盘_消息(句柄, 5, (byte)Keys.T);
                Thread.Sleep(int.Parse(textBox4.Text) * 1000);
            }
        }
        private void workFLoop()//F键循环    
        {
            while (Floop)
            {
                Console.WriteLine("F按下");
                API.键盘_消息(句柄, 3, (byte)Keys.F);
                Thread.Sleep(100);
                API.键盘_消息(句柄, 4, (byte)Keys.F);
                // Thread.Sleep(100);
            }
        }
        private void workELoop()//E键循环
        {
            while (Eloop)
            {
                Console.WriteLine("E按下");
                API.键盘_消息(句柄, 3, (byte)Keys.E);
                Thread.Sleep(100);
                API.键盘_消息(句柄, 4, (byte)Keys.E);
            }

        }
        private void workSloop()//自动孵化循环
        {
            while (Sloop)
            {
                //冰冻状态.Text = "自动冰冻中,无法释放按键";
                //API.鼠标_消息(句柄, 2, 3);
                if (checkBox4.Checked==true)
                    API.PostMessageA(句柄, 516, 2, 0);
                else API.mouse_event(API.MouseEventFlag.RightDown, 0, 0, 0, 0);
                API.鼠标_消息(句柄, 1, 1);
                Console.WriteLine("自动冻龙已开启");
                Thread.Sleep(int.Parse(冰冻时间.Text) * 1000);
                //冰冻状态.Text = "冰冻结束,可以关闭按键";            
                if (checkBox4.Checked == true)
                    API.PostMessageA(句柄, 517, 2, 0);
                else API.mouse_event(API.MouseEventFlag.RightUp, 0, 0, 0, 0);
                Thread.Sleep(int.Parse(冰冻间隔.Text) * 1000);
                Console.WriteLine("自动冻龙已关闭");
            }
            
        }
        private void workLeftMouseClickLoop()//鼠标左键单击循环
        {
            while (leftloop)
            {
                Console.WriteLine("后台鼠标按钮单机");
                if (checkBox4.Checked == true)
                    API.PostMessageA(句柄, 513, 1, 0);//左键按下
                else API.mouse_event(API.MouseEventFlag.LeftDown, 0, 0, 0, 0);

                Thread.Sleep(int.Parse(延迟时间.Text));

                if (checkBox4.Checked == true)
                    API.PostMessageA(句柄, 514, 0, 0);//左键放开
                else API.mouse_event(API.MouseEventFlag.LeftUp, 0, 0, 0, 0);
  
            }
        }
     
        private void 开始抽血器()
        {
            if (激活窗口.Checked == true)
            {
                API.窗口_激活显示(句柄);
            }
            Thread.Sleep(200);
            while (HPloop)
            {
                // Console.WriteLine(血量时钟.Interval + "正在抽血");
                if (鼠标.Checked == true)
                {
                    if (向左偏移一格.Checked == true)
                    {
                        鼠标_移动(int.Parse(textBox13.Text) - 100, int.Parse(textBox14.Text));
                    }
                    else
                    {
                        鼠标_移动(int.Parse(textBox13.Text), int.Parse(textBox14.Text));
                    }
                    Thread.Sleep(50);
                    if (HPloop == false)
                    {
                        Console.WriteLine("跳出循环");
                        break;
                    }
                    API.鼠标_单击(1);
                    Console.WriteLine("鼠标移动251, 284");
                }
                for (Time = int.Parse(血量值.Text); Time > 0; Time--)//抽血
                {
                    血量标签7.Text = "抽血还剩" + Time + "袋";
                    if (HPloop == false)
                    {
                        Console.WriteLine("跳出循环");
                        break;
                    }
                    Thread.Sleep(5500);
                    if (HPloop == false)
                    {
                        Console.WriteLine("跳出循环");
                        break;
                    }
                    API.键盘_消息(句柄, 5, (byte)Keys.E);
                    if (HPloop == false)
                    {
                        Console.WriteLine("跳出循环");
                        break;
                    }
                }
                血量标签7.Text = "抽血还剩" + Time + "袋";
                if (鼠标.Checked == true)
                {
                    if (向左偏移一格.Checked == true)
                    {
                        鼠标_移动(int.Parse(textBox16.Text) - 100, int.Parse(textBox15.Text));
                    }
                    else
                    {
                        鼠标_移动(int.Parse(textBox16.Text), int.Parse(textBox15.Text));
                    }

                    Thread.Sleep(50);
                    if (HPloop == false)
                    {
                        Console.WriteLine("跳出循环");
                        break;
                    }
                    API.鼠标_单击(1);
                    Console.WriteLine("鼠标移动350, 280");
                }
                Thread.Sleep(2000);
                for (Time = int.Parse(textBox12.Text); Time > 0; Time--)//加血
                {
                    血量标签7.Text = "血瓶还剩" + Time + "袋";
                    if (HPloop == false)
                    {
                        Console.WriteLine("跳出循环");
                        break;
                    }
                    Thread.Sleep(60);
                    if (HPloop == false)
                    {
                        Console.WriteLine("跳出循环");
                        break;
                    }
                    API.键盘_消息(句柄, 5, (byte)Keys.E);
                    if (HPloop == false)
                    {
                        Console.WriteLine("跳出循环");
                        break;
                    }
                }
                血量标签7.Text = "血瓶还剩" + Time + "袋";
            }
            血量标签7.Text = null;

        }
        private void 鼠标左键编辑框SelectedIndexChanged(object sender, EventArgs e)
        {
            if (鼠标左键编辑框.SelectedIndex == 自动前进编辑框.SelectedIndex)
            {
                鼠标左键编辑框.SelectedIndex = 0;              
                自动前进编辑框.SelectedIndex = 1;
                API.写配置项(功能配置路径, "Set", "moveleft", "0");
                API.写配置项(功能配置路径, "Set", "run", "1");
                信息框("连点器和自动前进按钮不能重复", "信息:", 5000);
            }
            else
            {

                if (鼠标左键编辑框.SelectedIndex == 0)
                {
                    API.写配置项(功能配置路径, "Set", "moveleft", "0");
                }
                else if (鼠标左键编辑框.SelectedIndex == 1)
                {
                    API.写配置项(功能配置路径, "Set", "moveleft", "1");
                }
                else if (鼠标左键编辑框.SelectedIndex == 2)
                {
                    API.写配置项(功能配置路径, "Set", "moveleft", "2");
                }
                else if (鼠标左键编辑框.SelectedIndex == 3)
                {
                    API.写配置项(功能配置路径, "Set", "moveleft", "3");
                }
                else if (鼠标左键编辑框.SelectedIndex == 4)
                {
                    API.写配置项(功能配置路径, "Set", "moveleft", "4");
                }
                else if (鼠标左键编辑框.SelectedIndex == 5)
                {
                    API.写配置项(功能配置路径, "Set", "moveleft", "5");
                }
                else if (鼠标左键编辑框.SelectedIndex == 6)
                {
                    API.写配置项(功能配置路径, "Set", "moveleft", "6");
                }
                else if (鼠标左键编辑框.SelectedIndex == 7)
                {
                    API.写配置项(功能配置路径, "Set", "moveleft", "7");
                }


                if (自动前进编辑框.SelectedIndex == 0)
                {
                    API.写配置项(功能配置路径, "Set", "run", "0");
                }
                else if (自动前进编辑框.SelectedIndex == 1)
                {
                    API.写配置项(功能配置路径, "Set", "run", "1");
                }
                else if (自动前进编辑框.SelectedIndex == 2)
                {
                    API.写配置项(功能配置路径, "Set", "run", "2");
                }
                else if (自动前进编辑框.SelectedIndex == 3)
                {
                    API.写配置项(功能配置路径, "Set", "run", "3");
                }
                else if (自动前进编辑框.SelectedIndex == 4)
                {
                    API.写配置项(功能配置路径, "Set", "run", "4");
                }
                else if (自动前进编辑框.SelectedIndex == 5)
                {
                    API.写配置项(功能配置路径, "Set", "run", "5");
                }
                else if (自动前进编辑框.SelectedIndex == 6)
                {
                    API.写配置项(功能配置路径, "Set", "run", "6");
                }
                else if (自动前进编辑框.SelectedIndex == 7)
                {
                    API.写配置项(功能配置路径, "Set", "run", "7");
                }

            }
            


        }
        private void 前进()
        {
            if (句柄 != (IntPtr)0)
            {
                if (延迟时间.Text != "")
                { 
                    if (wloop == true)//键盘前进单击循环
                    {
                        Console.WriteLine("前进");
                        wloop = false;
                        API.键盘_消息(句柄, 3, (byte)Keys.W);
                        //键盘_单机((byte)Keys.W, 2);
                        if (checkBox1.Checked == true)
                        {
                            buy.label1.Text = 自动前进编辑框.SelectedItem.ToString() + "前进开启";
                        }
                        label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进开启";
                    }
                    else
                    {
                        Console.WriteLine("前进停止");
                        wloop = true;
                        //键盘_单机((byte)Keys.W, 0);
                        API.键盘_消息(句柄, 4, (byte)Keys.W);
                        if (checkBox1.Checked == true)
                        {
                            buy.label1.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";
                        }
                        label7.Text = 自动前进编辑框.SelectedItem.ToString() + "前进停止";
                    }
                }
                else
                {
                    信息框("延迟时间不能为空", "信息:", 5000);

                }
            }
        }
        private void 自动前进SelectedIndexChanged(object sender, EventArgs e)
        {
            if (鼠标左键编辑框.SelectedIndex == 自动前进编辑框.SelectedIndex)
            {
                鼠标左键编辑框.SelectedIndex = 0;
                自动前进编辑框.SelectedIndex = 1;
                信息框("连点器和自动前进按钮不能重复", "信息:", 5000);

            }
        }
        private void 时钟检测软件_Tick(object sender, EventArgs e)
        {
            label51.Text = DateTime.Now.ToString();//显示系统时间
            if (API.进程_是否存在("ShooterGame") == true)
            {
                if (API.窗口_是否可见(句柄) == true)
                {
                    方舟未运行.Text = "方舟已运行";
                    label17.Text = "入梦方舟小工具: " + 方舟未运行.Text+ " (" + label63.Text+ ")";
                }
                else
                {
                    方舟未运行.Text = "方舟已运行(隐藏)";
                    label17.Text = "入梦方舟小工具: " + 方舟未运行.Text + " (" + label63.Text + ")";
                } 
                
                if (句柄 == (IntPtr)0)
                {
                    
                    句柄 = API.FindWindowA("UnrealWindow", "ARK: Survival Evolved");
                    Console.WriteLine("句柄" + 句柄);
                }

            }
            else
            {
                方舟未运行.Text = "方舟未运行";
                label17.Text = "入梦方舟小工具: " + 方舟未运行.Text;
                句柄 = (IntPtr)0;
                停止全部();
                if (checkBox1.Checked == true)
                {
                    buy.label1.Text = "初始化......";
                }
            }
        }

        private int 方舟崩溃1()
        {
            int hwd = API.窗口_取句柄(0, 0, "##32770", "The UE4-ShooterGame Game has crashed and will close");
            if (hwd == 0)
            {
                return 0;
            }
            return hwd;
        }
        private void 关闭_Click(object sender, EventArgs e)
        {
            Formclose();
        }
        private void Formclose()
        {
            if (notifyIcon1.Visible == true)
            {
                notifyIcon1.Visible = false;
            }
            Environment.Exit(0);
        }
        private void trackBar1_Scroll(object sender, EventArgs e)//透明度
        {
            this.Opacity = (float)trackBar1.Value / 100;
            透明度.Text = "透明度:" + trackBar1.Value.ToString();
        }
        private void 显示()
        {
            if (this.Visible == true)
            {
                this.Visible = false;
                if (notifyIcon1.Visible == false)
                {
                    notifyIcon1.Visible = true;//托盘按钮是否可见
                    notifyIcon1.Text = this.Text;
                }
            }
            else
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                //notifyIcon1.Visible = false;//托盘按钮是否可见
            }
        }
        int 自动1判断 = 1;
        private void textBox69_TextChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "textBox69", textBox69.Text);
        }
        private void 自动孵化_CheckedChanged(object sender, EventArgs e)
        {
            if (自动孵化.Checked == true)
            {
                API.写配置项(功能配置路径, "Hook", "alts", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", "alts", "0");
            }
        }
        private void 上传贡品_CheckedChanged(object sender, EventArgs e)
        {
            if (上传贡品.Checked == true)
            {
                API.写配置项(功能配置路径, "Hook", " altx", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", " altx", "0");
            }
        }
        private void 下载贡品_CheckedChanged(object sender, EventArgs e)
        {
            if (下载贡品.Checked == true)
            {
                API.写配置项(功能配置路径, "Hook", " altz", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", " altz", "0");
            }

        }
        private void 转移收取_CheckedChanged(object sender, EventArgs e)
        {
            if (转移收取.Checked == true)
            {
                API.写配置项(功能配置路径, "Hook", "alt1", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", "alt1", "0");
            }
        }
        private void 转移放下_CheckedChanged(object sender, EventArgs e)
        {
            if (转移放下.Checked == true)
            {
                API.写配置项(功能配置路径, "Hook", "alt2", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", "alt2", "0");
            }
        }
        private void ALTQ刷新背包_CheckedChanged(object sender, EventArgs e)
        {
            if (ALTQ刷新背包.Checked == true)
            {
                if (全部转移背包.Checked == false)
                {
                    全部转移背包.Checked = true;
                    API.写配置项(功能配置路径, "Set", "allesc", "1");
                }
                if (自动收菜1.Checked == false)
                {
                    自动收菜1.Checked = true;
                    API.写配置项(功能配置路径, "Set", "allf", "1");
                }
                API.写配置项(功能配置路径, "Hook", " ALTQ", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", " ALTQ", "0");
            }
        }
        private void ALTC自动C_CheckedChanged(object sender, EventArgs e)
        {
            if (ALTC自动C.Checked == true)
            {
                API.写配置项(功能配置路径, "Hook", "ALTC", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", "ALTC", "0");
            }
        }
        private void 选择框F9_CheckedChanged(object sender, EventArgs e)
        {
            if (选择框F9.Checked == true)
            {
                API.写配置项(功能配置路径, "Hook", "F9", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", "F9", "0");
            }
        }
        private void 选择框F10_CheckedChanged(object sender, EventArgs e)
        {
            if (选择框F10.Checked == true)
            {
                API.写配置项(功能配置路径, "Hook", "F10", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", "F10", "0");
            }
        }
        private void ALTR自动右键_CheckedChanged(object sender, EventArgs e)
        {
            if (ALTR自动右键.Checked == true)
            {
                API.写配置项(功能配置路径, "Hook", "ALTR", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", "ALTR", "0");
            }
        }
        private void ALT0分解种子_CheckedChanged(object sender, EventArgs e)
        {
            if (ALT0分解种子.Checked == true)
            {
                API.写配置项(功能配置路径, "Hook", "ALT0", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", "ALT0", "0");
            }

        }



        private void ALT3自动O_CheckedChanged(object sender, EventArgs e)
        {
            if (ALT3自动O.Checked == true)
            {
                API.写配置项(功能配置路径, "Hook", "ALT3", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", "ALT3", "0");
            }
        }

        private void 计时喝水_CheckedChanged(object sender, EventArgs e)
        {
            if (方舟未运行.Text == "方舟已运行")
            {
                if (激活窗口.Checked == true)
                {
                    API.窗口_激活显示(句柄);
                }
                if (识图自动吃喝.Checked == true)
                {
                    识图自动时钟.Stop();
                    识图自动吃喝.Checked = false;
                }
                if (计时喝水.Checked == true)
                {
                    计时喝水时钟.Interval = int.Parse(textBox10.Text) * 1000;
                    计时喝水时钟.Start();
                }
                else
                {
                    计时喝水时钟.Stop();
                    计时喝水.Checked = false;
                }
            }
            else
            {
                计时喝水时钟.Stop();
                计时喝水.Checked = false;
                if (方舟未运行.Text == "方舟已运行(隐藏)")
                {
                    if (激活窗口.Checked == true)
                    {
                        API.窗口_激活显示(句柄);
                    }
                }
                else
                {
                    // 信息框("方舟未运行", "信息:", 5000);
                }

            }
        }

        private void 计时吃饭_CheckedChanged(object sender, EventArgs e)
        {
            if (方舟未运行.Text == "方舟已运行")
            {
                if (激活窗口.Checked == true)
                {
                    API.窗口_激活显示(句柄);
                }
                if (识图自动吃喝.Checked == true)
                {
                    识图自动时钟.Stop();
                    识图自动吃喝.Checked = false;
                }
                if (计时吃饭.Checked == true)
                {
                    计时吃东西时钟.Interval = int.Parse(textBox11.Text) * 1000;
                    计时吃东西时钟.Start();
                }
                else
                {
                    计时吃东西时钟.Stop();
                    计时吃饭.Checked = false;
                }
            }
            else
            {
                计时吃东西时钟.Stop();
                计时吃饭.Checked = false;
                if (方舟未运行.Text == "方舟已运行(隐藏)")
                {
                    if (激活窗口.Checked == true)
                    {
                        API.窗口_激活显示(句柄);
                    }
                }
                else
                {
                    //信息框("方舟未运行", "信息:", 5000);
                }


            }

        }
        private void 计时喝水时钟_Tick(object sender, EventArgs e)
        {
            if (句柄 == (IntPtr)0)
            {
                计时喝水时钟.Stop();
                计时喝水.Checked = false;
            }
            else
            {
               
                Console.WriteLine("点击8喝水");
                API.键盘_消息(句柄, 5, (byte)Keys.D8);
                //if (checkBox1.Checked == true)
                //{
                //   // buy.label1.Text = "单机按键8喝水";
                //    //buy.label1.Text = "抽血器已开启";
                //}
            }

        }

        private void 计时吃东西时钟_Tick(object sender, EventArgs e)
        {
            if (句柄 == (IntPtr)0)
            {
                计时吃东西时钟.Stop();
                计时吃饭.Checked = false;
            }
            else
            {
                
                Console.WriteLine("点击9吃东西");
                //if (checkBox1.Checked == true)
                //{
                //    //buy.label1.Text = "单机按键9吃饭";
                //    //buy.label1.Text = "抽血器已开启";
                //}
                API.键盘_消息(句柄, 5, (byte)Keys.D9);

            }

        }
        int Time;
        private void ALT4抽血器_CheckedChanged(object sender, EventArgs e)
        {
            if (ALT4抽血器.Checked == true)
            {
                if (血瓶.Checked == false)
                {
                    血瓶.Checked = true;
                    API.写配置项(功能配置路径, "Set", "HP", "1");
                }
                if (鼠标.Checked == false)
                {
                    鼠标.Checked = true;
                    API.写配置项(功能配置路径, "Set", "move", "1");
                }
                API.写配置项(功能配置路径, "Hook", "ALT4", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", "ALT4", "0");
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            置顶();
        }
        private void 激活窗口_CheckedChanged(object sender, EventArgs e)
        {
            
            if (激活窗口.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "jihuo", "1");
            }
            else API.写配置项(功能配置路径, "Set", "jihuo", "0");
        }
        private void 置顶()
        {
            if (checkBox2.Checked == true)
            {
                TopMost = true;
                API.写配置项(功能配置路径, "Set", " Top", "1");
            }
            else
            {
                TopMost = false;
                API.写配置项(功能配置路径, "Set", " Top", "0");
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            提示透明度();
        }
        private void 提示透明度()
        {
            if (checkBox1.Checked == true)
            {
                buy.Opacity = Convert.ToDouble(API.读配置项(功能配置路径, "Set", "buyitems"));
                buy.Visible = true;
                API.写配置项(功能配置路径, "Set", "Opacity", "1");
            }
            else
            {
                buy.Visible = false;
                API.写配置项(功能配置路径, "Set", "Opacity", "0");
            }
        }
        private void 白框检测时钟_Tick(object sender, EventArgs e)
        {
            if (句柄 == (IntPtr)0)
            {
                checkBox3.Checked = false;
                白框检测时钟.Stop();
            }
            else
            {
                if (方舟崩溃1() != 0)
                {
                    白框检测时钟.Stop();
                    //进程_结束("ShooterGame.exe");
                    for (int i = 0; i < int.Parse(textBox18.Text); i++)
                    {
                        程序_延时(3000);
                        PlayAsync("游戏崩溃了");
                    }
                    //信息框("方舟白框,游戏崩溃","信息",5000);
                }
            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                白框检测时钟.Interval = 2000;
                白框检测时钟.Start();
                API.写配置项(功能配置路径, "Set", "timer", "1");
            }
            else
            {
                白框检测时钟.Stop();
                API.写配置项(功能配置路径, "Set", "timer", "0");
            }
        }
        private void f11停止全部提示()
        {
            if (checkBox1.Checked == true)
            {
                buy.label1.Text = "F11停止全部";
            }
        }


        private void F11停止全部_CheckedChanged(object sender, EventArgs e)
        {
            if (F11停止全部.Checked == true)
            {
                API.写配置项(功能配置路径, "Hook", "F11", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", "F11", "0");
            }

        }

        private void F12显示隐藏_CheckedChanged(object sender, EventArgs e)
        {
            if (F12显示隐藏.Checked == true)
            {
                API.写配置项(功能配置路径, "Hook", "Home", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", "Home", "0");
            }
        }



        private void 向左偏移一格_CheckedChanged(object sender, EventArgs e)
        {
            if (向左偏移一格.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "left", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "left", "0");
            }
        }

        private void 全部转移背包_CheckedChanged(object sender, EventArgs e)
        {
            if (全部转移背包.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "allesc", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "allesc", "0");
            }

        }

        private void 自动收菜1_CheckedChanged(object sender, EventArgs e)
        {
            if (自动收菜1.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "allf", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "allf", "0");
            }

        }

        private void 血瓶_CheckedChanged(object sender, EventArgs e)
        {
            if (血瓶.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "HP", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "HP", "0");
            }

        }

        private void 鼠标_CheckedChanged(object sender, EventArgs e)
        {
            if (鼠标.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "move", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "move", "0");
            }
        }

        private void 延迟时间_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(延迟时间.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                延迟时间.Text = "100";
                return;
            }
            API.写配置项(功能配置路径, "Set", "Nei", 延迟时间.Text);
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            显示();
        }

        private void 冰冻时间_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(冰冻时间.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                冰冻时间.Text = "14";
                return;
            }
            API.写配置项(功能配置路径, "Set", "bingdong", 冰冻时间.Text);
        }

        private void 冰冻间隔_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(冰冻间隔.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                冰冻间隔.Text = "16";
                return;
            }
            API.写配置项(功能配置路径, "Set", "jiange", 冰冻间隔.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox3.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox3.Text = "5";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox3", textBox3.Text);

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox4.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox4.Text = "5";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox4", textBox4.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox1.Text = "100";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox1", textBox1.Text);
        }

        private void 自动收菜编辑框_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(自动收菜编辑框.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                自动收菜编辑框.Text = "100";
                return;
            }
            API.写配置项(功能配置路径, "Set", "zidonshoucai", 自动收菜编辑框.Text);
        }

        private void 转移放下编辑框_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(转移放下编辑框.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                转移放下编辑框.Text = "100";
                return;
            }
            API.写配置项(功能配置路径, "Set", "zhuanyifangxia", 转移放下编辑框.Text);

        }

        private void 收菜编辑框1_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(收菜编辑框1.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                收菜编辑框1.Text = "200";
                return;
            }
            API.写配置项(功能配置路径, "Set", "shoucaitxt", 收菜编辑框1.Text);

        }

        private void 收菜编辑框2_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(收菜编辑框2.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                收菜编辑框2.Text = "1000";
                return;
            }
            API.写配置项(功能配置路径, "Set", "shoucaitxt2", 收菜编辑框2.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox2.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox2.Text = "1000";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox2", textBox2.Text);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox5.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox5.Text = "1000";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox5", textBox5.Text);

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox7.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox7.Text = "10";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox7", textBox7.Text);

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox6.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox6.Text = "10";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox6", textBox6.Text);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox8.Text, out _))
            {
                信息框("只能输入正负阿拉伯数字！", "信息:", 5000);
                textBox8.Text = "150";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox8", textBox8.Text);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox9.Text, out _))
            {
                信息框("只能输入正负阿拉伯数字！", "信息:", 5000);
                textBox9.Text = "-150";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox9", textBox9.Text);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (!long.TryParse(textBox10.Text, out _))
            {
                信息框("只能输入正负阿拉伯数字！", "信息:", 5000);
                textBox10.Text = "2000";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox10", textBox10.Text);
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (!long.TryParse(textBox11.Text, out _))
            {
                信息框("只能输入正负阿拉伯数字！", "信息:", 5000);
                textBox11.Text = "2500";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox11", textBox11.Text);

        }

        private void 血量值_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(血量值.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                血量值.Text = "11";
                return;
            }
            API.写配置项(功能配置路径, "Set", "hp1", 血量值.Text);

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox12.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox12.Text = "7";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox12", textBox12.Text);
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox13.Text, out _))
            {
                信息框("只能输入正负阿拉伯数字！", "信息:", 5000);
                textBox13.Text = "250";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox13", textBox13.Text);

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox14.Text, out _))
            {
                信息框("只能输入正负阿拉伯数字！", "信息:", 5000);
                textBox14.Text = "280";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox14", textBox14.Text);
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox16.Text, out _))
            {
                信息框("只能输入正负阿拉伯数字！", "信息:", 5000);
                textBox16.Text = "350";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox16", textBox16.Text);
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox15.Text, out _))
            {
                信息框("只能输入正负阿拉伯数字！", "信息:", 5000);
                textBox15.Text = "280";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox15", textBox15.Text);
        }


        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox18.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox18.Text = "3";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox18", textBox18.Text);

        }

        private void ALT5抽鞭子_CheckedChanged(object sender, EventArgs e)
        {
            if (ALT5抽鞭子.Checked == true)
            {
                API.写配置项(功能配置路径, "Hook", "ALT5", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", "ALT5", "0");
            }

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox19.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox19.Text = "600";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox19", textBox19.Text);
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox20.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox20.Text = "40";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox20", textBox20.Text);

        }
        private void 提示透明度选择框_SelectedIndexChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "buyitems", 提示透明度选择框.SelectedItem.ToString());

        }
        #endregion   
        #region 定时器
        private void 定时关机时钟_Tick(object sender, EventArgs e)
        {
            flagNum++;
            labl关机.Text = "";
            labl关机.Text = (dValue - flagNum) / 3600 + "时";
            labl关机.Text += (dValue - flagNum) % 3600 / 60 + "分";
            labl关机.Text += (dValue - flagNum) % 3600 % 60 + "秒 " + zhuangtai;
            try
            {
                if (flagNum >= dValue)
                {
                    定时关机时钟.Stop();
                    flagNum = 0;//MessageBox.Show("时间到了！！！","提示");
                    if (radioButton1.Checked)
                        SystemUtil.PowerOff();//关机

                    else if (radioButton2.Checked)
                        SystemUtil.Reboot();//重启

                    else if (radioButton3.Checked)
                        SystemUtil.LogoOff();//睡眠
                    else if (radioButton6.Checked)
                        API.进程_结束("ShooterGame");
                    else if (radioButton7.Checked)
                    {
                        for (int i = 0; i < int.Parse(textBox18.Text); i++)
                        {
                            程序_延时(3000);
                            if (textBox69.Text!="")
                            {
                                PlayAsync(textBox69.Text);
                            }
                            else PlayAsync("定时时间到了");
                        }
                    }
                    else 信息框("您没有赋予任务", "信息:", 5000);

                }
            }
            catch (Exception)
            {
                信息框("时间太大了", "信息:", 5000);

            }
        }
        private int flagNum = 0;
        private int dValue;
        private bool Flag = true;
        SystemUtil t = new SystemUtil();

        private void button4_Click(object sender, EventArgs e)
        {

            if ((comboBox1.Text == "") && (comboBox2.Text == "") && (comboBox3.Text == ""))
            {
                信息框("请输入时间", "信息:", 5000);
                return;
            }
            if (radioButton4.Checked == true)
            {
                dValue = Convert.ToInt16(comboBox6.Text) * 3600 + Convert.ToInt16(comboBox5.Text) * 60 + Convert.ToInt16(comboBox4.Text);
            }
            else
            {
                dValue = Convert.ToInt16(comboBox6.Text) * 3600 + Convert.ToInt16(comboBox5.Text) * 60 + Convert.ToInt16(comboBox4.Text) - DateTime.Now.Hour * 3600 - DateTime.Now.Minute * 60 - DateTime.Now.Second;
            }

            if (dValue <= 0)
            {
                dValue = 24 * 3600 + dValue;
            }

            labl关机.Text = "";
            labl关机.Text = (dValue - flagNum) / 3600 + "时";
            labl关机.Text += (dValue - flagNum) % 3600 / 60 + "分";
            labl关机.Text += (dValue - flagNum) % 3600 % 60 + "秒 " + zhuangtai; ;
            label54.Visible = true;
            labl关机.Visible = true;
            定时关机时钟.Interval = 1000;
            if (Flag)
            {
                定时关机时钟.Start();
                Flag = false;
                button4.Text = "取消";
                flagNum = 0;
            }
            else
            {
                定时关机时钟.Stop();
                Flag = true;
                labl关机.Visible = false;
                label54.Visible = false;
                button4.Text = "启动";
                flagNum = 0;
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "radioButton4", "1");
            API.写配置项(功能配置路径, "Set", "radioButton5", "0");
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "radioButton4", "0");
            API.写配置项(功能配置路径, "Set", "radioButton5", "1");
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                zhuangtai = radioButton1.Text;
            }
            API.写配置项(功能配置路径, "Set", "radioButton1", "1");
            API.写配置项(功能配置路径, "Set", "radioButton2", "0");
            API.写配置项(功能配置路径, "Set", "radioButton3", "0");
            API.写配置项(功能配置路径, "Set", "radioButton6", "0");
            API.写配置项(功能配置路径, "Set", "radioButton7", "0");
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                zhuangtai = radioButton2.Text;
            }
            API.写配置项(功能配置路径, "Set", "radioButton2", "1");
            API.写配置项(功能配置路径, "Set", "radioButton1", "0");
            API.写配置项(功能配置路径, "Set", "radioButton3", "0");
            API.写配置项(功能配置路径, "Set", "radioButton6", "0");
            API.写配置项(功能配置路径, "Set", "radioButton7", "0");
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                zhuangtai = radioButton3.Text;
            }
            API.写配置项(功能配置路径, "Set", "radioButton3", "1");
            API.写配置项(功能配置路径, "Set", "radioButton1", "0");
            API.写配置项(功能配置路径, "Set", "radioButton2", "0");
            API.写配置项(功能配置路径, "Set", "radioButton6", "0");
            API.写配置项(功能配置路径, "Set", "radioButton7", "0");
        }
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked == true)
            {
                zhuangtai = radioButton6.Text;
            }
            API.写配置项(功能配置路径, "Set", "radioButton3", "0");
            API.写配置项(功能配置路径, "Set", "radioButton1", "0");
            API.写配置项(功能配置路径, "Set", "radioButton2", "0");
            API.写配置项(功能配置路径, "Set", "radioButton6", "1");
            API.写配置项(功能配置路径, "Set", "radioButton7", "0");
        }
        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked == true)
            {
                zhuangtai = radioButton7.Text;
            }
            API.写配置项(功能配置路径, "Set", "radioButton3", "0");
            API.写配置项(功能配置路径, "Set", "radioButton1", "0");
            API.写配置项(功能配置路径, "Set", "radioButton2", "0");
            API.写配置项(功能配置路径, "Set", "radioButton6", "0");
            API.写配置项(功能配置路径, "Set", "radioButton7", "1");
        }
        private void button7_Click(object sender, EventArgs e)
        {
          
        }
        private void button8_Click_1(object sender, EventArgs e)
        {
        

        }
        private void login()
        {
                    识图自动吃喝.Enabled = true;
                    检测黑包.Enabled = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
            检测黑包.Checked = false;
            检测黑包时钟.Stop();
            识图自动吃喝.Checked = false;
            识图自动时钟.Stop();
            //验证面板.Visible = true;
            识图自动吃喝.Enabled = false;
            检测黑包.Enabled = false;
        }
        #endregion
        #region 识图吃喝
        private void ALTZ_CheckedChanged(object sender, EventArgs e)
        {
            if (ALTZ.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "ALTZ", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "ALTZ", "0");
            }

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "checkBox7", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "checkBox7", "0");
            }

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "checkBox8", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "checkBox8", "0");
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "checkBox9", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "checkBox9", "0");
            }

        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "checkBox10", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "checkBox10", "0");
            }

        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "checkBox11", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "checkBox11", "0");
            }
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "checkBox12", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "checkBox12", "0");
            }
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "checkBox13", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "checkBox13", "0");
            }
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "checkBox14", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "checkBox14", "0");
            }

        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox15.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "checkBox15", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "checkBox15", "0");
            }
        }


        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "textBox17", textBox17.Text);
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "textBox23", textBox23.Text);
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "textBox27", textBox27.Text);
        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "textBox25", textBox25.Text);
        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "textBox35", textBox35.Text);
        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "textBox33", textBox33.Text);

        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "textBox31", textBox31.Text);
        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "textBox29", textBox29.Text);
        }


        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox21.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox21.Text = "10";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox21", textBox21.Text);
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox22.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox22.Text = "10";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox22", textBox22.Text);
        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox26.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox26.Text = "10";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox26", textBox26.Text);

        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox24.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox24.Text = "10";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox24", textBox24.Text);
        }

        private void textBox34_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox34.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox34.Text = "10";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox34", textBox34.Text);
        }

        private void textBox32_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox32.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox32.Text = "10";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox32", textBox32.Text);

        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox30.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox30.Text = "10";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox30", textBox30.Text);

        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox28.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox28.Text = "10";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBo28", textBox28.Text);
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox19.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "checkBox19", "1");
            }
            else API.写配置项(功能配置路径, "Set", "checkBox19", "0");
            if (激活窗口.Checked == true)
            {
                API.窗口_激活显示(句柄);
            }
        }
        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox20.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "checkBox20", "1");
            }
            else API.写配置项(功能配置路径, "Set", "checkBox20", "0");

            if (激活窗口.Checked == true)
            {
                API.窗口_激活显示(句柄);
            }
        }

     
        private void textBox41_TextChanged(object sender, EventArgs e)
        {
            
            API.写配置项(功能配置路径, "Set", "textBox41", textBox41.Text);
        }

        private void textBox40_TextChanged(object sender, EventArgs e)
        {
            
            API.写配置项(功能配置路径, "Set", "textBox40", textBox40.Text);

        }
        private void textBox43_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox43.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox43.Text = "51";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox43", textBox43.Text);
        }

        private void textBox42_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox42.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox42.Text = "115";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox42", textBox42.Text);

        }

        private void textBox45_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox45.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox45.Text = "100";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox45", textBox45.Text);

        }

        private void textBox44_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox44.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox44.Text = "161";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox44", textBox44.Text);
        }

        private void textBox47_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox47.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox47.Text = "127";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox47", textBox47.Text);

        }

        private void textBox46_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox46.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox46.Text = "181";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox46", textBox46.Text);
        }

        private void textBox53_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox53.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox53.Text = "183";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox53", textBox53.Text);

        }

        private void textBox52_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox52.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox52.Text = "228";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox52", textBox52.Text);

        }

        private void textBox50_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox50.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox50.Text = "193";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox50", textBox50.Text);
        }

        private void textBox51_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox51.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox51.Text = "125";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox51", textBox51.Text);

        }

        private void textBox49_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox49.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox49.Text = "108";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox49", textBox49.Text);
        }

        private void textBox48_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox48.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox48.Text = "181";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox48", textBox48.Text);

        }
        private void textBox59_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox59.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox59.Text = "133";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox59", textBox59.Text);
        }
        private void textBox58_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox58.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox58.Text = "217";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox58", textBox58.Text);

        }
        private void textBox57_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox57.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox57.Text = "0";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox57", textBox57.Text);

        }
        private void textBox56_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox56.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox56.Text = "0";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox56", textBox56.Text);


        }
        private void textBox55_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox55.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox55.Text = "8";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox55", textBox55.Text);

        }
        private void textBox54_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox54.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox55.Text = "107";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox54", textBox54.Text);
        }
        private void textBox61_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox61.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox61.Text = "5";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox61", textBox61.Text);
        }
        private void 检测自动吃喝()
        {
            if (方舟未运行.Text == "方舟已运行")
            {
                if (计时喝水.Checked == true)
                {
                    计时喝水.Checked = false;
                    计时喝水时钟.Stop();
                }
                if (计时吃饭.Checked == true)
                {
                    计时吃饭.Checked = false;
                    计时吃东西时钟.Stop();
                }
                if (识图自动吃喝.Checked == true)
                {
                    识图自动时钟.Interval = int.Parse(textBox61.Text) * 1000;
                    识图自动时钟.Start();
                    timer1.Interval = 100;
                    timer1.Start();
                }
                else
                {
                    识图自动吃喝.Checked = false;
                    识图自动时钟.Stop();
                    timer1.Stop();
                    pictureBox4.Image = null;
                }
            }
            else
            {
                识图自动吃喝.Checked = false;
                识图自动时钟.Stop();
                if (方舟未运行.Text == "方舟已运行(隐藏)")
                {
                    if (激活窗口.Checked == true)
                    {
                        API.窗口_激活显示(句柄);
                    }
                }
                else
                {
                    //  信息框("方舟未运行", "信息:", 5000);
                }
            }
        }
        private void 识图自动吃喝_CheckedChanged(object sender, EventArgs e)
        {
            检测自动吃喝();
            //if (He == 1920 && Wi == 1080)
            //{
            //    检测自动吃喝();
            //}
            //else if (He == 2048 && Wi == 1152)
            //{
            //    textBox41.Text = "2280";
            //    textBox40.Text = "1350";
            //    检测自动吃喝();
            //}
            //else if (He == 2560 && Wi == 1440)
            //{
            //    textBox41.Text = "2274";
            //    textBox40.Text = "1350";
            //    检测自动吃喝();
            //}
            //else
            //{
            //    识图自动吃喝.Checked = false;
            //    信息框("当前分辨率不匹配,建议分辨率1080P或2K,否则AI识图将无法使用", "信息:", 5000);
            //}
        }
 

        private void 检测黑包_CheckedChanged(object sender, EventArgs e)
        {
            if (方舟未运行.Text == "方舟已运行")
            {
                if (diubaoloop)
                {
                    diubaoloop = false;
                }
                if (检测黑包.Checked == true)
                {
                    if (He == 1920 && Wi == 1080)
                    {
                        检测黑包时钟.Interval = int.Parse(textBox62.Text) * 1000;
                        检测黑包时钟.Start();
                    }
                    else if (He == 2048 && Wi == 1152)
                    {
                        textBox64.Text = "2389";
                        textBox63.Text = "26";
                        检测黑包时钟.Interval = int.Parse(textBox62.Text) * 1000;
                        检测黑包时钟.Start();
                    }
                    else if (He == 2560 && Wi == 1440)
                    {
                        textBox64.Text = "2389";
                        textBox63.Text = "26";
                        检测黑包时钟.Interval = int.Parse(textBox62.Text) * 1000;
                        检测黑包时钟.Start();
                    }
                    else
                    {
                        识图自动吃喝.Checked = false;
                        信息框("当前分辨率不匹配,建议分辨率1080P或2K,否则AI识图将无法使用", "信息:", 5000);
                    }
                 
                }
                else
                {
                    检测黑包.Checked = false;
                    检测黑包时钟.Stop();
                }
            }
            else
            {
                检测黑包.Checked = false;
                检测黑包时钟.Stop();
                if (方舟未运行.Text == "方舟已运行(隐藏)")
                {
                    if (激活窗口.Checked == true)
                    {
                        API.窗口_激活显示(句柄);
                    }
                }
                else
                {
                    //  信息框("方舟未运行", "信息:", 5000);
                }
            }

        }

        private void 检测黑包时钟_Tick(object sender, EventArgs e)
        {
            if (句柄 == (IntPtr)0)
            {
                检测黑包.Checked = false;
                检测黑包时钟.Stop();
            }
            else
            {
                Graphics g = Graphics.FromImage(t3);
                if (checkBox6.Checked == true)
                {
                    try
                    {
                        g.CopyFromScreen(int.Parse(textBox64.Text) - 100, int.Parse(textBox63.Text), 0, 0, new Size(60, 60));
                    }
                    catch
                    {

                        g.CopyFromScreen(1692, 19, 0, 0, new Size(60, 60));
                    }
                }
                else
                {
                    try
                    {
                        g.CopyFromScreen(int.Parse(textBox41.Text) - 100, int.Parse(textBox40.Text), 0, 0, new Size(60, 60));
                    }
                    catch
                    {

                        g.CopyFromScreen(1605, 1000, 0, 0, new Size(60, 60));
                    }
                }

                g.Dispose();
                t3.SetPixel(25, 22, Color.Black); t3.SetPixel(25, 23, Color.Black);
                t3.SetPixel(22, 25, Color.Black); t3.SetPixel(23, 25, Color.Black);
                t3.SetPixel(25, 28, Color.Black); t3.SetPixel(25, 27, Color.Black);
                t3.SetPixel(28, 25, Color.Black); t3.SetPixel(27, 25, Color.Black);
                Color c = t3.GetPixel(25, 25);

                if (c.A == 255 && c.R != 0 && c.G != 0 && c.B != 0)
                {
                    Graphics g1 = Graphics.FromImage(t3);
                    if (checkBox6.Checked == true)
                    {
                        try
                        {
                            g1.CopyFromScreen(int.Parse(textBox64.Text), int.Parse(textBox63.Text), 0, 0, new Size(60, 60));
                        }
                        catch
                        {
                            g1.CopyFromScreen(1792, 19, 0, 0, new Size(60, 60));
                        }
                    }
                    else
                    {
                        try
                        {
                            g1.CopyFromScreen(int.Parse(textBox41.Text), int.Parse(textBox40.Text), 0, 0, new Size(60, 60));
                        }
                        catch
                        {

                            g1.CopyFromScreen(1705, 1000, 0, 0, new Size(60, 60));
                        }
                    }
                    g1.Dispose();
                    t3.SetPixel(25, 22, Color.Black); t3.SetPixel(25, 23, Color.Black);
                    t3.SetPixel(22, 25, Color.Black); t3.SetPixel(23, 25, Color.Black);
                    t3.SetPixel(25, 28, Color.Black); t3.SetPixel(25, 27, Color.Black);
                    t3.SetPixel(28, 25, Color.Black); t3.SetPixel(27, 25, Color.Black);
                    Color c1 = t3.GetPixel(25, 25);
                    if (c1.A == 255 && c1.R == 0 && c1.G == 0 && c1.B == 0)
                    {
                        Console.WriteLine(c1);
                        Console.WriteLine("黑包");
                        if (ALTZ.Checked == false)
                        {
                            ALTZ.Checked = true;
                        }

                        if (checkBox7.Checked == false)
                        {
                            checkBox7.Checked = true;
                        }

                        diubaoloop停止1();
                        if (激活窗口.Checked == true)
                        {
                            API.窗口_激活显示(句柄);
                        }
                        diubaoloop = true;
                        if (checkBox1.Checked == true)
                        {
                            buy.label1.Text = "一键清包开始";
                        }
                        new Thread(diubaozloop).Start();
                    }
                }

            }

        }
        private void textBox62_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox62.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox62.Text = "8";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox62", textBox62.Text);
        }

        private void textBox64_TextChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "textBox64", textBox64.Text);
        }

        private void textBox63_TextChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "textBox63", textBox63.Text);

        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                checkBox22.Checked = true;
                API.写配置项(功能配置路径, "Set", "checkBox6", "1");
                checkBox6.Text = "龙物品栏";
                checkBox22.Text = "龙物品栏";
            }
            else
            {
                checkBox22.Checked = false;
                API.写配置项(功能配置路径, "Set", "checkBox6", "0");
                checkBox6.Text = "人物品栏";
                checkBox22.Text = "人物品栏";
            }
        }
        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.Checked == true)
            {
                checkBox6.Checked = true;
                API.写配置项(功能配置路径, "Set", "checkBox6", "1");
                checkBox6.Text = "龙物品栏";
                checkBox22.Text = "龙物品栏";
            }
            else
            {
                checkBox6.Checked = false;
                API.写配置项(功能配置路径, "Set", "checkBox6", "0");
                checkBox6.Text = "人物品栏";
                checkBox22.Text = "人物品栏";
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }
        private static void 联系入梦客服()
        {
            API.网页_打开指定网址("tencent://Message/?Uin=59765729&websiteName=www.oicqzone.com&Menu=yes");
        }
        private void button10_Click(object sender, EventArgs e)
        {
            联系入梦客服();
        }

  
        #endregion
        public 入梦方舟小工具()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;      
           this.Size = new Size(panel1.Width + 2, panel3.Height + panel1.Height + pictureBox1.Height);       
            this.BackColor = Color.SlateBlue;
            panel3.BackColor = Color.White;

        }
        public const string ver = "8.9.2";
        string severnumber = ""; //输入您的最新版本地址
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                配置初始化();
                初始化();
                鼠标左键编辑框.SelectedIndex = int.Parse(API.读配置项(功能配置路径, "Set", "moveleft"));
                自动前进编辑框.SelectedIndex = int.Parse(API.读配置项(功能配置路径, "Set", "run"));
                if (API.读配置项(功能配置路径, "Set", "timer") == "1")
                {
                    checkBox3.Checked = true;
                    白框检测时钟.Interval = 2000;
                    白框检测时钟.Start();
                }
                if (He != 1920 && Wi != 1080 || He != 1280 && Wi != 720 || He != 4960 && Wi != 2160 || He != 3840 && Wi != 2160 || He != 4096 && Wi != 3112 || He != 3656 && Wi != 2664)
                {
                    label65.Visible = true;
                }
                login();
            }
            catch 
            {
                停止();
                初始化配置常量();
                信息框("初始化成功!", "信息:", 5000);
            }
            try
            {
                if (API.读配置项(游戏路径, "Set", "Path") == "")
                {
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 346110", false);
                    string softwareName = key.GetValue("DisplayName", "").ToString();//获取软件名
                    string installLocation = key.GetValue("InstallLocation", "").ToString();//获取安装路径
                    if (softwareName.IndexOf("ARK: Survival Evolved") != -1)
                    {
                        API.写配置项(游戏路径, "Set", "Path", installLocation + "\\Engine\\Config\\ConsoleVariables.ini");
                        Console.WriteLine("注册表读取配置项路径");
                    }
                    else
                    {
                        延迟检测.Enabled = true;
                        延迟检测.Interval = 5000;
                        Console.WriteLine("路径读取配置项路径");
                    }
                }
            }
            catch 
            {
              延迟检测.Enabled = true;
              延迟检测.Interval = 5000;
              Console.WriteLine("路径读取配置项路径");
            }
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "checkBox21", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "checkBox21", "0");
            }
        }

        private void textBox37_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox37.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox37.Text = "1530";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox37", textBox37.Text);
        }

        private void textBox36_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox36.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox36.Text = "187";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox36", textBox36.Text);
        }

        private void textBox38_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox38.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox38.Text = "500";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox38", textBox38.Text);
        }

        private void textBox39_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox39.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox39.Text = "100";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox39", textBox39.Text);
        }



        private void button11_Click(object sender, EventArgs e)
        {
            API.网页_打开指定网址("https://item.taobao.com/item.htm?spm=a1z10.5-c-s.w4002-12206783493.37.23e67c14pt3thl&id=619978199844");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            联系入梦客服();          
        }
        Form3 buy1 = new Form3();
        double width;
        double height;
        private void checkBox16_CheckedChanged(object sender, EventArgs e)//准心大小
        {
            
            if (checkBox16.Checked == true)
            {
                buy1.Width = int.Parse(API.读配置项(功能配置路径, "Set", "textBox60"));
                buy1.Height = int.Parse(API.读配置项(功能配置路径, "Set", "textBox65"));
                buy1.Visible = true;
                API.写配置项(功能配置路径, "Set", "checkBox16", "1");              
            }
            else
            {
                buy1.Visible = false;
                API.写配置项(功能配置路径, "Set", "checkBox16", "0");
            }
        }
        private void textBox73_TextChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "textBox73", textBox73.Text);
        }

        private void textBox70_TextChanged(object sender, EventArgs e)
        {
            API.写配置项(功能配置路径, "Set", "textBox70", textBox70.Text);
        }
        private void textBox60_TextChanged(object sender, EventArgs e)
        {
            checkBox16.Checked=false;
            API.写配置项(功能配置路径, "Set", "textBox60", textBox60.Text);
        }

        private void textBox65_TextChanged(object sender, EventArgs e)
        {
            checkBox16.Checked = false;
            API.写配置项(功能配置路径, "Set", "textBox65", textBox65.Text);
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox17.Checked == true)
            {
                API.写配置项(功能配置路径, "Hook", "ALT6", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Hook", "ALT6", "0");
            }
        }

        private void textBox66_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox66.Text, out _))
            {
                信息框("只能输入阿拉伯数字！", "信息:", 5000);
                textBox66.Text = "10";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox66", textBox66.Text);
        }
    
        private void textBox68_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox68.Text, out _))
            {
                信息框("只能输入正负阿拉伯数字！", "信息:", 5000);
                textBox68.Text = "766";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox68", textBox68.Text);
        }

        private void textBox67_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox67.Text, out _))
            {
                信息框("只能输入正负阿拉伯数字！", "信息:", 5000);
                textBox67.Text = "941";
                return;
            }
            API.写配置项(功能配置路径, "Set", "textBox67", textBox67.Text);
        }



        private void button14_Click(object sender, EventArgs e)
        {
            联系入梦客服();
        }

        private void 入梦方舟小工具_FormClosing(object sender, FormClosingEventArgs e)
        {
            Formclose();
        }

 
        private void pictureBox10_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseUp = true;//鼠标左右键被按下
            Cursor = Cursors.Cross; //改变鼠标样式为十字架
        }

        private void pictureBox10_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseUp = false;//鼠标左右键被弹起
            Cursor = Cursors.Default;//改变鼠标样式为默认
            
        }
        private void pictureBox10_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseUp)//左键是否被按下
            {
                API.GetCursorPos(ref pi); //获取鼠标坐标值
                label95.Text = $"坐标: x:{pi.X} y:{pi.Y}";
                textBox41.Text = pi.X.ToString();
                textBox40.Text = pi.Y.ToString();
            }
        }
        Bitmap t1 = new Bitmap(60, 60);
        Bitmap t2 = new Bitmap(60, 60);
        Bitmap t3 = new Bitmap(60, 60);
        Bitmap t4 = new Bitmap(60, 60);
        Bitmap t5 = new Bitmap(60, 60);
        Bitmap jire = new Bitmap(60, 60);
        private void 识图自动时钟_Tick(object sender, EventArgs e)
        {
            if (句柄 == (IntPtr)0)
            {
                识图自动吃喝.Checked = false;
                识图自动时钟.Stop();

            }
            else
            {
                Graphics g1 = Graphics.FromImage(t4);
                g1.CopyFromScreen(int.Parse(textBox41.Text) - 200, int.Parse(textBox40.Text), 0, 0, new Size(60, 60));
                g1.Dispose();
                t4.SetPixel(25, 22, Color.Black); t4.SetPixel(25, 23, Color.Black);
                t4.SetPixel(22, 25, Color.Black); t4.SetPixel(23, 25, Color.Black);
                t4.SetPixel(25, 28, Color.Black); t4.SetPixel(25, 27, Color.Black);
                t4.SetPixel(28, 25, Color.Black); t4.SetPixel(27, 25, Color.Black);
                Color c1 = t4.GetPixel(25, 25);

                if (c1.A == 255 && c1.R != 0 && c1.G != 0 && c1.B != 0)//检测左侧便宜是否为黑色
                {
                    Graphics g = Graphics.FromImage(t1);
                    try
                    {
                        g.CopyFromScreen(int.Parse(textBox41.Text), int.Parse(textBox40.Text), 0, 0, new Size(60, 60));
                    }
                    catch
                    {

                        g.CopyFromScreen(1705, 1000, 0, 0, new Size(60, 60));
                    }
                    g.Dispose();
                    t1.SetPixel(25, 22, Color.Black); t1.SetPixel(25, 23, Color.Black);
                    t1.SetPixel(22, 25, Color.Black); t1.SetPixel(23, 25, Color.Black);
                    t1.SetPixel(25, 28, Color.Black); t1.SetPixel(25, 27, Color.Black);
                    t1.SetPixel(28, 25, Color.Black); t1.SetPixel(27, 25, Color.Black);
                    Color c = t1.GetPixel(25, 25);
                    Graphics g2 = Graphics.FromImage(t2);
                    try
                    {
                        g2.CopyFromScreen(int.Parse(textBox41.Text), int.Parse(textBox40.Text) + 10, 0, 0, new Size(60, 60));
                    }
                    catch
                    {

                        g2.CopyFromScreen(1705, 1010, 0, 0, new Size(60, 60));
                    }
                    g2.Dispose();
                    t2.SetPixel(25, 22, Color.Black); t2.SetPixel(25, 23, Color.Black);
                    t2.SetPixel(22, 25, Color.Black); t2.SetPixel(23, 25, Color.Black);
                    t2.SetPixel(25, 28, Color.Black); t2.SetPixel(25, 27, Color.Black);
                    t2.SetPixel(28, 25, Color.Black); t2.SetPixel(27, 25, Color.Black);
                    Color c2 = t1.GetPixel(25, 25);

                    Graphics g5 = Graphics.FromImage(t5);
                    try
                    {
                        g5.CopyFromScreen(int.Parse(textBox41.Text), int.Parse(textBox40.Text) - 100, 0, 0, new Size(60, 60));
                    }
                    catch
                    {

                        g5.CopyFromScreen(1705, 1000, 0, 0, new Size(60, 60));
                    }
                    g5.Dispose();
                    t5.SetPixel(25, 22, Color.Black); t5.SetPixel(25, 23, Color.Black);
                    t5.SetPixel(22, 25, Color.Black); t5.SetPixel(23, 25, Color.Black);
                    t5.SetPixel(25, 28, Color.Black); t5.SetPixel(25, 27, Color.Black);
                    t5.SetPixel(28, 25, Color.Black); t5.SetPixel(27, 25, Color.Black);
                    Color c5 = t5.GetPixel(25, 25);

                    if (checkBox18.Checked == true)
                    {
                        try
                        {
                            if (c5.A == 255 && c5.R != 0 && c5.G != 0 && c5.B != 0)
                            {
                                if (c.A == 255 && c.R >= int.Parse(textBox43.Text) && c.R <= int.Parse(textBox42.Text) && c.G >= int.Parse(textBox45.Text) && c.G <= int.Parse(textBox44.Text) && c.B >= int.Parse(textBox47.Text) && c.B <= int.Parse(textBox46.Text))
                                {
                                    if (c2.A == 255 && c2.R >= int.Parse(textBox43.Text) && c2.R <= int.Parse(textBox42.Text) && c2.G >= int.Parse(textBox45.Text) && c2.G <= int.Parse(textBox44.Text) && c2.B >= int.Parse(textBox47.Text) && c2.B <= int.Parse(textBox46.Text))
                                    {
                                        if (激活窗口.Checked == true)
                                        {
                                            API.窗口_激活显示(句柄);
                                        }
                                        Console.WriteLine("检测到渴了,喝水");
                                        API.键盘_消息(句柄, 5, (byte)Keys.D8);
                                    }
                                }
                            }
                        }
                        catch
                        {
                            信息框("色域值必须是整数阿拉伯数字", "信息:", 5000);
                        }

                    }
                    if (checkBox19.Checked == true)
                    {
                        try
                        {
                            if (c5.A == 255 && c5.R != 0 && c5.G != 0 && c5.B != 0)
                            {
                                if (c.A == 255 && c.R >= int.Parse(textBox53.Text) && c.R <= int.Parse(textBox52.Text) && c.G >= int.Parse(textBox51.Text) && c.G <= int.Parse(textBox50.Text) && c.B >= int.Parse(textBox49.Text) && c.B <= int.Parse(textBox48.Text))
                                {
                                    if (c2.A == 255 && c2.R >= int.Parse(textBox53.Text) && c2.R <= int.Parse(textBox52.Text) && c2.G >= int.Parse(textBox51.Text) && c2.G <= int.Parse(textBox50.Text) && c2.B >= int.Parse(textBox49.Text) && c2.B <= int.Parse(textBox48.Text))
                                    {
                                        if (激活窗口.Checked == true)
                                        {
                                            API.窗口_激活显示(句柄);
                                        }
                                        Console.WriteLine("检测到饿了,吃东西");
                                        API.键盘_消息(句柄, 5, (byte)Keys.D9);
                                    }
                                }
                            }
                        }

                        catch
                        {
                            信息框("色域值必须是整数阿拉伯数字", "信息:", 5000);
                        }
                    }

                    if (checkBox20.Checked == true)
                    {
                        Graphics gjire = Graphics.FromImage(jire);
                        if (He == 2560 && Wi == 1440)
                        {
                            gjire.CopyFromScreen(int.Parse(textBox41.Text), int.Parse(textBox40.Text) - 12, 0, 0, new Size(60, 60));
                        }
                        else if (He == 2048 && Wi == 1152)
                        {
                            gjire.CopyFromScreen(int.Parse(textBox41.Text), int.Parse(textBox40.Text) - 12, 0, 0, new Size(60, 60));
                        }
                        else
                        {
                            gjire.CopyFromScreen(int.Parse(textBox41.Text) - 120, int.Parse(textBox40.Text), 0, 0, new Size(60, 60));
                        }

                        gjire.Dispose();
                        jire.SetPixel(25, 22, Color.Black); jire.SetPixel(25, 23, Color.Black);
                        jire.SetPixel(22, 25, Color.Black); jire.SetPixel(23, 25, Color.Black);
                        jire.SetPixel(25, 28, Color.Black); jire.SetPixel(25, 27, Color.Black);
                        jire.SetPixel(28, 25, Color.Black); jire.SetPixel(27, 25, Color.Black);
                        Color cjire = jire.GetPixel(25, 25);
                        Console.WriteLine(cjire);
                        try
                        {
                            if (c5.A == 255 && c5.R != 0 && c5.G != 0 && c5.B != 0)
                            {
                                if (c2.A == 255 && c2.R >= int.Parse(textBox59.Text) && c2.R <= int.Parse(textBox58.Text) && c2.G >= int.Parse(textBox57.Text) && c2.G <= int.Parse(textBox56.Text) && c2.B >= int.Parse(textBox55.Text) && c2.B <= int.Parse(textBox54.Text))
                                {
                                    Console.WriteLine("检测到极热1来临");
                                    程序_延时(1500);
                                    PlayAsync("极热来了");
                                    if (checkBox1.Checked == true)
                                    {
                                        buy.label1.Text = "极热来了";
                                    }

                                }
                                else if (cjire.A == 255 && cjire.R >= int.Parse(textBox59.Text) && cjire.R <= int.Parse(textBox58.Text) && cjire.G >= int.Parse(textBox57.Text) && cjire.G <= int.Parse(textBox56.Text) && cjire.B >= int.Parse(textBox55.Text) && cjire.B <= int.Parse(textBox54.Text))
                                {
                                    Console.WriteLine("检测到极热2来临");
                                    程序_延时(1500);
                                    PlayAsync("极热来了");
                                    if (checkBox1.Checked == true)
                                    {
                                        buy.label1.Text = "极热来了";
                                    }
                                }
                                else
                                {
                                    //if (checkBox1.Checked == true)
                                    //{
                                    //    buy.label1.Text = "焦土极热结束";
                                    //}
                                }
                            }
                        }

                        catch
                        {
                            信息框("色域值必须是整数阿拉伯数字", "信息:", 5000);
                        }
                    }
                }
            }
        }
        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            if (激活窗口.Checked == true)
            {
                API.窗口_激活显示(句柄);
            }  
            if (checkBox18.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "checkBox18", "1");
            }
            else API.写配置项(功能配置路径, "Set", "checkBox18", "0");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics g1 = Graphics.FromImage(t4);

            g1.CopyFromScreen(int.Parse(textBox41.Text), int.Parse(textBox40.Text), 0, 0, new Size(60, 60));

            g1.Dispose();
            t4.SetPixel(25, 22, Color.Black); t4.SetPixel(25, 23, Color.Black);
            t4.SetPixel(22, 25, Color.Black); t4.SetPixel(23, 25, Color.Black);
            t4.SetPixel(25, 28, Color.Black); t4.SetPixel(25, 27, Color.Black);
            t4.SetPixel(28, 25, Color.Black); t4.SetPixel(27, 25, Color.Black);
            Color c1 = t4.GetPixel(25, 25);
            label95.Text = $"坐标: x:{textBox41.Text} y:{textBox40.Text}";
            label96.Text = "色域:" + c1.ToString();
            pictureBox4.Image = t1;
        }

        private void label78_Click(object sender, EventArgs e)
        {

        }
 
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label60_Click(object sender, EventArgs e)
        {

        }

        private void label61_Click(object sender, EventArgs e)
        {

        }

        private void label62_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label52_Click(object sender, EventArgs e)
        {

        }

        private void label80_Click(object sender, EventArgs e)
        {

        }

        private void label81_Click(object sender, EventArgs e)
        {

        }

        private void label82_Click(object sender, EventArgs e)
        {

        }

        private void label67_Click(object sender, EventArgs e)
        {

        }

        private void label70_Click(object sender, EventArgs e)
        {

        }

        private void label71_Click(object sender, EventArgs e)
        {

        }

        private void label72_Click(object sender, EventArgs e)
        {

        }

        private void label73_Click(object sender, EventArgs e)
        {

        }

        private void label75_Click(object sender, EventArgs e)
        {

        }

        private void label76_Click(object sender, EventArgs e)
        {

        }

        private void label68_Click(object sender, EventArgs e)
        {

        }

        private void label69_Click(object sender, EventArgs e)
        {

        }

        private void label74_Click(object sender, EventArgs e)
        {

        }

        private void label63_Click(object sender, EventArgs e)
        {

        }

        private void label59_Click(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                checkBox4.Text = "后台模式";
                   API.写配置项(功能配置路径, "Set", "houtai", "1");
            }
            else
            {
                checkBox4.Text = "前台模式";
                API.写配置项(功能配置路径, "Set", "houtai", "0");
            } 
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            API.网页_打开指定网址("http://www.bianshengruanjian.com");
        }


        string path = API.GetWindowPath(句柄);
        private void 延迟检测_Tick(object sender, EventArgs e)
        {
            if (API.读配置项(游戏路径, "Set", "Path") == "")
            {
                if (方舟未运行.Text != "方舟未运行")
                {
                    try
                    {
                       // MessageBox.Show("游戏存在"+path);
                        if (API.文本_是否存在(path, "ARKSurvivalEvolved") !=true)
                        {
                            API.写配置项(游戏路径, "Set", "Path", API.文本_取左边内容(API.GetWindowPath(句柄), "ARK") + "ARK\\Engine\\Config\\ConsoleVariables.ini");
                            //Console.WriteLine(API.文本_取左边内容(API.GetWindowPath(句柄), "ARK") + "ARK\\Engine\\Config\\ConsoleVariables.ini");
                        }
                        else
                        {
                            API.写配置项(游戏路径, "Set", "Path", API.文本_取左边内容(API.GetWindowPath(句柄), "ARKSurvivalEvolved") + "ARKSurvivalEvolved\\Engine\\Config\\ConsoleVariables.ini");
                           // Console.WriteLine(API.文本_取左边内容(API.GetWindowPath(句柄), "ARKSurvivalEvolved") + "ARKSurvivalEvolved\\Engine\\Config\\ConsoleVariables.ini");
                        }
                        延迟检测.Stop();
                    }
                    catch
                    {
                        API.写配置项(游戏路径, "Set", "Path", "");
                    }
                  
                }
            }
            else
            {              
                延迟检测.Stop();             
            }
           
        }

        private void button19_Click(object sender, EventArgs e)
        {
            API.网页_打开指定网址("http://www.teandy.com/compute/index.html");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            API.网页_打开指定网址("http://www.teandy.com/recipe.html");
        }

        private void button16_Click(object sender, EventArgs e)
        {
        API.网页_打开指定网址("http://www.teandy.com/boss.html");

        }

        private void button17_Click(object sender, EventArgs e)
        {
            API.网页_打开指定网址("http://www.teandy.com/dye.html");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            API.网页_打开指定网址("http://www.teandy.com/resource/notes.html");
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            API.网页_打开指定网址("http://www.teandy.com/kibble.html");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            API.网页_打开指定网址("http://www.teandy.com/resource/artifacts.html");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            API.网页_打开指定网址("http://www.teandy.com/resource/index.html");
        }

        private void button22_Click(object sender, EventArgs e)
        {
                SystemUtil.PowerOff();//关机
        }
        private void button23_Click(object sender, EventArgs e)
        {
            SystemUtil.Reboot();//重启
        }
        private void button24_Click(object sender, EventArgs e)
        {
            SystemUtil.LogoOff();//睡眠
        }
        private void button25_Click(object sender, EventArgs e)
        {
            API.进程_结束("ShooterGame");
        }
        private void button8_Click_2(object sender, EventArgs e)//3战斗画质
        {
            try
            {
                string txt = API.读配置项(游戏路径, "Set", "Path");
                if (txt != "")
                {
                    File.WriteAllText(txt, Properties.Resources.普通);//直接写出即可dao
                    Console.WriteLine("存在");
                    程序_延时(100);
                    if (限制60FPS.Checked == true)
                    {
                        API.WritePrivateProfileString("Startup", "t.maxfps ", "60", txt);
                    }
                    信息框("设置完成,重启游戏可载入画质", "提示:", 5000);
                }
                else
                {
                    API.写配置项(游戏路径, "Set", "Path", "");
                    信息框("请重启软件再试", "提示:", 5000);
                    Formclose();
                } 
            }
            catch (Exception)
            {
                API.写配置项(游戏路径, "Set", "Path", "");
                信息框("请重启软件再试", "提示:", 5000);
                Formclose();
            }
        }
        private void button11_Click_1(object sender, EventArgs e)//4战斗画质
        {
            try
            {
                string txt = API.读配置项(游戏路径, "Set", "Path");
                if (txt != "")
                {
                    File.WriteAllText(txt, Properties.Resources.极致);//直接写出即可dao
                    Console.WriteLine("存在");
                    程序_延时(100);
                    if (限制60FPS.Checked == true)
                    {
                        API.WritePrivateProfileString("Startup", "t.maxfps ", "60", txt);
                    }

                    信息框("设置完成,重启游戏可载入画质", "提示:", 5000);
                }
                else
                {
                    API.写配置项(游戏路径, "Set", "Path", "");
                    信息框("请重启软件再试", "提示:", 5000);
                    Formclose();
                } 
            }
            catch (Exception)
            {
                API.写配置项(游戏路径, "Set", "Path", "");
                信息框("请重启软件再试", "提示:", 5000);
                Formclose();
            }

        }
        private void button40_Click(object sender, EventArgs e)//战斗画质0
        {
            try
            {
                string txt = API.读配置项(游戏路径, "Set", "Path");
                if (txt != "")
                {
                    Console.WriteLine(txt);
                    File.WriteAllText(txt, null, Encoding.Default);
                    Console.WriteLine("存在");
                    程序_延时(100);
                    if (限制60FPS.Checked == true)
                    {
                        API.WritePrivateProfileString("Startup", "t.maxfps ", "60", txt);
                    }
                    //API.WritePrivateProfileString("Startup", "foliage.UseOcclusionType", "0", txt);//去除树叶和灌木丛
                    API.WritePrivateProfileString("Startup", "ShowFlag.Refraction", "0", txt);//关闭折射
                    //API.WritePrivateProfileString("Startup", "ShowFlag.Materials", "0", txt);//去除水下效果、致盲等
                    API.WritePrivateProfileString("Startup", "r.SceneColorFringe.Max", "0", txt);//去除水下效果、致盲等
                    //API.WritePrivateProfileString("Startup", "ShowFlag.LightComplexity", "0", txt);//地图表面覆盖光，去除黑暗 (造成屏幕一半黑一半白)
                    //API.WritePrivateProfileString("Startup", "ShowFlag.VisualizeLPV", "0", txt);//天空
                    API.WritePrivateProfileString("Startup", "ShowFlag.OverrideDiffuseAndSpecular", "0", txt);//覆盖反射效果
                    API.WritePrivateProfileString("Startup", "ShowFlag.StaticMeshes", "0", txt);//取消了大气雾的反射
                    API.WritePrivateProfileString("Startup", "ShowFlag.VisualizeSkyAtmosphere", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    API.WritePrivateProfileString("Startup", "ShowFlag.Atmosphere", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    API.WritePrivateProfileString("Startup", "ShowFlag.DepthOfField", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    信息框("设置完成,重启游戏可载入画质", "提示:", 5000);
                }
                else
                {
                    chongqi();
                }
            }
            catch (Exception)
            {
                chongqi();
            }
        }
        private void button5_Click(object sender, EventArgs e)//1战斗画质
        {
            try
            {
                string txt = API.读配置项(游戏路径, "Set", "Path");
                if (txt != "")
                {
                    Console.WriteLine(txt);
                    File.WriteAllText(txt, null, Encoding.Default);
                    Console.WriteLine("存在");
                    程序_延时(100);
                    if (限制60FPS.Checked == true)
                    {
                        API.WritePrivateProfileString("Startup", "t.maxfps ", "60", txt);
                    }
                    //API.WritePrivateProfileString("Startup", "foliage.UseOcclusionType", "0", txt);//去除树叶和灌木丛
                    API.WritePrivateProfileString("Startup", "ShowFlag.Refraction", "0", txt);//关闭折射
                    //API.WritePrivateProfileString("Startup", "ShowFlag.Materials", "0", txt);//去除水下效果、致盲等
                    API.WritePrivateProfileString("Startup", "r.SceneColorFringe.Max", "0", txt);//去除水下效果、致盲等
                    API.WritePrivateProfileString("Startup", "ShowFlag.LightComplexity", "0", txt);//地图表面覆盖光，去除黑暗 (造成屏幕一半黑一半白)
                    //API.WritePrivateProfileString("Startup", "ShowFlag.VisualizeLPV", "0", txt);//天空
                    API.WritePrivateProfileString("Startup", "ShowFlag.OverrideDiffuseAndSpecular", "0", txt);//覆盖反射效果
                    API.WritePrivateProfileString("Startup", "ShowFlag.StaticMeshes", "0", txt);//取消了大气雾的反射
                    API.WritePrivateProfileString("Startup", "ShowFlag.VisualizeSkyAtmosphere", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    API.WritePrivateProfileString("Startup", "ShowFlag.Atmosphere", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    API.WritePrivateProfileString("Startup", "ShowFlag.DepthOfField", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    信息框("设置完成,重启游戏可载入画质", "提示:", 5000);
                }
                else
                {
                    chongqi();
                } 
            }
            catch (Exception)
            {
                chongqi();
            }
        }
        private void button43_Click(object sender, EventArgs e)//2战斗画质
        {
            try
            {
                string txt = API.读配置项(游戏路径, "Set", "Path");
                if (txt != "")
                {
                    Console.WriteLine(txt);
                    File.WriteAllText(txt, null, Encoding.Default);
                    Console.WriteLine("存在");
                    程序_延时(100);
                    if (限制60FPS.Checked == true)
                    {
                        API.WritePrivateProfileString("Startup", "t.maxfps ", "60", txt);
                    }
                    API.WritePrivateProfileString("Startup", "foliage.UseOcclusionType", "0", txt);//去除树叶和灌木丛
                    API.WritePrivateProfileString("Startup", "ShowFlag.Refraction", "0", txt);//关闭折射
                    //API.WritePrivateProfileString("Startup", "ShowFlag.Materials", "0", txt);//去除水下效果、致盲等
                    API.WritePrivateProfileString("Startup", "r.SceneColorFringe.Max", "0", txt);//去除水下效果、致盲等
                    API.WritePrivateProfileString("Startup", "ShowFlag.LightComplexity", "0", txt);//地图表面覆盖光，去除黑暗 (造成屏幕一半黑一半白)
                    //API.WritePrivateProfileString("Startup", "ShowFlag.VisualizeLPV", "0", txt);//天空
                    API.WritePrivateProfileString("Startup", "ShowFlag.OverrideDiffuseAndSpecular", "0", txt);//覆盖反射效果
                    API.WritePrivateProfileString("Startup", "ShowFlag.StaticMeshes", "0", txt);//取消了大气雾的反射
                    API.WritePrivateProfileString("Startup", "ShowFlag.VisualizeSkyAtmosphere", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    API.WritePrivateProfileString("Startup", "ShowFlag.Atmosphere", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    API.WritePrivateProfileString("Startup", "ShowFlag.DepthOfField", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    信息框("设置完成,重启游戏可载入画质", "提示:", 5000);
                }
                else
                {
                    chongqi();
                }
            }
            catch (Exception)
            {
                chongqi();
            }
        }
        private void button13_Click_1(object sender, EventArgs e)//关闭战斗画质
        {
            try
            {
                string txt = API.读配置项(游戏路径, "Set", "Path");
                File.WriteAllText(txt, null, Encoding.Default);
                Console.WriteLine("存在"+ txt);
                信息框("设置完成,重启游戏可载入画质", "提示:", 5000);
            }
            catch (Exception)
            {
                API.写配置项(游戏路径, "Set", "Path", "");
                信息框("请重启软件再试", "提示:", 5000);
                Formclose();
            }
        }
        private void chongqi()
        {
            if (方舟未运行.Text != "方舟未运行")
            {
                API.写配置项(游戏路径, "Set", "Path", "");
                信息框("未检测到游戏配置文件路径,请点击初始化,重启软件再试", "提示:", 5000);
                Formclose();
            }
            else
            {
                信息框("方舟未运行!请您打开游戏重试", "提示:", 5000);
            }
            
        }
        private void button41_Click(object sender, EventArgs e)//3战斗画质
        {
            try
            {
                string txt = API.读配置项(游戏路径, "Set", "Path");
                if (txt != "")
                {
                    Console.WriteLine(txt);
                    File.WriteAllText(txt, null, Encoding.Default);
                    Console.WriteLine("存在");
                    程序_延时(100);
                    if (限制60FPS.Checked == true)
                    {
                        API.WritePrivateProfileString("Startup", "t.maxfps ", "60", txt);
                    }
                    程序_延时(100);
                    //API.WritePrivateProfileString("Startup", "foliage.UseOcclusionType", "0", txt);//去除树叶和灌木丛
                    API.WritePrivateProfileString("Startup", "ShowFlag.Refraction", "0", txt);//关闭折射
                    API.WritePrivateProfileString("Startup", "ShowFlag.Materials", "0", txt);//去除水下效果、致盲等
                    API.WritePrivateProfileString("Startup", "r.SceneColorFringe.Max", "0", txt);//去除水下效果、致盲等
                    API.WritePrivateProfileString("Startup", "ShowFlag.LightComplexity", "0", txt);//地图表面覆盖光，去除黑暗 (造成屏幕一半黑一半白)
                    //API.WritePrivateProfileString("Startup", "ShowFlag.VisualizeLPV", "0", txt);//天空
                    API.WritePrivateProfileString("Startup", "ShowFlag.OverrideDiffuseAndSpecular", "0", txt);//覆盖反射效果
                    API.WritePrivateProfileString("Startup", "ShowFlag.StaticMeshes", "0", txt);//取消了大气雾的反射
                    API.WritePrivateProfileString("Startup", "ShowFlag.VisualizeSkyAtmosphere", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    API.WritePrivateProfileString("Startup", "ShowFlag.Atmosphere", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    API.WritePrivateProfileString("Startup", "ShowFlag.DepthOfField", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    信息框("设置完成,重启游戏可载入画质", "提示:", 5000);
                }
                else
                {
                    chongqi();
                }
            }
            catch (Exception)
            {
                chongqi();
            }
        }

        private void button42_Click(object sender, EventArgs e)//4战斗画质
        {
            try
            {
                string txt = API.读配置项(游戏路径, "Set", "Path");
                if (txt != "")
                {
                    Console.WriteLine(txt);
                    File.WriteAllText(txt, null, Encoding.Default);
                    Console.WriteLine("存在");
                    程序_延时(100);
                    if (限制60FPS.Checked == true)
                    {
                        API.WritePrivateProfileString("Startup", "t.maxfps ", "60", txt);
                    }
                    程序_延时(100);
                    API.WritePrivateProfileString("Startup", "foliage.UseOcclusionType", "0", txt);//去除树叶和灌木丛
                    API.WritePrivateProfileString("Startup", "ShowFlag.Refraction", "0", txt);//关闭折射
                    API.WritePrivateProfileString("Startup", "ShowFlag.Materials", "0", txt);//去除水下效果、致盲等
                    API.WritePrivateProfileString("Startup", "r.SceneColorFringe.Max", "0", txt);//去除水下效果、致盲等
                    API.WritePrivateProfileString("Startup", "ShowFlag.LightComplexity", "0", txt);//地图表面覆盖光，去除黑暗
                    API.WritePrivateProfileString("Startup", "ShowFlag.VisualizeLPV", "0", txt);//天空
                    API.WritePrivateProfileString("Startup", "ShowFlag.OverrideDiffuseAndSpecular", "0", txt);//覆盖反射效果
                    API.WritePrivateProfileString("Startup", "ShowFlag.StaticMeshes", "0", txt);//取消了大气雾的反射
                    API.WritePrivateProfileString("Startup", "ShowFlag.VisualizeSkyAtmosphere", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    API.WritePrivateProfileString("Startup", "ShowFlag.Atmosphere", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    API.WritePrivateProfileString("Startup", "ShowFlag.DepthOfField", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    信息框("设置完成,重启游戏可载入画质", "提示:", 5000);
                }
                else
                {
                    chongqi();
                }
            }
            catch (Exception)
            {
                chongqi();
            }
        }
        private void button3_Click(object sender, EventArgs e)//5战斗画质
        {
            try
            {
                string txt = API.读配置项(游戏路径, "Set", "Path");
                if (txt != "")
                {
                    Console.WriteLine(txt);
                    File.WriteAllText(txt, null, Encoding.Default);
                    Console.WriteLine("存在");
                    程序_延时(100);
                    if (限制60FPS.Checked == true)
                    {
                        API.WritePrivateProfileString("Startup", "t.maxfps ", "60", txt);
                    }
                    程序_延时(100);
                    API.WritePrivateProfileString("Startup", "foliage.UseOcclusionType", "0", txt);//去除树叶和灌木丛
                    API.WritePrivateProfileString("Startup", "ShowFlag.Refraction", "0", txt);//关闭折射
                    API.WritePrivateProfileString("Startup", "ShowFlag.Materials", "0", txt);//去除水下效果、致盲等
                    API.WritePrivateProfileString("Startup", "r.SceneColorFringe.Max", "0", txt);//去除水下效果、致盲等
                    API.WritePrivateProfileString("Startup", "ShowFlag.LightComplexity", "0", txt);//地图表面覆盖光，去除黑暗
                    API.WritePrivateProfileString("Startup", "ShowFlag.VisualizeLPV", "0", txt);//天空
                    API.WritePrivateProfileString("Startup", "ShowFlag.OverrideDiffuseAndSpecular", "0", txt);//覆盖反射效果
                    API.WritePrivateProfileString("Startup", "ShowFlag.StaticMeshes", "0", txt);//取消了大气雾的反射
                    API.WritePrivateProfileString("Startup", "ShowFlag.VisualizeSkyAtmosphere", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    API.WritePrivateProfileString("Startup", "ShowFlag.Atmosphere", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）
                    API.WritePrivateProfileString("Startup", "ShowFlag.DepthOfField", "0", txt);//略微去除雾气效果和远处景深（效果不明显，可以忽略）                  
                    API.WritePrivateProfileString("Startup", "ShowFlag.Navigation", "0", txt);//关闭粒子系统
                    API.WritePrivateProfileString("Startup", "ShowFlag.GameplayDebug", "0", txt);//除水画质，同时也除粒子
                    API.WritePrivateProfileString("Startup", "ShowFlag.Selection", "0", txt);//去除天空和水面,只保留地形
                    信息框("设置完成,重启游戏可载入画质", "提示:", 5000);
                }
                else
                {
                    API.写配置项(游戏路径, "Set", "Path", "");
                    信息框("请重启软件再试", "提示:", 5000);
                    Formclose();
                } 
            }
            catch (Exception)
            {
                API.写配置项(游戏路径, "Set", "Path", "");
                信息框("请重启软件再试", "提示:", 5000);
                Formclose();
            }

        }

        private void button1_Click(object sender, EventArgs e)//代码指令
        {
            API.网页_打开指定网址("http://www.teandy.com/code.html");
        }

        private void button26_Click(object sender, EventArgs e)//全生物索引
        {
            API.网页_打开指定网址("http://www.teandy.com/units/index.html");
        }

        private void button27_Click(object sender, EventArgs e)//宝箱位置图
        {
            API.网页_打开指定网址("http://www.teandy.com/resource/lootcrate.html");
        }

        private void button28_Click(object sender, EventArgs e)//资源采集表
        {
            API.网页_打开指定网址("http://www.teandy.com/resource/reseff.html");
        }

        private void button34_Click(object sender, EventArgs e)
        {
            API.网页_打开指定网址("http://www.teandy.com/units/carry1.html");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            API.网页_打开指定网址("https://island_ranger.gitee.io/hln-a-vault/#/creatures.html");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            API.网页_打开指定网址("https://island_ranger.gitee.io/hln-a-vault/#/items.html");
        }

        private void button37_Click(object sender, EventArgs e)
        {
            API.网页_打开指定网址("https://island_ranger.gitee.io/hln-a-vault/#/status-effects.html");
        }

        private void button38_Click(object sender, EventArgs e)
        {
            API.网页_打开指定网址("https://island_ranger.gitee.io/hln-a-vault/#/hexagon-exchange.html");
        }

        private void button30_Click(object sender, EventArgs e)//自定义食谱
        {
            API.网页_打开指定网址("http://www.teandy.com/custom.html");
        }

        private void button36_Click(object sender, EventArgs e)//颜色ID大全
        {
            API.网页_打开指定网址("http://www.teandy.com/color.html");
        }

        private void button35_Click(object sender, EventArgs e)
        {
            API.网页_打开指定网址("http://www.teandy.com/whistle.html");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            API.网页_打开指定网址("http://www.teandy.com/baike/index.html");
        }

        private void button32_Click(object sender, EventArgs e)
        {
            API.网页_打开指定网址("https://space.bilibili.com/386420067?spm_id_from=333.1007.0.0");
        }

        private void button39_Click(object sender, EventArgs e)
        {
            if (File.Exists(Environment.CurrentDirectory + "\\RM\\RuMengARKPath.ini") == true)
            {
               // API.信息框("请把配置文件详细路径写在Path=后面", "信息:", 5000);
                Process.Start(Environment.CurrentDirectory + "\\RM\\RuMengARKPath.ini");
            }
            else
            {
                File.WriteAllText(Environment.CurrentDirectory + "\\RM\\RuMengARKPath.ini", null, Encoding.Default);
                程序_延时(20);
                Process.Start(Environment.CurrentDirectory + "\\RM\\RuMengARKPath.ini");
            }
          
        }

        private void 限制60FPS_CheckedChanged(object sender, EventArgs e)//限制帧数
        {
            if (限制60FPS.Checked == true)
            {
                API.写配置项(功能配置路径, "Set", "60FPS", "1");
            }
            else
            {
                API.写配置项(功能配置路径, "Set", "60FPS", "0");
            }
        }

        private void button44_Click(object sender, EventArgs e)//教程
        {
            button7_Click_1(sender, e);
        }

        private void button45_Click(object sender, EventArgs e)//BUG修复
        {
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 346110", false);
                string installLocation = key.GetValue("InstallLocation", "").ToString();//获取安装路径
                string path = installLocation + "\\ShooterGame\\Content\\Localization\\Game\\zh\\ShooterGame.locres";
                string softwareName = key.GetValue("DisplayName", "").ToString();//获取软件名
                if (softwareName.IndexOf("ARK: Survival Evolved") != -1)
                {
                    API.文件_复制(Environment.CurrentDirectory + "\\RM\\ShooterGame.locres", path, 0);
                    信息框("替换成功,BUG修复完成", "提示:", 5000);
                }
            }
            catch
            {
                if (API.读配置项(游戏路径, "Set", "Path") != "")
                {
                    if (方舟未运行.Text != "方舟未运行")
                    {
                        try
                        {
                            // MessageBox.Show("游戏存在"+path);
                            if (API.文本_是否存在(path, "ARK") == true) //steam为真
                            {
                                string steam路径 = API.文本_取左边内容(API.GetWindowPath(句柄), "ARK") + "ARK\\ShooterGame\\Content\\Localization\\Game\\zh\\ShooterGame.locres";
                                API.文件_复制(Environment.CurrentDirectory + "\\RM\\ShooterGame.locres", steam路径, 0);
                                信息框("替换成功,BUG修复完成", "提示:", 5000);
                               // MessageBox.Show("steam路径" + steam路径);
                            }
                            else
                            {
                                string EPIC路径 = API.文本_取左边内容(API.GetWindowPath(句柄), "ARKSurvivalEvolved") + "ARKSurvivalEvolved\\ShooterGame\\Content\\Localization\\Game\\zh\\ShooterGame.locres";
                                API.文件_复制(Environment.CurrentDirectory + "\\RM\\ShooterGame.locres", EPIC路径, 0);
                                信息框("替换成功,BUG修复完成", "提示:", 5000);
                              //  MessageBox.Show("EPIC路径" + EPIC路径);
                            }
                        }
                        catch
                        {
                            信息框("替换失败!", "提示:", 5000);
                        }

                    }
                    
                }
                else
                {
                    信息框("路径不存在!", "提示:", 5000);
                }
            }
        }

        private void button46_Click(object sender, EventArgs e)//听雨网
        {
            Process.Start("http://www.teandy.com");
        }

        private void button47_Click(object sender, EventArgs e)
        {
            if (File.Exists(Environment.CurrentDirectory + "\\RM\\补丁说明.txt") ==true)
            {
                Process.Start(Environment.CurrentDirectory + "\\RM\\补丁说明.txt");
            }
            else
            {
                信息框("补丁说明.txt不存在", "提示:", 5000);
            }
        }
    }
}
