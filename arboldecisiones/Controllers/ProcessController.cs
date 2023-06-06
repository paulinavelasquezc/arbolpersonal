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
    public class ProcessController : Controller
    {
        private arboldecisionesContext db = new arboldecisionesContext();

        // GET: Process
        public ActionResult Index()
        {

            var processList = new List<Process>();
            foreach (var item in db.Process)
            {
                var process = new Process();
                process.ProcessID = item.ProcessID;
                process.Name = item.Name;
                process.Active = item.Active;
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(item.UserID);
                process.UserID = user.Name + ' ' + user.LastName;
                process.UpdateDate = item.UpdateDate;

                processList.Add(process);
            }
            return View(processList);

        }

        // GET: Process/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Process processId = db.Process.Find(id);
            var process = new Process();
            process.ProcessID = processId.ProcessID;
            process.Name = processId.Name;
            process.Active = processId.Active;
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(processId.UserID);
            process.UserID = user.Name + ' ' + user.LastName;
            process.UpdateDate = processId.UpdateDate;
            process.Multimedia = processId.Multimedia;
            process.Multimedia.Url = (processId.Multimedia.Url).Replace("~", "../..");


            if (process == null)
            {
                return HttpNotFound();
            }
            return View(process);


        }

        // GET: Process/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: Process/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Process/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process process = db.Process.Find(id);
            if (process == null)
            {
                return HttpNotFound();
            }
            return View(process);            
        }

        // POST: Process/Edit/5
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

        // GET: Process/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process processId = db.Process.Find(id);
            var process = new Process();
            process.ProcessID = processId.ProcessID;
            process.Name = processId.Name;
            process.Active = processId.Active;
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(processId.UserID);
            process.UserID = user.Name + ' ' + user.LastName;
            process.UpdateDate = processId.UpdateDate;
            process.Multimedia = processId.Multimedia;
            process.Multimedia.Url = (processId.Multimedia.Url).Replace("~", "../..");


            if (process == null)
            {
                return HttpNotFound();
            }
            return View(process);

        }

        // POST: Process/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var val = db.CategoryContainers.Where(c => c.ProcessID == id).ToList();
            Process processId = db.Process.Find(id);
            if (val.Count == 0)
            {
                db.Process.Remove(processId);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Error = "La proceso no se puede eliminar, esta relacionada.";
           
            var process = new Process();
            process.ProcessID = processId.ProcessID;
            process.Name = processId.Name;
            process.Active = processId.Active;
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(processId.UserID);
            process.UserID = user.Name + ' ' + user.LastName;
            process.UpdateDate = processId.UpdateDate;
            process.Multimedia = processId.Multimedia;
            process.Multimedia.Url = (processId.Multimedia.Url).Replace("~", "../..");
            return View(process);
        }


        [HttpPost]
        public JsonResult SaveProcess(HttpPostedFileBase MultimediaFile, string typeFile, string Name)
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


                var Process = new Process
                {
                    Name = Name,
                    Active = true,
                    UserID = User.Identity.GetUserId(),
                    UpdateDate = DateTime.Now,
                    Multimedia = Multimedia
                };
                db.Process.Add(Process);
                db.SaveChanges();

                Status = true;
            }
            catch (Exception e)
            {
                Status = false;
                throw e;
            }
            return new JsonResult { Data = new { status = Status, save = Save} };
        }


    }
}
