using arboldecisiones.Models;
using arboldecisiones.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace arboldecisiones.Controllers
{
    [Authorize]
    public class TreeOrganizationController : Controller
    {
        private arboldecisionesContext db = new arboldecisionesContext();

        public ActionResult ListDefectsTree()
        {

            var treesDefects = new List<TreesDefects>();

            var treeConfiguration = db.TreeConfigurations.ToList();

            foreach (var item in treeConfiguration)
            {
                var treeImagen = (from image in db.TreeMultimedia
                                  join configXimage in db.TreeConfigurationXMultimedias on image.TreeMultimediaID equals configXimage.TreeMultimediaID
                                  where configXimage.TreeConfigurationID == item.TreeConfigurationID
                                  select image).ToList();

                var treeConfig = new TreesDefects();
                treeConfig.TreeConfiguration = item;
                treeConfig.TreeMultimedia = treeImagen;
                treesDefects.Add(treeConfig);

            }

            return View(treesDefects.ToList());
        }


        public ActionResult TreeDecisions(int? TreeConfigurationID)
        {

            return View();
        }

        public JsonResult getChartData()
        {
                   
            var TreeDecisionsOrganizationList = new List<TreeDecisionsOrganization>();

            var treeConfig = db.TreeConfigurations.FirstOrDefault(c => c.TreeConfigurationID == 70);

            var principal = new TreeDecisionsOrganization(){
                IDProncipal = treeConfig.TreeConfigurationID.ToString(),
                Title = "name",
                Description = treeConfig.Definition,
                ReportsTo = "0"
            };
            TreeDecisionsOrganizationList.Add(principal);

            var treeDeciXconfig = db.TreeDecisions.Where(c => c.TreeConfigurationID == treeConfig.TreeConfigurationID).ToList();

            foreach (var desi in treeDeciXconfig)
            {
                var segundarios = new TreeDecisionsOrganization();
                if (desi.FatherID == 0)
                {
                    segundarios.IDProncipal = desi.TreeDecisionID.ToString();
                    segundarios.Title = desi.Name;
                    segundarios.Description = desi.Description;
                    segundarios.ReportsTo = treeConfig.TreeConfigurationID.ToString();
                    
                }
                else
                {

                    segundarios.IDProncipal = desi.TreeDecisionID.ToString();
                    segundarios.Title = desi.Name;
                    segundarios.Description = desi.Description;
                    segundarios.ReportsTo = desi.FatherID.ToString();
                   
                }
            TreeDecisionsOrganizationList.Add(segundarios);
        }

            return new JsonResult { Data = TreeDecisionsOrganizationList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}