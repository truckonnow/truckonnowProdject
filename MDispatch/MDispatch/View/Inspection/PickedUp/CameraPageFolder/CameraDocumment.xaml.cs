﻿using MDispatch.NewElement;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.PickedUp.CameraPageFolder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraDocumment : CameraPage
    {
        Ask2Page ask2Page = null;

        public CameraDocumment(Ask2Page ask2Page)
        {
            InitializeComponent();
            this.ask2Page = ask2Page;
        }

        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            if (!result.Success)
                return;
            ask2Page.AddPhotoDocumments(result.Result);
            await Navigation.PopAsync();
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}