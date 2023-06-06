using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace arboldecisiones.Models
{
    public class Process
    {
        [Key]
        [Display(Name = "Proceso")]
        public int ProcessID { get; set; }

        [Display(Name = "Nombre Proceso")]
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

        public int MultimediaID { get; set; }


        public virtual Multimedia Multimedia { get; set; }
        public virtual ICollection<CategoryContainer> CategoryContainers { get; set; }
    }
}