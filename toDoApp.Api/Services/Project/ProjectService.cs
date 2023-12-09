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
        var tasks = new List<TaskEntity>()
        {
            new()
            {
                Id = 1,
                Name = "Task1",
                Duration = 10,
                Difficulty = 1,
                AssignedDeveloper = "Sara"
            },
            new()
            {
                Id = 2,
                Name = "Task2",
                Duration = 5,
                Difficulty = 2,
                AssignedDeveloper = "Miguel"
            },
            new()
            {
                Id = 3,
                Name = "Task5",
                Duration = 3,
                Difficulty = 3,
                AssignedDeveloper = "Elena"
            },
            new()
            {
                Id = 1,
                Name = "Task1",
                Duration = 6,
                Difficulty = 1,
                AssignedDeveloper = "1"
            },
            new()
            {
                Id = 2,
                Name = "Task2",
                Duration = 5,
                Difficulty = 3,
                AssignedDeveloper = "1"
            },
            new()
            {
                Id = 3,
                Name = "Task3",
                Duration = 2,
                Difficulty = 2,
                AssignedDeveloper = "3"
            },
            new()
            {
                Id = 4,
                Name = "Task4",
                Duration = 2,
                Difficulty = 5,
                AssignedDeveloper = "5"
            },
            new()
            {
                Id = 1,
                Name = "Task1",
                Duration = 10,
                Difficulty = 1,
                AssignedDeveloper = "Olivia"
            },
            new()
            {
                Id = 2,
                Name = "Task2",
                Duration = 5,
                Difficulty = 2,
                AssignedDeveloper = "Noah"
            },
            new()
            {
                Id = 3,
                Name = "Task5",
                Duration = 3,
                Difficulty = 3,
                AssignedDeveloper = "Carlos"
            }
        };

        //var tasks = _database.GetTasks();
        var weeklyTaskPlan = new WeeklyTaskPlan();

        foreach (var developer in _developers)
        {
            var tasksForDeveloper = tasks
                .Where(task => task.Difficulty == int.Parse(developer.Value))
                .ToList();

            DistributeTasks(weeklyTaskPlan, developer.Key, tasksForDeveloper);
        }

        CalculateTotalDurationHours(weeklyTaskPlan);
        CalculateTotalWeeks(weeklyTaskPlan);

        return new SuccessDataResult<WeeklyTaskPlan>(weeklyTaskPlan);
    }

    private void DistributeTasks(WeeklyTaskPlan weeklyTaskPlan, string developer, IEnumerable<TaskEntity> tasks)
    {
        weeklyTaskPlan.DeveloperTasks[developer] = new List<TaskEntity>();
        var remainingHours = 45;

        foreach (var task in tasks.OrderBy(t => t.Duration))
        {
            var requiredHours = task.Duration / int.Parse(_developers[developer]);

            if (requiredHours <= remainingHours)
            {
                weeklyTaskPlan.DeveloperTasks[developer].Add(task);
                task.AssignedDeveloper = developer;
                remainingHours -= requiredHours;
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