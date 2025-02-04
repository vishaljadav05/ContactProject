using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactProject.Models;

public class t_User
{
    [Key]
    public int c_UserId { get; set; }

    [Required]
    [StringLength(100)]
    public string c_UserName { get; set; }

    [Required]
    [StringLength(100)]
    public string c_Email { get; set; }

    [Required]
    [StringLength(100)]
    public string c_Password { get; set; }


    [StringLength(500)]
    public string? c_Address { get; set; }


    [StringLength(50)]
    public string? c_Mobile { get; set; }

    [StringLength(10)]
    public string? c_Gender { get; set; }

    [StringLength(4000)]
    public string? c_Image { get; set; }

}