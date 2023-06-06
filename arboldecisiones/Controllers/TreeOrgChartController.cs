using arboldecisiones.Classes;
using arboldecisiones.Models;
using arboldecisiones.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace arboldecisiones.Controllers
{
    public class TreeOrgChartController : Controller
    {
        private arboldecisionesContext db;

        public TreeOrgChartController()
        {
            db = new arboldecisionesContext();
        }


        public ActionResult TreeConfig()
        {
            var category = db.Categories.ToList();
            category.Add(new Category { CategoryID = 0, Name = "Seleccione la categoría." });
            category = category.OrderBy(c => c.CategoryID).ToList();
            ViewBag.CategoryID = new SelectList(category, "CategoryID", "Name");


            var defects = db.Defects.ToList();
            defects.Add(new Defect { DefectID = 0, Name = "Seleccione el defecto." });
            defects = defects.OrderBy(c => c.DefectID).ToList();
            ViewBag.DefectID = new SelectList(defects, "DefectID", "Name");

            return View();
        }


        [HttpPost]
        public JsonResult SaveTreeConfiguration(HttpPostedFileBase[] MultimediaFile, string[] typeFile, string[] Description, string Definition, int CategoryID, int DefectID)
        {
            byte[] fileByte = Convert.FromBase64String(Definition);
            var dats = System.Text.Encoding.UTF8.GetString(fileByte);
            bool Status = false;
            bool Save = false;
            int TreeConfigurationID = 0;
            try
            {

                //var reg = (from config in db.TreeConfigurations select config).Count();

                //if (reg < 25)
                //{
                Save = true;
                var TreeConfiguration = new TreeConfiguration
                {
                    Definition = dats,
                    Active = true,
                    UserID = "0",
                    UpdateDate = DateTime.Now,
                    CategoryID = CategoryID,
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

                    byte[] DescriptionByte = Convert.FromBase64String(Description[i]);
                    var _description = System.Text.Encoding.UTF8.GetString(DescriptionByte);

                    Multimedia.Description = _description;
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
                //}
                //else
                //{

                //}

                Status = true;
            }
            catch (Exception e)
            {
                TreeConfigurationID = 0;
                Status = false;
                throw e;
            }
            return new JsonResult { Data = new { status = Status, save = Save, treeConfigurationID = TreeConfigurationID } };
        }

        [HttpPost]
        public JsonResult UpdateTreeConfiguration(HttpPostedFileBase[] MultimediaFile, string[] typeFile, string[] Description, int[] idMultimedia, string Definition, int CategoryID, int DefectID, int TreeConfigurationID)
        {
            bool Status = false;
            try
            {
                byte[] fileByte = Convert.FromBase64String(Definition);
                var dats = System.Text.Encoding.UTF8.GetString(fileByte);
                TreeConfiguration TreeConfiguration = db.TreeConfigurations.Find(TreeConfigurationID);
                TreeConfiguration.Definition = dats;
                //TreeConfiguration.UserID = UserID;
                TreeConfiguration.UpdateDate = DateTime.Now;
                TreeConfiguration.CategoryID = CategoryID;
                TreeConfiguration.DefectID = DefectID;
                db.SaveChanges();


                //Se consultan las imágenes que tiene asociados cada defecto
                var MultTreeConfID = (from imageID in db.TreeConfigurationXMultimedias
                                      where imageID.TreeConfigurationID == TreeConfigurationID
                                      select imageID).ToList();


                foreach (var item in MultTreeConfID)
                {

                    var existe = false;
                    if (idMultimedia != null)
                    {
                        for (int i = 0; i < idMultimedia.Length; i++)
                        {
                            if (idMultimedia[i] == item.TreeMultimediaID)
                            {
                                existe = true;
                                break;
                            }

                        }
                    }
                    if (!existe)
                    {
                        var multimedia = db.TreeMultimedia.Find(item.TreeMultimediaID);
                        var multXconfig = (from multConfigID in db.TreeConfigurationXMultimedias
                                           where multConfigID.TreeConfigurationID == TreeConfigurationID && multConfigID.TreeMultimediaID == item.TreeMultimediaID
                                           select multConfigID).FirstOrDefault();
                        db.TreeConfigurationXMultimedias.Remove(multXconfig);
                        db.TreeMultimedia.Remove(multimedia);
                        db.SaveChanges();
                    }
                }


                for (int i = 0; i < Description.Count(); i++)
                {
                    //ruta de la imagen pic
                    var pic = string.Empty;
                    var folde = "~/Content/Images/TreeDecisions";


                    if (idMultimedia[i] == 0)
                    {
                        if (MultimediaFile[i] != null)
                        {
                            pic = FilesHelper.UploadMultimedia(MultimediaFile[i], folde);
                            pic = string.Format("{0}/{1}", folde, pic);
                        }

                        byte[] DescriptionByte = Convert.FromBase64String(Description[i]);
                        var _description = System.Text.Encoding.UTF8.GetString(DescriptionByte);
                        var Multimedia = new TreeMultimedia();
                        Multimedia.Description = _description;
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
                    else
                    {
                        byte[] DescriptionByte = Convert.FromBase64String(Description[i]);
                        var _description = System.Text.Encoding.UTF8.GetString(DescriptionByte);

                        TreeMultimedia TreeMultimedia = db.TreeMultimedia.Find(idMultimedia[i]);
                        TreeMultimedia.Description = _description;
                        db.SaveChanges();
                    }
                }
                Status = true;
            }
            catch (Exception e)
            {
                Status = false;
                throw e;
            }
            return new JsonResult { Data = new { status = Status, treeConfigurationID = TreeConfigurationID } };
        }


        [HttpPost]
        public JsonResult SaveTreeDecision(int treeConfigID, int treeID, string txtTitle, string textDescription, bool ckbACtivo, string type, string colourHex, HttpPostedFileBase[] MultimediaFile, string[] typeFile, string[] Description)
        {
            bool Status = false;
            int TreeDecisionID = 0;
            try
            {
                var typeCS = 0;

                if (type == "Causa")
                {
                    typeCS = db.Types.FirstOrDefault(c => c.Name == "Causa").TypeID;
                }

                if (type == "Solución")
                {
                    typeCS = db.Types.FirstOrDefault(c => c.Name == "Solución").TypeID;
                }

                if (type == "Decisión")
                {
                    typeCS = db.Types.FirstOrDefault(c => c.Name == "Decisión").TypeID;
                }

                if (type == "Unión")
                {
                    typeCS = db.Types.FirstOrDefault(c => c.Name == "Unión").TypeID;
                }

                byte[] fileByte = Convert.FromBase64String(textDescription);
                var _textDescription = System.Text.Encoding.UTF8.GetString(fileByte);

                var TreeDecisionObj = new TreeDecision
                {
                    Name = txtTitle,
                    Description = _textDescription,
                    Active = ckbACtivo,
                    UserID = "0",
                    UpdateDate = DateTime.Now,
                    TreeConfigurationID = treeConfigID,
                    FatherID = treeID,
                    NumberSolutions = 0,
                    TypeID = typeCS,
                    ColourHex = colourHex
                };

                db.TreeDecisions.Add(TreeDecisionObj);
                db.SaveChanges();

                TreeDecisionID = TreeDecisionObj.TreeDecisionID;

                if (Description != null)
                {
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

                        byte[] DescriptionByte = Convert.FromBase64String(Description[i]);
                        var _description = System.Text.Encoding.UTF8.GetString(DescriptionByte);

                        var Multimedia = new TreeMultimedia();
                        Multimedia.Description = _description;
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

        [HttpPost]
        public JsonResult UpdateTreeDecision(int treeDecisionID, string txtTitle, string textDescription, bool ckbACtivo, string type, string colourHex, HttpPostedFileBase[] MultimediaFile, string[] typeFile, string[] Description, int[] idMultimedia)
        {
            bool Status = false;
            var ColourHex = "";
            var Father = false;
            try
            {
                var typeCS = 0;

                if (type == "Causa")
                {
                    typeCS = db.Types.FirstOrDefault(c => c.Name == "Causa").TypeID;
                }

                if (type == "Solución")
                {
                    typeCS = db.Types.FirstOrDefault(c => c.Name == "Solución").TypeID;
                }

                if (type == "Decisión")
                {
                    typeCS = db.Types.FirstOrDefault(c => c.Name == "Decisión").TypeID;
                }

                if (type == "Unión")
                {
                    typeCS = db.Types.FirstOrDefault(c => c.Name == "Unión").TypeID;
                }


                if (type != "Unión")
                {
                    var treeFather = (from TreeDecis in db.TreeDecisions
                                      where TreeDecis.JoinFatherID == treeDecisionID
                                      select TreeDecis).FirstOrDefault();
                    if (treeFather != null)
                    {
                        Father = true;
                    }

                }

                byte[] fileByte = Convert.FromBase64String(textDescription);
                var _textDescription = System.Text.Encoding.UTF8.GetString(fileByte);

                TreeDecision TreeDecisionObj = db.TreeDecisions.Find(treeDecisionID);
                TreeDecisionObj.Name = txtTitle;
                TreeDecisionObj.Description = _textDescription;
                TreeDecisionObj.Active = ckbACtivo;
                TreeDecisionObj.UserID = "0";
                TreeDecisionObj.UpdateDate = DateTime.Now;

                if (TreeDecisionObj.TypeID == 4)
                {
                    if ((type != "Unión") && (Father == false))
                    {
                        TreeDecisionObj.ColourHex = colourHex;
                    }
                }
                else
                {
                    TreeDecisionObj.ColourHex = colourHex;
                }
                ColourHex = TreeDecisionObj.ColourHex;

                if (!Father)
                {
                    TreeDecisionObj.TypeID = typeCS;
                }
                db.SaveChanges();

                //Se consultan las imágenes que tiene asociados cada defecto
                var MultTreeConfID = (from imageID in db.TreeDecisionXMultimedia
                                      where imageID.TreeDecisionID == treeDecisionID
                                      select imageID).ToList();

                foreach (var item in MultTreeConfID)
                {

                    var existe = false;
                    if (idMultimedia != null)
                    {
                        for (int i = 0; i < idMultimedia.Length; i++)
                        {
                            if (idMultimedia[i] == item.TreeMultimediaID)
                            {
                                existe = true;
                                break;
                            }
                        }
                    }

                    if (!existe)
                    {
                        var multimedia = db.TreeMultimedia.Find(item.TreeMultimediaID);
                        var multXDecis = (from multDecisID in db.TreeDecisionXMultimedia
                                          where multDecisID.TreeDecisionID == treeDecisionID && multDecisID.TreeMultimediaID == item.TreeMultimediaID
                                          select multDecisID).FirstOrDefault();
                        db.TreeDecisionXMultimedia.Remove(multXDecis);
                        db.TreeMultimedia.Remove(multimedia);
                        db.SaveChanges();
                    }
                }

                if (!Father)
                {
                    if (Description != null)
                    {
                        for (int i = 0; i < Description.Count(); i++)
                        {

                            //ruta de la imagen pic
                            var pic = string.Empty;
                            var folde = "~/Content/Images/TreeDecisions";

                            if (idMultimedia[i] == 0)
                            {

                                if (MultimediaFile[i] != null)
                                {
                                    pic = FilesHelper.UploadMultimedia(MultimediaFile[i], folde);
                                    pic = string.Format("{0}/{1}", folde, pic);
                                }

                                byte[] DescriptionByte = Convert.FromBase64String(Description[i]);
                                var _description = System.Text.Encoding.UTF8.GetString(DescriptionByte);

                                var Multimedia = new TreeMultimedia();
                                Multimedia.Description = _description;
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
                            else
                            {
                                byte[] DescriptionByte = Convert.FromBase64String(Description[i]);
                                var _description = System.Text.Encoding.UTF8.GetString(DescriptionByte);
                                TreeMultimedia TreeMultimedia = db.TreeMultimedia.Find(idMultimedia[i]);
                                TreeMultimedia.Description = _description;
                                db.SaveChanges();
                            }
                        }
                    }
                }
                Status = true;
            }
            catch (Exception e)
            {

                Status = false;
                throw e;
            }
            return new JsonResult { Data = new { status = Status, father = Father, colourHex = ColourHex } };
        }


        [HttpPost]
        public JsonResult QueryTreeConfiguration(int _treeConfigurationID)
        {
            bool Status = false;
            //Se trae el modelo de Defectos
            var TreConfig = new TreeConfiguration();
            var TreeMutimedia = new List<TreeMultimedia>();
            try
            {

                var treeConfiguration = (from Defect in db.TreeConfigurations
                                         where Defect.TreeConfigurationID == _treeConfigurationID
                                         select Defect).FirstOrDefault();

                TreConfig.TreeConfigurationID = treeConfiguration.TreeConfigurationID;
                TreConfig.Definition = treeConfiguration.Definition;
                TreConfig.Active = treeConfiguration.Active;
                TreConfig.UserID = treeConfiguration.UserID;
                TreConfig.UpdateDate = treeConfiguration.UpdateDate;
                TreConfig.CategoryID = treeConfiguration.CategoryID;
                TreConfig.DefectID = treeConfiguration.DefectID;


                //Se consultan las imágenes que tiene asociados cada defecto
                var treeMulXConfig = (from image in db.TreeMultimedia
                                      join configXimage in db.TreeConfigurationXMultimedias on image.TreeMultimediaID equals configXimage.TreeMultimediaID
                                      where configXimage.TreeConfigurationID == treeConfiguration.TreeConfigurationID
                                      select image).ToList();


                foreach (var item in treeMulXConfig)
                {
                    var TreeMul = new TreeMultimedia()
                    {
                        TreeMultimediaID = item.TreeMultimediaID,
                        Description = item.Description,
                        Url = item.Url,
                        Type = item.Type
                    };
                    TreeMutimedia.Add(TreeMul);
                }

                Status = true;
            }
            catch (Exception e)
            {

                Status = false;
                throw e;
            }
            return new JsonResult { Data = new { status = Status, treConfig = TreConfig, treeMultimedia = TreeMutimedia }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public JsonResult QueryTreeDecision(int _treeDecisionID)
        {
            bool Status = false;
            //Se trae el modelo de Defectos
            var TreeDecision = new TreeDecision();
            var TreeMutimedia = new List<TreeMultimedia>();
            try
            {

                var treeDecisions = (from Decisions in db.TreeDecisions
                                     where Decisions.TreeDecisionID == _treeDecisionID
                                     select Decisions).FirstOrDefault();

                TreeDecision.TreeDecisionID = treeDecisions.TreeDecisionID;
                TreeDecision.FatherID = treeDecisions.FatherID;
                TreeDecision.Name = treeDecisions.Name;
                TreeDecision.Description = treeDecisions.Description;
                TreeDecision.Active = treeDecisions.Active;
                TreeDecision.NumberSolutions = treeDecisions.NumberSolutions;
                TreeDecision.UserID = treeDecisions.UserID;
                TreeDecision.UpdateDate = treeDecisions.UpdateDate;
                TreeDecision.TreeConfigurationID = treeDecisions.TreeConfigurationID;
                TreeDecision.TypeID = treeDecisions.TypeID;
                TreeDecision.JoinFatherID = treeDecisions.JoinFatherID;

                //Se consultan las imágenes que tiene asociados cada defecto
                var treeMulXDecis = (from image in db.TreeMultimedia
                                     join DecisXimage in db.TreeDecisionXMultimedia on image.TreeMultimediaID equals DecisXimage.TreeMultimediaID
                                     where DecisXimage.TreeDecisionID == treeDecisions.TreeDecisionID
                                     select image).ToList();


                foreach (var item in treeMulXDecis)
                {
                    var TreeMul = new TreeMultimedia()
                    {
                        TreeMultimediaID = item.TreeMultimediaID,
                        Description = item.Description,
                        Url = item.Url,
                        Type = item.Type
                    };
                    TreeMutimedia.Add(TreeMul);
                }

                Status = true;
            }
            catch (Exception e)
            {

                Status = false;
                throw e;
            }
            return new JsonResult { Data = new { status = Status, treeDecision = TreeDecision, treeMultimedia = TreeMutimedia }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult QueryDecisionsXDefect(int _treeConfigurationID)
        {
            bool Status = false;
            //Se trae el modelo de Defectos

            var TreeDecisionList = new List<TreeDecision>();

            try
            {

                var treeDecisions = (from Decisions in db.TreeDecisions
                                     where Decisions.TreeConfigurationID == _treeConfigurationID && Decisions.TypeID == 4
                                     select Decisions).ToList();


                foreach (var item in treeDecisions)
                {
                    var TreeDecision = new TreeDecision();
                    TreeDecision.TreeDecisionID = item.TreeDecisionID;
                    TreeDecision.FatherID = item.FatherID;
                    TreeDecision.Name = item.Name;
                    TreeDecision.Description = item.Description;
                    TreeDecision.Active = item.Active;
                    TreeDecision.NumberSolutions = item.NumberSolutions;
                    TreeDecision.UserID = item.UserID;
                    TreeDecision.UpdateDate = item.UpdateDate;
                    TreeDecision.TreeConfigurationID = item.TreeConfigurationID;
                    TreeDecision.TypeID = item.TypeID;

                    TreeDecisionList.Add(TreeDecision);

                }

                Status = true;
            }
            catch (Exception e)
            {

                Status = false;
                throw e;
            }
            return new JsonResult { Data = new { status = Status, treeDecision = TreeDecisionList }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult UpdateJoinDecision(int _TreeDecisionID, int _Father)
        {
            bool Status = false;
            var ColourHex = "";
            try
            {
                TreeDecision TreeDecisionObj = db.TreeDecisions.Find(_TreeDecisionID);
                TreeDecisionObj.JoinFatherID = _Father;
                db.SaveChanges();

                if (_Father != 0)
                {
                    var _treeDecisionFather = (from Decisions in db.TreeDecisions
                                               where Decisions.TreeDecisionID == _Father
                                               select Decisions).FirstOrDefault();
                    ColourHex = _treeDecisionFather.ColourHex;
                }
                else
                {
                    ColourHex = "";
                }



                Status = true;
            }
            catch (Exception e)
            {

                Status = false;
                throw e;
            }
            return new JsonResult { Data = new { status = Status, colourHex = ColourHex } };
        }


        //[HttpPost]
        //public JsonResult Config(int _config)
        //{
        //    var reg = (from config in db.TreeConfigurations select config).Count();

        //    if (reg < 25)
        //    {
        //        return new JsonResult { Data = new { save = true } };
        //    }
        //    else
        //    {
        //        return new JsonResult { Data = new { save = false } };
        //    }

        //}

        [HttpPost]
        public JsonResult DeleteTreeDecision(int _treeDecisionID, int _treeConfigurationID)
        {
            bool Status = false;
            //Se trae el modelo de Defectos
            var Remove = 0;
            try
            {
                var treeDecisionsFather = (from Decisions in db.TreeDecisions
                                           where Decisions.FatherID == _treeDecisionID && Decisions.TreeConfigurationID == _treeConfigurationID
                                           select Decisions).ToList();

                var treeDecisions = (from Decisions in db.TreeDecisions
                                     where Decisions.TreeDecisionID == _treeDecisionID && Decisions.TreeConfigurationID == _treeConfigurationID && Decisions.JoinFatherID != 0
                                     select Decisions).ToList();

                var treeDecisionsUnion = (from Decisions in db.TreeDecisions
                                          where Decisions.JoinFatherID == _treeDecisionID && Decisions.TreeConfigurationID == _treeConfigurationID
                                          select Decisions).ToList();

                if ((treeDecisionsFather.Count == 0) && (treeDecisions.Count == 0) && (treeDecisionsUnion.Count == 0))
                {
                    var treeDecisionsDets = (from DecisionDets in db.TreeDecisionDet
                                             where DecisionDets.TreeDecisionID == _treeDecisionID
                                             select DecisionDets).ToList();

                    if (treeDecisionsDets.Count == 0)
                    {

                        var TreeDecisions = db.TreeDecisions.Find(_treeDecisionID);

                        var TreeDecXMult = db.TreeDecisionXMultimedia.Where(c => c.TreeDecisionID == _treeDecisionID).ToList();

                        foreach (var tdm in TreeDecXMult)
                        {
                            db.TreeMultimedia.Remove(tdm.TreeMultimedia);
                            db.TreeDecisionXMultimedia.Remove(tdm);
                        }
                        db.TreeDecisions.Remove(TreeDecisions);
                        db.SaveChanges();
                        Remove = 1;
                    }
                    else
                    {
                        Remove = 3;
                    }
                }
                else
                {
                    Remove = 2;
                }

                Status = true;
            }
            catch (Exception e)
            {

                Status = false;
                throw e;
            }
            return new JsonResult { Data = new { status = Status, remove = Remove }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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