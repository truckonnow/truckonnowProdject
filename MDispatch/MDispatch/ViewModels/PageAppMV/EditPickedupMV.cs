using MDispatch.Models;
using MDispatch.Service;
using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.PageAppMV
{
    public class EditPickedupMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public DelegateCommand SavePikedUpCommand { get; set; }
        public INavigation Navigationn { get; set; }

        public EditPickedupMV(ManagerDispatchMob managerDispatchMob, Shipping shipping)
        {
            this.managerDispatchMob = managerDispatchMob;
            Shipping = shipping;
            SavePikedUpCommand = new DelegateCommand(SavePikedUp);
        }

        private Shipping shipping = null;
        public Shipping Shipping
        {
            get => shipping;
            set => SetProperty(ref shipping, value);
        }

        //private string feedback = "";
        //public string Feedback
        //{
        //    get => feedback;
        //    set => SetProperty(ref feedback, value);
        //}

        private async void SavePikedUp()
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() =>
            {
                state = managerDispatchMob.OrderOneWork("SavePikedUp", token, Shipping.Id, Shipping.NameP, Shipping.ContactNameP, Shipping.AddresP, Shipping.CityP, Shipping.StateP, Shipping.ZipD, Shipping.PhoneP, Shipping.EmailP, ref description);
            });
            if (state == 1)
            {
                //FeedBack = "Not Network";
            }
            else if (state == 2)
            {
                //FeedBack = description;
            }
            else if (state == 3)
            {
                await Navigationn.PopAsync(true);
                //Feedback = "";
            }
            else if (state == 4)
            {
                //FeedBack = "Technical work on the service";
            }
        }
    }
}