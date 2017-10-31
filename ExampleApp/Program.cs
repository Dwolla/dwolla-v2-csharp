using System;
using System.Linq;
using System.Threading.Tasks;
using Dwolla.Client;
using static System.Console;
using ExampleApp.Tasks;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;

namespace ExampleApp
{
    public class Program
    {
        static void Main()
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
                var broker = new DwollaBroker(DwollaClient.Create(isSandbox: true));

                Task.Run(async () => await broker.SetAuthroizationHeader(key, secret)).Wait();

                while (running)
                {
                    Write("What would you like to do? (Press ? for options): ");
                    var input = ReadLine();

                    switch (input.ToLower().Trim())
                    {
                        case "?":
                            WriteLine(@"Options:
 - Quit (q)
 - Help (?)");
                            GetTasks().ForEach(ta => WriteLine($" - {ta.Description} ({ta.Command})"));
                            break;

                        case "quit":
                        case "q":
                        case "exit":
                            running = false;
                            break;

                        default:
                            BeginTask(input.ToLower().Trim(), broker);
                            break;
                    }
                }
            }
        }

        private static Dictionary<string, Type> _tasks = null;
        private static List<TaskAttribute> GetTasks()
        {
            if (_tasks == null)
            {
                _tasks = Assembly.GetEntryAssembly().GetTypes()
                    .Where(x => typeof(BaseTask).IsAssignableFrom(x) && !x.GetTypeInfo().IsAbstract && x.GetTypeInfo().GetCustomAttribute<TaskAttribute>() != null)
                    .ToDictionary(x => x.GetTypeInfo().GetCustomAttribute<TaskAttribute>().Command);
            }

            return _tasks
                .OrderBy(x => x.Value.Name)
                .Select(x => x.Value.GetTypeInfo().GetCustomAttribute<TaskAttribute>())
                .ToList();
        }

        private static void BeginTask(string command, DwollaBroker broker)
        {
            if (!_tasks.ContainsKey(command))
            {
                WriteLine("That option is not recognized as a valid option");
            }
            else
            {
                var type = _tasks[command];
                BaseTask task = (BaseTask)type.GetConstructor(new Type[0]).Invoke(new object[0]);
                task.Broker = broker;
                Task.Run(async () => await task.Run()).Wait();
            }
        }
    }
}