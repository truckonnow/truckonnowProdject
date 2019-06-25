using System;

namespace MDispatch.NewElement
{
    public class PhotoResultEventArgs : EventArgs
    {
        public PhotoResultEventArgs()
        {
            Success = false;
        }

        public PhotoResultEventArgs(byte[] result, int width = 0, int height = 0)
        {
            Success = true;
            Result = result;
            Width = width;
            Height = height;
        }

        public byte[] Result { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool Success { get; private set; }
    }
}