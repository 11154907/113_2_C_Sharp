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

        // 記錄原始檔案路徑
        private string teamsFilePath = "";
        private string winnerFilePath = "";

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
            teamsFilePath = SelectFileWithDialog("請選擇球隊資料檔案");
            if (string.IsNullOrEmpty(teamsFilePath))
            {
                MessageBox.Show("未選擇球隊資料檔案，程式將結束。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            // 讓使用者選擇冠軍資料檔案
            MessageBox.Show("請選擇冠軍資料檔案", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            winnerFilePath = SelectFileWithDialog("請選擇冠軍資料檔案");
            if (string.IsNullOrEmpty(winnerFilePath))
            {
                MessageBox.Show("未選擇冠軍資料檔案，程式將結束。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            // 讀取檔案
            readTeams(teamsFilePath);
            readWinner(winnerFilePath);

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
                teamList.Clear();
                listBox1.Items.Clear();
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
                winnerList.Clear();
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
            // 先處理 1903~2009（排除 1904、1994）
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
            // 處理 2010 年以後的資料
            while (winnerIndex < winnerList.Count)
            {
                int thisYear = endYear + (winnerIndex - (endYear - startYear + 1 - skipYears.Count)) + 1;
                if (thisYear > 2024) break;
                if (str == winnerList[winnerIndex])
                {
                    numWin++;
                    winYears.Add(thisYear);
                }
                winnerIndex++;
            }

            string yearsText = winYears.Count > 0 ? string.Join("、", winYears) : "無";
            int lastYear = winYears.Count > 0 ? winYears[winYears.Count - 1] : endYear;
            label1.Text = str + " 從 1903 年到 " + lastYear + " 年共獲得冠軍 " + numWin + " 次。\n奪冠年份：" + yearsText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 離開前將 teamList 與 winnerList 分別存回原始檔案
            try
            {
                // 儲存球隊資料
                using (StreamWriter sw = new StreamWriter(teamsFilePath, false, Encoding.UTF8))
                {
                    foreach (string team in teamList)
                    {
                        sw.WriteLine(team);
                    }
                }
                // 儲存冠軍資料
                using (StreamWriter sw = new StreamWriter(winnerFilePath, false, Encoding.UTF8))
                {
                    // 只儲存到 2024 年
                    int year = startYear;
                    int winnerIndex = 0;
                    int existedYearCount = 0;
                    // 1903~2009（排除 1904、1994）
                    while (year <= endYear && winnerIndex < winnerList.Count)
                    {
                        if (skipYears.Contains(year))
                        {
                            year++;
                            continue;
                        }
                        sw.WriteLine(winnerList[winnerIndex]);
                        year++;
                        winnerIndex++;
                        existedYearCount++;
                    }
                    // 2010~2024
                    int thisYear = endYear + 1;
                    while (winnerIndex < winnerList.Count && thisYear <= 2024)
                    {
                        sw.WriteLine(winnerList[winnerIndex]);
                        winnerIndex++;
                        thisYear++;
                    }
                }
                MessageBox.Show("資料已成功儲存，程式即將結束。", "儲存完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("儲存資料時發生錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 提示使用者選擇 2010 年以後的 MLB 冠軍資料檔案
            MessageBox.Show("請選擇 2010 年以後的 MLB 冠軍資料檔案", "新增資料", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string newWinnerFile = SelectFileWithDialog("請選擇 2010 年以後的 MLB 冠軍資料檔案");
            if (string.IsNullOrEmpty(newWinnerFile))
            {
                MessageBox.Show("未選擇檔案，未進行更新。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 計算目前 winnerList 中已經有的年份數量（不含 1904、1994）
            int existedYearCount = 0;
            for (int y = startYear; y <= endYear; y++)
            {
                if (!skipYears.Contains(y))
                    existedYearCount++;
            }
            int nextYear = endYear + 1 + (winnerList.Count - existedYearCount);

            // 讀取新資料並更新 winnerList
            try
            {
                using (StreamReader inputFile = File.OpenText(newWinnerFile))
                {
                    string line;
                    while ((line = inputFile.ReadLine()) != null)
                    {
                        // 若年份超過 2024 則停止
                        if (nextYear > 2024)
                            break;

                        winnerList.Add(line);
                        // 若有新隊伍則加入 teamList 與 listBox1
                        if (!teamList.Contains(line))
                        {
                            teamList.Add(line);
                            listBox1.Items.Add(line);
                        }
                        nextYear++;
                    }
                }
                MessageBox.Show("資料已成功新增並更新顯示。", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀取新增冠軍資料時發生錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
