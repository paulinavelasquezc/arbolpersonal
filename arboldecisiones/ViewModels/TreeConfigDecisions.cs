using arboldecisiones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arboldecisiones.ViewModels
{
    public class TreeConfigDecisions
    {
        public TreeConfi TreeConfi { get; set; }
        public List<TreeDecis> TreeDecis { get; set; }
    }

    public class TreeConfi
    {
        public int TreeConfigurationID { get; set; }
        public string Name { get; set; }
        public string Defecto { get; set; }

    }

    public class TreeDecis
    {
        public int TreeDecisionID { get; set; }
        public int FatherID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int JoinFatherID { get; set; }
        public string ColourHex { get; set; }
    }
}