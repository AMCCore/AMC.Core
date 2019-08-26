using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Utils.KeyGenerator
{
    public class Generate
    {
        static Random random = new Random();

        const int _randomHole = 4;
        //private const string chars = "0123456789abcdefghijklmnopqrstuvwxyz"; //36
        private const string charsNoNumbers = "abcdefghijklmnopqrstuvwxyz"; //26
        private string _prefix = null;
        private int _curentNumber;


        public Generate() : this(5) { }

        public Generate(int PrefixLength) : this(PrefixLength, false) { }

        public Generate(int PrefixLength, bool PrefixUsesNumbers)
        {
            _prefix = Prefix.RandomPrefix(PrefixLength, PrefixUsesNumbers);
            _curentNumber = 17576 + random.Next(charsNoNumbers.Length);
        }

        public string NextKey()
        {
            var result = ConvertToNNum(_curentNumber, charsNoNumbers.ToCharArray());
            _curentNumber += random.Next(_randomHole);
            return result;
        }

        private static string ConvertToNNum(int number, char[] basisChars)
        {
            int basis = basisChars.Length;
            int temp = 0;
            string result = string.Empty;
            if (number > 0)
            {
                while (number >= (basis - 1))
                {
                    temp = number % basis;
                    number = (number - temp) / basis;
                    result = Convert.ToString(basisChars[temp]) + result;
                }
                result = Convert.ToString(basisChars[number]) + result;
            }

            return result;
        }

        public static string GenerateRandomKey(int Length, bool UseNumbers = true, bool RandomCaps = true)
        {
            return Prefix.RandomPrefix(Length, UseNumbers, RandomCaps);
        }
    }
}
