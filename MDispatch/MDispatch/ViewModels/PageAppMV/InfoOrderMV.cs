using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View.PageApp;
using Prism.Commands;
using Prism.Mvvm;
using Xamarin.Forms;

namespace MDispatch.ViewModels.PageAppMV
{
    public class InfoOrderMV : BindableBase
    {
        private ManagerDispatchMob managerDispatchMob = null;
        public DelegateCommand ToInstructionComand { get; set; }
        public DelegateCommand ToEditPikedUpCommand { get; set; }
        public INavigation Navigation { get; set; }
        public InfoOrderMV(ManagerDispatchMob managerDispatchMob, Shipping shipping)
        {
            this.managerDispatchMob = managerDispatchMob;
            Shipping = shipping;
            ToInstructionComand = new DelegateCommand(ToInstruction);
            ToEditPikedUpCommand = new DelegateCommand(ToEditPikedUp);
        }

        private Shipping shipping = null;
        public Shipping Shipping
        {
            get => shipping;
            set => SetProperty(ref shipping, value);
        }

        private int count = 0;
        public int Count
        {
            get => Shipping.VehiclwInformations.Count;
            set => SetProperty(ref count, value);
        }

        private void ToInstruction()
        {
            Navigation.PushAsync(new Instruction(this));
        }

        private async void ToEditPikedUp()
        {
            await Navigation.PushAsync(new EditPicupInfo(managerDispatchMob, Shipping), true);
        }
    }
}