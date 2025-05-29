using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell_Phone_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The GetPhoneData method accepts a CellPhone object
        // as an argument. It assigns the data entered by the
        // user to the object's properties.
        private void GetPhoneData(CellPhone phone)
        {
            decimal price;

            phone.Brand = brandTextBox.Text;
            phone.Model = modelTextBox.Text;

            if (decimal.TryParse(priceTextBox.Text, out price))
            {
                phone.Price = price;
            }
            else
            {
                // 如果價格輸入無效，顯示錯誤訊息並清除價格欄位
                MessageBox.Show("Please enter a valid price.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                priceTextBox.Clear();
               
            }
        }

        private void createObjectButton_Click(object sender, EventArgs e)
        {
            //建立一個新的 CellPhone 物件
            CellPhone myphone = new CellPhone();

            //呼叫 GetPhoneData 方法，將 myphone 作為參數傳遞
            GetPhoneData(myphone);

            brandLabel.Text = myphone.Brand;
            modelLabel.Text = myphone.Model;
            priceLabel.Text = myphone.Price.ToString("C2"); // 格式化為貨幣格式
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // Close the form.
            this.Close();
        }
    }
}
