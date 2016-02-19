using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REST_magic1311.Models
{
    public class MailModel
    {
        [Required]
        [StringLength(140)]
        [Display(Name ="Subject")]
        public string Subject { get; set; }
        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }
    }
}