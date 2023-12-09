using System.Reflection;
using Microsoft.OpenApi.Models;
using toDoApp.Api.Middlewares;
using toDoApp.Api.MvcFilters;
using toDoApp.Api.Services.Project;
using toDoApp.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDatabase, Database>();

builder.Services.AddScoped<IProjectService, ProjectService>();


builder.Services.AddScoped<ResponseLoggingActionAttribute>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Project API",
        Description = builder.Configuration.GetSection("swaggerDesc").Value
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<AuthorizationMiddleware>();
app.UseMiddleware<CorrelationIdMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();