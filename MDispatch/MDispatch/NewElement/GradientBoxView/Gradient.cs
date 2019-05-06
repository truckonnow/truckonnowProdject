

using Xamarin.Forms;

namespace MDispatch.NewElement.GradientBoxView
{
    public class Gradient : Xamarin.Forms.View
    {
        
             public static readonly BindableProperty GradientColorsProperty = BindableProperty.Create<Gradient, Color[]>(p => p.GradientColors, new Color[] { Color.White });

        public Color[] GradientColors
        {
            get { return (Color[])base.GetValue(GradientColorsProperty); }
            set { base.SetValue(GradientColorsProperty, value); }
        }

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create<Gradient, double>(p => p.CornerRadius, 0);

        public double CornerRadius
        {
            get { return (double)base.GetValue(CornerRadiusProperty); }
            set { base.SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty ViewHeightProperty = BindableProperty.Create<Gradient, double>(p => p.ViewHeight, 0);

        public double ViewHeight
        {
            get { return (double)base.GetValue(ViewHeightProperty); }
            set { base.SetValue(ViewHeightProperty, value); }
        }

        public static readonly BindableProperty ViewWidthProperty = BindableProperty.Create<Gradient, double>(p => p.ViewWidth, 0);

        public double ViewWidth
        {
            get { return (double)base.GetValue(ViewWidthProperty); }
            set { base.SetValue(ViewWidthProperty, value); }
        }

        public static readonly BindableProperty RoundCornersProperty = BindableProperty.Create<Gradient, bool>(p => p.RoundCorners, false);

        public bool RoundCorners
        {
            get { return (bool)base.GetValue(RoundCornersProperty); }
            set { base.SetValue(RoundCornersProperty, value); }
        }

        [System.Obsolete]
        public static readonly BindableProperty LeftToRightProperty = BindableProperty.Create<Gradient, bool>(p => p.LeftToRight, true);

        public bool LeftToRight
        {
            get { return (bool)base.GetValue(LeftToRightProperty); }
            set { base.SetValue(LeftToRightProperty, value); }
        }

    }
}
