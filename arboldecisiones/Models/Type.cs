using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace arboldecisiones.Models
{
    public class Type
    {
        [Key]
        public int TypeID { get; set; }

        [Display(Name = "Tipo")]
        [StringLength(25, ErrorMessage = "", MinimumLength = 3)]
        public string Name { get; set; }
    }
}