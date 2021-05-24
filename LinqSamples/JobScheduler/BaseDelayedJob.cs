using System;
using System.Threading.Tasks;
using AnallyticsProgram.Jobs;
using JobScheduler;

namespace JobScheduler
{
    public abstract class BaseDelayedJob : BaseJob, IDelayedJob
    {
        private bool _hasRun;
        private readonly DateTime _startAt;

        protected BaseDelayedJob(DateTime signalTime)
        {
            _startAt = signalTime;
        }

        public override Task Execute(DateTime signalTime)
        {
            _hasRun = true;
        }

        public override async Task<bool> ShouldRun(DateTime signalTime)
        {
            bool baseResult =await base.ShouldRun(signalTime);
            return baseResult && _startAt < signalTime && !_hasRun;
        }
    }
}
