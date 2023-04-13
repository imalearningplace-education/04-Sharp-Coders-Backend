using System.ComponentModel.DataAnnotations;

namespace Domain.Model;

public class User : Entity {

    [Required]
    public string Username { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public bool IsActive { get; set; } = true;

}
