using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Program5_14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader inputFile;
            int sum = 0; //宣告變數sum，用來存放總和
            int count = 0; //宣告變數count，用來存資料筆數
            int temp; //宣告變數temp，用來存放讀取的資料

            try
            {
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    inputFile = File.OpenText(openFile.FileName);

                    while (!inputFile.EndOfStream) //當沒有讀到檔案結尾時(代表檔案中還有資料)


                    {
                        count++; //每讀一筆資料，count加1
                        temp = int.Parse(inputFile.ReadLine());
                        sum += temp;
                        listBox1.Items.Add(temp);
                    }
                    listBox1.Items.Add("總共有" + count + "個數字\n總和 :" + sum);
                    inputFile.Close();
                }
                else
                {
                    MessageBox.Show("沒有選擇檔案");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
            
            
    }
}
