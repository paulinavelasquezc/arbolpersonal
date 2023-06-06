using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace arboldecisiones.Models
{
    public class TreeConfigurationXMultimedia
    {
        [Key]
        public int TreeConfigurationXMultimediaID { get; set; }
        public int TreeConfigurationID { get; set; }
        public int TreeMultimediaID { get; set; }

        public virtual TreeConfiguration TreeConfiguration { get; set; }
        public virtual TreeMultimedia TreeMultimedia { get; set; }
    }
}