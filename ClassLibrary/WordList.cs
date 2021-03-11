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
            string localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Vocables");

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

        private List<Word> wordsList = new List<Word>();        //Lista för listorna?

        public List<string[]> wordListen { get; set; }                 //Lista för alla ord i vald lista. 

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
            string localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Vocables"); //TODO göra localPath global?
            string[] listName = Directory.GetFiles(localPath, "*.dat");       //Söker och listar alla filer som slutar på .dat

            return listName;
        }


        public static WordList LoadList(string name)
        //Laddar in ordlistan(name anges utan filändelse) och returnerar som WordList.
        //Bygg ihop sökväg + ändelse, finns fil, skapa streamreader, häng på trimend för plocka ort ; sen split på ; split ger array av strängar, 
        //dekl ny wordlist, går in while-loop !not end stream, 
        {
            WordList words = null;
            List<string[]> localList = new List<string[]>();

            var localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Vocables", $"{name}.dat");

            if (!File.Exists(localPath))
            {
                return null;
            }

            using (var file = new StreamReader(localPath))
            {
                var languageDefinition = file.ReadLine().TrimEnd(';', ' ').Split(';');
                words = new WordList(name, languageDefinition);

                while (!file.EndOfStream)
                {
                    var translations = file.ReadLine().TrimEnd(';', ' ').Split(';');
                    localList.Add(translations);
                }
            }
            words.wordListen = localList;
            return words;
        }
        public void Save()
        //Sparar listan till en fil med samma namn som listan och filändelse.dat
        {            
            
        }


        public void Add(params string[] translations)
        //Lägger till ord i listan. Kasta ArgumentException om det är fel antal translations.
        {
            StringBuilder stringBuilder = new StringBuilder();

            //For each language in Languages, add language
            foreach (var language in Languages)
            {
                stringBuilder.Append($"{language};");
            }

            //For each word in WordList(the list for lists), add new line and the translation
            foreach (Word words in wordsList)
            {
                stringBuilder.Append($"\n");
                foreach (var translation in words.Translations)
                {
                    stringBuilder.Append($"{translation};");
                }
            }

            //Get the name of the list and set .dat in end.
            File.WriteAllText($"{Environment.SpecialFolder.LocalApplicationData}{Name}.dat", stringBuilder.ToString());


            //foreach (Word word in wordsList)
            //{

            //}

            //wordsList.Add(new Word(translations)); //??
            //string newWord;
            //for (int i = 0; i < wordsList.Count; i++)
            //{
            //    newWord = Console.ReadLine();
            //    Add(newWord);
            //}

        }

    //public bool Remove(int translation, string word)
    ////translation motsvarar index i Languages.Sök igenom språket och ta bort ordet.
    //{
    //   wordListen.Remove

    //    for (int i = 0; i < wordList.Count; i++)
    //    {
    //        if (Languages[i] == wordList.)
    //        {

    //        }

    //    }
    //}

    public int Count() => wordListen.Count;
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


        //Kunna använda ett lamdauttryck

        //public Word GetWordToPractice()
        ////Returnerar slumpmässigt Word från listan, med slumpmässigt valda FromLanguage och ToLanguage(dock inte samma).
        //{
        //    Random rnd = new Random();
        //    int index = rnd.Next(wordList.Count);

        //    int fromLanguage = rnd.Next(0, Languages.Length);
        //    int toLanguage = rnd.Next(0, Languages.Length);

        //    while (fromLanguage == toLanguage)
        //    {

        //    }
        //    return new Word(fromLanguage, toLanguage, wordList[index].Translations);
        //}
    }
}

