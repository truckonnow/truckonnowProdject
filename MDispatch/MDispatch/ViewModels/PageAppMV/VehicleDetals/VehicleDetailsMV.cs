using MDispatch.Models;
using MDispatch.Service;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MDispatch.ViewModels.PageAppMV.VehicleDetals
{
    public class VehicleDetailsMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigationn { get; set; }

        public VehicleDetailsMV(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation)
        {
            this.managerDispatchMob = managerDispatchMob;
            VehiclwInformation = vehiclwInformation;
        }

        private VehiclwInformation vehiclwInformation = null;
        public VehiclwInformation VehiclwInformation
        {
            get => vehiclwInformation;
            set => SetProperty(ref vehiclwInformation, value);
        }
    }
}
