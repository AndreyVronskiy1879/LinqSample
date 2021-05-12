using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace JobScheduler
{
    public class JobScheduler
    {
        private readonly Timer _timer;
        private Action<DateTime> _action;
        private readonly List<IJob> _jobs = new();
        public JobScheduler(int intervalMs)
        {
            var timer = new System.Timers.Timer(intervalMs);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = false;
        }
        public void AddHandler(IJob job)
        {
            _jobs.Add(job);
        }

        public void Start()
        {
            if (_jobs.Count == 0)
            {
                throw new ArgumentException("Not added jobs!");
            }

            _timer.Start();
        }

        public void Stop() => _timer.Stop();

        private void OnTimedEvent(object sender, ElapsedEventArgs @event)
        {
            foreach (var job in _jobs.Where(j => !j.IsFailed))
            {
                try
                {
                    job.Execute(@event.SignalTime);
                }
                catch
                {
                    Console.WriteLine($"error {job.GetType().Name}. DateTime: {DateTime.Now}");
                    job.IsFailed = true;
                }
            }
        }
    }
}
