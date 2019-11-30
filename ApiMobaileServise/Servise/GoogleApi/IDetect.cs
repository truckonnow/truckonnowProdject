using ApiMobaileServise.Servise;
using System.Drawing;

namespace ApiMobaileServise.Servise.GoogleApi
{
    public interface IDetect
    {
        void AuchGoole(SqlCommandApiMobile sqlCommandApiMobil);
        bool DetectText(params object[] parames);
    }
}