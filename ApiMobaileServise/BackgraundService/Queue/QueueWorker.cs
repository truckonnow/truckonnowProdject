using ApiMobaileServise.Servise;
using FluentScheduler;
using System;
using System.Collections.Generic;

namespace ApiMobaileServise.BackgraundService.Queue
{
    public class QueueWorker : IJob
    {
        private ManagerMobileApi managerMobileApi = null;
        public static List<string> queues = new List<string>();
        public static int countQueues = 0;
        public static bool isWork = false;

        public void Execute()
        {
            managerMobileApi = new ManagerMobileApi();
            QWork();
        }

        private async void QWork()
        {
            if (queues.Count != 0 && !isWork)
            {
                isWork = true;
                int tmpCount = queues.Count > 2000 ? queues.Count - (countQueues - 2000) : queues.Count;
                for (int i = 0; i < tmpCount; i++)
                {
                    if(queues[i].Split(',')[0] == "Load")
                    {
                        await managerMobileApi.LoadTask(queues[i].Split(',')[1], queues[i].Split(',')[2]);
                    }
                    else if (queues[i].Split(',')[0] == "End")
                    {
                        await managerMobileApi.EndTask(queues[i].Split(',')[1], queues[i].Split(',')[2]);
                    }
                    countQueues--;
                }
                queues.RemoveRange(0, tmpCount);
                isWork = false;
            }
        }
    }
}