using System;
using System.ComponentModel.Composition;
using HBD.Mef.Console;
using HBD.Mef.Logging;
using HBD.Mef.Modularity;

namespace Job.Module
{
    [PluginExport("Job2", typeof(Job2))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class Job2 : ConsoleModuleBase
    {
        public override void Initialize()
        {
        }

        public override void Run(params string[] args)
        {
            var message = $"{this.GetType().Name} was ran with parameters '{string.Join(",", args)}'";

            Console.WriteLine(message);
            Console.Read();

            this.Logger.Info(message);
        }
    }
}