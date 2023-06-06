using arboldecisiones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arboldecisiones.ViewModels
{
    public class ConfigTreeDecisions
    {
        public TreeConfiguration TreeConfiguration { get; set; }
        public TreeDecision TreeDecision { get; set; }
        public TreeMultimediaViewModels TreeMultimediaViewModels { get; set; }        
    }
}