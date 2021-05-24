using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnallyticsProgram.Jobs
{
    public class LogExecutionTimeInConsole : BaseJob
    {
        public override Task Execute(DateTime signalTime)
        {
            Console.WriteLine($"Executed:{signalTime}");
            return Task.CompletedTask;
        }
    }
}
