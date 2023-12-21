using System;

namespace FootballLeague.BLL.CustomExeptions
{
    public class CRUDException : Exception
    {
        public CRUDException(string message)
           : base(message)
        {
        }

        public CRUDException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
