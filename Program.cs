using csharpteam2.Models;
using csharpteam2.Models.DTOs;
var builder = WebApplication.CreateBuilder(args);

List<ToDoItem> toDoItems = new List<ToDoItem>
{
    new ToDoItem { Id = 1, UserId = 1, Task = "Get milk", Completed = false, DueDate = new DateTime (2024, 11, 15) },
    new ToDoItem { Id = 2, UserId = 2, Task = "Feed cat", Completed = true, DueDate = new DateTime (2024, 12, 10) },
    new ToDoItem { Id = 3, UserId = 3, Task = "Walk dog", Completed = false, DueDate = new DateTime (2024, 12, 12) },
    new ToDoItem { Id = 4, UserId = 1, Task = "Pay taxes", Completed = false, DueDate = new DateTime (2024, 11, 22) }
};

List<User> users = new List<User>
{
    new User { Id = 1, Name = "Robert" },
    new User { Id = 2, Name = "Ben" },
    new User { Id = 3, Name = "Mike" }
};


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/hello", () => 
{
    Console.WriteLine("hello");
});

app.MapGet("/todoitems", () =>
{
    return toDoItems.Select(t => new ToDoItemDTO
    {
        Id = t.Id,
        UserId = t.UserId,
        Task = t.Task,
        Completed = t.Completed,
        DueDate = t.DueDate
    });
});

app.MapPost("/todoitems", (ToDoItem toDoItem) => 
{
    toDoItem.Id = toDoItems.Max(t => t.Id) + 1;
    toDoItems.Add(toDoItem);

    return Results.Created($"/todoitems/{toDoItem.Id}", new ToDoItemDTO
    {
        Id = toDoItem.Id,
        UserId = toDoItem.UserId,
        Task = toDoItem.Task,
        Completed = toDoItem.Completed,
        DueDate = toDoItem.DueDate
    });
});

app.MapDelete("todoitems/{id}", (int id) => 
{
    ToDoItem toDoItem = toDoItems.FirstOrDefault(t => t.Id == id);

    if (toDoItem == null) 
    {
        return Results.NotFound();
    }
    toDoItems.Remove(toDoItem);

    return Results.NoContent();
});

app.MapPut("todoitems/{id}/complete", (int id) =>
{
    ToDoItem toDoItem = toDoItems.FirstOrDefault(t => t.Id == id);

    if (toDoItem == null)
    {
        return Results.NotFound();
    }

    toDoItem.Completed = true;

    return Results.Ok(new ToDoItemDTO
    {
        Id = toDoItem.Id,
        UserId = toDoItem.UserId,
        Task = toDoItem.Task,
        Completed = toDoItem.Completed,
        DueDate = toDoItem.DueDate 
    });
});

app.MapGet("todoitems/{id}", (int id) => 
{
    ToDoItem toDoItem = toDoItems.FirstOrDefault(t => t.Id == id);

    
    if (toDoItem == null)
    {
        return Results.NotFound("Please enter a valid Id.");
    }

    User user = users.FirstOrDefault(u => u.Id == toDoItem.UserId);
    return Results.Ok(new ToDoItemDTO
    {
        Id = toDoItem.Id,
        UserId = toDoItem.UserId,
        User = user == null ? null : new UserDTO
        {
            Id = user.Id,
            Name = user.Name
        },
        Task = toDoItem.Task,
        Completed = toDoItem.Completed,
        DueDate = toDoItem.DueDate 
    });

});

app.Run();
