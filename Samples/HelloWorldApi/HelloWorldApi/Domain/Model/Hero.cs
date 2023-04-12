using System.ComponentModel.DataAnnotations;

namespace HelloWorldApi.Domain.Model;

public class Hero
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required!")]
    public string Name { get; set; }

    public string RealName { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "The age must be greater than 1!")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Power is required!")]
    public string Power { get; set; } = null!;

    [Required(ErrorMessage = "Retired status is required!")]
    public bool IsRetired { get; set; }

}
