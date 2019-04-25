using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.Servise
{
    public class ManagerParser
    {
        private ConnectorDispatch connectorDispatch = null;

        public ManagerParser()
        {
            connectorDispatch = new ConnectorDispatch();
            WorkParser();
        }

        private void WorkParser()
        {
            LogEr.Logerr("Info", "Start Parser", "WorkParser", DateTime.Now.ToShortTimeString());
            int horseInmMiliSeconds = 60000 * 60;
            Task.Run(() =>
            {
                while (true)
                {
                    LogEr.Logerr("Info", "Start pulling data from the site", "WorkParser", DateTime.Now.ToShortTimeString());
                    connectorDispatch.Worker();
                    LogEr.Logerr("Info", "Pulling data from the site successfully, The following data will be drawn from the site after 1 hour", "WorkParser", DateTime.Now.ToShortTimeString());
                    Thread.Sleep(horseInmMiliSeconds);
                }
            }).GetAwaiter().GetResult();
        }
    }
}
