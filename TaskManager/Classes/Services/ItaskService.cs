using CLassLIbrary.DTO;

namespace taskMeneger;

public interface ITaskService
{
    Task AddTask(CreateTaskDTO addTaskDTO);
    Task EditTask(EditTaskDTO editTaskDTO);
    Task RemoveTask(int id);  // Usuwanie po ID, bez potrzeby przekazywania TaskInfo
    List<TaskInfo> GetAllTasks();
}