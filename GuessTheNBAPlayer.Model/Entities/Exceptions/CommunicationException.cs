using System;

namespace GuessTheNBAPlayer.Model.Entities.Exceptions
{
    public class CommunicationException : Exception
    {
        public CommunicationException(string message) : base(message) { }
    }
}