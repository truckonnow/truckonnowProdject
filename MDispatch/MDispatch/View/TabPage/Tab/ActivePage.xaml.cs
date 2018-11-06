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

        public ActivePage (ActiveMV activeMV, ManagerDispatchMob managerDispatchMob)
		{
            this.activeMV = activeMV;
			InitializeComponent ();
            BindingContext = this.activeMV;
		}
	}
}