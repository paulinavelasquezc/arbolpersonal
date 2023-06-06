using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace arboldecisiones.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "Categoría")]
        public int CategoryID { get; set; }

        [Display(Name = "Nombre Categoría")]
        [Required(ErrorMessage = "Debes ingresar {0}")]
        [StringLength(25, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

        [Display(Name = "Usuario")]
        public string UserID { get; set; }

        [Display(Name = "Actualización")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<TreeConfiguration> TreeConfigurations { get; set; }
        

    }
}