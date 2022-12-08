using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quantify_alg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region 读取采集数据
            List<int> real1 = new List<int>();
            string text = System.IO.File.ReadAllText(@"E:\code\CSharpvscode\EDDY\log_real1_.txt");
            char[] chs = { ' ' };
            string[] res = text.Split(chs, options: StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in res)
            {
                real1.Add(Convert.ToInt16(item));
            }
            #endregion

        }
    }
}
