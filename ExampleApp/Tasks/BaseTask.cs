using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

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

        protected  static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
