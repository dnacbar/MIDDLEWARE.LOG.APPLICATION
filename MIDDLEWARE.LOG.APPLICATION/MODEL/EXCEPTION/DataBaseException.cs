namespace MIDDLEWARE.LOG.APPLICATION.MODEL.EXCEPTION
{
    public sealed class DataBaseException : Exception
    {
        public DataBaseException(string? message) : base(message)
        {
        }

        public DataBaseException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
