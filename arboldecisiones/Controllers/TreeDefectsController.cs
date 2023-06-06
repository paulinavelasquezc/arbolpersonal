using arboldecisiones.Models;
using arboldecisiones.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace arboldecisiones.Controllers
{
    [Authorize]
    public class TreeDefectsController : Controller
    {
        private arboldecisionesContext db = new arboldecisionesContext();

        public ActionResult SearchClassification()
        {

            var category = db.Categories.ToList();
            category.Add(new Category { CategoryID = 0, Name = "Consultar Todo" });
            category = category.OrderBy(c => c.CategoryID).ToList();
            ViewBag.CategoryID = new SelectList(category, "CategoryID", "Name");
            return View();
        }

        public ActionResult TreeProcess()
        {
            return View();
        }

        public ActionResult TreeBottleDefect()
        {
            return View();
        }

        public ActionResult TreeDefectDescription()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult ViewParcialDefectos(int _locationID = 0, int _categoryID = 0)
        {
            //Se trae el modelo de Defectos
            var treesDefects = new List<TreesDefects>();

            IQueryable<TreeConfiguration> treeConfiguration = db.TreeConfigurations;

            if (_locationID != 0)
            {
                //treeConfiguration = treeConfiguration.Where(c => c.LocationID == _locationID);
            }

            if (_categoryID != 0)
            {
                treeConfiguration = treeConfiguration.Where(c => c.CategoryID == _categoryID);
            }

            var treeConfign = (from config in treeConfiguration
                               select config).ToList();

            //Se realiza el foreach a cada defecto para consultar su imagen
            foreach (var item in treeConfign)
            {
                //Se consultan las imágenes que tiene asociados cada defecto
                var treeImagen = (from image in db.TreeMultimedia
                                  join configXimage in db.TreeConfigurationXMultimedias on image.TreeMultimediaID equals configXimage.TreeMultimediaID
                                  where configXimage.TreeConfigurationID == item.TreeConfigurationID
                                  select image).ToList();

                var treeConfig = new TreesDefects();
                treeConfig.TreeConfiguration = item;
                treeConfig.TreeMultimedia = treeImagen;
                treesDefects.Add(treeConfig);

            }
            return PartialView("TreesDefects", treesDefects.ToList());
        }

        [HttpGet]
        public virtual ActionResult ViewParcialCausas(int _treeConfigurationID)
        {
            var treesCauses = new List<TreesCauses>();

            var treeDecisions = db.TreeDecisions.Where(c => c.TreeConfigurationID == _treeConfigurationID && c.FatherID == 0).ToList();

            foreach (var item in treeDecisions)
            {
                var treeImagen = (from image in db.TreeMultimedia
                                  join desiciXimage in db.TreeDecisionXMultimedia on image.TreeMultimediaID equals desiciXimage.TreeMultimediaID
                                  where desiciXimage.TreeDecisionID == item.TreeDecisionID
                                  select image).ToList();

                var treesCausesObj = new TreesCauses();
                treesCausesObj.TreeDecision = item;
                treesCausesObj.TreeMultimedia = treeImagen;
                treesCauses.Add(treesCausesObj);
            }
            return PartialView("TreesCauses", treesCauses);
        }

        /*Este método trae las causas y las soluciones del defecto*/
        [HttpGet]
        public virtual ActionResult ViewParcialCausasSoluciones(int _treeConfigurationID, int _treeDecisionID, bool _defect)
        {
            //se valida si es un defecto
            if (_defect)
            {
                /*Insertar en la tabla detalle la infomración*/
                var treeConfigDet = new TreeConfigurationDet()
                {
                    TreeConfigurationID = _treeConfigurationID,
                    IdUser = User.Identity.GetUserId(),
                    UpdateDate = DateTime.Now

                };

                db.TreeConfigurationDet.Add(treeConfigDet);
                db.SaveChanges();
                int treeConfigDetId = treeConfigDet.TreeConfigurationDetID;
                Session["treeConfigDetId"] = treeConfigDetId;

            }
            else
            {
                var treeConfigDetId = (int)Session["treeConfigDetId"];
                //Consultar el orden maximo
                var TreeDecisionDet = (from desc in db.TreeDecisionDet
                                       where desc.TreeConfigurationDetID == treeConfigDetId
                                       select desc).ToList();

                var order = 0;
                if (TreeDecisionDet.Count != 0)
                {
                    order = TreeDecisionDet.Max(c => c.OrderDet);
                }

                var treeDecDet = new TreeDecisionDet()
                {

                    TreeConfigurationDetID = (int)Session["treeConfigDetId"],
                    TreeDecisionID = _treeDecisionID,
                    UpdateDate = DateTime.Now,
                    OrderDet = order + 1

                };

                db.TreeDecisionDet.Add(treeDecDet);
                db.SaveChanges();
                int treeDecgDetId = treeDecDet.TreeDecisionDetID;
            }



            var treesCauses = new List<TreesCauses>();

            var treeDecisJoinFather = db.TreeDecisions.Where(c => c.TreeConfigurationID == _treeConfigurationID && c.TreeDecisionID == _treeDecisionID).FirstOrDefault();

            var treeDecisions = new List<TreeDecision>();

            if (treeDecisJoinFather != null)
            {
                if (treeDecisJoinFather.JoinFatherID == 0)
                {
                    treeDecisions = db.TreeDecisions.Where(c => c.TreeConfigurationID == _treeConfigurationID && c.FatherID == _treeDecisionID).ToList();
                }
                else
                {
                    treeDecisions = db.TreeDecisions.Where(c => c.TreeConfigurationID == _treeConfigurationID && c.TreeDecisionID == treeDecisJoinFather.JoinFatherID).ToList();
                }
            }
            else
            {
                treeDecisions = db.TreeDecisions.Where(c => c.TreeConfigurationID == _treeConfigurationID && c.FatherID == _treeDecisionID).ToList();
            }
            
            if (treeDecisions.Count() == 1)
            {
                if (treeDecisions[0].TypeID == 4)
                {
                    var _treDecisID = treeDecisions[0].TreeDecisionID;
                    treeDecisions = db.TreeDecisions.Where(c => c.TreeConfigurationID == _treeConfigurationID && c.FatherID == _treDecisID).ToList();
                }
            }

            foreach (var item in treeDecisions)
            {
                var treeImagen = (from image in db.TreeMultimedia
                                  join desiciXimage in db.TreeDecisionXMultimedia on image.TreeMultimediaID equals desiciXimage.TreeMultimediaID
                                  where desiciXimage.TreeDecisionID == item.TreeDecisionID
                                  select image).ToList();

                var treesCausesObj = new TreesCauses();
                treesCausesObj.TreeDecision = item;
                treesCausesObj.TreeMultimedia = treeImagen;
                treesCauses.Add(treesCausesObj);
            }

            ViewBag.TreeConfigurationID = _treeConfigurationID;
            ViewBag.TreeDecisionID = _treeDecisionID;
            
            return PartialView("TreesCausesSolutions", treesCauses);
        }

        [HttpGet]
        public virtual ActionResult ViewParcialSolution(int _treeDecisionID)
        {

            var treesCauses = new List<TreesCauses>();

            var treeDecisions = db.TreeDecisions.Where(c => c.FatherID == _treeDecisionID).ToList();
            ViewBag.TreeConfigurationID = treeDecisions[0].TreeConfigurationID;
            foreach (var item in treeDecisions)
            {
                var treeMultimedia = (from image in db.TreeMultimedia
                                      join desiciXimage in db.TreeDecisionXMultimedia on image.TreeMultimediaID equals desiciXimage.TreeMultimediaID
                                      where desiciXimage.TreeDecisionID == item.TreeDecisionID
                                      select image).ToList();

                var treesCausesObj = new TreesCauses();
                treesCausesObj.TreeDecision = item;
                treesCausesObj.TreeMultimedia = treeMultimedia;
                treesCauses.Add(treesCausesObj);
            }

            return PartialView("TreesSolutions", treesCauses);
        }

        public JsonResult GetSearchValueDefect(string search)
        {
            var query = (from config in db.TreeConfigurations
                         where config.Defect.Name.Contains(search)
                         group config by config.Defect.Name into qrGr
                         select new
                         {
                             Name = qrGr.Key
                         }).ToList();

            return new JsonResult { Data = query, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public virtual ActionResult ViewParcialDefectosFilter(string _filter)
        {
            var treesDefects = new List<TreesDefects>();

            var treeConfign = db.TreeConfigurations.Where(c => c.Defect.Name.Contains(_filter)).ToList();

            foreach (var item in treeConfign)
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
            return PartialView("TreesDefects", treesDefects.ToList());
        }


        [HttpGet]
        public virtual ActionResult CorrectSolution(int _treeConfigurationID, int _treeDecisionID, bool _solution)
        {
            var treeConfigDetId = (int)Session["treeConfigDetId"];
            //Consultar el orden maximo
            var TreeDecisionDet = (from desc in db.TreeDecisionDet
                                   where desc.TreeConfigurationDetID == treeConfigDetId
                                   select desc).ToList();

            var order = 0;
            if (TreeDecisionDet.Count != 0)
            {
                order = TreeDecisionDet.Max(c => c.OrderDet);
            }

            var treeDecDet = new TreeDecisionDet()
            {

                TreeConfigurationDetID = (int)Session["treeConfigDetId"],
                TreeDecisionID = _treeDecisionID,
                UpdateDate = DateTime.Now,
                Solution = _solution,
                OrderDet = order + 1
            };

            db.TreeDecisionDet.Add(treeDecDet);
            db.SaveChanges();
            int treeDecgDetId = treeDecDet.TreeDecisionDetID;

            var category = db.Categories.ToList();
            category.Add(new Category { CategoryID = 0, Name = "Consultar Todo" });
            category = category.OrderBy(c => c.CategoryID).ToList();
            ViewBag.CategoryID = new SelectList(category, "CategoryID", "Name");

            return View("SearchClassification");
        }


        [HttpGet]
        public virtual ActionResult ReturnCausasSoluciones(int _treeConfigurationID, int _treeDecisionID)
        {
            var treeDecisions = db.TreeDecisions.Where(c => c.TreeConfigurationID == _treeConfigurationID && c.TreeDecisionID == _treeDecisionID).FirstOrDefault();

            var _bool = false;
            if(treeDecisions.FatherID == 0)
            {
                _bool = true;
            }
            return ViewParcialCausasSoluciones(_treeConfigurationID, treeDecisions.FatherID, _bool);
        }
      }
}