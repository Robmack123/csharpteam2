namespace csharpteam2.Models.DTOs;
public class ToDoItemDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Task { get; set; }
    public bool Completed { get; set; }
    public DateTime DueDate { get; set; }
    public UserDTO User { get; set; }
}