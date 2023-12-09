using System.Xml.Serialization;
using Newtonsoft.Json;

namespace toDoApp.Command.Models.ProjectModels;

public class DeveloperModel
{
    [JsonProperty("name")]
    [XmlElement("dev_name")]
    public required string Name { get; set; }

    [JsonProperty("hourly_capacity")]
    [XmlElement("capacity_per_hour")]
    public int HourlyCapacity { get; set; }
}