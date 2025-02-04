using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactProject.Models
{
    public class vm_Login
    {
        [StringLength(100)]
        [Required(ErrorMessage ="Email is Required!")]
        [EmailAddress(ErrorMessage ="Invalid Email Format!")]
        public string c_Email{get;set;}

        [StringLength(100)]
        [Required(ErrorMessage ="Password is required!")]
        public string c_Password{get;set;}
    }
}