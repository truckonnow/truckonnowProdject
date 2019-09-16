using MDispatch.View.AskPhoto;
using MDispatch.ViewModels.InspectionMV.Servise.Retake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPhotForAsk : ContentPage
    {
        private Xamarin.Forms.View view = null;
        private object ask = null;
        private string namePage = null;

        public ViewPhotForAsk(Xamarin.Forms.View view, object ask, string namePage)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.view = view;
            this.ask = ask;
            parentPhoto.Source = ((Image)view).Source;
            this.namePage = namePage;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            IRetake retake = GetRetake();
            await Navigation.PushAsync(new RetakePage(retake));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private IRetake GetRetake()
        {
            IRetake retake = null;
            switch(namePage)
            {
                case "Ask1":
                    {
                        retake = new RetakeAsk2((AskPage)ask, view);
                        break;
                    }
                case "Ask5":
                    {
                        retake = new RetakeAsk5((Ask1Page)ask, view);
                        break;
                    }
                case "Ask6":
                    {
                        retake = new RetakeAsk6((Ask1Page)ask, view);
                        break;
                    }
            }
            return retake;
        }
    }
}