using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Color_Spectrum
{
    enum Spectrum
    {
        紅色, 橙色, 黃色, 綠色,
        藍色, 靛色, 紫色
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 將所有與顏色相關的 label 的 Text 屬性設為 null  
            redLabel.Text = null;
            orangeLabel.Text = null;
            yellowLabel.Text = null;
            greenLabel.Text = null;
            blueLabel.Text = null;
            indigoLabel.Text = null;
            violetLabel.Text = null;
            colorLabel.Text = null;
        }

        // DisplayColor 方法顯示顏色的名稱  
        private void DisplayColor(Spectrum color)
        {
            colorLabel.Text = color.ToString();
        }

        private void redLabel_Click(object sender, EventArgs e)
        {
            DisplayColor(Spectrum.紅色);
        }

        private void orangeLabel_Click(object sender, EventArgs e)
        {
            DisplayColor(Spectrum.橙色);
        }

        private void yellowLabel_Click(object sender, EventArgs e)
        {
            DisplayColor(Spectrum.黃色);
        }

        private void greenLabel_Click(object sender, EventArgs e)
        {
            DisplayColor(Spectrum.綠色);
        }

        private void blueLabel_Click(object sender, EventArgs e)
        {
            DisplayColor(Spectrum.藍色);
        }

        private void indigoLabel_Click(object sender, EventArgs e)
        {
            DisplayColor(Spectrum.靛色);
        }

        private void violetLabel_Click(object sender, EventArgs e)
        {
            DisplayColor(Spectrum.紫色);
        }
    }
}
