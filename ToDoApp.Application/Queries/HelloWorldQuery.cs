using MediatR;

namespace ToDoApp.Application.Queries;

public record HelloWorldQuery : IRequest<string>;

internal sealed class HelloWorldQueryHandler : IRequestHandler<HelloWorldQuery, string>
{
    public Task<string> Handle(HelloWorldQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult("Hello World!");
    }
}
