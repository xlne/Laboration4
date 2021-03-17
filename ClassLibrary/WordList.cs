using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class WordList
    {
        //localPath pekar på användarens \AppData\Local\Vocables
        public static string localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Vocables");


        public static void CreateFolder()
        {
            try
            {
                if (Directory.Exists(localPath))
                {
                    Console.WriteLine(localPath + " already exists. No action taken.");
                }
                else
                {
                    DirectoryInfo cd = Directory.CreateDirectory(localPath);
                    Console.WriteLine(localPath + " was successfully created.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private List<Word> wordsList = new List<Word>();        //Lista för listorna?
               
        public string Name { get; }
        //Namnet på listan.
        public string[] Languages { get; }
        //Namnen på språken.

        public WordList(string name, params string[] languages)
        //Konstruktor.Sätter properites Name och Languages till parametrarnas värden.
        {
            this.Name = name;
            this.Languages = languages;

        }

        public static string[] GetLists()
        //Returnerar array med namn på alla listor som finns lagrade(utan filändelsen).
        {

            string[] listLists = Directory.GetFiles(localPath, "*.dat");       //Söker och listar alla filer som slutar på .dat

            return listLists;
        }


        public static WordList LoadList(string name)
        //Laddar in ordlistan(name anges utan filändelse) och returnerar som WordList.
        //Bygg ihop sökväg + ändelse, finns fil, skapa streamreader, häng på trimend för plocka ort ; sen split på ; split ger array av strängar, 
        //dekl ny wordlist, går in while-loop !not end stream, 
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
        public void Save() // Sparar listan till en fil med samma namn som listan och filändelse.dat
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
        //Lägger till ord i listan. Kasta ArgumentException om det är fel antal translations.
        {
            StringBuilder stringBuilder = new StringBuilder();

            int length = translations.Length % Languages.Length;
            if (length == 0)
            {
                List<string> tempList = new List<string>();
                for (int i = 0; i < translations.Length; i++)
                {
                    tempList.Add(translations[i]);
                    if (i % Languages.Length - 1 == 0 && i != 0)
                    {
                        wordsList.Add(new Word(tempList.ToArray()));
                        tempList = new List<string>();
                    }
                }
            }
            else
            {
                throw new ArgumentException("Invalid number of strings!");
            }
            Save();

            //ta ut längd på ord och spara i en separat lista
            //Kolla längd på vår lista. trans = lang list. trans.Length % lang.Length. 
        }

        public bool Remove(int translation, string word)
        //translation motsvarar index i Languages.Sök igenom språket och ta bort ordet.
        {
            for (int i = 0; i < wordsList.Count; i++)
            {
                //Om inmatade ordet är översättningen,-arna på position i wordsList 
                if (word.Equals(wordsList[i].Translations[translation], StringComparison.InvariantCultureIgnoreCase))
                {
                    wordsList.RemoveAt(i);
                    //Save(); //Inte ha den här för då sparar den efter varje ord och inte när alla ord är borttagna
                    return true;
                }
            }
            return false;
        }

        public int Count() => wordsList.Count;
        //Räknar och returnerar antal ord i listan.


        //public void List(int sortByTranslation, Action<string[]> showTranslations)
        ////sortByTranslation = Vilket språk listan ska sorteras på.showTranslations = Callback som anropas för varje ord i listan.
        //{
        //    if (sortByTranslation < 0 || sortByTranslation >= Languages.Count())
        //    {

        //    }
        //    foreach (Word word in wordsList.OrderBy(w => w.Translations[sortByTranslation]).ToList())
        //    {
        //        showTranslations?.Invoke(word.Translations);
        //    }
        //}


        public Word GetWordToPractice()
        //Returnerar slumpmässigt Word från listan, med slumpmässigt valda FromLanguage och ToLanguage(dock inte samma).
        //TODO kanske lägga till kontroll att det inte ska vara samma ord. kanske en enkel if?
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

