using System;
using System.Drawing;
using System.IO;
using Xamarin.Forms;

namespace MDispatch.Service.Tasks
{
    public class TaskDashbordVechle : ITask
    {
        public async void StartTask(params object[] task)
        {
            string base64 = (string)task[0];
            //string res = await DependencyService.Get<IORC>().ORCWorkDashbordVehicle(Convert.FromBase64String(base64));

        }
    }
}
