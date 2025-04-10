using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lottery_Numbers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
           const int SIZE = 5; // 陣列大小
            int[] lotteryNumbers = new int[SIZE];  
            Random rand = new Random();

            // 使用迴圈生成5個1到42之間的隨機數，並存入lotteryNumbers陣列中
            for (int i = 0; i < lotteryNumbers.Length; i++)
            {
                // 產生1到42之間的亂數(包含1和42)，確認產生的亂數沒有與陣列中元素重複，在放入陣列中
                int number;
                do
                {
                    number = rand.Next(1, 43);
                } while (lotteryNumbers.Contains(number));
                lotteryNumbers[i] = number; // 將隨機數放入陣列中 
            }

            //將lotteryNumbers 陣列中的數字由小到大排序
            Array.Sort(lotteryNumbers);  //使用Array.Sort()方法將 lotteryNumbers陣列中的數字進行排序




            Label[] showLabel = { firstLabel, secondLabel, thirdLabel, fourthLabel, fifthLabel }; // 將5個Label放入陣列中

            for (int i = 0; i < lotteryNumbers.Length; i++)
            {
                showLabel[i].Text = lotteryNumbers[i].ToString(); // 將隨機數顯示在Label上
            }
        }

        

        private void exitButton_Click(object sender, EventArgs e)
        {
            // Close the form.
            this.Close();
        }
    }
}
