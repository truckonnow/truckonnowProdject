using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.PickedUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageAddDamage : ContentPage
	{
		public PageAddDamage (ImageSource imageSource)
		{
			InitializeComponent ();
            touchImage.Source = imageSource;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void TouchImage_TouchAction(object sender, NewElement.TouchCordinate.TouchActionEventArgs e)
        {
            Image image = new Image()
            {
                Source = "Active.png",
                WidthRequest = 10,
                HeightRequest = 10,
            };
            AbsoluteLayout.SetLayoutBounds(image, new Rectangle(e.XInterest*0.0001, e.YInterest*0.0001, 10, 10));
            AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.PositionProportional);
            absla.Children.Add(image);
        }
    }
}