using System;
using System.Collections.Generic;
using System.IO;
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
                WordList wordList1 = wordList.Invoke(args[1]);
                //wordList1.Count();
                Console.WriteLine(wordList1.Count());
            }
            else if (args[0] == "-add") //Lägger till nya ord i vald lista, avslutas när anv matar in en tom rad.
            {
                LoadWordList wordList = new LoadWordList(WordList.LoadList);
                WordList wordList1 = wordList.Invoke(args[1]);              //Lista för språken
                string[] languages = wordList1.Languages;

                List<string> tempWordList = new List<string>();
                int i = 0;

                while (true)
                {
                    Console.WriteLine($"Enter a {languages[i % languages.Length]} word : ");
                    string input = Console.ReadLine();
                    if (input == "" || input == " ")
                    {
                        break;
                    }
                    tempWordList.Add(input);
                    i++;
                }
                wordList1.Add(tempWordList.ToArray()); //TODO Lägga till orden i lista ist för skriva över


            }
            else if (args[0] == "-new") //Skapar en ny fil/lista med angivna språk
            {
                List<string> tempargslist = new List<String>();
                //Skriver in semikolonseparerade språk i listan, oavsett hur många det är. Dvs ett "godtyckligt" antal. :)
                for (int j = 2; j < args.Length; j++)
                {
                    tempargslist.Add(args[j]);
                }
                WordList wordlist = new WordList(args[1],tempargslist.ToArray());
                Console.WriteLine($"The file {args[1]}.dat was created successfully.");
                //TODO lägg till en trycatch
                //TODO hur kalla på -add?
                                   
                string[] languages = wordlist.Languages;

                List<string> tempWordList = new List<string>();
                int i = 0;

                while (true)
                {
                    Console.WriteLine($"Enter a {languages[i % languages.Length]} word : ");
                    string input = Console.ReadLine();
                    if (input == "" || input == " ")
                    {
                        break;
                    }
                    tempWordList.Add(input);
                    i++;
                }
                wordlist.Add(tempWordList.ToArray());
            }
            else
            {
                PrintUsage();
            }

            Console.WriteLine("Press any key to end program.");
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
