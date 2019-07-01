using Xamarin.Forms;

namespace MDispatch.View
{
    public interface IAnimatonPage
    {
        void UnInitElement();
        void InitElement();
        void AnimationNextPage(Page page);
    }
}