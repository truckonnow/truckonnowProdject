using MDispatch.Models;
using MDispatch.Service;
using MDispatch.ViewModels.PageAppMV.VehicleDetals;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.PageApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VechicleDetails : ContentPage
	{
        VehicleDetailsMV vehicleDetailsMV = null;

        public VechicleDetails (VehiclwInformation vehiclwInformation, ManagerDispatchMob managerDispatchMob)
		{
            vehicleDetailsMV = new VehicleDetailsMV(managerDispatchMob, vehiclwInformation);
			InitializeComponent ();
            BindingContext = vehicleDetailsMV;
        }
	}
}