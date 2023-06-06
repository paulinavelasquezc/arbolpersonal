using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace arboldecisiones.Models
{
    public class TreeMultimedia
    {
         [Key]        
        public int TreeMultimediaID { get; set; }

        [Display(Name = "Descripción Archivo")]
        //[Required(ErrorMessage = "Debes ingresar {0}")]        
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        
        public string Url { get; set; }
        public string Type { get; set; }

        public virtual ICollection<TreeConfigurationXMultimedia> TreeConfigurationXMultimedias { get; set; }
        public virtual ICollection<TreeDecisionXMultimedia> TreeDecisionXMultimedias { get; set; }
    }
}