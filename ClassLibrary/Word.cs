using System;

namespace ClassLibrary
{
    public class Word
    {
        public string[] Translations { get; }
        public int FromLanguage { get; }
        public int ToLanguage { get; }

        WordList.GetWordToPractice();

        public Word(params string[] translations);
        //initialiserar ’Translations’ med data som skickas in som ’translations’

        public Word(int fromLanguage, int toLanguage, params string[] translations);
        //som ovan, fast sätter även FromLanguage och ToLanguage.
    }
}
