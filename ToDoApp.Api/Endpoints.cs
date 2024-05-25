namespace ToDoApp.Api;

public static class Endpoints
{
    public static WebApplication MapGetHelloWorld(this WebApplication app)
    {
        app.MapGet("/", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new HelloWorldQuery());
            return Results.Ok(result);
        })
        .WithName("Hello World")
        .Produces<string>(200, "application/plain");
        return app;
    }
}
