using MDispatch.Service;
using MDispatch.View.A_R;
using MDispatch.View.TabPage.Tab;
using MDispatch.ViewModels.TAbbPage;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.TabPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabPage : Xamarin.Forms.TabbedPage
    {
        private TablePageMV tablePageMV = null;

        public TabPage (ManagerDispatchMob managerDispatchMob)
        {
            tablePageMV = new TablePageMV(managerDispatchMob);
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            BindingContext = this.tablePageMV;
            InitActivePage(managerDispatchMob);
            InitDeiveredPage(managerDispatchMob);
            InitArchivedPage(managerDispatchMob);
        }

        private void InitActivePage(ManagerDispatchMob managerDispatchMob)
        {
            NavigationPage navigationPage = new NavigationPage(new ActivePage(tablePageMV.activeMV, managerDispatchMob));
            navigationPage.Title = "Active";
            navigationPage.BarBackgroundColor = Color.FromHex("#f3f3f3");
            navigationPage.BarTextColor = Color.Black;
            Children.Add(navigationPage);
        }

        private void InitDeiveredPage(ManagerDispatchMob managerDispatchMob)
        {
            NavigationPage navigationPage = new NavigationPage(new DeiveredPage());
            navigationPage.Title = "Deiveredge";
            navigationPage.BarBackgroundColor = Color.FromHex("#f3f3f3");
            navigationPage.BarTextColor = Color.Black;
            Children.Add(navigationPage);
        }

        private void InitArchivedPage(ManagerDispatchMob managerDispatchMob)
        {
            NavigationPage navigationPage = new NavigationPage(new ArchivedPage());
            navigationPage.Title = "Archived";
            navigationPage.BarBackgroundColor = Color.FromHex("#f3f3f3");
            navigationPage.BarTextColor = Color.Black;
            Children.Add(navigationPage);
        }
    }
}