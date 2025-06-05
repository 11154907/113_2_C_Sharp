using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Program7_5
{ 
public partial class Form1 : Form
    {
        // 宣告 OpenFileDialog 元件，供使用者選擇檔案
        private OpenFileDialog openFileDialog1 = new OpenFileDialog();

        public Form1()
        {
            InitializeComponent();
            // 設定表單標題為繁體中文
            this.Text = "世界大賽冠軍查詢";
        }

        // 使用 List<string> 來儲存球隊名稱
        List<string> teamList = new List<string>();
        // 使用 List<string> 來儲存每年冠軍球隊名稱
        List<string> winnerList = new List<string>();

        // MLB 冠軍年份起始與結束（1903~2009，1904與1994未舉辦）
        int startYear = 1903;
        int endYear = 2009;
        HashSet<int> skipYears = new HashSet<int> { 1904, 1994 };

        // 表單載入時執行，透過對話方塊選擇檔案並讀取球隊與冠軍資料
        private void Form1_Load(object sender, EventArgs e)
        {
            // 讓使用者選擇球隊資料檔案
            MessageBox.Show("請先選擇球隊資料檔案", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string teamsFile = SelectFileWithDialog("請選擇球隊資料檔案");
            if (string.IsNullOrEmpty(teamsFile))
            {
                MessageBox.Show("未選擇球隊資料檔案，程式將結束。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }
            

            // 讓使用者選擇冠軍資料檔案
            MessageBox.Show("請選擇冠軍資料檔案", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string winnerFile = SelectFileWithDialog("請選擇冠軍資料檔案");
            if (string.IsNullOrEmpty(winnerFile))
            {
                MessageBox.Show("未選擇冠軍資料檔案，程式將結束。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }
            

            // 讀取檔案
            readTeams(teamsFile);
            readWinner(winnerFile);

            // 設定提示標籤為繁體中文
            label1.Text = "請選擇一支球隊以顯示冠軍次數";
        }

        // 使用 openFileDialog1 讓使用者選擇檔案，回傳檔案路徑
        private string SelectFileWithDialog(string title)
        {
            openFileDialog1.Title = title;
            openFileDialog1.Filter = "文字檔案 (*.txt)|*.txt|所有檔案 (*.*)|*.*";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }
            else
            {
                return null;
            }
        }

        // 讀取 Teams.txt 檔案，將球隊名稱加入 listBox1 並存入 teamList
        private void readTeams(string filePath)
        {
            try
            {
                using (StreamReader inputFile = File.OpenText(filePath))
                {
                    string line;
                    while ((line = inputFile.ReadLine()) != null)
                    {
                        listBox1.Items.Add(line);
                        teamList.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀取球隊資料時發生錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 讀取 WorldSeries.txt 檔案，將每年冠軍球隊名稱存入 winnerList
        private void readWinner(string filePath)
        {
            try
            {
                using (StreamReader inputFile = File.OpenText(filePath))
                {
                    string line;
                    while ((line = inputFile.ReadLine()) != null)
                    {
                        winnerList.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀取冠軍資料時發生錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 當使用者選取 listBox1 中的球隊時，計算該球隊奪冠次數並顯示於 label1，並列出奪冠年份
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = listBox1.SelectedItem.ToString();
            int numWin = 0;
            List<int> winYears = new List<int>();

            // 計算該球隊奪冠次數並記錄年份
            int year = startYear;
            int winnerIndex = 0;
            while (year <= endYear && winnerIndex < winnerList.Count)
            {
                if (skipYears.Contains(year))
                {
                    year++;
                    continue;
                }
                if (str == winnerList[winnerIndex])
                {
                    numWin++;
                    winYears.Add(year);
                }
                year++;
                winnerIndex++;
            }

            // 組合年份字串
            string yearsText = winYears.Count > 0 ? string.Join("、", winYears) : "無";
            // 將結果顯示於 label1（繁體中文）
            label1.Text = str + " 從 1903 年到 2009 年共獲得冠軍 " + numWin + " 次。\n奪冠年份：" + yearsText;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void openFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
