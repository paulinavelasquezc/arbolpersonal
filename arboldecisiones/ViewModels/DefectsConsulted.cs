using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arboldecisiones.ViewModels
{
    public class DefectsConsulted
    {
        public int TreeConfigurationDetID { get; set; }
        public string NameConfiguration { get; set; }
        public string NameLocation { get; set; }        
        public string DefectUpdateDate { get; set; }
        public int Cantidad { get; set; }
    }
}