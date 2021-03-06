#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
  [Key]
  public int UserId { get; set; }

  [Required(ErrorMessage = "Is Required.")]
  [MinLength(2, ErrorMessage = "Must be at least 2 characters.")]
  [Display(Name = "First Name")]
  public string FirstName { get; set; }

  [Required(ErrorMessage = "Is Required.")]
  [MinLength(2, ErrorMessage = "Must be at least 2 characters.")]
  [Display(Name = "Last Name")]
  public string LastName { get; set; }

  [Required(ErrorMessage = "Is Required.")]
  [EmailAddress]
  public string Email { get; set; }

  [Required(ErrorMessage = "Is Required.")]
  [MinLength(8, ErrorMessage = "Must be at least 8 characters long.")]
  [DataType(DataType.Password)]
  public string Password { get; set; }

  [NotMapped]
  [Compare("Password", ErrorMessage = "Password do not match! Please check your input.")]
  [DataType(DataType.Password)]
  public string ConfirmPassword { get; set; }

  public DateTime createdAt { get; set; } = DateTime.Now;
  public DateTime updatedAt { get; set; } = DateTime.Now;
}

