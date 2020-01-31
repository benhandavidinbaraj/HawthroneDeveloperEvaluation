using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomDomainGenerator
{
    public class RandomDomain : RandomWord
    {
        string[] extensions = new string[] { "com", "net" };

        /// <summary>
        /// Method to get list of random domains
        /// </summary>
        /// <param name="iTotalDomain">int: Total no of domains to be generated</param>
        /// <param name="iNoOfWords">int: No of words in a domain</param>
        /// <param name="iMinWordLength">int: Min length of each word</param>
        /// <returns>string[]: List of domains</returns>
        public string[] GetListOfRandomDomain(int iTotalDomain, int iNoOfWords, int iMinWordLength)
        {
            string[] stDomains = new string[iTotalDomain];            
            string stRandomDomain;
            for (int iCount = 0; iCount < iTotalDomain;)
            {
                foreach(string stextension in extensions)
                {
                    stRandomDomain = CreateRandomDomain(iNoOfWords, iMinWordLength, stextension);
                    if (!stDomains.Contains(stRandomDomain) && iCount < iTotalDomain)
                    {
                        stDomains[iCount] = stRandomDomain;
                        iCount++;
                    }
                }
            }
            return stDomains;
        }

        /// <summary>
        /// Create random domain based on no ofwords and min word length
        /// </summary>
        /// <param name="iNoOfWords">int:  No of words in domain</param>
        /// <param name="iMinWordLength">int: Min characters in a word</param>
        /// <param name="stExtension">string: Domain extension</param>
        /// <returns></returns>
        private string CreateRandomDomain(int iNoOfWords, int iMinWordLength, string stExtension)
        {
            string[] stRandomWords = new string[iNoOfWords];
            string stRandomWord;
            for (int i = 0; i < iNoOfWords; i++)
            {
                stRandomWord = GetRandomWord(iMinWordLength);
                while (stRandomWords.Contains(stRandomWord))
                    stRandomWord = GetRandomWord(iMinWordLength);
                stRandomWords[i] = stRandomWord;
            }
            return string.Join("-", stRandomWords) + "." + stExtension;
        }
    }
}
