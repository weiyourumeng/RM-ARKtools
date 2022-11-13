using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using 入梦方舟小工具.方法;


namespace 入梦方舟小工具
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        API aPI = new API();
        public string 功能配置路径 = "\\RM\\RuMengARK.ini";

        private void Form3_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            this.TransparencyKey = Color.White;
            this.Width = int.Parse(API.读配置项(功能配置路径, "Set", "textBox60"));
            this.Height= int.Parse(API.读配置项(功能配置路径, "Set", "textBox65"));      
            
        }
    }
}
