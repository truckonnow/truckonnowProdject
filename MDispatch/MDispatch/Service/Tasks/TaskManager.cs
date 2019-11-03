using System.Threading.Tasks;

namespace MDispatch.Service.Tasks
{
    public class TaskManager
    {
        public static bool isWorkTask = true;

        public static async void CommandToDo(string nameCommand, params object[] tasks)
        {
            ITask task = null;
            await Task.Delay(1000);
            switch(nameCommand)
            {
                case "DashbordVehicle":
                    {
                        task = new TaskDashbordVechle();
                        break;
                    }
                case "SavePhoto":
                    {
                        task = new SavePhoto();
                        break;
                    }
                case "SaveInspactionDriver":
                    {
                        task = new SaveInspactionDriver();
                        break;
                    }
                case "SaveRecount":
                    {
                        task = new SaveRecount();
                        break;
                    }
                case "CheckTask":
                    {
                        task = new CheckTask();
                        break;
                    }
            }
            if (task != null)
            {
                System.Threading.Tasks.Task.Run(() => task.StartTask(tasks));
            }
        }
    }
}
