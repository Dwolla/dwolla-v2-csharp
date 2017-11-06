using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ExampleApp.Tasks
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    class TaskAttribute : Attribute
    {
        public TaskAttribute(string command, string description)
        {
            this.Command = command;
            this.Description = description;
        }

        public string Description { get; set; }
        public string Command { get; set; }
    }
}
