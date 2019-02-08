using Xamarin.Forms;

namespace MDispatch.Models
{
    public class Damage
    {
        public int ID { get; set; }
        public string IndexImageVech { get; set; }
        public string TypePrefDamage { get; set; }
        public string TypeDamage { get; set; }
        public string TypeCurrentStatus { get; set; }
        public int IndexDamage { get; set; }
        public string FullNameDamage { get; set; }
        public double XInterest { get; set; }
        public double YInterest { get; set; }
        public Image Image { get; set; }
        public ImageSource ImageSource { get; set; }
    }
}
