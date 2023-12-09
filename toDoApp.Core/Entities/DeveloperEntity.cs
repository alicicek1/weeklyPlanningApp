namespace toDoApp.Core.Entities;

public struct DeveloperEntity : IEntity
{
    public string Name { get; set; }
    public int HourlyCapacity { get; set; }
    public int Id { get; set; }
}