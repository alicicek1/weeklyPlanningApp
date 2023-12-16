using Microsoft.AspNetCore.Mvc;
using toDoApp.Api.Attributes;
using toDoApp.Api.Services.Project;

namespace toDoApp.Api.Controllers;

public class ProjectsController : BaseController
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    //[TokenRequired]
    [Produces("application/json")]
    public IActionResult GetWeeklyPlan()
    {
        return ApiResponse(_projectService.GetWeeklyPlan());
    }
}