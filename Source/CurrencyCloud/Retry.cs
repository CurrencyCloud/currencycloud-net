using System;
using System.Net;

namespace CurrencyCloud
{
    public struct Retry
    {
        private static int numRetries = 7;
        private static int minWait = new Random().Next(750);
        private static int maxWait = new Random().Next(60000, 90000);
        private static int jitter = new Random().Next(1000);

        public static bool Enabled { get; set; }

        public static int NumRetries
        {
            get { return numRetries; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("NumRetries", "Value cannot be less than 1");
                }

                numRetries = value;
            }
        }

        public static int MinWait
        {
            get { return minWait; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("MinWait", "Value cannot be less than 0");
                }

                minWait = value;
            }

        }

        public static int MaxWait
        {
            get { return maxWait; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("MaxWait", "Value cannot be less than 1");
                }

                maxWait = value;
            }
        }

        public static int Jitter
        {
            get { return jitter; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Jitter", "Value cannot be less than 0");
                }

                jitter = value;
            }
        }

        public static HttpStatusCode[] OnError { get; set; } =
        {
            HttpStatusCode.Unauthorized,
            HttpStatusCode.RequestTimeout,
            (HttpStatusCode)429,
            HttpStatusCode.InternalServerError,
            HttpStatusCode.BadGateway,
            HttpStatusCode.ServiceUnavailable,
            HttpStatusCode.GatewayTimeout
        };
    }
}
