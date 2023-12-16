using System.Text;
using toDoApp.Api.Model.CommonResutlts;
using toDoApp.Api.Models.ResponseModels;
using toDoApp.Core.Entities;
using toDoApp.Database;

namespace toDoApp.Api.Services.Project;

public class ProjectService : IProjectService
{
    private readonly IDatabase _database;
    private readonly Dictionary<string, string> _developers;

    public ProjectService(IDatabase database)
    {
        _database = database;
        _developers = new()
        {
            {"dev5", "5"},
            {"dev4", "4"},
            {"dev3", "3"},
            {"dev2", "2"},
            {"dev1", "1"}
        };
    }

    public DataResult<WeeklyTaskPlan> GetWeeklyPlan()
    {
        var tasks = _database.GetTasks();
        var weeklyTaskPlan = new WeeklyTaskPlan();
        var remainingHours = 45;

        foreach (var developer in _developers)
        {
            var tasksForDeveloper = tasks
                .Where(task => task.Difficulty == int.Parse(developer.Value))
                .OrderByDescending(task => task.Difficulty) // Sort tasks by difficulty in descending order
                .ThenBy(task => task.Duration) // Then sort by duration in ascending order
                .ToList();

            if (tasksForDeveloper.Any())
            {
                DistributeTasks(weeklyTaskPlan, developer.Key, tasksForDeveloper, ref remainingHours);
            }
        }

        CalculateTotalDurationHours(weeklyTaskPlan);
        CalculateTotalWeeks(weeklyTaskPlan);

        return new SuccessDataResult<WeeklyTaskPlan>(weeklyTaskPlan);
    }

    private void DistributeTasks(WeeklyTaskPlan weeklyTaskPlan, string developer, IEnumerable<TaskEntity> tasks,
        ref int remainingHours)
    {
        weeklyTaskPlan.DeveloperTasks[developer] = new List<TaskEntity>();

        foreach (var task in tasks)
        {
            if (task.Duration <= remainingHours)
            {
                weeklyTaskPlan.DeveloperTasks[developer].Add(task);
                task.AssignedDeveloper = developer;
                remainingHours -= task.Duration;
            }
            else
            {
                continue;
            }
        }
    }

    private void CalculateTotalDurationHours(WeeklyTaskPlan weeklyTaskPlan)
    {
        weeklyTaskPlan.TotalDurationHours = weeklyTaskPlan.DeveloperTasks
            .SelectMany(kv => kv.Value)
            .Sum(task => task.Duration);
    }

    private void CalculateTotalWeeks(WeeklyTaskPlan weeklyTaskPlan)
    {
        int totalHours = weeklyTaskPlan.TotalDurationHours;

        int fullWeeks = totalHours / 45;
        int remainingHours = totalHours % 45;
        int remainingDays = remainingHours / 9;
        int remainingHoursOfDay = remainingHours % 9;

        var remainingTimeString = new StringBuilder();
        if (fullWeeks > 0)
        {
            remainingTimeString.Append($"{fullWeeks} hafta");
        }

        if (remainingDays > 0)
        {
            remainingTimeString.Append($" {remainingDays} gün");
        }

        if (remainingHoursOfDay > 0)
        {
            remainingTimeString.Append($" {remainingHoursOfDay} saat");
        }

        weeklyTaskPlan.TotalDurationString = remainingTimeString.ToString().Trim();
    }
}