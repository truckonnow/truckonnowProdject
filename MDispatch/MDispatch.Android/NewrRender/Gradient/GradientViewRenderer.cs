using MDispatch.Droid.NewrRender.Gradient;
using MDispatch.NewElement.GradientBoxView;
using Android.Widget;
using Android.Content;
using Xamarin.Forms.Platform.Android;
using System.Linq;
using Android.Graphics.Drawables;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Gradient), typeof(GradientViewRenderer))]
namespace MDispatch.Droid.NewrRender.Gradient
{
    class GradientViewRenderer : Xamarin.Forms.Platform.Android.ViewRenderer<NewElement.GradientBoxView.Gradient, Android.Views.View>
    {
        LinearLayout layout;
        Xamarin.Forms.Color[] gradientColors;
        float cornerRadius;
        double viewHeight;
        double viewWidth;
        bool roundCorners;

        public GradientViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<NewElement.GradientBoxView.Gradient> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                layout = new LinearLayout(Android.App.Application.Context);
                layout.SetBackgroundColor(Android.Graphics.Color.White);

                gradientColors = (Xamarin.Forms.Color[])e.NewElement.GradientColors;
                cornerRadius = (float)e.NewElement.CornerRadius;
                viewWidth = (double)e.NewElement.ViewWidth;
                viewHeight = (double)e.NewElement.ViewHeight;
                roundCorners = (bool)e.NewElement.RoundCorners;

                CreateLayout();
            }

            if (e.OldElement != null)
            {
                // Unsubscribe from event handlers and cleanup any resources
            }

            if (e.NewElement != null)
            {
                // Configure the control and subscribe to event handlers
                gradientColors = (Xamarin.Forms.Color[])e.NewElement.GradientColors;
                cornerRadius = (float)e.NewElement.CornerRadius;
                viewWidth = (double)e.NewElement.ViewWidth;
                viewHeight = (double)e.NewElement.ViewHeight;
                roundCorners = (bool)e.NewElement.RoundCorners;

                CreateLayout();
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == NewElement.GradientBoxView.Gradient.ViewHeightProperty.PropertyName)
            {
                this.viewHeight = (double)this.Element.ViewHeight;
                CreateLayout();
            }
            else if (e.PropertyName == NewElement.GradientBoxView.Gradient.ViewWidthProperty.PropertyName)
            {
                this.viewWidth = (double)this.Element.ViewWidth;
                CreateLayout();
            }
            else if (e.PropertyName == NewElement.GradientBoxView.Gradient.GradientColorsProperty.PropertyName)
            {
                this.gradientColors = (Xamarin.Forms.Color[])this.Element.GradientColors;
                CreateLayout();
            }
            else if (e.PropertyName == NewElement.GradientBoxView.Gradient.CornerRadiusProperty.PropertyName)
            {
                this.cornerRadius = (float)this.Element.CornerRadius;
                CreateLayout();
            }
            else if (e.PropertyName == NewElement.GradientBoxView.Gradient.RoundCornersProperty.PropertyName)
            {
                this.roundCorners = (bool)this.Element.RoundCorners;
                CreateLayout();
            }
        }

        private void CreateLayout()
        {
            layout.SetMinimumWidth((int)viewWidth);
            layout.SetMinimumHeight((int)viewHeight);

            CreateGradient();

            SetNativeControl(layout);
        }

        public void CreateGradient()
        {
            //Need to convert the colors to Android Color objects
            int[] androidColors = new int[gradientColors.Count()];

            for (int i = 0; i < gradientColors.Count(); i++)
            {
                Xamarin.Forms.Color temp = gradientColors[i];
                androidColors[i] = temp.ToAndroid();
            }

            GradientDrawable gradient = new GradientDrawable(GradientDrawable.Orientation.LeftRight, androidColors);

            if (roundCorners)
                gradient.SetCornerRadii(new float[] { cornerRadius, cornerRadius, cornerRadius, cornerRadius, cornerRadius, cornerRadius, cornerRadius, cornerRadius });

            layout.SetBackground(gradient);
        }
    }
}