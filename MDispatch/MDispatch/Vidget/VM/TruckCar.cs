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
                        nameTruck = "Running cluster 1/45";
                        break;
                    }
                case 2:
                    {
                        nameTruck = "Trailer Brake level 2/45";
                        break;
                    }
                case 3:
                    {
                        nameTruck = "Front Bumper 3/45";
                        break;
                    }
                case 4:
                    {
                        nameTruck = "R headlight 4/45";
                        break;
                    }
                case 5:
                    {
                        nameTruck = "L headlight 5/45";
                        break;
                    }
                case 6:
                    {
                        nameTruck = "Grille 6/45";
                        break;
                    }
                case 7:
                    {
                        nameTruck = "R F tire with meter and tire pressure 7/45";
                        break;
                    }
                case 8:
                    {
                        nameTruck = "L F tire with meter and tire pressure 8/45";
                        break;
                    }
                case 9:
                    {
                        nameTruck = "D side  whole side of the truck.gas / def caps 9/45";
                        break;
                    }
                case 10:
                    {
                        nameTruck = "Driver side rear tires with meter 10/45";
                        break;
                    }
                case 11:
                    {
                        nameTruck = "Driver side rear bumper corner 11/45";
                        break;
                    }
                case 12:
                    {
                        nameTruck = "Locked tool box with keys 12/45";
                        break;
                    }
                case 13:
                    {
                        nameTruck = "Full gas canister 13/45";
                        break;
                    }
                case 14:
                    {
                        nameTruck = "Full diesel canister 14/45";
                        break;
                    }
                case 15:
                    {
                        nameTruck = "Whole bed space 15/45";
                        break;
                    }
                case 16:
                    {
                        nameTruck = "Rear trunk lid 16/45";
                        break;
                    }
                case 17:
                    {
                        nameTruck = "Passenger side bumper corner 17/45";
                        break;
                    }
                case 18:
                    {
                        nameTruck = "Passenger side dually tires #1 with meter 18/45";
                        break;
                    }
                case 19:
                    {
                        nameTruck = "Passenger side dually tires #2 with meter 19/45";
                        break;
                    }
                case 20:
                    {
                        nameTruck = "Passenger side whole 20/45";
                        break;
                    }
                case 21:
                    {
                        nameTruck = "Passenger front tires with meter 21/45";
                        break;
                    }
                case 22:
                    {
                        nameTruck = "Windshield 22/45";
                        break;
                    }
                case 23:
                    {
                        nameTruck = "Whole engine bay 23/45";
                        break;
                    }
                case 24:
                    {
                        nameTruck = "Coolant level 24/45";
                        break;
                    }
                case 25:
                    {
                        nameTruck = "Transmission level 25/45";
                        break;
                    }
                case 26:
                    {
                        nameTruck = "Engine oil level 26/45";
                        break;
                    }
                case 27:
                    {
                        nameTruck = "Engine oil cap 27/45";
                        break;
                    }
                case 28:
                    {
                        nameTruck = "Pin 28/45";
                        break;
                    }
                case 29:
                    {
                        nameTruck = "Chains 29/45";
                        break;
                    }
                case 30:
                    {
                        nameTruck = "Ball 30/45";
                        break;
                    }
                case 31:
                    {
                        nameTruck = "Break away cable 31/45";
                        break;
                    }
                case 32:
                    {
                        nameTruck = "Coupler l side 32/45";
                        break;
                    }
                case 33:
                    {
                        nameTruck = "Coupler r side 33/45";
                        break;
                    }
                case 34:
                    {
                        nameTruck = "Whole r side trailer (with visible locks on all doors) 34/45";
                        break;
                    }
                case 35:
                    {
                        nameTruck = "#1 tire with meter 35/45";
                        break;
                    }
                case 36:
                    {
                        nameTruck = "#2 tire with meter 36/45";
                        break;
                    }
                case 37:
                    {
                        nameTruck = "#3 tire with meter 37/45";
                        break;
                    }
                case 38:
                    {
                        nameTruck = "Trailer rear l corner 38/45";
                        break;
                    }
                case 39:
                    {
                        nameTruck = "Rear ramp (with Locks) 39/45";
                        break;
                    }
                case 40:
                    {
                        nameTruck = "Interior light 40/45";
                        break;
                    }
                case 41:
                    {
                        nameTruck = "Minimum 8 working  straps ( Together with ratchets ) . 4 extra new straps 41/45";
                        break;
                    }
                case 42:
                    {
                        nameTruck = "Vacuumed floor 42/45";
                        break;
                    }
                case 43:
                    {
                        nameTruck = "Both ramps on the door with strap 43/45";
                        break;
                    }
                case 44:
                    {
                        nameTruck = "Trailer rear right corner with lights on 44/45";
                        break;
                    }
                case 45:
                    {
                        nameTruck = "Plate # 45/45";
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

        public async void Orinteble(int indexTruckPhoto)
        {
            DependencyService.Get<IOrientationHandler>().ForceLandscape();
            //if (indexTruckPhoto == 1 || indexTruckPhoto == 2 || indexTruckPhoto == 3 || indexTruckPhoto == 6 || indexTruckPhoto == 7 || indexTruckPhoto == 8 || indexTruckPhoto == 9 || indexTruckPhoto == 15 || indexTruckPhoto == 16
            //    || indexTruckPhoto == 20 || indexTruckPhoto == 21 || indexTruckPhoto == 22 || indexTruckPhoto == 23 || indexTruckPhoto == 24 || indexTruckPhoto == 25 || indexTruckPhoto == 26 || indexTruckPhoto == 31 || indexTruckPhoto == 32
            //    || indexTruckPhoto == 33 || indexTruckPhoto == 34 || indexTruckPhoto == 38 || indexTruckPhoto == 39 || indexTruckPhoto == 40 || indexTruckPhoto == 13)
            //{
            //    DependencyService.Get<IOrientationHandler>().ForceLandscape();
            //}
            //else if(indexTruckPhoto == 4 || indexTruckPhoto == 5 || indexTruckPhoto == 10 || indexTruckPhoto == 11 || indexTruckPhoto == 12 || indexTruckPhoto == 14 || indexTruckPhoto == 17 || indexTruckPhoto == 18
            //    || indexTruckPhoto == 19 || indexTruckPhoto == 27 || indexTruckPhoto == 28 || indexTruckPhoto == 29 || indexTruckPhoto == 30 || indexTruckPhoto == 35 || indexTruckPhoto == 36 || indexTruckPhoto == 37 || indexTruckPhoto == 45)
            //{
            //    DependencyService.Get<IOrientationHandler>().ForcePortrait();
            //}
        }
    }
}