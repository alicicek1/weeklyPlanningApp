namespace toDoaPP.Command;

public class Config
{
    public PlanningApiClientSetting? PlanningApiClientSetting { get; set; }
}

public class PlanningApiClientSetting
{
    public required string[] RequestingEndpoints { get; set; }
}