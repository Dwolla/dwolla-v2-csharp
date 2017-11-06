using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.Tasks
{
    abstract class BaseTask
    {
        public DwollaBroker Broker { get; set; }

        public abstract Task Run();

        protected static void WriteLine(string value) => Console.WriteLine(value);
        protected static void Write(string value) => Console.Write(value);
        protected static string ReadLine() => Console.ReadLine();

        private static readonly Random Random = new Random();

        protected static string RandomNumericString(int length)
        {
            const string chars = "0123456789";
            return RandomString(chars, length);
        }

        protected static string RandomAlphaNumericString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return RandomString(chars, length);
        }

        private static string RandomString(string charset, int length)
        {
            return new string(Enumerable.Repeat(charset, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}