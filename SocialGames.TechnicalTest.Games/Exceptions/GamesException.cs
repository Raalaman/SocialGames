using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SocialGames.TechnicalTest.Games.Exceptions
{
    public class GamesException : Exception
    {
        public GamesException() : base() { }
        public GamesException(string message) : base(message) { }
        public GamesException(string message, Exception inner) : base(message, inner) { }
    }
}
