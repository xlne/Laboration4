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
            {
                /*
                 * Ber användaren översätta ett slumpvis valt ord ur listan från ett slumpvis valt språk till ett annat. Skriver ut om det var rätt eller fel, och fortsätter fråga efter ord tills användaren lämnar en tom inmatning. Då skrivs antal övade ord ut, samt hur stor andel av orden man haft rätt på.
                 */

                if (File.Exists($"{WordList.localPath}\\{args[1]}.dat"))
                {
                    if (args.Length < 1)
                    {
                        Console.WriteLine("Enter listname to begin word practice.");
                        return;
                    }
                    LoadWordList wordList = new LoadWordList(WordList.LoadList); //Laddar in listan
                    WordList wordList1 = wordList.Invoke(args[1]); //Skapa ny lista där vi kallar på listnamn/lägger in ord?

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
            }
            else if (args[0] == "-remove")
            {
                if (File.Exists($"{WordList.localPath}\\{args[1]}.dat"))
                {
                    if (args.Length < 4)
                    {
                        // skriv felmeddelande här
                        return;
                    }

                    LoadWordList wordList = new LoadWordList(WordList.LoadList); //Laddar in listan
                    WordList wordList1 = wordList.Invoke(args[1]); //Skapa ny lista där vi kallar på listnamn?
                    string[] languages = wordList1.Languages; //Lägger in översättningar till nya listan
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
                                // skriv ut borttaget ord
                                Console.WriteLine($"{w} was removed from list.");
                            }
                            else
                            {
                                Console.WriteLine("Could not find the word.");
                            }
                        }
                        wordList1.Save(); //Sparar efter loopen för minska prestandapåverkan?
                    }
                }
            }
            else if (args[0] == "-words")
            {
                LoadWordList wordList = new LoadWordList(WordList.LoadList);
                WordList wordList1 = wordList.Invoke(args[1]);
                string[] languages = wordList1.Languages;
                //TODO hur få in args[2] i sortByTranslation?

                Action<string[]> showTranslations = (string[] translations) =>
                {
                    Console.WriteLine(string.Join(',', translations));
                };

                if (args.Length < 3) //TODO Om man skriver fler språk, lägg till kontroll
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
            else if (args[0] == "-add") //Lägger till nya ord i vald lista, avslutas när anv matar in en tom rad.
            {
                LoadWordList wordList = new LoadWordList(WordList.LoadList);
                WordList wordList1 = wordList.Invoke(args[1]);              //Lista för språken
                string[] languages = wordList1.Languages;

                //List<string> tempWordList = new List<string>();
                //string[] translations = new string[languages.Length];

                {
                    bool areWordsAdded = false;

                    while (true)
                    {
                        Console.Write("Write a word in {0}: ", wordList1.Languages[0]);

                        string[] translations = new string[wordList1.Languages.Length];
                        string firstTranslation = translations[0] = Console.ReadLine();

                        if (firstTranslation == string.Empty)
                            break;

                        for (int i = 1; i < translations.Length; i++)
                        {
                            string transl;
                            do
                            {
                                Console.Write("Translate {0} to {1}: ", firstTranslation, wordList1.Languages[i]);
                                transl = Console.ReadLine();

                            } while (transl == string.Empty);

                            translations[i] = transl;
                        }

                        wordList1.Add(translations);
                        areWordsAdded = true;
                    }

                    if (areWordsAdded)
                        wordList1.Save();
                }

                //int i = 0;

                //while (true)
                //{
                //    Console.WriteLine($"Enter a {languages[i % languages.Length]} word : ");
                //    string input = Console.ReadLine();
                //    //tempWordList.Add(input);
                //    i++;
                //    //wordList1.Add(tempWordList.ToArray());
                //    translations[i] == input;


                //    if (input == "" || input == " ")
                //    {
                //        break;
                //    }
                //}
            }
            else if (args[0] == "-new") //Skapar en ny fil/lista med angivna språk
            {
                List<string> tempArgsList = new List<String>();
                //Skriver in semikolonseparerade språk i listan, oavsett hur många det är. Dvs ett "godtyckligt" antal. :)
                for (int j = 2; j < args.Length; j++)
                {
                    tempArgsList.Add(args[j]);
                }
                WordList wordList = new WordList(args[1], tempArgsList.ToArray());
                Console.WriteLine($"The file {args[1]}.dat was created successfully.");
                //TODO lägg till en trycatch? lägga till en if?

                string[] languages = wordList.Languages;

                {
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
                            string transl;
                            do
                            {
                                Console.Write("Translate {0} to {1}: ", firstTranslation, wordList.Languages[i]);
                                transl = Console.ReadLine();

                            } while (transl == string.Empty);

                            translations[i] = transl;
                        }

                        wordList.Add(translations);
                        areWordsAdded = true;
                    }

                    if (areWordsAdded)
                        wordList.Save();
                }

                //List<string> tempWordList = new List<string>();
                //int i = 0;

                //while (true)
                //{
                //    Console.WriteLine($"Enter a {languages[i % languages.Length]} word : "); //TODO kapar sista orden som läggs in
                //    string input = Console.ReadLine();
                //    if (input == "" || input == " ")
                //    {
                //        break;
                //    }
                //    tempWordList.Add(input);
                //    i++;
                //}
                ////TODO Skriver över, lägger inte till i existerande lista
                //wordList.Add(tempWordList.ToArray());
                //wordList.Save();
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
