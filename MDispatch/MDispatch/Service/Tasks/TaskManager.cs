namespace MDispatch.Service.Tasks
{
    public class TaskManager
    {
        public static bool isWorkTask = true;

        public static void CommandToDo(string nameCommand, params object[] tasks)
        {
            ITask task = null;
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
