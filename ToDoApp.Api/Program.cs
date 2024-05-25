using MediatR;
using ToDoApp.Application.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registers handlers and mediator types from the specified assemblies.
builder.Services.AddMediatR(cfg =>
{
    // register handlers from application project
    cfg.RegisterServicesFromAssemblies(typeof(HelloWorldQuery).Assembly);
});

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", async (IMediator mediator) =>
{
    var result = await mediator.Send(new HelloWorldQuery());
    return Results.Ok(result);
});

app.Run();
