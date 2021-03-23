using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    public class WordList
    {
        //localPath defines the user's \AppData\Local\Vocables
        public static string localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Vocables");

        //Creates a folder to save the lists, if the folder does not already exists
        public static void CreateFolder()
        {
            try
            {
                if (!Directory.Exists(localPath))
                {
                    DirectoryInfo cd = Directory.CreateDirectory(localPath);
                    
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        //A list to save the translated words
        private List<Word> wordsList = new List<Word>();

        public string Name { get; }        
        public string[] Languages { get; }    
        public WordList(string name, params string[] languages)
        //A constructor.Sets properites Name and Languages to the value of the parameters
        {
            this.Name = name;
            this.Languages = languages;
        }

        public static string[] GetLists()
        //Return the array with name of all the lists that is stored
        {
            string[] listLists = Directory.GetFiles(localPath, "*.dat");
            return listLists;
        }

        public static WordList LoadList(string name)
        //Load the list and return as WordList
        {
            WordList words = null;
            List<string[]> localList = new List<string[]>();
            var localPathListName = Path.Combine(localPath, $"{name}.dat");

            if (!File.Exists(localPathListName))
            {
                return null;
            }

            using (var file = new StreamReader(localPathListName))
            {
                var languageDefinition = file.ReadLine().TrimEnd(';', ' ').Split(';');
                words = new WordList(name, languageDefinition);

                while (!file.EndOfStream)
                {
                    var translations = file.ReadLine().TrimEnd(';', ' ').Split(';');
                    localList.Add(translations);
                }
            }

            List<Word> tempwordList = new List<Word>();
            for (int i = 0; i < localList.Count; i++)
            {
                tempwordList.Add(new Word(localList[i]));
            }
            words.wordsList = tempwordList;
            return words;
        }
        public void Save() 
        //Saves the list to a file with the same name as the list
        {
            StringBuilder stringBuilder = new StringBuilder();
            string fileName = Path.Combine(localPath, $"{Name}.dat");
            if (!File.Exists(fileName))
            {
                using (StreamWriter streamWriter = File.CreateText(fileName))
                {
                    foreach (var item in Languages)
                    {
                        stringBuilder.Append(item + ";");
                    }
                    streamWriter.WriteLine(stringBuilder.ToString());
                }
            }
            else
            {
                using (StreamWriter streamWriter = new StreamWriter(fileName))
                {
                    foreach (var item in Languages)
                    {
                        stringBuilder.Append(item + ";");
                    }
                    streamWriter.WriteLine(stringBuilder.ToString());
                    stringBuilder = new StringBuilder();

                    foreach (var item in wordsList)
                    {
                        foreach (var wordInItem in item.Translations)
                        {
                            stringBuilder.Append(wordInItem + ";");
                        }
                        streamWriter.WriteLine(stringBuilder.ToString());
                        stringBuilder = new StringBuilder();
                    }
                }
            }
        }

        public void Add(params string[] translations)
        //Adds words to the list. Throw ArgumentException if the number of translations is not correct
        {

            if (translations.Length != Languages.Length)
            {
                throw new ArgumentException("Wrong number of translations.");
            }
            else
            {
                wordsList.Add(new Word(translations));
            }
            Save();
        }

        public bool Remove(int translation, string word)
        //Searches for the word and removes the word with translations
        {
            for (int i = 0; i < wordsList.Count; i++)
            {                
                if (word.Equals(wordsList[i].Translations[translation], StringComparison.InvariantCultureIgnoreCase))
                {
                    wordsList.RemoveAt(i);                    
                    return true;
                }
            }
            return false;
        }

        public int Count() => wordsList.Count;
        //Counts and returns the number of words in the wordlist

        public void List(int sortByTranslation, Action<string[]> showTranslations)
        //Sorts the list by translation
        {
            if (sortByTranslation < 0 || sortByTranslation >= Languages.Count())
            {
                sortByTranslation = 0;
            }
            foreach (Word word in wordsList.OrderBy(w => w.Translations[sortByTranslation]).ToList())
            {
                showTranslations?.Invoke(word.Translations);
            }
        }

        public Word GetWordToPractice()
        //Returns a word to be translated        
        {
            Random random = new Random();
            int index = random.Next(wordsList.Count);

            int fromLanguage = random.Next(0, Languages.Length);
            int toLanguage = random.Next(0, Languages.Length);

            while (fromLanguage == toLanguage)
            {
                fromLanguage = random.Next(0, Languages.Length);
            }
            return new Word(fromLanguage, toLanguage, wordsList[index].Translations);
        }
    }
}

