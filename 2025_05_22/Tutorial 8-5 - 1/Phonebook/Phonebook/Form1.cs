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
using System.Xml.Linq;
using System.Drawing.Text;

namespace Phonebook
{
    struct PhoneBookEntry
    {
        public string name;
        public string phone;
    }

    public partial class Form1 : Form
    {
        // Field to hold a list of PhoneBookEntry objects.
        private List<PhoneBookEntry> phoneList = 
            new List<PhoneBookEntry>();

        private object name ;
        private object phone;

        public Form1()
        {
            InitializeComponent();
        }

        // The ReadFile method reads the contents of the
        // PhoneList.txt file and stores it as PhoneBookEntry
        // objects in the phoneList.
        private void ReadFile()
        {
            StreamReader inputFile;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Open the file.
                    inputFile = File.OpenText(openFile.FileName);
                    string line;
                    while (!inputFile.EndOfStream)
                    {
                        line = inputFile.ReadLine().Trim();
                        // Split the line into name and phone number.
                        string[] parts = line.Split(',');
                        PhoneBookEntry entry;
                        entry.name = parts[0].Trim();
                        entry.phone = parts[1].Trim();
                        // Add the entry to the list.
                        phoneList.Add(entry);
                    }
                    // Close the file.
                    inputFile.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("檔案格式錯誤");
                }
            }
        }

        // The DisplayNames method displays the list of names
        // in the namesListBox control.
        private void DisplayNames()
        {
            foreach (PhoneBookEntry entry in phoneList)
            {
                // Add the name to the list box.
                nameListBox.Items.Add(entry.name);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           ReadFile();

            DisplayNames();
        }

        private void nameListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = nameListBox.SelectedIndex;
            if (index != -1)
            {
                // Display the phone number of the selected name.
                phoneLabel.Text = phoneList[index].phone;
            }
            else
            {
                // Clear the phone number text box.
                phoneLabel.Text = "無資料";
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // Close the form.
            this.Close();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void phoneLabel_Click(object sender, EventArgs e)
        {

        }

        private void selectedPhoneDescriptionLabel_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (name is string nameString && phone is string phoneString)
            {
                PhoneBookEntry entry;
                entry.name = nameString.Trim();
                entry.phone = phoneString.Trim();
                phoneList.Add(entry);
                nameListBox.Items.Add(entry.name);
            }
            else
            {
                MessageBox.Show("無效的姓名或電話號碼");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            string phone = phoneTextBox.Text;

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(phone))
            {
                AddPhoneBookEntry(name, phone);
                nameTextBox.Clear();
                phoneTextBox.Clear();
            }
            else
            {
                MessageBox.Show("請輸入姓名和電話號碼");
            }
        }
            private void AddPhoneBookEntry(string name, string phone)
            {
                PhoneBookEntry entry;
                entry.name = name;
                entry.phone = phone;
                phoneList.Add(entry);
                nameListBox.Items.Add(entry.name);
            }
        }
    }

