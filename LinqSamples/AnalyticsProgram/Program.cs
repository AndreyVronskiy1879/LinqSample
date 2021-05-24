using System;

namespace AnalyticsProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var scheduler = new JobScheduler.JobScheduler(1000);
            scheduler.RegisterJob(new GitHubRepositoryParserJob());
        }
    }
}
