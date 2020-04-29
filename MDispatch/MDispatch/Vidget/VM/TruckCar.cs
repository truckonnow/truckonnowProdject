using System.Collections.Generic;
using System.Threading.Tasks;
using MDispatch.NewElement;
using MDispatch.View.GlobalDialogView;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MDispatch.Vidget.VM
{
    public class TruckCar
    {
        public int CountPhoto { get; set; }
        public string Type { get; set; }
        public bool IsNextInspection { get; set; }
        public List<string> NamePatern { get; set; }
        public string PlateTruck { get; set; }
        public string PlateTraler { get; set; }
    }
}