using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MDispatch.Droid.NewrRender.Sider;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(Slider), typeof(CusstomSider))]
namespace MDispatch.Droid.NewrRender.Sider
{
    class CusstomSider : SliderRenderer
    {
        public CusstomSider(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.SetProgressDrawableTiled(
                Resources.GetDrawable(
                Resource.Drawable.custom_progressbar_style,
                (this.Context).Theme));
                Control.SetThumb(new ColorDrawable(Android.Graphics.Color.Transparent));
            }
        }
    }
}