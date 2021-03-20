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

        private void button2_Click(object sender, EventArgs e)
        {
            //TODO Felhantering.
            localPath = openFileDialog.FileName.Split('.')[0];
            WordList list = WordList.LoadList(localPath);

        }

    }
}
