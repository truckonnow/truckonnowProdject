using MDispatch.NewElement;
using MDispatch.View.ServiceView.ResizeImage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.PageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPhoto : ContentPage
    {
        public ViewPhoto(byte[] image)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            imageFull.Source = ImageSource.FromStream(() => new MemoryStream(ResizeImage(image)));
        }

        protected byte[] ResizeImage(byte[] image)
        {
            var assembly = typeof(ViewPhoto).GetTypeInfo().Assembly;
            byte[] imageData;
            using (MemoryStream ms = new MemoryStream(image))
            {
                imageData = ms.ToArray(); ;
            }
            int width = DependencyService.Get<IResizeImage>().GetWidthImage(imageData);
            int heigth = DependencyService.Get<IResizeImage>().GetHeigthImage(imageData);
            if(width < heigth)
            {
                DependencyService.Get<IOrientationHandler>().ForcePortrait();
            }
            else
            {
                DependencyService.Get<IOrientationHandler>().ForceLandscape();
            }
            return DependencyService.Get<IResizeImage>().ResizeImage(imageData, (width * 100) / 40, (heigth * 100) / 40);
        }

        protected override bool OnBackButtonPressed()
        {
            DependencyService.Get<IOrientationHandler>().ForceSensor();
            return base.OnBackButtonPressed();
        }
    }
}