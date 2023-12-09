using System.Collections;
using toDoApp.Core.Entities;

namespace toDoApp.Database;

public interface IDatabase
{
    int AddTask(TaskEntity entity);
    List<TaskEntity> GetTasks();

    int AddDeveloper(DeveloperEntity entity);
    List<DeveloperEntity> GetDevelopers();
}