using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace arboldecisiones.Models
{
    public class Multimedia
    {
        [Key]
        public int MultimediaID { get; set; }

        [Display(Name = "Descripción Archivo")]
        //[Required(ErrorMessage = "Debes ingresar {0}")]        
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Url { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Process> Process { get; set; }
        public virtual ICollection<CategoryContainer> CategoryContainers { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Defect> Defects { get; set; }
    }
}