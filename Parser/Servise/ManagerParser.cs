using Parser.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.Servise
{
    public class ManagerParser
    {
        private SqlEntityFramworkeP _sqlEntityFramworkeP = null;
        private ConnectorDispatch connectorDispatch = null;

        public ManagerParser()
        {
            connectorDispatch = new ConnectorDispatch();
            _sqlEntityFramworkeP = new SqlEntityFramworkeP();
            WorkParser();
        }

        private void WorkParser()
        {
            int horseInmMiliSeconds = 60000 * 60; 
            Task.Run(() =>
            {
                while(true)
                {
                    connectorDispatch.Worker();
                    Thread.Sleep(horseInmMiliSeconds);
                }
            }).GetAwaiter().GetResult();
        }
    }
}
