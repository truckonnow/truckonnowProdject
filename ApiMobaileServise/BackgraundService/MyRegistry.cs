using ApiMobaileServise.BackgraundService.InspactionDrive;
using ApiMobaileServise.BackgraundService.OrderWork;
using ApiMobaileServise.BackgraundService.Queue;
using FluentScheduler;

namespace ApiMobaileServise.BackgraundService
{
    public class MyRegistry : Registry
    {
        public MyRegistry()
        {
            Schedule<ManagerInspectionTodayTimeDriver>().ToRunEvery(1).Days().At(5, 59);
            Schedule<ManagerInspectionTimeDriver>().ToRunEvery(1).Days().At(11, 59);
            Schedule<OrderGOToArchive>().ToRunNow().AndEvery(2).Hours();
            Schedule<QueueWorker>().ToRunNow().AndEvery(1).Seconds();
            Schedule<QueueWorkerAsk>().ToRunNow().AndEvery(1).Seconds();
        }
    }
}