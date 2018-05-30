using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dwolla.Client;
using ExampleApp.Tasks;
using static System.Console;

namespace ExampleApp
{
    public class Program
    {
        private static void Main()
        {
            var key = Environment.GetEnvironmentVariable("DWOLLA_APP_KEY");
            var secret = Environment.GetEnvironmentVariable("DWOLLA_APP_SECRET");

            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(secret))
            {
                WriteLine("Set DWOLLA_APP_KEY and DWOLLA_APP_SECRET env vars and restart IDE. Press any key to exit..");
                ReadLine();
            }
            else
            {
                var running = true;
                var broker = new DwollaBroker(DwollaClient.Create(true));

                Task.Run(async () => await broker.SetAuthroizationHeader(key, secret)).Wait();

                WriteHelp();

                while (running)
                {
                    Write("What would you like to do? (Press ? for options): ");
                    var i = ReadLine();
                    var input = i == null ? "" : i.ToLower().Trim();

                    switch (input)
                    {
                        case "?":
                            WriteHelp();
                            break;

                        case "quit":
                        case "q":
                        case "exit":
                            running = false;
                            break;

                        default:
                            BeginTask(input, broker);
                            break;
                    }
                }
            }
        }

        private static void WriteHelp()
        {
            WriteLine(@"Options:
 - Quit (q)
 - Help (?)");
            GetTasks().ForEach(ta => WriteLine($" - {ta.Description} ({ta.Command})"));
        }

        private static Dictionary<string, Type> _tasks;

        private static List<TaskAttribute> GetTasks()
        {
            if (_tasks == null)
                _tasks = Assembly.GetEntryAssembly().GetTypes()
                    .Where(x => typeof(BaseTask).IsAssignableFrom(x) &&
                                !x.GetTypeInfo().IsAbstract &&
                                x.GetTypeInfo().GetCustomAttribute<TaskAttribute>() != null)
                    .ToDictionary(x => x.GetTypeInfo().GetCustomAttribute<TaskAttribute>().Command);

            return _tasks
                .OrderBy(x => x.Value.FullName)
                .Select(x => x.Value.GetTypeInfo().GetCustomAttribute<TaskAttribute>())
                .ToList();
        }

        private static void BeginTask(string command, DwollaBroker broker)
        {
            if (!_tasks.ContainsKey(command))
            {
                WriteLine("Unrecognized option");
            }
            else
            {
                var type = _tasks[command];
                var task = (BaseTask) type.GetConstructor(new Type[0]).Invoke(new object[0]);
                task.Broker = broker;
                Task.Run(async () => await task.Run()).Wait();
            }
        }
    }
}