using System.ComponentModel.DataAnnotations;

namespace MyDoo.Views;

public class UserRegisterView
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string TGName { get; set; }
}