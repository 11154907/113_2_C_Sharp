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
        private OpenFileDialog openFileDialog; // Declare OpenFileDialog as an instance variable  

        public Form1()
        {
            InitializeComponent();
            openFileDialog = new OpenFileDialog(); // Initialize OpenFileDialog instance  
        }

        // The Average method accepts an int array argument  
        // and returns the Average of the values in the array.  
        private double Average(List<int> scores)
        {
            int total = 0;
        }

        // The Highest method accepts an int array argument  
        // and returns the highest value in that array.  
        private int Highest(int[] numbers)
        {
            return numbers.Max();
        }

        // The Lowest method accepts an int array argument  
        // and returns the lowest value in that array.  
        private int Lowest(int[] numbers)
        {
            return numbers.Min();
        }

        private void getScoresButton_Click(object sender, EventArgs e)
        {
            int[] textscores = new int[SIZE];
            int index = 0;
            int highestScore = 0;
            int lowestScore = 0;
            double averageScore = 0.0;
            StreamReader inputFile;
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK) // Use the instance variable  
                {
                    inputFile = File.OpenText(openFileDialog.FileName);
                    while (!inputFile.EndOfStream)
                    {
                        // Read the next score from the file.  
                        textscores[index] = Convert.ToInt32(inputFile.ReadLine());
                        index++;
                    }
                    inputFile.Close();
                    // Call the Average method and display the result.  
                    averageScore = Average(textscores);

                    highestScore = Highest(textscores);

                    lowestScore = Lowest(textscores);
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
