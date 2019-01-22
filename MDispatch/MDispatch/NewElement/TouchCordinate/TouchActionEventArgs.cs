using System;
using Xamarin.Forms;

namespace MDispatch.NewElement.TouchCordinate
{
    public class TouchActionEventArgs : EventArgs
    {
        public TouchActionEventArgs(Point location, double heigh, double width, double xInterest, double yInterest)
        {
            Location = location;
            Height = heigh;
            Width = width;
            YInterest = Math.Round(yInterest, 0);
            XInterest = Math.Round(xInterest, 0);
        }
        
        public Point Location { private set; get; }
        public double Height { private set; get; }
        public double Width { private set; get; }
        public double XInterest { private set; get; }
        public double YInterest { private set; get; }
    }
}