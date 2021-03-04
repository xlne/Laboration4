using System;
using System.Linq;
using ClassLibrary;

namespace Vocables
{
    class Program
    {
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
                // Do stuff
            }
            else if (args[0] == "-add")
            {
                WordList.Add();
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
                "-new < list name > < language 1 > < language 2 > .. < language n >\n " +
                "-add < list name >\n " +
                "-remove < list name > < language > < word 1 > < word 2 > .. < word n >\n " +
                "-words <listname> < sortByLanguage >\n " +
                "-count < listname >\n " +
                "-practice < listname >\n ");
        }        
    }
}
