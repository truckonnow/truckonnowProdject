using System;

namespace MDispatch.NewElement.ImageSize
{
    public class LoadImageActionEventArgs : EventArgs
    {
        public LoadImageActionEventArgs(double heigh, double width)
        {
            Height = heigh;
            Width = width;
        }

        public double Height { private set; get; }
        public double Width { private set; get; }
    }
}