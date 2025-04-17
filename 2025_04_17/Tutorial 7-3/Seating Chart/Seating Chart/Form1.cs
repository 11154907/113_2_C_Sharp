using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seating_Chart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void displayPriceButton_Click(object sender, EventArgs e)
        {
            const int ROWS = 6;
            const int COLS = 4;

            int row;
            int col;

            //定義座位價格的二微陣列，每個元素代表對應價格
            decimal[,] seatprices =  { {450m, 450m, 450m, 450m},
                                 {425m, 425m, 425m, 425m},
                                 {400m, 400m, 400m, 400m},
                                 {375m, 375m, 375m, 375m},
                                 {375m, 375m, 375m, 375m},
                                 {350m, 350m, 350m, 350m}
                               };

            // Declare variables to store parsed row and column values.  
           

            if (int.TryParse(rowTextBox.Text, out row))
            {
                if (int.TryParse(colTextBox.Text, out col))
                {
                    if (row >= 0 && row < ROWS)
                    {
                        if (col >= 0 && col < COLS)
                        { 
                            MessageBox.Show("The price of the seat is " + seatprices[row, col].ToString("C"));
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("輸入有效的行號");
                        rowTextBox.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("輸入有效的列號");
                    colTextBox.Focus();
                    return;
                }
            }
        }


        private void exitButton_Click(object sender, EventArgs e)
        {
            // Close the form.
            this.Close();
        }
    }
}
