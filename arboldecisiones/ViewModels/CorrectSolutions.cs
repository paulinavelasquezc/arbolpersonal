using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arboldecisiones.ViewModels
{
    public class CorrectSolutions
    {
        public int DecisionID { get; set; }
        public string NameDefect { get; set; }
        public string NameDecision { get; set; }
        public string DescriptionDecision { get; set; }
        public int Cantidad { get; set; }
    }
}