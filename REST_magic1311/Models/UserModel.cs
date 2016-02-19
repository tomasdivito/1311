using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REST_magic1311.Models
{
    public class UserModel
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        [Display(Name="Email address: ")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(200), MinLength(6)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Name: ")]
        public string Name { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Lastname: ")]
        public string Lastname { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Country: ")]
        public string Country { get; set; }

        //Optional social data information
        [StringLength(50)]
        [Display(Name = "Facebook Account: ")]
        public string Facebook { get; set; }

        [StringLength(50)]
        [Display(Name = "Twitter Account: ")]
        public string Twitter { get; set; }

        [StringLength(50)]
        [Display(Name = "Linkedin: ")]
        public string Linkedin { get; set; }

        public string PasswordSalt { get; set; }

        [Display(Name = "Activation code: ")]
        public string ActivationCode { get; set; }
    }
}