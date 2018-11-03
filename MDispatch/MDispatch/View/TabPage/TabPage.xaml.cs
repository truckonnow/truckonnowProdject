using MDispatch.Service;
using MDispatch.View.A_R;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.TabPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabPage : Xamarin.Forms.TabbedPage
    {
        ManagerDispatchMob managerDispatchMob = null;
        public TabPage (ManagerDispatchMob managerDispatchMob)
        {
            this.managerDispatchMob = managerDispatchMob;
            InitializeComponent();
            
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            BindingContext = this.managerDispatchMob;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {

            var navigationPage = new NavigationPage(new Avtorization());
            navigationPage.Title = "Schedule";
            
        }
    }
}