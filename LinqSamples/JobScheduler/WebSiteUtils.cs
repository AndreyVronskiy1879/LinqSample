using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticsProgram.Utils
{
    class WebSiteUtils
    {
        private const string UserAgent = "Mozilla/4.0(compatible;MSIE 6.0;Windows NT 5.2;.NET CLR 1.0.3705;)";

        public static void Download(string websitePath, string fileNumber)
        {
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", UserAgent);
            string reply = client.DownloadString(websitePath);

            FileUtils.WriteToFile(fileName, reply);
        }
    }
}
