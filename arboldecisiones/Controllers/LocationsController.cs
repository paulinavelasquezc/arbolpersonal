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
    public class LocationsController : Controller
    {
        private arboldecisionesContext db = new arboldecisionesContext();

        // GET: Locations
        public ActionResult Index()
        {
            var categoryContainers = db.CategoryContainers.ToList();
            categoryContainers.Add(new CategoryContainer { CategoryContainerID = 0, Name = "Seleccione la categoria envase." });
            categoryContainers = categoryContainers.OrderBy(c => c.CategoryContainerID).ToList();
            ViewBag.CategoryContainerID = new SelectList(categoryContainers, "CategoryContainerID", "Name");

            return View();
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Location locationId = db.Locations.Find(id);

            var location = new Location();
            location.LocationID = locationId.LocationID;
            location.Name = locationId.Name;
            location.Active = locationId.Active;
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(locationId.UserID);
            location.UserID = user.Name + ' ' + user.LastName;
            location.UpdateDate = locationId.UpdateDate;
            location.Multimedia = locationId.Multimedia;
            location.Multimedia.Url = (locationId.Multimedia.Url).Replace("~", "../..");

            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            var categoryContainers = db.CategoryContainers.ToList();
            categoryContainers.Add(new CategoryContainer { CategoryContainerID = 0, Name = "Seleccione la categoria envase." });
            categoryContainers = categoryContainers.OrderBy(c => c.ProcessID).ToList();
            ViewBag.CategoryContainerID = new SelectList(categoryContainers, "CategoryContainerID", "Name");

            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationID,Name,Active,UserID,UpdateDate")] Location location)
        {
            if (ModelState.IsValid)
            {
                //Se obtiene el id del usuario que esta en sesion
                location.UserID = User.Identity.GetUserId();
                //Se obtiene la fecha actual
                location.UpdateDate = DateTime.Now;
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(location);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationID,Name,Active,UserID,UpdateDate")] Location location)
        {
            if (ModelState.IsValid)
            {
                location.UserID = User.Identity.GetUserId();
                location.UpdateDate = DateTime.Now;

                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location locationId = db.Locations.Find(id);

            var location = new Location();
            location.LocationID = locationId.LocationID;
            location.Name = locationId.Name;
            location.Active = locationId.Active;
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(locationId.UserID);
            location.UserID = user.Name + ' ' + user.LastName;
            location.UpdateDate = locationId.UpdateDate;
            location.Multimedia = locationId.Multimedia;
            location.Multimedia.Url = (locationId.Multimedia.Url).Replace("~", "../..");


            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

       // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var valDelect = db.Defects.Where(c => c.LocationID == id).ToList();
            Location locationId = db.Locations.Find(id);
            if (valDelect.Count == 0)
            {
                db.Locations.Remove(locationId);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Error = "La ubicación no se puede eliminar, esta relacionada.";

            var location = new Location();
            location.LocationID = locationId.LocationID;
            location.Name = locationId.Name;
            location.Active = locationId.Active;
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(locationId.UserID);
            location.UserID = user.Name + ' ' + user.LastName;
            location.UpdateDate = locationId.UpdateDate;
            location.Multimedia = locationId.Multimedia;
            location.Multimedia.Url = (locationId.Multimedia.Url).Replace("~", "../..");

            return View(location);
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
        public virtual ActionResult ViewParcialListar(int _categoryContainerID = 0)
        {
            var locationsList = new List<Location>();
            foreach (var item in db.Locations.Where(c => c.CategoryContainerID == _categoryContainerID).ToList())
            {
                var location = new Location();
                location.LocationID = item.LocationID;
                location.Name = item.Name;
                location.Active = item.Active;
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(item.UserID);
                location.UserID = user.Name + ' ' + user.LastName;
                location.UpdateDate = item.UpdateDate;

                locationsList.Add(location);
            }
            return PartialView("SearchLocations", locationsList);
        }

        [HttpPost]
        public JsonResult Save(HttpPostedFileBase MultimediaFile, string typeFile, string Name, int CategoryContainerID)
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


                var Location = new Location
                {
                    Name = Name,
                    Active = true,
                    UserID = User.Identity.GetUserId(),
                    UpdateDate = DateTime.Now,
                    Multimedia = Multimedia,
                    CategoryContainerID = CategoryContainerID
                };
                db.Locations.Add(Location);
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
