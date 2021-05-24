using AnallyticsProgram.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticsProgram.Jobs
{
    public class DownloadWebsiteJob : BaseJob
    {
        private const string FilePath = "StackOverflow.txt";
        private const WebsitePath= "https://stackoverflow.com/questions/2633/fastest-c-sharp-code-download-a-web-page";

        public override Task Execute(DateTime signalTime)
        {
            WebsiteUtils.Download(WebsitePath, FilePath);
            return Task.CompletedTask;
        }
    }
}
