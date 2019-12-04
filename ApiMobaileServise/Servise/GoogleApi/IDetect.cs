using ApiMobaileServise.Servise;
using System.Drawing;
using System.Threading.Tasks;

namespace ApiMobaileServise.Servise.GoogleApi
{
    public interface IDetect
    {
        void AuchGoole(SqlCommandApiMobile sqlCommandApiMobil);
        void DetectText(params object[] parames);
    }
}