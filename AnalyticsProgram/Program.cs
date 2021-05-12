using System;

namespace AnalyticsProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var scheduler = new JobScheduler.JobScheduler(1000);
            scheduler.AddHendler((dateTime) => Console.WriteLine($"Execute{dateTime}"));
        }
    }
}
