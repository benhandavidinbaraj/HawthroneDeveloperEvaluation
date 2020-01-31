using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace RandomDomainGenerator
{
    class Program
    {
        static void Main()
        {
            Display("Welcome to Random Domain Generator...");
            GenerateRandomDomain();
        }

        /// <summary>
        /// Method that generate and display random domains
        /// </summary>
        private static void GenerateRandomDomain()
        {
            int iNoOfDomain = GetNoOfDomain();
            int iNoOfWords = GetNoOfWords();
            int iMinWordLength = 4;
            DisplayRandomDomains(iNoOfDomain, iNoOfWords, iMinWordLength);
            Console.ReadLine();
        }

        /// <summary>
        /// Method to display random domain
        /// </summary>
        /// <param name="iNoOfDomain">int: No of domain</param>
        /// <param name="iNoOfWords">int: No of words in domain</param>
        /// <param name="iMinWordLength">int: Minimum word length</param>
        private static void DisplayRandomDomains(int iNoOfDomain, int iNoOfWords, int iMinWordLength)
        {
            Display("-------------------------");
            string[] stDomains = GetRandomDomains(iNoOfDomain, iNoOfWords, iMinWordLength);

            foreach (string domain in stDomains)
            {
                Display(domain);
                Display("-------------------------");
            }
            CreateJSON(stDomains);
            Display("------------END----------");
            Display("-------------------------");
        }

        /// <summary>
        /// Method to get number of words from user
        /// </summary>
        /// <returns>int: No of words in domain</returns>
        private static int GetNoOfWords()
        {
            Display("Please enter no of words to be added in domain name: ");
            int iNoOfWords = 2;
            String stNoOfWords;
            bool result = false;
            while (result == false)
            {
                stNoOfWords = Console.ReadLine();
                if (Int32.TryParse(stNoOfWords, out iNoOfWords) && iNoOfWords < 5)
                {
                    result = true;
                }
                else
                    Display("Please enter valid number less than 5.");
            }

            return iNoOfWords;
        }

        /// <summary>
        /// Method to get number of domain to be generated from user
        /// </summary>
        /// <returns>int: No of domain</returns>
        private static int GetNoOfDomain()
        {
            Display("Please enter no of domains to be generated: ");
            int iNoOfDomain = 1;
            bool result = false;
            String stDomain;
            while (result == false)
            {
                stDomain = Console.ReadLine();
                if (Int32.TryParse(stDomain, out iNoOfDomain) && iNoOfDomain < 50)
                {
                    result = true;
                }
                else
                    Display("Please enter valid number less than 50.");
            }
            return iNoOfDomain;
        }

        /// <summary>
        /// Get random domains
        /// </summary>
        /// <param name="iNoOfDomain">int: No of domain</param>
        /// <param name="iNoOfWords">int: No of words in domain</param>
        /// <param name="iMinWordLength">int: Minimum word length</param>s
        /// <returns>string[]: Array of domain</returns>
        static string[] GetRandomDomains(int iTotalDomain, int iNoOfWords, int iMinWordLength)
        {
            try
            {
                RandomDomain domain = new RandomDomain();
                return domain.GetListOfRandomDomain(iTotalDomain, iNoOfWords, iMinWordLength);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to display message in console
        /// </summary>
        /// <param name="stMessage">string: input message to display</param>
        private static void Display(string stMessage)
        {
            Console.WriteLine(stMessage);
        }

        /// <summary>
        /// Method to created random domains as JSON file that can be used in rate domain application
        /// </summary>
        /// <param name="stDomains"></param>
        private static void CreateJSON(string[] stDomains)
        {
            string JSONresult = JsonConvert.SerializeObject(stDomains);
            string path = @"Asset\Domain.json";
            using (var tw = new StreamWriter(path, false))
            {
                tw.WriteLine(JSONresult.ToString());
                tw.Close();
            }
        }
    }
}
