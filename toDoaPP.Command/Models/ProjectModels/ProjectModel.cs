using System.Xml.Serialization;
using Newtonsoft.Json;

namespace toDoApp.Command.Models.ProjectModels;

[XmlRoot("project")]
public class ProjectModel
{
    [JsonProperty("developers")]
    [XmlArray("developers")]
    [XmlArrayItem("developer")]
    public List<DeveloperModel> Developers { get; set; } = new();

    [JsonProperty("tasks")]
    [XmlArray("tasks")]
    [XmlArrayItem("task")]
    public List<TaskModel> Tasks { get; set; } = new();
}