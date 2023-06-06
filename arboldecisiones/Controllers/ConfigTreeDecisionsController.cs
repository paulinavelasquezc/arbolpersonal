using arboldecisiones.Classes;
using arboldecisiones.Models;
using arboldecisiones.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;

namespace arboldecisiones.Controllers
{
    [Authorize]
    public class ConfigTreeDecisionsController : Controller
    {
        private arboldecisionesContext db;

        public ConfigTreeDecisionsController()
        {
            db = new arboldecisionesContext();
        }

        public ActionResult NewTree()
        {
            var configTree = new ConfigTreeDecisions();
            configTree.TreeConfiguration = new TreeConfiguration();
            configTree.TreeDecision = new TreeDecision();
            configTree.TreeMultimediaViewModels = new TreeMultimediaViewModels();


            var category = db.Categories.ToList();
            category.Add(new Category { CategoryID = 0, Name = "[Seleccione un tipo de categoría.]" });
            category = category.OrderBy(c => c.Name).ToList();
            ViewBag.CategoryID = new SelectList(category, "CategoryID", "Name");


            var defects = db.Defects.ToList();
            defects.Add(new Defect { DefectID = 0, Name = "[Seleccione un tipo de defecto.]" });
            defects = defects.OrderBy(c => c.Name).ToList();
            ViewBag.DefectID = new SelectList(defects, "DefectID", "Name");


            var location = db.Locations.ToList();
            location.Add(new Location { LocationID = 0, Name = "[Seleccione un tipo de ubicación.]" });
            location = location.OrderBy(c => c.Name).ToList();
            ViewBag.LocationID = new SelectList(location, "LocationID", "Name");
            return View(configTree);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewTree(ConfigTreeDecisions view)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //ruta de la imagen pic
                    var pic = string.Empty;
                    var folde = "~/Content/Images/TreeDecisions";

                    if (view.TreeMultimediaViewModels.MultimediaFile != null)
                    {
                        pic = FilesHelper.UploadMultimedia(view.TreeMultimediaViewModels.MultimediaFile, folde);
                        pic = string.Format("{0}/{1}", folde, pic);
                    }
                    var TreeMultimedia = ToTreeMultimedia(view.TreeMultimediaViewModels);
                    TreeMultimedia.Url = pic;
                    //int LatestTreeImageID = TreeImage.TreeImageID;


                    var CategoryID = int.Parse(Request["CategoryID"]);
                    var LocationID = int.Parse(Request["LocationID"]);
                    var DefectID = int.Parse(Request["DefectID"]);
                    var obTreeConfiguration = new TreeConfiguration()
                    {
                        Definition = view.TreeConfiguration.Definition,
                        UserID = view.TreeConfiguration.UserID,
                        UpdateDate = DateTime.Now,
                        CategoryID = CategoryID,
                        //LocationID = LocationID,
                        DefectID = DefectID,
                    };

                    db.TreeConfigurations.Add(obTreeConfiguration);
                    db.SaveChanges();
                    db.TreeMultimedia.Add(TreeMultimedia);
                    db.SaveChanges();

                    var TreeConfigurationXMultimedia = new TreeConfigurationXMultimedia
                    {
                        TreeConfiguration = view.TreeConfiguration,
                        TreeMultimedia = TreeMultimedia
                    };

                    db.TreeConfigurationXMultimedias.Add(TreeConfigurationXMultimedia);

                    db.SaveChanges();

                }
                catch (Exception e)
                {

                    throw e;
                }

            }

