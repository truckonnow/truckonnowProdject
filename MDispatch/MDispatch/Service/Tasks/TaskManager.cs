namespace MDispatch.Service.Tasks
{
    public class TaskManager
    {
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
            }
            if (task != null)
            {
                System.Threading.Tasks.Task.Run(() => task.StartTask(tasks));
            }
        }
    }
}
