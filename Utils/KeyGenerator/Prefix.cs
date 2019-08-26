using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMC.Core.Utils.KeyGenerator
{
    internal static class Prefix
    {
        //private const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        //private const string charsNoNumbers = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string charsNoNumbers = "abcdefghijklmnopqrstuvwxyz";
        static Random random = new Random();

        internal static string RandomPrefix(int length, bool useNumbers = false, bool RandomCaps = false)
        {
            if(useNumbers)
            {
                return new string(Enumerable.Repeat(chars, length).Select(s => RandomUpperCasae(s[random.Next(s.Length)], RandomCaps)).ToArray());
            }
            else
            {
                return new string(Enumerable.Repeat(charsNoNumbers, length).Select(s => RandomUpperCasae(s[random.Next(s.Length)], RandomCaps)).ToArray());
            }
        }

        private static char RandomUpperCasae(char input, bool useRandomCase)
        {
            if (!useRandomCase)
                return input;

            bool upper = random.Next(2) != 0;

            if (upper)
                return char.ToUpper(input);

            return input;
        }
    }
}
