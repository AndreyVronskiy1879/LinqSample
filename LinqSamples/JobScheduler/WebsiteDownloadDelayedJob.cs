using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnallyticsProgram.Jobs
{
    class WebsiteDownloadDelayedJob: BaseDelayedJob
    {
        public WebsiteDownloadDelayedJob(DateTime startAt):base(startAt)
        {

        }
        public override Task Execute(DateTime signalTime)
        {
            base.Execute(signalTime);
            WebSiteUtils.Download("https://tut.by", "tutby.txt");
            return Task.CompletedTask;
        }
    }
}
