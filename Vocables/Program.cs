using System;
using System.Linq;
using ClassLibrary;

namespace Vocables
{
    class Program
    {
        public delegate WordList LoadWordList(string name);
        static void Main(string[] args)
        {
            WordList.CreateFolder();

                       

            Console.WriteLine();

            if (args.Count() == 0)
            {
                PrintUsage();
            }
            else if (args[0] == "-lists")
            {
                foreach (string s in WordList.GetLists())
                {
                    Console.WriteLine(s);
                }
            }
            else if (args[0] == "-practice")
            {
                // Do stuff

            }
            else if (args[0] == "-remove")
            {
                // Do stuff
            }
            else if (args[0] == "-words")
            {
                // Do stuff
            }
            else if (args[0] == "-count")
            {
                LoadWordList wordList = new LoadWordList(WordList.LoadList);
                WordList wordList1 = wordList.Invoke(args[1]); //Skriv listnamnet i
                //wordList1.Count();
                Console.WriteLine(wordList1.Count());
            }
            else if (args[0] == "-add")
            {
                LoadWordList wordList = new LoadWordList(WordList.LoadList);
                WordList wordList1 = wordList.Invoke(args[1]); //Skriv listnamnet i
                wordList1.Add();                
            }
            else if (args[0] == "-new")
            {
                // Do stuff
            }
            else
            {
                PrintUsage();
            }
            Console.WriteLine();
        
            Console.ReadKey();
        }

        public static void PrintUsage()
        {
            Console.WriteLine("Use any of the following parameters:\n " +
                "-lists\n " +
                "-new < listname > < language 1 > < language 2 > .. < language n >\n " +
                "-add < listname >\n " +
                "-remove < listname > < language > < word 1 > < word 2 > .. < word n >\n " +
                "-words <listname> < sortByLanguage >\n " +
                "-count < listname >\n " +
                "-practice < listname >\n ");
        }        
    }
}
