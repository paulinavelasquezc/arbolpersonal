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
    public class MachinesController : Controller
    {
        private arboldecisionesContext db = new arboldecisionesContext();

        // GET: Machines
        public ActionResult Index()
        {
            var machineList = new List<Machine>();
            foreach (var item in db.Machines)
            {
                var machine = new Machine();
                machine.MachineID = item.MachineID;
                machine.Name = item.Name;
                machine.Active = item.Active;
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(item.UserID);
                machine.UserID = user.Name + ' ' + user.LastName;
                machine.UpdateDate = item.UpdateDate;

                machineList.Add(machine);
            }


            return View(machineList);
        }

        // GET: Machines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machines.Find(id);

            var machineU = new Machine();
            machineU.MachineID = machine.MachineID;
            machineU.Name = machine.Name;
            machineU.Active = machine.Active;
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(machine.UserID);
            machineU.UserID = user.Name + ' ' + user.LastName;
            machineU.UpdateDate = machine.UpdateDate;

            if (machineU == null)
            {
                return HttpNotFound();
            }
            return View(machineU);
        }

        // GET: Machines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Machines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MachineID,Name,Active")] Machine machine)
        {
            if (ModelState.IsValid)
            {
                //Se obtiene el id del usuario que esta en sesion
                machine.UserID = User.Identity.GetUserId();
                //Se obtiene la fecha actual
                machine.UpdateDate = DateTime.Now;

                db.Machines.Add(machine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(machine);
        }

        // GET: Machines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machines.Find(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // POST: Machines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MachineID,Name,Active")] Machine machine)
        {
            if (ModelState.IsValid)
            {
                machine.UserID = User.Identity.GetUserId();
                machine.UpdateDate = DateTime.Now;
                db.Entry(machine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(machine);
        }

        // GET: Machines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machines.Find(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // POST: Machines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Machine machine = db.Machines.Find(id);
            db.Machines.Remove(machine);
            db.SaveChanges();
            return RedirectToAction("Index");
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
