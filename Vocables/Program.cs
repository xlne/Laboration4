using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClassLibrary;

namespace Vocables
{
    class Program
    {
        //Delegate to reach LoadWordList
        public delegate WordList LoadWordList(string name);

        static void Main(string[] args)
        {
            WordList.CreateFolder();

            if (args.Count() == 0)
            {
                PrintUsage();
            }
            else if (args[0] == "-lists")
            {
                foreach (string s in WordList.GetLists())
                {
                    Console.WriteLine(Path.GetFileNameWithoutExtension(s));
                }
            }
            else if (args[0] == "-practice")
            //Returns random word to the user to translate
            {
                if (args.Length < 1)
                {
                    Console.WriteLine("Enter listname to begin word practice.");
                    return;
                }
                LoadWordList wordList = new LoadWordList(WordList.LoadList);
                WordList wordList1 = wordList.Invoke(args[1]);

                int totalGuesses = 0;
                int correctAnswers = 0;

                while (true)
                {
                    Word word = wordList1.GetWordToPractice();
                    Console.WriteLine(
                        $"Translate the \"{wordList1.Languages[word.FromLanguage]}\" word: " +
                        $"{word.Translations[word.FromLanguage]}, " +
                        $"to \"{wordList1.Languages[word.ToLanguage]}\"");

                    string input = Console.ReadLine();

                    if (input == "" || input == " ")
                    {
                        Console.WriteLine($"You guessed correct {correctAnswers} out of {totalGuesses} times.");
                        Console.ReadKey();
                        break;
                    }
                    else if (input == word.Translations[word.ToLanguage])
                    {
                        Console.WriteLine("That is the correct answer!");
                        totalGuesses++;
                        correctAnswers++;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Wrong answer. Try again.");
                        totalGuesses++;
                        continue;
                    }
                }
            }
            else if (args[0] == "-remove")
            {
                if (args.Length < 4)
                {
                    Console.WriteLine("You have entered too few arguments.");
                    return;
                }

                LoadWordList wordList = new LoadWordList(WordList.LoadList);
                WordList wordList1 = wordList.Invoke(args[1]);
                string[] languages = wordList1.Languages;
                string lang = args[2];
                string[] wordsToBeRemoved = args[3..];
                int langIndex = -1;

                for (int i = 0; i < languages.Length; i++)
                {
                    if (languages[i].Equals(lang, StringComparison.InvariantCultureIgnoreCase))
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
                            Console.WriteLine($"{w} was removed from list.");
                        }
                        else
                        {
                            Console.WriteLine("Could not find the word.");
                        }
                    }
                    wordList1.Save();
                }
            }
            else if (args[0] == "-words")
            //Calls for the method List
            {
                LoadWordList wordList = new LoadWordList(WordList.LoadList);
                WordList wordList1 = wordList.Invoke(args[1]);
                string[] languages = wordList1.Languages;

                Action<string[]> showTranslations = (string[] translations) =>
                {
                    Console.WriteLine(string.Join(',', translations));
                };

                if (args.Length < 3 || args.Length > 3)
                {
                    wordList1.List(0, showTranslations);
                    return;
                }

                for (int i = 0; i < languages.Length; i++)
                {
                    if (languages[i] == args[2])
                    {
                        wordList1.List(i, showTranslations);
                        break;
                    }
                }
            }
            else if (args[0] == "-count")
            {
                LoadWordList wordList = new LoadWordList(WordList.LoadList);
                WordList wordList1 = wordList.Invoke(args[1]);
                Console.WriteLine(wordList1.Count());
            }
            else if (args[0] == "-add")
            {
                LoadWordList wordList = new LoadWordList(WordList.LoadList);
                WordList wordList1 = wordList.Invoke(args[1]);
                string[] languages = wordList1.Languages;
                bool wordsAreAdded = false;

                while (true)
                {
                    Console.Write("Write a word in {0}: ", wordList1.Languages[0]);

                    string[] translations = new string[wordList1.Languages.Length];
                    string firstTranslation = translations[0] = Console.ReadLine();

                    if (firstTranslation == string.Empty)
                        break;

                    for (int i = 1; i < translations.Length; i++)
                    {
                        string wordsToTranslate;
                        do
                        {
                            Console.Write("Translate {0} to {1}: ", firstTranslation, wordList1.Languages[i]);
                            wordsToTranslate = Console.ReadLine();
                        }
                        while (wordsToTranslate == string.Empty);
                        translations[i] = wordsToTranslate;
                    }
                    wordList1.Add(translations);
                    wordsAreAdded = true;
                }

                if (wordsAreAdded)
                {
                    wordList1.Save();
                }
            }
            else if (args[0] == "-new")
            {
                List<string> tempArgsList = new List<String>();

                for (int j = 2; j < args.Length; j++)
                {
                    tempArgsList.Add(args[j]);
                }
                WordList wordList = new WordList(args[1], tempArgsList.ToArray());
                Console.WriteLine($"The file {args[1]}.dat was created successfully.");

                string[] languages = wordList.Languages;
                bool areWordsAdded = false;

                while (true)
                {
                    Console.Write("Write a word in {0}: ", wordList.Languages[0]);

                    string[] translations = new string[wordList.Languages.Length];
                    string firstTranslation = translations[0] = Console.ReadLine();

                    if (firstTranslation == string.Empty)
                        break;

                    for (int i = 1; i < translations.Length; i++)
                    {
                        string wordsToTranslate;
                        do
                        {
                            Console.Write("Translate {0} to {1}: ", firstTranslation, wordList.Languages[i]);
                            wordsToTranslate = Console.ReadLine();

                        } while (wordsToTranslate == string.Empty);

                        translations[i] = wordsToTranslate;
                    }
                    wordList.Add(translations);
                    areWordsAdded = true;
                }
                if (areWordsAdded)
                    wordList.Save();
            }
            else
            {
                PrintUsage();
            }
            Console.WriteLine("Press any key to end program.");
            Console.ReadKey();
        }
        public static void PrintUsage()
        //Shows the available arguments to use 
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
