using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace arboldecisiones.Models
{
    public class TreeConfiguration
    {
        [Key]
        [Display(Name = "Configuración árbol")]
        public int TreeConfigurationID { get; set; }

        [Display(Name = "Definición:")]
        [Required(ErrorMessage = "Debes ingresar {0}")]
        [StringLength(900, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres", MinimumLength = 1)]
        [DataType(DataType.MultilineText)]
        public string Definition { get; set; }

        [Display(Name = "Activo:")]
        public bool Active { get; set; }

        public string UserID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdateDate { get; set; }

        [Display(Name = "Categoría:")]
        public int CategoryID { get; set; }
        [Display(Name = "Defecto:")]
        public int DefectID { get; set; }        

        public virtual Category Category { get; set; }
        public virtual Defect Defect { get; set; }


        public virtual ICollection<TreeConfigurationXMultimedia> TreeConfigurationXMultimedias { get; set; }
        public virtual ICollection<TreeConfigurationDet> TreeConfigurationDet { get; set; }

    }
}