using System;

namespace MDispatch.NewElement.ResIzeImage
{
    public class TouchActionEventArgs : EventArgs
    {
        public TouchActionEventArgs(int increasePerUnit)
        {
            IncreasePerUnit = increasePerUnit;
        }

        public int IncreasePerUnit { get; private set; }
    }
}