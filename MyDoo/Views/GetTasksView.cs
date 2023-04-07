using System.ComponentModel.DataAnnotations;
using MyDoo.Entities;

namespace MyDoo.Views;

public class GetTasksView
{
    public int Id { get; set; }
    public string Text { get; set; }
    public TaskType Type { get; set; } 
    
    [Required]
    public int UserId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public bool Important { get; set; }
    [Required]
    public bool Complete { get; set; }
}