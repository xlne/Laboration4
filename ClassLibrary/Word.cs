using System;

namespace ClassLibrary
{
    public class Word
    {
        public string[] Translations { get; }
        public int FromLanguage { get; }
        public int ToLanguage { get; }
        
        //WordList.GetWordToPractice(); //TODO Ska denna vara här?

        public Word(params string[] translations)
        //initialiserar ’Translations’ med data som skickas in som ’translations’
        {
            for (int i = 0; i < translations.Length; i++)
            {
            Translations[i] = translations[i];

            }
        }


        public Word(int fromLanguage, int toLanguage, params string[] translations)
        //som ovan, fast sätter även FromLanguage och ToLanguage.
        {
            Translations = translations;
            FromLanguage = fromLanguage;
            ToLanguage = toLanguage;
        }
    }
}
