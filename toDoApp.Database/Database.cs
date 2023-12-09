using toDoApp.Core.Entities;

namespace toDoApp.Database;

public class Database : IDatabase
{
    private static readonly List<DeveloperEntity> DeveloperEntities = new();
    private static readonly List<TaskEntity> TaskEntities = new();

    public int AddTask(TaskEntity entity)
    {
        entity.Id = GetRandomNumber();
        TaskEntities.Add(entity);
        return entity.Id;
    }

    public List<TaskEntity> GetTasks()
    {
        return TaskEntities;
    }

    public int AddDeveloper(DeveloperEntity entity)
    {
        entity.Id = GetRandomNumber();
        DeveloperEntities.Add(entity);
        return entity.Id!;
    }

    public List<DeveloperEntity> GetDevelopers()
    {
        return DeveloperEntities;
    }

    private static readonly Random GetRandom = new Random();

    private static int GetRandomNumber()
    {
        lock (GetRandom)
        {
            return GetRandom.Next(0, 1234567890);
        }
    }
}