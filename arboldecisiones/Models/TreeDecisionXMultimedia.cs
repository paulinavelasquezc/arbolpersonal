using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace arboldecisiones.Models
{
    public class TreeDecisionXMultimedia
    {
        [Key]
        public int TreeDecisionXMultimediaID { get; set; }
        public int TreeDecisionID { get; set; }
        public int TreeMultimediaID { get; set; }

        public virtual TreeDecision TreeDecision { get; set; }
        public virtual TreeMultimedia TreeMultimedia { get; set; }
    }
}