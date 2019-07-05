using MDispatch.View.Inspection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDispatch.ViewModels.InspectionMV.Servise.Retake
{
    class RetakeAsk5 : IRetake
    {
        private Ask1Page ask1Page = null;
        private Xamarin.Forms.View view = null;

        public RetakeAsk5(Ask1Page ask1Page, Xamarin.Forms.View view)
        {
            this.ask1Page = ask1Page;
            this.view = view;
        }

        public void SetRetakePhoto(byte[] photo)
        {
            ask1Page.ReSetPhoto2(view, photo);
        }
    }
}
