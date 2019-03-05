using MDispatch.ViewModels.InspectionMV.DelyveryMV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.Delyvery
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddDamageForScan : ContentPage
    {
        private FullPagePhotoDelyveryMV fullPagePhotoDelyveryMV = null;

        public AddDamageForScan (FullPagePhotoDelyveryMV fullPagePhotoDelyveryMV)
        {
            this.fullPagePhotoDelyveryMV = fullPagePhotoDelyveryMV;
            InitializeComponent ();
		}
	}
}