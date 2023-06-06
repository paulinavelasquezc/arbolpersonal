using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace arboldecisiones.Models
{
    public class TreeDecision
    {
        [Key]
        [Display(Name = "Árbol Decisiones")]
        public int TreeDecisionID { get; set; }
        public int FatherID { get; set; }

        [Display(Name = "Titulo defecto:")]
        [Required(ErrorMessage = "Debes ingresar {0}")]
        [StringLength(50, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres", MinimumLength = 4)]
        public string Name { get; set; }


        [Display(Name = "Descripción:")]
        [Required(ErrorMessage = "Debes ingresar {0}")]
        [StringLength(900, ErrorMessage = "El campo {0} debe estar entre {1} y {2} caracteres", MinimumLength = 4)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
     
        [Display(Name = "Activo:")]        
        public bool Active { get; set; }

        public int NumberSolutions { get; set; }

        public string UserID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdateDate { get; set; }

        public int TreeConfigurationID { get; set; }

        public int JoinFatherID { get; set; }

        public string ColourHex { get; set; }
        public virtual TreeConfiguration TreeConfiguration { get; set; }

        public int TypeID { get; set; }
        public virtual Type Type { get; set; }        

        public virtual ICollection<TreeDecisionXMultimedia> TreeDecisionXMultimedias { get; set; }
        public virtual ICollection<TreeDecisionDet> TreeDecisionDet { get; set; }
    }
}