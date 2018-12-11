using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FuegoBox.Presentation.Models
{
    public class AddressModel
    {
        [Required]
        [Display(Name = "AddressLine 1")]
        public string AddressLine1 { get; set; }

        [Required]
        [Display(Name = "AddressLine 2")]
        public string AddressLine2 { get; set; }
        
        [Required]
        public int PIN { get; set; }

    }
}