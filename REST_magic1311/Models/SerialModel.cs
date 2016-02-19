using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REST_magic1311.Models
{
    public class SerialModel
    {
        [Required]
        [StringLength(16)]
        [Display(Name = "SERIAL CODE: ")]
        public string Serial { get; set; }

        [Required]
        [StringLength(3)]
        [Display(Name = "PRODUCT: ")]
        public string AppID { get; set; }
        
        public string Usado { get; set; }
        
        public string Registrado { get; set; }
        
        public string Usuario { get; set; }
    }
}