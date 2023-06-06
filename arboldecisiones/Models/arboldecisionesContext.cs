using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace arboldecisiones.Models
{
    public class arboldecisionesContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public arboldecisionesContext() : base("name=arboldecisionesContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<arboldecisiones.Models.TreeConfiguration> TreeConfigurations { get; set; }

        public System.Data.Entity.DbSet<arboldecisiones.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<arboldecisiones.Models.Defect> Defects { get; set; }

        public System.Data.Entity.DbSet<arboldecisiones.Models.Location> Locations { get; set; }

        public System.Data.Entity.DbSet<arboldecisiones.Models.TreeDecision> TreeDecisions { get; set; }

        public System.Data.Entity.DbSet<arboldecisiones.Models.TreeMultimedia> TreeMultimedia { get; set; }
        public System.Data.Entity.DbSet<arboldecisiones.Models.TreeConfigurationXMultimedia> TreeConfigurationXMultimedias { get; set; }
        public System.Data.Entity.DbSet<arboldecisiones.Models.TreeDecisionXMultimedia> TreeDecisionXMultimedia { get; set; }
        public System.Data.Entity.DbSet<arboldecisiones.Models.Type> Types { get; set; }
        public System.Data.Entity.DbSet<arboldecisiones.Models.TreeConfigurationDet> TreeConfigurationDet { get; set; }
        public System.Data.Entity.DbSet<arboldecisiones.Models.TreeDecisionDet> TreeDecisionDet { get; set; }

        public System.Data.Entity.DbSet<arboldecisiones.Models.Machine> Machines { get; set; }
        public System.Data.Entity.DbSet<arboldecisiones.Models.Process> Process { get; set; }
        public System.Data.Entity.DbSet<arboldecisiones.Models.CategoryContainer> CategoryContainers { get; set; }

        public System.Data.Entity.DbSet<arboldecisiones.Models.Multimedia> Multimedias { get; set; }
    }
}
