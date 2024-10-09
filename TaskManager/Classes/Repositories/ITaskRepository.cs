namespace taskMeneger;

public interface ITaskRespository
{
    List<TaskInfo> GetAll();
    Task CreateTask(TaskInfo task);

    Task<TaskInfo?>GetById(int id);
    Task DeleteTaskAsync(TaskInfo task);
    Task UpdateTaskAsync(TaskInfo task);
}