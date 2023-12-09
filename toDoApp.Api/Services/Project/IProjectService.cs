using toDoApp.Api.Model.CommonResutlts;
using toDoApp.Api.Models.ResponseModels;

namespace toDoApp.Api.Services.Project;

public interface IProjectService
{
    DataResult<WeeklyTaskPlan> GetWeeklyPlan();
}