using System.Globalization;

namespace CFEventHandler.API.Exceptions
{
    /// <summary>
    /// General exception
    /// </summary>
    public class GeneralException : Exception
    {
        public GeneralException()
        {
        }

        public GeneralException(string message) : base(message)
        {
        }

        public GeneralException(string message, Exception innerException) : base(message, innerException)
        {
        }


        public GeneralException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
