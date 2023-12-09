using System.Xml.Serialization;
using Newtonsoft.Json;

namespace toDoApp.Command.Models.ProjectModels;

public class TaskModel
{
    [JsonProperty("id")]
    [XmlElement("task_id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    [XmlElement("task_name")]
    public required string Name { get; set; }

    [JsonProperty("duration")]
    [XmlElement("task_duration")]
    public int Duration { get; set; }

    [JsonProperty("difficulty")]
    [XmlElement("task_difficulty")]
    public int Difficulty { get; set; }

    [JsonProperty("assigned_developer")]
    [XmlElement("task_assigned_to")]
    public required string AssignedDeveloper { get; set; }
}