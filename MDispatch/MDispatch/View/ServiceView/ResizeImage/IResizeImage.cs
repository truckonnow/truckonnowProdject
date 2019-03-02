namespace MDispatch.View.ServiceView.ResizeImage
{
    public interface IResizeImage
    {
        byte[] ResizeImage(byte[] imageData, float width, float height);
        int GetHeigthImage(byte[] imageData);
        int GetWidthImage(byte[] imageData);
    }
}
