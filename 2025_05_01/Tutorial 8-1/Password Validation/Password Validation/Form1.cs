using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Password_Validation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // NumberUpperCase 方法接受一個字串作為參數，
        // 並回傳該字串中包含的大寫字母數量。
        private int NumberUpperCase(string str)
        {
            int upperCaseCount = 0; // 計數器，初始值為 0
            foreach (char c in str)
            {
                if (char.IsUpper(c)) // 檢查字元是否為大寫字母
                {
                    // 如果是大寫字母，計數器加 1
                    upperCaseCount++;
            }
        }
            return upperCaseCount; // 回傳大寫字母的數量
        }



        // NumberLowerCase 方法接受一個字串作為參數，
        // 並回傳該字串中包含的小寫字母數量。
        private int NumberLowerCase(string str)
        {
            int lowerCaseCount = 0; // 計數器，初始值為 0
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsLower(str[i])) // 檢查字元是否為小寫字母
                {
                    // 如果是小寫字母，計數器加 1
                    lowerCaseCount++;
                }
            }
            return lowerCaseCount; // 回傳小寫字母的數量
        }

        // NumberDigits 方法接受一個字串作為參數，
        // 並回傳該字串中包含的數字字元數量。
        private int NumberDigits(string str)
        {
            int digits = 0;
            foreach (char c in str)
            {
                if (char.IsDigit(c)) // 檢查字元是否為數字
                {
                    // 如果是數字，計數器加 1
                    digits++;
                }
            }
            return digits; // 回傳數字的數量
        }

        // 當使用者按下檢查密碼按鈕時，觸發此事件處理方法。
        // 此方法應包含檢查密碼是否符合特定條件的邏輯。
        private void checkPasswordButton_Click(object sender, EventArgs e)
        {
            const int MIN_LENGTH = 8; // 密碼的最小長度
            string password = passwordTextBox.Text; // 取得使用者輸入的密碼
            if ( password.Length >= MIN_LENGTH && 
                   NumberUpperCase(password) > 0 &&
                   NumberLowerCase(password) > 0 &&
                   NumberDigits(password) > 0)
            {
                MessageBox.Show("密碼有效","密碼檢查結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("密碼無效", "密碼檢查結果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        // 當使用者按下離開按鈕時，觸發此事件處理方法。
        // 此方法會關閉目前的表單。
        private void exitButton_Click(object sender, EventArgs e)
        {
            // 關閉表單。
            this.Close();
        }
    }
}
