using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arboldecisiones.ViewModels
{
    public class TreeDecisionsOrganization
    {
        public string IDProncipal { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
        public string ReportsTo { get; set; }      
    }
}