            return View(view);
        }

        private TreeMultimedia ToTreeMultimedia(TreeMultimediaViewModels view)
        {
            return new TreeMultimedia
            {
                Description = view.Description,
                Url = view.Url
            };
        }

        [HttpPost]
        public void GuardarImagen(HttpPostedFileBase[] MultimediaFile, string[] Description)
        {
            List<TreeMultimediaViewModels> viewMultimedia = new List<TreeMultimediaViewModels>();

            for (int i = 0; i < Description.Count(); i++)
            {
                TreeMultimediaViewModels multimedia = new TreeMultimediaViewModels();
                multimedia.MultimediaFile = MultimediaFile[i];
                multimedia.Description = Description[i];

                viewMultimedia.Add(multimedia);
            }

            Session["TreeImageViewModels"] = viewMultimedia;
        }


        [HttpPost]
        public JsonResult SaveTreeConfiguration(HttpPostedFileBase[] MultimediaFile, string[] typeFile, string[] Description, string Definition, int CategoryID, int LocationID, int DefectID)
        {
            bool Status = false;
            int TreeConfigurationID = 0;
            try
            {
                var TreeConfiguration = new TreeConfiguration
                {
                    Definition = Definition,
                    Active = true,
                    UserID = "0",
                    UpdateDate = DateTime.Now,
                    CategoryID = CategoryID,
                    //LocationID = LocationID,
                    DefectID = DefectID
                };
                db.TreeConfigurations.Add(TreeConfiguration);
                db.SaveChanges();

                TreeConfigurationID = TreeConfiguration.TreeConfigurationID;

                for (int i = 0; i < Description.Count(); i++)
                {

                    //ruta de la imagen pic
                    var pic = string.Empty;
                    var folde = "~/Content/Images/TreeDecisions";

                    if (MultimediaFile[i] != null)
                    {
                        pic = FilesHelper.UploadMultimedia(MultimediaFile[i], folde);
                        pic = string.Format("{0}/{1}", folde, pic);
                    }

                    var Multimedia = new TreeMultimedia();
                    Multimedia.Description = Description[i];
                    Multimedia.Url = pic;
                    Multimedia.Type = typeFile[i];
                    db.TreeMultimedia.Add(Multimedia);

                    var TreeConfigurationXMultimedia = new TreeConfigurationXMultimedia
                    {
                        TreeConfiguration = TreeConfiguration,
                        TreeMultimedia = Multimedia
                    };
                    db.SaveChanges();
                    db.TreeConfigurationXMultimedias.Add(TreeConfigurationXMultimedia);
                    db.SaveChanges();
                }
                Status = true;
            }
            catch (Exception e)
            {
                TreeConfigurationID = 0;
                Status = false;
                throw e;
            }
            return new JsonResult { Data = new { status = Status, treeConfigurationID = TreeConfigurationID } };
        }



        [HttpPost]
        public JsonResult SaveTreeDecision(int treeConfigID, int treeID, string txtTitle, string textDescription, bool ckbACtivo, string type, HttpPostedFileBase[] MultimediaFile, string[] typeFile, string[] Description)
        {
            bool Status = false;
            int TreeDecisionID = 0;
            try
            {
                var typeCS = 0;

                if (type == "Cause")
                {
                    typeCS = db.Types.FirstOrDefault(c => c.Name == "CAUSA").TypeID;
                }

                if (type == "Solution")
                {
                    typeCS = db.Types.FirstOrDefault(c => c.Name == "SOLUCIÓN").TypeID;
                }

                var TreeDecisionObj = new TreeDecision
                {
                    Name = txtTitle,
                    Description = textDescription,
                    Active = ckbACtivo,
                    UserID = "0",
                    UpdateDate = DateTime.Now,
                    TreeConfigurationID = treeConfigID,
                    FatherID = treeID,
                    NumberSolutions = 0,
                    TypeID = typeCS
                };

                db.TreeDecisions.Add(TreeDecisionObj);
                db.SaveChanges();

                TreeDecisionID = TreeDecisionObj.TreeDecisionID;

                for (int i = 0; i < Description.Count(); i++)
                {

                    //ruta de la imagen pic
                    var pic = string.Empty;
                    var folde = "~/Content/Images/TreeDecisions";

                    if (MultimediaFile[i] != null)
                    {
                        pic = FilesHelper.UploadMultimedia(MultimediaFile[i], folde);
                        pic = string.Format("{0}/{1}", folde, pic);
                    }

                    var Multimedia = new TreeMultimedia();
                    Multimedia.Description = Description[i];
                    Multimedia.Url = pic;
                    Multimedia.Type = typeFile[i];
                    db.TreeMultimedia.Add(Multimedia);

                    var TreeDecisionXMultimediaObj = new TreeDecisionXMultimedia
                    {
                        TreeDecision = TreeDecisionObj,
                        TreeMultimedia = Multimedia
                    };
                    db.SaveChanges();

                    db.TreeDecisionXMultimedia.Add(TreeDecisionXMultimediaObj);
                    db.SaveChanges();
                }

                Status = true;
            }
            catch (Exception e)
            {

                Status = false;
                throw e;
            }
            return new JsonResult { Data = new { status = Status, treeDecisionID = TreeDecisionID } };
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}