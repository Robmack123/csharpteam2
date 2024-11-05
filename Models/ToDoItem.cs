namespace csharpteam2.Models;
public class ToDoItem 
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Task { get; set; }
    public bool Completed { get; set; }
    public DateTime DueDate { get; set; }
    public User User { get; set; }
}