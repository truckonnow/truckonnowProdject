using FormsControls.Base;
using MDispatch.NewElement.Tabs;
using MDispatch.Service;
using MDispatch.View.TabPage.Tab;
using MDispatch.ViewModels.TAbbPage;
using Plugin.Badge.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.TabPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabPage : CustomTabbedPage
    {
        private TablePageMV tablePageMV = null;

        public TabPage (ManagerDispatchMob managerDispatchMob)
        {
            tablePageMV = new TablePageMV(managerDispatchMob);
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetPrefersStatusBarHidden(StatusBarHiddenMode.True)
                .SetPreferredStatusBarUpdateAnimation(UIStatusBarAnimation.Fade);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            BindingContext = this.tablePageMV;
            InitActivePage(managerDispatchMob);
            InitDeiveredPage(managerDispatchMob);
            InitArchivedPage(managerDispatchMob);
        }

        private void InitActivePage(ManagerDispatchMob managerDispatchMob)
        {
            AnimationNavigationPage navigationPage = new AnimationNavigationPage(new ActivePage(managerDispatchMob, Navigation));
            navigationPage.Title = "Active";
            navigationPage.IconImageSource = "Aktiv.png";
            Children.Add(navigationPage);
        }

        private void InitDeiveredPage(ManagerDispatchMob managerDispatchMob)
        {
            AnimationNavigationPage navigationPage = new AnimationNavigationPage(new DeiveredPage(managerDispatchMob, Navigation));
            navigationPage.Title = "Deiveredge";
            navigationPage.IconImageSource = "Delivery.png";
            Children.Add(navigationPage);
        }

        private void InitArchivedPage(ManagerDispatchMob managerDispatchMob)
        {
            AnimationNavigationPage navigationPage = new AnimationNavigationPage(new ArchivedPage(managerDispatchMob, Navigation));
            navigationPage.Title = "Archived";
            navigationPage.IconImageSource = "Archive.png";
            Children.Add(navigationPage);
        }
    }
}