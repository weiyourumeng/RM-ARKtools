using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace 入梦方舟小工具
{
    public partial class Form2 : Form
    {
      
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

      
        public bool 窗口_置位置和大小(IntPtr 窗口句柄, int 左边位置, int 顶边位置, int 新宽度, int 新高度)
        {
            return (MoveWindow(窗口句柄, 左边位置, 顶边位置, 新宽度, 新高度, true));
        }
        public Form2()
        {
            InitializeComponent();
            this.Location = new Point(0,0);
            this.Size = new Size(90,30);
            窗口_置位置和大小(this.Handle, 0, 0, 110, 20);
            label1.Location = new Point(0,0);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }
    }
}
