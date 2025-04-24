using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Test_Average
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The Average method accepts an int array argument
        // and returns the Average of the values in the array.
        private double  Average( List<int> scores  )
        {
            int total = 0;
            foreach (int score in scores)
            {
                total += score;
            }   
        }

        // The Highest method accepts an int array argument
        // and returns the highest value in that array.
        private int  Highest(List<int> scores    )
        {
            int highest = scores[0];
            for (int i = 1; i < scores.Count; i++)
            {
                if (scores[i] > highest)
                {
                    highest = scores[i];
                }
            }
        }

        // The Lowest method accepts an int array argument
        // and returns the lowest value in that array.
        private  Lowest(   )
        {
           
        }

        private void getScoresButton_Click(object sender, EventArgs e)
        {
           
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // Close the form.
            this.Close();
        }
        private List<int> testScores = new List<int>();

        private void deleteButton_Click(object sender, EventArgs e)
        {
            // 確認是否有選取項目
            if (testScoresListBox.SelectedItem == null)
            {
                MessageBox.Show("請選擇要刪除的成績。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 取得選取的成績
            int selectedScore = (int)testScoresListBox.SelectedItem;

            // 從 testScores 中移除選取的成績
            testScores.Remove(selectedScore);

            // 更新 testScoresListBox 的顯示內容
            testScoresListBox.Items.Clear();
            foreach (int score in testScores)
            {
                testScoresListBox.Items.Add(score);
            }

            // 更新 SortedScoresListBox 的顯示內容
            SortedScoresListBox.Items.Clear();
            foreach (int score in testScores.OrderBy(s => s))
            {
                SortedScoresListBox.Items.Add(score);
            }
        }
    }
}
