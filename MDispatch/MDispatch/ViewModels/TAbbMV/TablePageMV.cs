using MDispatch.Service;
using MDispatch.ViewModels.TAbbMV;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDispatch.ViewModels.TAbbPage
{
    public class TablePageMV
    {
        private ManagerDispatchMob managerDispatchMob = null;
        public ActiveMV activeMV = null;

        public TablePageMV(ManagerDispatchMob managerDispatchMob)
        {
            activeMV = new ActiveMV(managerDispatchMob);
            this.managerDispatchMob = managerDispatchMob;
        }
    }
}
