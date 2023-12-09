using toDoApp.Core.Entities;

namespace toDoApp.Api.Models.ResponseModels;

public class WeeklyTaskPlan : IResponse
{
    public Dictionary<string, List<TaskEntity>> DeveloperTasks { get; set; } =
        new Dictionary<string, List<TaskEntity>>();

    public int TotalDurationHours { get; set; }
    public string? TotalDurationString { get; set; }
}