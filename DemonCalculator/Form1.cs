using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemonCalculator
{
    public partial class Form1 : Form
    {
        List<double> moneylist = new List<double>();
        int index = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                double basicProperty = double.Parse(textBox1.Text);
                double price = double.Parse(textBox2.Text);
                int step = int.Parse(textBox3.Text);

                if (chart1.Series[0].Points.Count == 0) { 
                    chart1.Series[0].Points.Add(0);
                    chart1.Series[0].Points.Add(price);
                    moneylist.Add(0);
                    moneylist.Add(price);
                }

                for (int i = 0; i < step; i++)
                {
                    next(0, index,price, basicProperty);
                    index++;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error Input");
            }
        }

        public void next(int seriesIndex, int x,double price,double basic)
        {
            double money = moneylist[x];
            double profit = priceDownByProperty(price, money, basic);

            moneylist.Add(money + profit);
            //chart1.Series[0].Points.Add(profit + money);
            chart1.Series[0].Points.AddXY(Math.Round(money + profit, 0), profit);
        }

        /// <summary>
        /// 计算函数
        /// </summary>
        /// <param name="price">价格</param>
        /// <param name="property">当前资产</param>
        /// <param name="basicProperty">基线</param>
        /// <returns></returns>
        public static double priceDownByProperty(double price, double property, double basicProperty)
        {
            return ((price) / Math.Pow(Math.Exp((property / basicProperty)), 0.5)
                    + (price / (1 + property / basicProperty))) / 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            moneylist.Clear();
            index = 1;
            chart1.Series[0].Points.Clear();
        }
    }
}
