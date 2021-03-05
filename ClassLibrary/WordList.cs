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
        //Ha en metod som läser in från filen - loadLIst
        //TODO Borde vara någon koppling till den streamreader för listorna?
        //GetFileName??


        public static void CreateFolder()
        {
            string localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Vocable\");

            if (Directory.Exists(localPath))
            {
                Console.WriteLine(localPath + " already exists.");
            }
            else
            {
                DirectoryInfo cd = Directory.CreateDirectory(localPath);
                Console.WriteLine(localPath + " was successfully created.");
            }
        }


        private List<Word> wordsList = new List<Word>();

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
            string[] listName = Directory.GetFiles("Vocable");       //TODO Vad stoppa in?
            for (int i = 0; i < listName.Length; i++)
            {
                listName[i]; //sökväg på något sätt till listan
            }

            return listName;
        }


        public static WordList LoadList(string name)
        //Laddar in ordlistan(name anges utan filändelse) och returnerar som WordList.
        //Bygg ihop sökväg + ändelse, finns fil, skapa streamreader, häng på trimend för plocka ort ; sen split på ; split ger array av strängar, 
        //dekl ny wordlist, går in while-loop !not end stream, 
        {
            WordList words = null;
            var localpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Vocable", $"{name}.dat");

            if (!File.Exists(localpath))
            {
                return null;
            }

            using (var file = new StreamReader(localpath))
            {
                var languageDefinition = file.ReadLine().TrimEnd(';', ' ').Split(';');
                words = new WordList(name, languageDefinition);

                while (!file.EndOfStream)
                {
                    var translations = file.ReadLine().TrimEnd(';', ' ').Split(';');
                    words.Add(translations);
                }
            }
            return words;
        }
        public void Save();
        // Sparar listan till en fil med samma namn som listan och filändelse.dat


        public void Add(params string[] translations)
        //Lägger till ord i listan.Kasta ArgumentException om det är fel antal translations.
        {
            foreach (var definition in )
            {

            }

            //wordList.Add(new Word(translations)); //??
            //string newWord;
            //for (int i = 0; i < wordList.Count; i++)
            //{                
            //    newWord = Console.ReadLine();
            //    Add(newWord);
            //}

        }

        public bool Remove(int translation, string word)
        //translation motsvarar index i Languages.Sök igenom språket och ta bort ordet.
        {
            for (int i = 0; i < wordList.Count; i++)
            {
                if (Languages[i] == wordList.)
                {

                }

            }
        }


        public int Count()
        //Räknar och returnerar antal ord i listan.
        {
            using (var file = new StreamReader(LoadList.localpath)) //TODO Fixa sökvägen alt hela using-parametern
            {
                int wordCount = -1;          
                string line = File.ReadLines();
                while (file != null)
                {
                    wordCount++;
                    line = File.ReadLines();
                }
                return wordCount;
            }

        }
        public void List(int sortByTranslation, Action<string[]> showTranslations)
        //sortByTranslation = Vilket språk listan ska sorteras på.showTranslations = Callback som anropas för varje ord i listan.
        {
            if (sortByTranslation < 0 || sortByTranslation >= Languages.Count())
            {

            }
            foreach (Word word in wordsList.OrderBy(w => w.Translations[sortByTranslation]).ToList())
            {
                showTranslations?.Invoke(wordsList.Translations);
            }
        }


        //Kunna använda ett lamdauttryck

        public Word GetWordToPractice()
        //Returnerar slumpmässigt Word från listan, med slumpmässigt valda FromLanguage och ToLanguage(dock inte samma).
        {
            Random rnd = new Random();
            int index = rnd.Next(wordList.Count);

            int fromLanguage = rnd.Next(0, Languages.Length);
            int toLanguage = rnd.Next(0, Languages.Length);

            while (fromLanguage == toLanguage)
            {

            }
            return new Word(fromLanguage, toLanguage, wordList[index].Translations);
        }

    }
}
