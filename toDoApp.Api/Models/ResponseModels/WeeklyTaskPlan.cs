using toDoApp.Core.Entities;

namespace toDoApp.Api.Models.ResponseModels;

public class WeeklyTaskPlan : IResponse
{
    public Dictionary<string, List<TaskEntity>> DeveloperTasks { get; } = new();
    public int TotalDurationHours { get; set; }
    public string? TotalDurationString { get; set; }
}