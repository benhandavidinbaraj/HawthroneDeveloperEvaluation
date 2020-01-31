using RandomDomainGenerator.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RandomDomainGenerator
{
    public abstract class RandomWord
    {
        List<WordModel> WordList;
        public RandomWord()
        {
            InitializeWords();
        }

        /// <summary>
        /// Method to initialize words from text file
        /// </summary>
        private void InitializeWords()
        {
            WordList = new List<WordModel>();
            int iCounter = 1;
            foreach(string stWord in File.ReadLines("Asset/Words.txt"))
            {
                WordList.Add(new WordModel(iCounter, stWord));
                iCounter++;
            }
        }

        /// <summary>
        /// Gets random word
        /// </summary>
        /// <param name="iMinWordLength">int: Min no characters in aword</param>
        /// <returns>string: Random word</returns>
        protected string GetRandomWord(int iMinWordLength)
        {
            try
            {
                Random rand = new Random();
                if (WordList == null || WordList.Count == 0)
                    InitializeWords();
                IEnumerable<WordModel> liWords = from word in WordList
                                      where word.Word.Length > iMinWordLength
                                      select word;
                int iRandom = rand.Next(liWords.Count());
                return liWords.ElementAt(iRandom).Word;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
