using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;
using System.IO;
using Microsoft.VisualBasic;


namespace WinFormsVocables
{
    public partial class Form1 : Form
    {
        public static string localPath = WordList.localPath;
        public string fileName;

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_viewList_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = localPath;
                openFileDialog.Filter = "(*.dat)|*.dat";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    listView1.Items.Clear();
                    localPath = openFileDialog.FileName.Split('.')[0];

                    WordList list = WordList.LoadList(localPath);
                    string[] lang = list.Languages;
                    for (int i = 0; i < lang.Length; i++)
                    {
                        listView1.Items.Add(lang[i]);
                    }
                    label1.Visible = true;
                    label1.Text = "Number of words: " + list.Count().ToString(); // +""; gör int till en string. så den vet det.
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) //New-knappen.
        {


            //TODO Felhantering.
            //localPath = openFileDialog.FileName.Split('.')[0];
            //WordList list = WordList.LoadList(localPath);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string fileNameInput = Interaction.InputBox("Please type the name of the new file you want to create." +
                " Exclude the file type.", "New list", "", -1, -1);
            if (fileNameInput == "" || fileNameInput == " ")
            {
                MessageBox.Show("Invalid input. Filename can't be empty.", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var fileCreated = File.Create(Path.Combine(localPath, fileNameInput + ".dat"));
                fileCreated.Close();
                MessageBox.Show(fileNameInput + ".dat created successfully.", "New list created");

                string languagesInput = Interaction.InputBox($"Please type the languages you want in {fileNameInput}." +
                    $"\nSeparate languages with semicolon.", "Languages", "", -1, -1);
                if (languagesInput == "" || languagesInput == " ")
                {
                    MessageBox.Show("Invalid input. Languages needs to be added.", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        File.WriteAllText(Path.Combine(localPath, fileNameInput + ".dat"), languagesInput);
                        
                        MessageBox.Show($"{languagesInput} added to {fileNameInput}", "Languages added successfully.");
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message, "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //TODO Make loop to enter new words into the new list.
            }

        }
    }
}
