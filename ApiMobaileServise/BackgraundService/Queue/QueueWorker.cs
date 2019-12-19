using ApiMobaileServise.Servise;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiMobaileServise.BackgraundService.Queue
{
    public class QueueWorker : IJob
    {
        private ManagerMobileApi managerMobileApi = null;
        public static List<string> queues = new List<string>();
        public static int countQueues = 0;

        public void Execute()
        {
            managerMobileApi = new ManagerMobileApi();
            Task.Run(() => QWork());
        }

        private async void QWork()
        {
            if (queues.Count != 0)
            {
                int tmpCount = queues.Count > 5000 ? queues.Count - (countQueues - 5000) : queues.Count;
                for (int i = 0; i < tmpCount; i++)
                {
                    if(queues[i].Split(',')[0] == "Load")
                    {
                        await managerMobileApi.LoadTask(queues[i].Split(',')[1], queues[i].Split(',')[2]);
                    }
                    else if (queues[i].Split(',')[0] == "End" && queues.FirstOrDefault(q => q.Split(',')[0] == "Load") == null)
                    {
                        await managerMobileApi.EndTask(queues[i].Split(',')[1], queues[i].Split(',')[2]);
                    }
                    countQueues--;
                }
                queues.RemoveRange(0, tmpCount);
            }
        }
    }
}