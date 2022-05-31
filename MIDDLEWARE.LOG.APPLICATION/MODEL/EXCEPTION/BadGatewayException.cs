﻿namespace MIDDLEWARE.LOG.APPLICATION.MODEL.EXCEPTION
{
    public sealed class BadGatewayException : Exception
    {
        public BadGatewayException(string? message) : base(message)
        {
        }

        public BadGatewayException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
