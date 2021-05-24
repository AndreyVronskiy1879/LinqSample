using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler
{
    public interface IJob
    {
        Task <bool> ShoudRun(DateTime signalTime);
        void MarkAsFailed();

        Task Execute(DateTime signalTime);
    }
}
