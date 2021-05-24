using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace JobScheduler
{
    public class JobScheduler
    {
        private readonly System.Timers.Timer _timer;
        private readonly List<IJob> _jobs = new();
        private readonly List<IDelayedJob> _delayedJobs = new();
        private CancellationTokenSource _cancelTokenSource;
        private CancellationToken _token;

        public JobScheduler(int intervalMs)
        {
            _timer = new System.Timers.Timer(intervalMs);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = false;
        }

        public void RegisterJob(IJob job)
        {
            _jobs.Add(job);
        }

        public void RegisterJob(IDelayedJob job)
        {
            _delayedJobs.Add(job);
        }

        public void Start()
        {
            if (_jobs.Count == 0 && _delayedJobs.Count == 0)
            {
                throw new ArgumentException("Not added jobs!");
            }

            _cancelTokenSource = new();
            _token = _cancelTokenSource.Token;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _cancelTokenSource.Cancel();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs @event)
        {
            OnTimedEventAsync(@event).GetAwaiter().GetResult();
        }

        private async Task OnTimedEventAsync(ElapsedEventArgs @event)
        {
            await ExecuteSimpleJobs(@event);
            await ExecuteDelayedJobs(@event);
        }

        private async Task ExecuteSimpleJobs(ElapsedEventArgs @event)
        {
            await ExecuteJobs(_jobs, @event.SignalTime);
        }

        private async Task ExecuteDelayedJobs(ElapsedEventArgs @event)
        {
            await ExecuteJobs(_delayedJobs.Select(x => x as IJob), @event.SignalTime);
        }

        private async Task ExecuteJobs(IEnumerable<IJob> jobs, DateTime startAt)
        {
            foreach (var job in jobs)
            {
                if (await job.ShouldRun(startAt))
                {
                    ExecuteJob(job, startAt);
                }
            }
        }

        private void ExecuteJob(IJob job, DateTime signalTime)
        {
            try
            {
                job.Execute(signalTime, _token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                job.MarkAsFailed();
            }
        }
    }
}
