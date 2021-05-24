using JobScheduler;
using System;
using System.Threading.Tasks;

namespace AnallyticsProgram.Jobs
{
    public abstract class BaseJob : IJob
    {
        private bool _isFailed;
        public abstract Task Execute(DateTime signalTime);

        public virtual Task <bool> ShouldRun(DateTime signalTime)
        {
            return Task.FromResult(!_isFailed);
        }

        public virtual void MarkAsFailed()
        {
            _isFailed = true;
        }
        public abstract Task<bool> ShoudRun(DateTime signalTime);
    }
}
