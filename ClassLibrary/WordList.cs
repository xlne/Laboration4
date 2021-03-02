using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class WordList
    {
        List<Word> wordList = new List<Word>();

        public string Name { get; }
        //Namnet på listan.
        public string[] Languages { get; }
        //Namnen på språken.

        public Wordlist(string name, params string[] languages);
        //Konstruktor.Sätter properites Name och Languages till parametrarnas värden.
        public static string[] GetLists();
        //Returnerar array med namn på alla listor som finns lagrade(utan filändelsen).
        public static Wordlist LoadList(string name);
        //Laddar in ordlistan(name anges utan filändelse) och returnerar som WordList.
        public void Save();
        // Sparar listan till en fil med samma namn som listan och filändelse.dat
        public void Add(params string[] translations);
        //Lägger till ord i listan.Kasta ArgumentException om det är fel antal translations.
        public bool Remove(int translation, string word);
        //translation motsvarar index i Languages.Sök igenom språket och ta bort ordet.
        public int Count();
        //Räknar och returnerar antal ord i listan.
        public void List(int sortByTranslation, Action<string[]> showTranslations);
        //sortByTranslation = Vilket språk listan ska sorteras på.showTranslations = Callback som anropas för varje ord i listan.
        public Word GetWordToPractice();
        //Returnerar slumpmässigt Word från listan, med slumpmässigt valda FromLanguage och ToLanguage(dock inte samma).

    }
}
