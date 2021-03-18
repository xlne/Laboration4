using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class Word
    {
        public string[] Translations { get; }
        public int FromLanguage { get; }
        public int ToLanguage { get; }

        public Word(params string[] translations)
        //Initialize Translations with the data from translations
        {
            List<string> translation = new List<string>();
            foreach (var @string in translations)
            {
                translation.Add(@string);
            }
            Translations = translation.ToArray();
        }

        public Word(int fromLanguage, int toLanguage, params string[] translations)
        //Initializes FromLanguage, ToLanguage and Translations.
        {
            Translations = translations;
            FromLanguage = fromLanguage;
            ToLanguage = toLanguage;
        }
    }
}
