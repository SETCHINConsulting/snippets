using System;
using Microsoft.Extensions.Configuration.CommandLine;

namespace CommandLineArgs
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new CommandLineConfigurationProvider(args);
            c.Load();
            string serverName = string.Empty;
            c.TryGet("server", out serverName);

            string username = string.Empty;
            c.TryGet("username", out username);

            string database = string.Empty;
            c.TryGet("database", out database);
        }
    }
}
