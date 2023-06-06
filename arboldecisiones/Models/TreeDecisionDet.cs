using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace arboldecisiones.Models
{
    public class TreeDecisionDet
    {
        [Key]
        [Display(Name = "Árbol Decisiones Detalle")]
        public int TreeDecisionDetID { get; set; }

        [Display(Name = "Árbol Decisiones")]
        public int TreeDecisionID { get; set; }

        [Display(Name = "Configuración árbol detalle")]
        public int TreeConfigurationDetID { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdateDate { get; set; }

        [Display(Name = "Orden Detalle")]
        public int OrderDet { get; set; }

        [Display(Name = "Solución")]
        public bool Solution { get; set; }

        public virtual TreeConfigurationDet TreeConfigurationDet { get; set; }

        public virtual TreeDecision TreeDecision { get; set; }
   
    }
}