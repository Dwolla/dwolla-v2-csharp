using System;

namespace ExampleApp.HttpServices.Tasks
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class TaskAttribute : Attribute
    {
        public TaskAttribute(string command, string description)
        {
            Command = command;
            Description = description;
        }

        public string Description { get; set; }
        public string Command { get; set; }
    }
}