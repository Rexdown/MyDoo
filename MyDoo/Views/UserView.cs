using System.ComponentModel.DataAnnotations;

namespace MyDoo.Views;

public class UserView
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}