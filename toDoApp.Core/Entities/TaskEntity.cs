namespace toDoApp.Core.Entities;

public class TaskEntity : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Duration { get; set; }
    public int Difficulty { get; set; }
    public string? AssignedDeveloper { get; set; }
}