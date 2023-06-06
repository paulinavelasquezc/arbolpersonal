using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace arboldecisiones.Models
{
    public class Defect
    {
        [Key]
        [Display(Name = "Defectos")]
        public int DefectID { get; set; }

        [Display(Name = "Nombre Defectos")]
        [Required(ErrorMessage = "Debes ingresar {0}")]
        [StringLength(60, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Activo:")]
        public bool Active { get; set; }

        [Display(Name = "Usuario")]
        public string UserID { get; set; }

        [Display(Name = "Actualización")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UpdateDate { get; set; }


        [Display(Name = "Ubicación")]
        public int LocationID { get; set; }
  
        public int MultimediaID { get; set; }


        public virtual Multimedia Multimedia { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<TreeConfiguration> TreeConfigurations { get; set; }
    }
}