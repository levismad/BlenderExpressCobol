using System;

namespace Levismad.Framework
{
    [Serializable]
    public class ValidacaoException : Exception
    {
        public int? ErrorCode { get; set; }
        public bool TerminationError { get; set; }
        public ValidacaoException()
        {
            TerminationError = true;
        }

        public ValidacaoException(string message,int? code = null,bool terminate= true)
            : base(message) {
            ErrorCode = code;
            TerminationError = terminate;
        }        

        public ValidacaoException(string message, Exception innerException, int? code = null, bool terminate = true)
            : base(message, innerException)
        {
            ErrorCode = code;
            TerminationError = terminate;
        }
    } 

}
