using MDispatch.NewElement;
using MDispatch.View.GlobalDialogView;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MDispatch.Vidget.VM
{
    public class TruckCar
    {
        public int CountPhotoTruck { get; private set; } = 45;

        public string GetNameTruck(int indexTruckPhoto)
        {
            string nameTruck = null;
            switch(indexTruckPhoto)
            {
                case 1:
                    {
                        nameTruck = "Running cluster";
                        break;
                    }
                case 2:
                    {
                        nameTruck = "Trailer Brake level";
                        break;
                    }
                case 3:
                    {
                        nameTruck = "Front Bumper";
                        break;
                    }
                case 4:
                    {
                        nameTruck = "R headlight";
                        break;
                    }
                case 5:
                    {
                        nameTruck = "L headlight";
                        break;
                    }
                case 6:
                    {
                        nameTruck = "Grille";
                        break;
                    }
                case 7:
                    {
                        nameTruck = "R F tire with meter and tire pressure";
                        break;
                    }
                case 8:
                    {
                        nameTruck = "L F tire with meter and tire pressure";
                        break;
                    }
                case 9:
                    {
                        nameTruck = "D side  whole side of the truck.gas / def caps";
                        break;
                    }
                case 10:
                    {
                        nameTruck = "Driver side rear tires with meter";
                        break;
                    }
                case 11:
                    {
                        nameTruck = "Driver side rear bumper corner";
                        break;
                    }
                case 12:
                    {
                        nameTruck = "Locked tool box with keys";
                        break;
                    }
                case 13:
                    {
                        nameTruck = "Full gas canister";
                        break;
                    }
                case 14:
                    {
                        nameTruck = "Full diesel canister";
                        break;
                    }
                case 15:
                    {
                        nameTruck = "Whole bed space";
                        break;
                    }
                case 16:
                    {
                        nameTruck = "Rear trunk lid";
                        break;
                    }
                case 17:
                    {
                        nameTruck = "Passenger side bumper corner";
                        break;
                    }
                case 18:
                    {
                        nameTruck = "Passenger side dually tires #1 with meter";
                        break;
                    }
                case 19:
                    {
                        nameTruck = "Passenger side dually tires #2 with meter";
                        break;
                    }
                case 20:
                    {
                        nameTruck = "Passenger side whole";
                        break;
                    }
                case 21:
                    {
                        nameTruck = "Passenger front tires with meter";
                        break;
                    }
                case 22:
                    {
                        nameTruck = "Windshield";
                        break;
                    }
                case 23:
                    {
                        nameTruck = "Whole engine bay";
                        break;
                    }
                case 24:
                    {
                        nameTruck = "Coolant level";
                        break;
                    }
                case 25:
                    {
                        nameTruck = "Transmission level";
                        break;
                    }
                case 26:
                    {
                        nameTruck = "Engine oil level";
                        break;
                    }
                case 27:
                    {
                        nameTruck = "Engine oil cap";
                        break;
                    }
                case 28:
                    {
                        nameTruck = "Pin";
                        break;
                    }
                case 29:
                    {
                        nameTruck = "Chains";
                        break;
                    }
                case 30:
                    {
                        nameTruck = "Ball";
                        break;
                    }
                case 31:
                    {
                        nameTruck = "Break away cable";
                        break;
                    }
                case 32:
                    {
                        nameTruck = "Coupler l side";
                        break;
                    }
                case 33:
                    {
                        nameTruck = "Coupler r side";
                        break;
                    }
                case 34:
                    {
                        nameTruck = "Whole r side trailer (with visible locks on all doors)";
                        break;
                    }
                case 35:
                    {
                        nameTruck = "#1 tire with meter";
                        break;
                    }
                case 36:
                    {
                        nameTruck = "#2 tire with meter";
                        break;
                    }
                case 37:
                    {
                        nameTruck = "#3 tire with meter";
                        break;
                    }
                case 38:
                    {
                        nameTruck = "Trailer rear l corner";
                        break;
                    }
                case 39:
                    {
                        nameTruck = "Rear ramp (with Locks)";
                        break;
                    }
                case 40:
                    {
                        nameTruck = "Interior light";
                        break;
                    }
                case 41:
                    {
                        nameTruck = "Minimum 8 working  straps ( Together with ratchets ) . 4 extra new straps";
                        break;
                    }
                case 42:
                    {
                        nameTruck = "Vacuumed floor";
                        break;
                    }
                case 43:
                    {
                        nameTruck = "Both ramps on the door with strap";
                        break;
                    }
                case 44:
                    {
                        nameTruck = "Trailer rear right corner with lights on";
                        break;
                    }
                case 45:
                    {
                        nameTruck = "Plate # ";
                        break;
                    }
            }
            return nameTruck;
        }

        public async void GetModalAlert(int indexTruckPhoto)
        {
            switch(indexTruckPhoto)
            {
                case 1:
                    {
                        await PopupNavigation.PushAsync(new Errror("Please do interior inspection", null));
                        break;
                    }
                case 3:
                    {
                        await PopupNavigation.PushAsync(new Errror("Please do exterior inspection", null));
                        break;
                    }
                case 23:
                    {
                        await PopupNavigation.PushAsync(new Errror("Please do the inspection under the hood, do the inspection with the engine off for 5 minutes", null));
                        break;
                    }
                case 28:
                    {
                        await PopupNavigation.PushAsync(new Errror("Please do еrailer connections inspection", null));
                        break;
                    }
            }
        }

        public void Orinteble(int indexTruckPhoto)
        {
            DependencyService.Get<IOrientationHandler>().ForceSensor();
        }
    }
}
