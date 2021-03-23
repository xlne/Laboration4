﻿using System;
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
        public delegate WordList LoadWordList(string name);

        public static string localPath = WordList.localPath;
        public string fileName;

        //button-controls
        //btn_NewWord.Enabled = false;


        public Form1()
        {
            InitializeComponent();
        }

        private void btn_viewList_Click(object sender, EventArgs e) //Load list-button
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

                    string output = string.Join(',', lang);
                    listView1.Items.Add(output);

                    //for (int i = 0; i < lang.Length; i++)
                    //{
                    //    listView1.Items.Add(lang[i]);
                    //}
                    label1.Visible = true;
                    label1.Text = "Number of words: " + list.Count().ToString(); // +""; gör int till en string. så den vet det.
                }
            }
            btn_NewWord.Enabled = true;
            btn_removeWord.Enabled = true;
            btn_sortList.Enabled = true;
            btn_practice.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e) //New word-knappen.
        {
            if (listView1 != null)
            {
                LoadWordList wordList = new LoadWordList(WordList.LoadList);
                WordList wordList1 = wordList.Invoke(localPath);
                string[] languages = wordList1.Languages;
                bool areWordsAdded = false;

                while (true)
                {
                    string[] translations = new string[wordList1.Languages.Length];
                    string firstTranslation = translations[0] = Interaction.InputBox($"Type a word in {wordList1.Languages[0]}");

                    if (firstTranslation == string.Empty)
                        break;

                    for (int i = 1; i < translations.Length; i++)
                    {
                        string wordsToTranslate;
                        do
                        {
                            wordsToTranslate = Interaction.InputBox($"Translate {firstTranslation} to {wordList1.Languages[i]}");

                        } while (wordsToTranslate == string.Empty);

                        translations[i] = wordsToTranslate;
                    }
                    wordList1.Add(translations);
                    areWordsAdded = true;
                }
                if (areWordsAdded)
                    wordList1.Save();


            }
            else if (listView1 == null)
            {
                MessageBox.Show("You have to choose a list to add words to.", "Warning!");
                btn_NewWord.Enabled = false;
            }


            //TODO Felhantering.
            //localPath = openFileDialog.FileName.Split('.')[0];
            //WordList list = WordList.LoadList(localPath);

        }

        private void button4_Click(object sender, EventArgs e) //New list - button
        {
            //TODO ta bort andra rutan att språk lagts till?? 
            string fileNameInput = Interaction.InputBox("Enter the name of the new file.\n " + " Exclude the file type.", "New list", "", -1, -1);
            if (fileNameInput == "" || fileNameInput == " ")
            {
                MessageBox.Show("Invalid input. Filename can't be empty.", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string languagesInput = Interaction.InputBox($"Enter which languages to be in \"{fileNameInput}\"." +
                    $"\nSeparate the languages with semicolon.", "Languages", "", -1, -1);
                if (languagesInput == "" || languagesInput == " ")
                {
                    MessageBox.Show("Invalid input. Languages needs to be added.", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        var fileCreated = File.Create(Path.Combine(localPath, fileNameInput + ".dat"));
                        fileCreated.Close();
                        MessageBox.Show(fileNameInput + ".dat created successfully.", "New list created");

                        File.WriteAllText(Path.Combine(localPath, fileNameInput + ".dat"), languagesInput);

                        MessageBox.Show($"{languagesInput} added to {fileNameInput}", "Languages added successfully.");
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message, "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                                
                LoadWordList wordList = new LoadWordList(WordList.LoadList);
                WordList wordList1 = wordList.Invoke(fileNameInput);
                string[] languages = wordList1.Languages; 
                bool areWordsAdded = false;

                while (true)
                {
                    string[] translations = new string[wordList1.Languages.Length];
                    string firstTranslation = translations[0] = Interaction.InputBox($"Type a word in {wordList1.Languages[0]}");

                    if (firstTranslation == string.Empty)
                        break;

                    for (int i = 1; i < translations.Length; i++)
                    {
                        string wordsToTranslate;
                        do
                        {
                            wordsToTranslate = Interaction.InputBox($"Translate {firstTranslation} to {wordList1.Languages[i]}");

                        } while (wordsToTranslate == string.Empty);

                        translations[i] = wordsToTranslate;
                    }
                    wordList1.Add(translations);
                    areWordsAdded = true;
                }
                if (areWordsAdded)
                    wordList1.Save();
            }
        }

        private void button3_Click(object sender, EventArgs e) //remove-button
        {
            LoadWordList wordList = new LoadWordList(WordList.LoadList);
            WordList wordList1 = wordList.Invoke(localPath);
            string[] languages = wordList1.Languages;
            string languagesInList = Interaction.InputBox("Choose the language you want to remove a word from: ");
            string[] wordsToBeRemoved = Array.Empty<string>();
            int langIndex = -1;
            //TODO EJ FÄRDIG METOD. Får ej in ord att radera i arrayen.
            for (int i = 0; i < languages.Length; i++)
            {
                string inputRemoveWord = Interaction.InputBox("Please enter a word you wish to remove", "Remove word");
                wordsToBeRemoved[i] = inputRemoveWord;

            }

            for (int i = 0; i < languages.Length; i++)
            {
                if (languages[i].Equals(languagesInList, StringComparison.InvariantCultureIgnoreCase))
                {
                    langIndex = i;
                    break;
                }
            }

            if (langIndex > -1)
            {
                foreach (var w in wordsToBeRemoved)
                {
                    if (wordList1.Remove(langIndex, w))
                    {
                        Console.WriteLine($"{w} was removed from list.");
                    }
                    else
                    {
                        Console.WriteLine("Could not find the word.");
                    }
                }
                wordList1.Save();
            }
        }

        private void btn_sortList_Click(object sender, EventArgs e)
        {
            LoadWordList wordList = new LoadWordList(WordList.LoadList);
            WordList wordList1 = wordList.Invoke(localPath);
            string[] languages = wordList1.Languages;
            string sortByLanguage = Interaction.InputBox("Type the language you want to sort as...");

            Action<string[]> showTranslations = (string[] translations) =>
            {
                string output = string.Join(',', translations);
                
                listView1.Items.Add(output);
            };

            if (sortByLanguage == "")
            {
                wordList1.List(0, showTranslations);
                return;
            }

            for (int i = 0; i < languages.Length; i++)
            {
                if (sortByLanguage == languages[i])
                {
                    wordList1.List(i, showTranslations);
                    break;
                }
            }
        }

        private void btn_practice_Click(object sender, EventArgs e)
        {
            LoadWordList wordList = new LoadWordList(WordList.LoadList);
            WordList wordList1 = wordList.Invoke(localPath);

            int totalGuesses = 0;
            int correctAnswers = 0;

            while (true)
            {
                Word word = wordList1.GetWordToPractice();
                
                string input = Interaction.InputBox(
                    $"Translate the \"{wordList1.Languages[word.FromLanguage]}\" word: " +
                    $"{word.Translations[word.FromLanguage]}, " +
                    $"to \"{wordList1.Languages[word.ToLanguage]}\"" +
                    $"\n\nLeave empty to exit"
                    , "Practice: Translations input");

                if (input == "" || input == " ")
                {
                    MessageBox.Show($"You guessed correct {correctAnswers} out of {totalGuesses} times.", "Result");
                    
                    break;
                }
                else if (input == word.Translations[word.ToLanguage])
                {
                    MessageBox.Show("That is the correct answer!", "Correct");
                    
                    totalGuesses++;
                    correctAnswers++;
                    continue;
                }
                else
                {
                    MessageBox.Show("Wrong answer. :( \n" +
                        "Let's try a another word.", "Wrong");
                    
                    totalGuesses++;
                    continue;
                }
            }
        }


    }
}

