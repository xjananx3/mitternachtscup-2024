using System.ComponentModel.DataAnnotations;

namespace MitternachtsCup.ViewModels;

public class LoginVm
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}