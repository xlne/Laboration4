using System;
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
                WordList wordList1 = wordList.Invoke(args[1]);
                wordList1.Add();                
            }
            else if (args[0] == "-new") //Skapar en ny fil/lista med angivna språk
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(WordList.localPath, $"{args[1]}.dat")))
                {
                    int i;
                    //Skriver in semikolonseparerade språk i listan, oavsett hur många det är. Dvs ett "godtyckligt" antal. :)
                    for (i = 2; i < args.Length; i++)
                    {
                        sw.Write($"{args[i].ToString()};");
                    }

                }
                Console.WriteLine($"The file {args[1]}.dat was created successfully.");
                //TODO hur kalla på -add?
                
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
