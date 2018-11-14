using MDispatch.Service;
using MDispatch.ViewModels.TAbbMV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.TabPage.Tab
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActivePage : ContentPage
	{
        private ActiveMV activeMV = null;
        private StackLayout SelectStackLayout = null;

        public ActivePage (ActiveMV activeMV, ManagerDispatchMob managerDispatchMob)
		{
            this.activeMV = activeMV;
			InitializeComponent ();
            BindingContext = this.activeMV;
		}

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            activeMV.Init();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            StackLayout stackLayout = ((StackLayout)sender);
            if (SelectStackLayout != null)
            {
                SelectStackLayout.BackgroundColor = Color.White;
            }
            SelectStackLayout = stackLayout;
            SelectStackLayout.BackgroundColor = Color.FromHex("#f5c8c8");
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (SelectStackLayout != null)
            {
                SelectStackLayout.BackgroundColor = Color.White;
                SelectStackLayout = null;
            }
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            TapGestureRecognizer_Tapped(sender, e);
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            TapGestureRecognizer_Tapped_1(sender, e);
        }
    }
}