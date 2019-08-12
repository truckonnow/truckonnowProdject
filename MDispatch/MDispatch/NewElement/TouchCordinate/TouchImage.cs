
using System;
using Xamarin.Forms;

namespace MDispatch.NewElement.TouchCordinate
{
    public class TouchImage : Image
    {
        public event TouchActionEventHandler TouchAction;
        
        public void FireClick(TouchActionEventArgs e)
        {
            if (this.TouchAction != null)
                this.TouchAction(this, e);
        }
    }
}
