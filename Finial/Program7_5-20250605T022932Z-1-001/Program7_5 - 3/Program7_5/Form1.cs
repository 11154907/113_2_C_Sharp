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
    // 定義 TeamData 結構，包含球隊名稱、獲勝次數、獲勝年份清單
    public struct TeamData
    {
        public string TeamName;           // 球隊名稱
        public int WinCount;              // 獲勝次數
        public List<int> WinYears;        // 獲勝年份清單

        public TeamData(string name)
        {
            TeamName = name;
            WinCount = 0;
            WinYears = new List<int>();
        }
    }

    public partial class Form1 : Form
    {
        // 宣告 OpenFileDialog 元件，供使用者選擇檔案
        private OpenFileDialog openFileDialog1 = new OpenFileDialog();

        // 使用 List<TeamData> 來儲存所有球隊資料
        List<TeamData> teamDataList = new List<TeamData>();
        // 使用 List<string> 來儲存每年冠軍球隊名稱
        List<string> winnerList = new List<string>();

        // MLB 冠軍年份起始與結束（1903~2009，1904與1994未舉辦）
        int startYear = 1903;
        int endYear = 2009;
        HashSet<int> skipYears = new HashSet<int> { 1904, 1994 };

        // 讀取 Teams.txt 檔案，建立 teamDataList
        private void readTeams(string filePath)
        {
            try
            {
                teamDataList.Clear();
                using (StreamReader inputFile = File.OpenText(filePath))
                {
                    string line;
                    while ((line = inputFile.ReadLine()) != null)
                    {
                        // 建立 TeamData 結構並加入清單
                        teamDataList.Add(new TeamData(line));
                        listBox1.Items.Add(line); // 若有 listBox1 也同步顯示
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀取球隊資料時發生錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 讀取 WorldSeries.txt 檔案，建立 winnerList
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

        // 將重複的 readTeams 方法重新命名為 ReadTeamsFile 以解決 CS0111，並依 IDE1006 命名規範改為 PascalCase。
        private void ReadTeamsFile(string filePath)
        {
            try
            {
                teamDataList.Clear();
                listBox1.Items.Clear();
                using (StreamReader inputFile = File.OpenText(filePath))
                {
                    string line;
                    while ((line = inputFile.ReadLine()) != null)
                    {
                        // 建立 TeamData 結構並加入清單
                        teamDataList.Add(new TeamData(line));
                        listBox1.Items.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀取球隊資料時發生錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 將重複的 readWinner 方法重新命名為 ReadWinnerFile 以解決 CS0111，並依 IDE1006 命名規範改為 PascalCase。
        private void ReadWinnerFile(string filePath)
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

        // 依 winnerList 統計每隊獲勝次數與年份，更新 teamDataList
        private void UpdateTeamDataList()
        {
            // 先清空每隊的 WinCount 與 WinYears
            for (int i = 0; i < teamDataList.Count; i++)
            {
                TeamData td = teamDataList[i];
                td.WinCount = 0;
                td.WinYears = new List<int>();
                teamDataList[i] = td;
            }

            int year = startYear;
            int winnerIndex = 0;
            // 1903~2009（排除 1904、1994）
            while (year <= endYear && winnerIndex < winnerList.Count)
            {
                if (skipYears.Contains(year))
                {
                    year++;
                    continue;
                }
                string winner = winnerList[winnerIndex];
                int idx = teamDataList.FindIndex(t => t.TeamName == winner);
                if (idx >= 0)
                {
                    TeamData td = teamDataList[idx];
                    td.WinCount++;
                    td.WinYears.Add(year);
                    teamDataList[idx] = td;
                }
                year++;
                winnerIndex++;
            }
            // 2010年以後
            int thisYear = endYear + 1;
            while (winnerIndex < winnerList.Count && thisYear <= 2024)
            {
                string winner = winnerList[winnerIndex];
                int idx = teamDataList.FindIndex(t => t.TeamName == winner);
                if (idx >= 0)
                {
                    TeamData td = teamDataList[idx];
                    td.WinCount++;
                    td.WinYears.Add(thisYear);
                    teamDataList[idx] = td;
                }
                thisYear++;
                winnerIndex++;
            }
        }

        // 讀取完檔案後，呼叫此方法
        private void AfterReadFiles()
        {
            UpdateTeamDataList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;

            string teamName = listBox1.SelectedItem.ToString();
            // 從 teamDataList 找到該隊伍的 TeamData
            TeamData? data = teamDataList.FirstOrDefault(td => td.TeamName == teamName);

            if (data != null)
            {
                string yearsText = data.Value.WinYears.Count > 0
                    ? string.Join("、", data.Value.WinYears)
                    : "無";
                int lastYear = data.Value.WinYears.Count > 0
                    ? data.Value.WinYears[data.Value.WinYears.Count - 1]
                    : 2009;
                label1.Text = teamName + " 從 1903 年到 " + lastYear +
                    " 年共獲得冠軍 " + data.Value.WinCount + " 次。\n奪冠年份：" + yearsText;
            }
            else
            {
                label1.Text = teamName + " 查無資料。";
            }
        }

        // 其餘程式碼（如載入、檔案選擇、讀檔、查詢等）可依原本邏輯進行
        // 只需將 teamList 替換為 teamDataList，並在查詢時更新 WinCount 與 WinYears
    }
}
