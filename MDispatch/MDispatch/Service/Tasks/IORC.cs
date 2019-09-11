using System.Threading.Tasks;

namespace MDispatch.Service.Tasks
{
    public interface IORC
    {
         Task<string> ORCWorkDashbordVehicle(byte[] data);
    }
}
