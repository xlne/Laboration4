using ClassLibrary;
using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace WinFormsVocables
{
    public partial class Form1 : Form
    {
        public delegate WordList LoadWordList(string name);

        public static string localPath = WordList.localPath;
        public string fileName;

        public Form1()
        {
            InitializeComponent();
            WordList.CreateFolder();
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
                    //listView1.Items.Add(output);
                    lbl_languages.Visible = true;
                    lbl_languages.Text = $"Languages: " + output;

                    //Enables the counter-lable
                    label1.Visible = true;
                    label1.Text = "Number of words: " + list.Count().ToString();
                    lbl_fileName.Visible = true;
                    lbl_fileName.Text = "List loaded:\n" + localPath;
                }
            }
            //Disables buttons at initial start.
            btn_NewWord.Enabled = true;
            btn_removeWord.Enabled = true;
            btn_sortList.Enabled = true;
            btn_practice.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e) //New word-button.
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
                label1.Text = "Number of words: " + wordList1.Count().ToString();

            }
            else if (listView1 == null)
            {
                MessageBox.Show("You have to choose a list to add words to.", "Warning!");
                btn_NewWord.Enabled = false;
            }

        }

        private void button4_Click(object sender, EventArgs e) //New list - button
        {
            string localPath = WordList.localPath;
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
                        MessageBox.Show(fileNameInput + " created successfully.", "New list created");

                        File.WriteAllText(Path.Combine(localPath, fileNameInput + ".dat"), languagesInput);
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
                label1.Text = "Number of words: " + wordList1.Count().ToString();
            }

        }

        private void button3_Click(object sender, EventArgs e) //remove-button
        {
            LoadWordList wordList = new LoadWordList(WordList.LoadList);
            WordList wordList1 = wordList.Invoke(localPath);
            string[] languages = wordList1.Languages;
            string languagesInList = Interaction.InputBox("Choose the language you want to remove a word from: ");

            string inputRemoveWord;
            int langIndex = -1;

            inputRemoveWord = Interaction.InputBox("Please enter a word(s) you wish to remove." +
                "\nSeparate the words with semicolon", "Remove word");
            string[] wordsToBeRemoved = inputRemoveWord.Split(' ', ';').ToArray<string>();


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
                        MessageBox.Show($"{w} was removed from list.");

                    }
                    else
                    {
                        MessageBox.Show($"Could not find the word {w}");

                    }
                }
                wordList1.Save();
                label1.Text = "Number of words: " + wordList1.Count().ToString();
            }
        }

        private void btn_sortList_Click(object sender, EventArgs e) // SortList-button
        {


            LoadWordList wordList = new LoadWordList(WordList.LoadList);
            WordList wordList1 = wordList.Invoke(localPath);
            string[] languages = wordList1.Languages;
            string sortByLanguage = Interaction.InputBox("Type the language you want to sort as...", "Title");

            Action<string[]> showTranslations = (string[] translations) =>
            {
                string output = string.Join(',', translations);

                listView1.Items.Add(output);
            };

            listView1.Items.Clear();
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

        private void btn_practice_Click(object sender, EventArgs e) //Practice-button
        {
            LoadWordList wordList = new LoadWordList(WordList.LoadList);
            WordList wordList1 = wordList.Invoke(localPath);

            int totalGuesses = 0;
            int correctAnswers = 0;
            listView1.Items.Clear();
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

        private void lbl_languages_Click(object sender, EventArgs e)
        {

        }
    }
}

