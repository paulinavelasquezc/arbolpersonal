using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace arboldecisiones.Models
{
    public class TreeConfigurationDet
    {
        [Key]
        [Display(Name = "Configuración árbol detalle")]
        public int TreeConfigurationDetID { get; set; }

        [Display(Name = "Configuración árbol")]
        public int TreeConfigurationID { get; set; }

        [Display(Name = "Id Usuario")]
        public string IdUser { get; set; }
        
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdateDate { get; set; }
        
        public virtual TreeConfiguration TreeConfiguration { get; set; }
  
        public virtual ICollection<TreeDecisionDet> TreeDecisionDet { get; set; }

    }
}