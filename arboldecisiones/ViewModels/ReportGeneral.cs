using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arboldecisiones.ViewModels
{
    public class ReportGeneral
    {
        public int TreeDecisionDetID { get; set; }
        public string NameConfiguration { get; set; }
        public string NameLocation { get; set; }
        public string NameUser { get; set; }
        public string DefectUpdateDate { get; set; }
        //Decision
        public string NameDecision { get; set; }
        public string DecisionUpdateDate { get; set; }
        public bool Solution { get; set; }
        public string NameMachine { get; set; }
    }
}