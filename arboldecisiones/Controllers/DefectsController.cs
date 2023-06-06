using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using arboldecisiones.Classes;
using arboldecisiones.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace arboldecisiones.Controllers
{
    [Authorize]
    public class DefectsController : Controller
    {
        private arboldecisionesContext db = new arboldecisionesContext();

        // GET: Defects
        public ActionResult Index()
        {
            var locations = db.Locations.ToList();
            locations.Add(new Location { LocationID = 0, Name = "Seleccione la Ubicación." });
            locations = locations.OrderBy(c => c.LocationID).ToList();
            ViewBag.LocationID = new SelectList(locations, "LocationID", "Name");
            return View();
        }

        // GET: Defects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
 
            Defect defectId = db.Defects.Find(id);

            var defect = new Defect();
            defect.DefectID = defectId.DefectID;
            defect.Name = defectId.Name;
            defect.Active = defectId.Active;
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(defectId.UserID);
            defect.UserID = user.Name + ' ' + user.LastName;
            defect.UpdateDate = defectId.UpdateDate;
            defect.Multimedia = defectId.Multimedia;
            defect.Multimedia.Url = (defectId.Multimedia.Url).Replace("~", "../..");


            if (defect == null)
            {
                return HttpNotFound();
            }
            return View(defect);
        }

        // GET: Defects/Create
        public ActionResult Create()
        {
            var locations = db.Locations.ToList();
            locations.Add(new Location { LocationID = 0, Name = "Seleccione la Ubicación." });
            locations = locations.OrderBy(c => c.LocationID).ToList();
            ViewBag.LocationID = new SelectList(locations, "LocationID", "Name");

            return View();
        }

        // POST: Defects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DefectID,Name,Active,UserID,UpdateDate")] Defect defect)
        {
            if (ModelState.IsValid)
            {
                defect.UserID = User.Identity.GetUserId();
                //Se obtiene la fecha actual
                defect.UpdateDate = DateTime.Now;

                db.Defects.Add(defect);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(defect);
        }

        // GET: Defects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Defect defect = db.Defects.Find(id);
            if (defect == null)
            {
                return HttpNotFound();
            }
            return View(defect);
        }

        // POST: Defects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DefectID,Name,Active,UserID,UpdateDate")] Defect defect)
        {
            if (ModelState.IsValid)
            {
                defect.UserID = User.Identity.GetUserId();
                defect.UpdateDate = DateTime.Now;

                db.Entry(defect).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(defect);
        }

        // GET: Defects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Defect defectId = db.Defects.Find(id);

            var defect = new Defect();
            defect.DefectID = defectId.DefectID;
            defect.Name = defectId.Name;
            defect.Active = defectId.Active;
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(defectId.UserID);
            defect.UserID = user.Name + ' ' + user.LastName;
            defect.UpdateDate = defectId.UpdateDate;
            defect.Multimedia = defectId.Multimedia;
            defect.Multimedia.Url = (defectId.Multimedia.Url).Replace("~", "../..");


            if (defect == null)
            {
                return HttpNotFound();
            }
            return View(defect);
        }

        // POST: Defects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var valDelect = db.TreeConfigurations.Where(c => c.DefectID == id).ToList();
            Defect defectId = db.Defects.Find(id);
            if (valDelect.Count == 0)
            {
                db.Defects.Remove(defectId);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Error = "La defecto no se puede eliminar, esta relacionada.";

            var defect = new Defect();
            defect.DefectID = defectId.DefectID;
            defect.Name = defectId.Name;
            defect.Active = defectId.Active;
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(defectId.UserID);
            defect.UserID = user.Name + ' ' + user.LastName;
            defect.UpdateDate = defectId.UpdateDate;
            defect.Multimedia = defectId.Multimedia;
            defect.Multimedia.Url = (defectId.Multimedia.Url).Replace("~", "../..");

            return View(defect);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public virtual ActionResult ViewParcialListar(int _locationID = 0)
        {
            var defectList = new List<Defect>();
            foreach (var item in db.Defects.Where(c => c.LocationID == _locationID).ToList())
            {
                var defect = new Defect();
                defect.DefectID = item.DefectID;
                defect.Name = item.Name;
                defect.Active = item.Active;
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(item.UserID);
                defect.UserID = user.Name + ' ' + user.LastName;
                defect.UpdateDate = item.UpdateDate;

                defectList.Add(defect);
            }

            return PartialView("SearchDefecto", defectList);
        }

        [HttpPost]
        public JsonResult Save(HttpPostedFileBase MultimediaFile, string typeFile, string Name, int LocationID)
        {
            bool Status = false;
            bool Save = false;
            try
            {
                Save = true;
                //ruta de la imagen pic
                var pic = string.Empty;
                var folde = "~/Content/Images/imgConfig";

                if (MultimediaFile != null)
                {
                    pic = FilesHelper.UploadMultimedia(MultimediaFile, folde);
                    pic = string.Format("{0}/{1}", folde, pic);
                }

                var Multimedia = new Multimedia();


                Multimedia.Description = Name;
                Multimedia.Url = pic;
                Multimedia.Type = typeFile;
                db.Multimedias.Add(Multimedia);


                var Defect = new Defect
                {
                    Name = Name,
                    Active = true,
                    UserID = User.Identity.GetUserId(),
                    UpdateDate = DateTime.Now,
                    Multimedia = Multimedia,
                    LocationID = LocationID
                };
                db.Defects.Add(Defect);
                db.SaveChanges();

                Status = true;
            }
            catch (Exception e)
            {
                Status = false;
                throw e;
            }
            return new JsonResult { Data = new { status = Status, save = Save } };
        }
    }
}
