using ApiMobaileServise.BackgraundService.InspactionDrive;
using FluentScheduler;

namespace ApiMobaileServise.BackgraundService
{
    public class MyRegistry : Registry
    {
        public MyRegistry()
        {
            Schedule<ManagerInspectionTodayTimeDriver>().ToRunEvery(1).Days().At(5, 59);
            Schedule<ManagerInspectionTimeDriver>().ToRunEvery(1).Days().At(11, 59);
        }
    }
}