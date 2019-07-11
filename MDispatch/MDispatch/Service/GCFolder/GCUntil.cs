using System;
using System.Threading;

namespace MDispatch.Service.GCFolder
{
    public class GCUntil
    {
        private static bool isWork = false;
        private static Timer timer = null;

        public static void StartClereing()
        {
            if(!isWork)
            {
                timer = new Timer(new TimerCallback(WorkGC), null, 100000, 100000);
                isWork = true;
            }
        }

        private static void WorkGC(object state)
        {
            if (isWork)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public static void StopClereing()
        {
            if (isWork)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                timer = null;
                isWork = false;
            }
        }
    }
}