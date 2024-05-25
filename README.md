# ToDoApp


I created the projects using the following commands:

```bash
dotnet new classlib -n ToDoApp.Core
dotnet new classlib -n ToDoApp.Application
dotnet new classlib -n ToDoApp.Infrastructure

dotnet sln ToDoApp.sln add ToDoApp.Core/ToDoApp.Core.csproj
dotnet sln ToDoApp.sln add ToDoApp.Application/ToDoApp.Application.csproj
dotnet sln ToDoApp.sln add ToDoApp.Infrastructure/ToDoApp.Infrastructure.csproj
```