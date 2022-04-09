using System;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Main exception thrown when parsing failed.
    /// </summary>
    public class ParsingException : Exception
    {
        public ParsingException() : base("Parsing exception!")
        {
        }

        public ParsingException(string message) : base(message)
        {
        }
    }
}