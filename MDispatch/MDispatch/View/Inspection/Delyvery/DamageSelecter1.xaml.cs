using MDispatch.ViewModels.InspectionMV.DelyveryMV;
using Rg.Plugins.Popup.Pages;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.PickedUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DamageSelecter1 : PopupPage
    {
        private FullPagePhotoDelyveryMV fullPagePhotoDelyveryMV = null;
        private PageAddDamage1 pageAddDamage1 = null;

        public DamageSelecter1(FullPagePhotoDelyveryMV fullPagePhotoDelyveryMV, PageAddDamage1 pageAddDamage1)
        {
            this.pageAddDamage1 = pageAddDamage1;
            fullPagePhotoDelyveryMV = fullPagePhotoDelyveryMV;
            InitializeComponent();
            damageCollection.ItemsSource = collectionDamadge;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            pageAddDamage1.indexSelectDamage = collectionDamadge.IndexOf((string)e.SelectedItem);
            pageAddDamage1.prefNameDamage = e.SelectedItem.ToString().Remove(e.SelectedItem.ToString().IndexOf('-')-1);
            pageAddDamage1.nameDamage = e.SelectedItem.ToString().Remove(0, e.SelectedItem.ToString().IndexOf('-')+2);
            pageAddDamage1.stateSelect = 0;
        }

        protected override bool OnBackgroundClicked()
        {
            pageAddDamage1.stateSelect = 2;
            return true;
        }

        List<string> collectionDamadge = new List<string>()
        {
            "S - Scratched",
            "MS - Multiple Scratcheds",
            "D - Dent",
            "PC - Paint Chip",
            "MD - Major Damage",
            "CR - Cracked",
            "SC - Scuffed",
            "R - Rubbed",
            "FF - Foreingn Fluid",
            "M - Missing",
            "BR - Broken",
            "F - Faded",
            "FT - Flat Tire",
            "G - Gouge",
            "HD - Hail Damage",
            "LC - Loose Contents",
            "RU - Rust",
            "O - Other"
        };
    }
}