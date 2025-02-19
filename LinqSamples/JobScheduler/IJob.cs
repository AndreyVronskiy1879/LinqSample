﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler
{
    public interface IJob
    {
        bool IsFailed { get; set; }
        DateTime StartJobAt { get; set; }

        void Execute(DateTime signalTime);
    }
}
