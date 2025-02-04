using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactProject.Models;

public class t_User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int c_UserId { get; set; }

    [Required(ErrorMessage = "Username is Required!")]
    [StringLength(100)]
    public string c_UserName { get; set; }

    [Required(ErrorMessage = "Email is Required!")]
    [EmailAddress(ErrorMessage = "Invalid Email Format!")]
    [StringLength(100)]
    public string c_Email { get; set; }

    [Required(ErrorMessage = "Password is Required!")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 char long!")]
    [StringLength(100)]
    public string c_Password { get; set; }

    [StringLength(100)]
    [Required(ErrorMessage = "Confirm password is Required!")]
    [Compare("c_Password", ErrorMessage = "Password does not match!")]
    public string c_ConfirmPassword { get; set; }


    [StringLength(500)]
    public string? c_Address { get; set; }


    [StringLength(50)]
    public string? c_Mobile { get; set; }

    [StringLength(10)]
    public string? c_Gender { get; set; }

    [StringLength(4000)]
    public string? c_Image { get; set; }

    public IFormFile?ProfilePicture{get;set;}

}