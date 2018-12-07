using MDispatch.Models;
using MDispatch.Service;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDispatch.ViewModels.AskPhoto
{
    public class AskPageMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;

        public AskPageMV(ManagerDispatchMob managerDispatchMob)
        {
            this.managerDispatchMob = managerDispatchMob;
        }

        private List<Ask> asks = null;
        public List<Ask> Asks
        {
            get => asks;
            set => SetProperty(ref asks, value);
        }


    }
}
