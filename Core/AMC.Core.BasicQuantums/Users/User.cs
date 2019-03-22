using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.BasicQuantums.Users
{
    public class User : AQuant
    {
        public string Password { private get; set; }

        public DateTime? LastTimeLogin { get; }

        public long? Creator { get; }

        public DateTime? LastTimePasswordChange { get;  }

        protected override string CacheKeyPattern()
        {
            return _cacheKeyPattern;
        }
        private const string _cacheKeyPattern = "User({0})";
    }
}
