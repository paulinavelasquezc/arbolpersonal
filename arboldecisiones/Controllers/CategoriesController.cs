using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using arboldecisiones.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace arboldecisiones.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private arboldecisionesContext db = new arboldecisionesContext();

        // GET: Categories
        public ActionResult Index()
        {
            var categoryList = new List<Category>();
            foreach (var item in db.Categories)
            {
                var category = new Category();
                category.CategoryID = item.CategoryID;
                category.Name = item.Name;
                category.Active = item.Active;
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(item.UserID);
                category.UserID = user.Name + ' ' + user.LastName;
                category.UpdateDate = item.UpdateDate;

                categoryList.Add(category);
            }
            return View(categoryList);
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);

            var categoryU = new Category();
            categoryU.CategoryID = category.CategoryID;
            categoryU.Name = category.Name;
            categoryU.Active = category.Active;
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(category.UserID);
            categoryU.UserID = user.Name + ' ' + user.LastName;
            categoryU.UpdateDate = category.UpdateDate;            

            if (categoryU == null)
            {
                return HttpNotFound();
            }
            return View(categoryU);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,Name,Active,UserID,UpdateDate")] Category category)
        {
            if (ModelState.IsValid)
            {
                //Se obtiene el id del usuario que esta en sesion
                category.UserID = User.Identity.GetUserId();
                //Se obtiene la fecha actual
                category.UpdateDate = DateTime.Now;
                
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,Name,Active,UserID,UpdateDate")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.UserID = User.Identity.GetUserId();
                category.UpdateDate = DateTime.Now;

                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var valDelect = db.TreeConfigurations.Where(c => c.CategoryID == id).ToList();
            Category category = db.Categories.Find(id);
            if (valDelect.Count == 0)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Error = "La categoría no se puede eliminar, esta relacionada con un defecto.";
            return View(category);
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
