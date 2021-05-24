using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler
{
    class GitHubRepositoryParserJob:BaseJob
    {
        private static readonly HttpClient client = new ();

        public GitHubRepositoryParserJob()
        {
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            Client.DefaultRequestHeaders.Add ("User-Agent",".NET Foundation Repository Reporter");
        }

        public override async Task Execute(DateTime signalTime)
        {
            var result =await ProcessRepositoties();
            Console.WriteLine(result);
        }

        public override async Task<bool> ShouldRun(DateTime signalTime)
        {
            string isConnected = await client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
            return await base.ShouldRun(signalTime) && isConnected.Length>0 ;
        }
        private static async Task ProcessRepositoties()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            return await stringTask;
        }
    }
}
