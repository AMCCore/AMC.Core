using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMC.Core.Utils.KeyGenerator
{
    internal static class Prefix
    {
        private const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const string charsNoNumbers = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static Random random = new Random();

        internal static string RandomPrefix(int length, bool useNumbers = false)
        {
            if(useNumbers)
            {
                return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            }
            else
            {
                return new string(Enumerable.Repeat(charsNoNumbers, length).Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }
    }
}
