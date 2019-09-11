namespace MDispatch.Service.Tasks
{
    public interface ITask
    {
        void StartTask(params object[] task);
    }
}
