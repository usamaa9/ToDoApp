using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using System.Reflection;
using ToDoApp.Application.Commands;
using ToDoApp.Application.Validators;
using ToDoApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

# region snippet_ConfigureServices

// add config from appsettings.local.json
builder.Configuration.AddJsonFile("appsettings.local.json", optional: true);

builder.Services.AddLogging();
var serviceProvider = builder.Services.BuildServiceProvider();
var logger = serviceProvider.GetService<ILogger<Program>>();

// Add services to the container. Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // List of XML documentation files from different projects
    var xmlFiles = new[]
    {
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml", // API project
        "ToDoApp.Core.xml", // Core project
        "ToDoApp.Application.xml", // Application project
        "ToDoApp.Infrastructure.xml" // Infrastructure project
    };

    // Combine paths to XML documentation files
    foreach (var xmlFile in xmlFiles)
    {
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    }
});

// Registers handlers and mediator types from the specified assemblies.
builder.Services.AddMediatR(cfg =>
{
    // register handlers from application project
    cfg.RegisterServicesFromAssemblies(typeof(GetToDoQuery).Assembly);
});

builder.Services.AddScoped<IValidator<CreateToDoCommand>, CreateToDoCommandValidator>();

// Add all validators from the application project
builder.Services.AddFluentValidationAutoValidation(configuration =>
{
    // Replace the default result factory with a custom implementation.
    configuration.OverrideDefaultResultFactoryWith<ValidationResultFactory>();
});

builder.Services.AddInfrastructure(builder.Configuration, logger!);

# endregion snippet_ConfigureServices

# region snippet_Configure

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Map endpoints
app.MapAllEndpoints();

app.Run();

# endregion snippet_Configure
