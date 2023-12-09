namespace toDoApp.Api.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
public class TokenRequiredAttribute : System.Attribute
{
}