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
        float wScale = 1.0f;
        double Max = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            List<int> pData = new List<int>();//通道数组              
            int count = 0;//通道长度
            {
                string text = System.IO.File.ReadAllText(@"C:\Users\沈鑫晨\Desktop\0\log_real1_.txt");
                char[] chs = { ' ' };
                string[] res = text.Split(chs, options: StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in res)
                {
                    pData.Add(Convert.ToInt32(item));
                }
                count = pData.Count();
            }
            for (int i = 0; i < pData.Count(); i++)
            {
                chart1.Series[0].Points.AddXY(i, pData[i]);
            }




            int w = 64;
            const int c1 = 3;
            int[] arr = new int[1 + c1 * 2];
            double[] data = new double[count];


            

            for (int j = 0; j < c1; j++)
			{
				// 中值滤波
				int c2 = 1 + j * 2;
                for (int k = 0; k < count; k++)
				{
					int n = pData[k];
					if (j > 0 && k >= j && k < count - j )
					{
						for (int l = 0; l < c2; l++)
						{
							arr[l] = pData[k - j + l];
						}
						Array.Sort(arr);
						n = arr[j];
					}
					data[k] = n;

                }

            }
                // 滑动窗口内最大偏差(比较基准：直线方程，首尾点连线，之前已经做了中值滤波，无需再计算均值点)
            for (int k = w; k < count; k++)
			{
                double vMax = 0;
                {						
                    Point pt1 = new Point(0, data[k - w]);
                    Point pt2 = new Point(w - 1, data[k - 1]);
                    // 计算斜率
                    double slope = (pt2.Y - pt1.Y) / (pt2.X - pt1.X);
                    // 计算y轴截距
                    double yIntercept =(pt1.Y - slope * pt1.X);
                    for (int l = 1; l < w - 1; l++)
                    {
                        Point pt = new Point(l, data[k - w + l]);
                        double dist = Math.Abs((slope * pt.X + yIntercept) - pt.Y) / Math.Sqrt(slope * slope + 1);
                        if (vMax < dist)
                        {
                            vMax = dist;
                        }
                    }                        
                       
                }
                if (Max < vMax)
                {
                    Max = vMax;
                }
            }
			
            for (int i = 0; i < data.Length; i++)
            {
                chart2.Series[0].Points.AddXY(i, data[i]);
            }

            textBox1.Text = Max.ToString();


        }




    }
}
