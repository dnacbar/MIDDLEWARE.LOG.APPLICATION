using MIDDLEWARE.LOG.APPLICATION.MODEL.ENUM;
using System.Net;

namespace MIDDLEWARE.LOG.APPLICATION.LOG
{
    public sealed class LogObject
    {
        public string Id { get; set; }
        public string UserLog { get; set; }
        public string InfoLog { get; set; }
        public EnumLogLevel LevelLog { get; set; }
        public DateTime TimeLog { get; set; }
        public IPAddress IPAddress { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }

        public string LogMessage()
        {
            return @"ID: " + Id + " DATETIME: " + TimeLog + " USER: " + UserLog + Environment.NewLine +
                    "IP: " + IPAddress?.MapToIPv4().ToString() + Environment.NewLine +
                    "LOG: " + InfoLog + Environment.NewLine + Environment.NewLine;
        }
    }
}
