using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.PickedUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PhotoInspectionWeb : ContentPage
	{
		public PhotoInspectionWeb(string urlPhotoInspect)
		{
			InitializeComponent ();
            photoInspWeb.Source = urlPhotoInspect;
		}

        private void StackLayout_SizeChanged(object sender, EventArgs e)
        {
            photoInspWeb.HeightRequest = Application.Current.MainPage.Height - 50;
            photoInspWeb.WidthRequest = Application.Current.MainPage.Width -50;
        }
    }
}