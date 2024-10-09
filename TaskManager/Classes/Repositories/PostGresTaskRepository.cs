namespace taskMeneger;

public class PostGresTaskRepository(TaskMenegerDbContext dbContext) : ITaskRespository
{
    public List<TaskInfo> GetAll()
    {
        return dbContext.Tasks.ToList();
    }

    public async Task CreateTask(TaskInfo task)
    {
        dbContext.Tasks.Add(task);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(TaskInfo task)
    {
        dbContext.Tasks.Remove(task);
        await dbContext.SaveChangesAsync();
    }

    public async Task<TaskInfo?> GetById(int id)
    {
        return await dbContext.Tasks.FindAsync(id);
    }

    public async Task UpdateTaskAsync(TaskInfo task)
    {
        var excistingTask = await dbContext.Tasks.FindAsync(task.Id);
        if (excistingTask != null)
        {
            excistingTask.Title = task.Title;
            excistingTask.Description = task.Description;
            excistingTask.Deadline = task.Deadline;
            excistingTask.Priority = task.Priority;
            
            dbContext.Tasks.Update(excistingTask);
            await dbContext.SaveChangesAsync();
        }
        else
        {
            throw new NullReferenceException("Task not found");
        }
    }
}