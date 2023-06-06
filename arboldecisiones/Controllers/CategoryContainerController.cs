using arboldecisiones.Classes;
using arboldecisiones.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace arboldecisiones.Controllers
{
    [Authorize]
    public class CategoryContainerController : Controller
    {
        private arboldecisionesContext db = new arboldecisionesContext();

        // GET: CategoryContainer
        public ActionResult Index()
        {

            var process = db.Process.ToList();
            process.Add(new Process { ProcessID = 0, Name = "Seleccione la Proceso." });
            process = process.OrderBy(c => c.ProcessID).ToList();
            ViewBag.ProcessID = new SelectList(process, "ProcessID", "Name");

            return View();            
        }

        // GET: CategoryContainer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryContainer CategoryContainerId = db.CategoryContainers.Find(id);

            var CategoryContainer = new CategoryContainer();
            CategoryContainer.CategoryContainerID = CategoryContainerId.CategoryContainerID;
            CategoryContainer.Name = CategoryContainerId.Name;
            CategoryContainer.Active = CategoryContainerId.Active;
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(CategoryContainerId.UserID);
            CategoryContainer.UserID = user.Name + ' ' + user.LastName;
            CategoryContainer.UpdateDate = CategoryContainerId.UpdateDate;
            CategoryContainer.Multimedia = CategoryContainerId.Multimedia;
            CategoryContainer.Multimedia.Url = (CategoryContainerId.Multimedia.Url).Replace("~", "../..");


            if (CategoryContainer == null)
            {
                return HttpNotFound();
            }
            return View(CategoryContainer);
        }

        // GET: CategoryContainer/Create
        public ActionResult Create()
        {
            var process = db.Process.ToList();
            process.Add(new Process { ProcessID = 0, Name = "Seleccione la Proceso." });
            process = process.OrderBy(c => c.ProcessID).ToList();
            ViewBag.ProcessID = new SelectList(process, "ProcessID", "Name");

            return View();
        }

        // POST: CategoryContainer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryContainer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryContainer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryContainer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryContainer CategoryContainerId = db.CategoryContainers.Find(id);

            var CategoryContainer = new CategoryContainer();
            CategoryContainer.CategoryContainerID = CategoryContainerId.CategoryContainerID;
            CategoryContainer.Name = CategoryContainerId.Name;
            CategoryContainer.Active = CategoryContainerId.Active;
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(CategoryContainerId.UserID);
            CategoryContainer.UserID = user.Name + ' ' + user.LastName;
            CategoryContainer.UpdateDate = CategoryContainerId.UpdateDate;
            CategoryContainer.Multimedia = CategoryContainerId.Multimedia;
            CategoryContainer.Multimedia.Url = (CategoryContainerId.Multimedia.Url).Replace("~", "../..");


            if (CategoryContainer == null)
            {
                return HttpNotFound();
            }
            return View(CategoryContainer);
        }

        // POST: CategoryContainer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var val = db.Locations.Where(c => c.CategoryContainerID == id).ToList();
            CategoryContainer CategoryContainerId = db.CategoryContainers.Find(id);
            if (val.Count == 0)
            {
                db.CategoryContainers.Remove(CategoryContainerId);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Error = "La categoria no se puede eliminar, esta relacionada.";

            var CategoryContainer = new CategoryContainer();
            CategoryContainer.CategoryContainerID = CategoryContainerId.CategoryContainerID;
            CategoryContainer.Name = CategoryContainerId.Name;
            CategoryContainer.Active = CategoryContainerId.Active;
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(CategoryContainerId.UserID);
            CategoryContainer.UserID = user.Name + ' ' + user.LastName;
            CategoryContainer.UpdateDate = CategoryContainerId.UpdateDate;
            CategoryContainer.Multimedia = CategoryContainerId.Multimedia;
            CategoryContainer.Multimedia.Url = (CategoryContainerId.Multimedia.Url).Replace("~", "../..");

            return View(CategoryContainer);
        }

        [HttpPost]
        public JsonResult Save(HttpPostedFileBase MultimediaFile, string typeFile, string Name, int ProcessID)
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


                var CategoryContainer = new CategoryContainer
                {
                    Name = Name,
                    Active = true,
                    UserID = User.Identity.GetUserId(),
                    UpdateDate = DateTime.Now,
                    Multimedia = Multimedia,
                    ProcessID = ProcessID
                };
                db.CategoryContainers.Add(CategoryContainer);
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


        [HttpGet]
        public virtual ActionResult ViewParcialListar(int _processID = 0)
        {
            var CategoryContainerList = new List<CategoryContainer>();

            foreach (var item in db.CategoryContainers.Where(c => c.ProcessID == _processID).ToList())
            {
                var CategoryContainer = new CategoryContainer();
                CategoryContainer.CategoryContainerID = item.CategoryContainerID;
                CategoryContainer.Name = item.Name;
                CategoryContainer.Active = item.Active;
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(item.UserID);
                CategoryContainer.UserID = user.Name + ' ' + user.LastName;
                CategoryContainer.UpdateDate = item.UpdateDate;

                CategoryContainerList.Add(CategoryContainer);
            }
            return PartialView("SearchCategoryContainer", CategoryContainerList);
        }
    }
}
