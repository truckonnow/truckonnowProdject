using MDispatch.View.ServiceView.ResizeImage;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.PageApp.DialogPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewPhoto : PopupPage
    {
		public ViewPhoto (byte[] image)
		{
			InitializeComponent ();


            imgFull.Source = ImageSource.FromStream(() => new MemoryStream(ResizeImage(JsonConvert.SerializeObject(image))));

        }

        protected byte[] ResizeImage(string base64)
        {
            var assembly = typeof(ViewPhoto).GetTypeInfo().Assembly;
            byte[] imageData;

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64.Replace("\"", ""))))
            {
                imageData = ms.ToArray();
            }
            return DependencyService.Get<IResizeImage>().ResizeImage(imageData, 1080, 1920);
        }

        private void StackLayout_SizeChanged(object sender, EventArgs e)
        {
            imgFull.HeightRequest = Application.Current.MainPage.Height - 100;
            imgFull.WidthRequest = Application.Current.MainPage.Width - 50;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
    }
}