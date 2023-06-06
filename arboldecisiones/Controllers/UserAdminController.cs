using arboldecisiones.Models;
using arboldecisiones.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace arboldecisiones.Controllers
{
    [Authorize]
    public class UsersAdminController : Controller
    {
        private arboldecisionesContext db = new arboldecisionesContext();
        public UsersAdminController()
        {
        }

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Users/
        [HttpGet]
        public async Task<ActionResult> Index()
        {

            var user = await UserManager.Users.ToListAsync();

            var UserViewModel = new List<UserViewModel>();
            foreach (var item in user)
            {
                var useViewModel = new UserViewModel();
                useViewModel.Id = item.Id;
                useViewModel.Identity = item.Identity;
                useViewModel.Name = item.Name;
                useViewModel.LastName = item.LastName;
                useViewModel.UserName = item.UserName;
                var maquina = db.Machines.Where(p => p.MachineID == item.MachineID).FirstOrDefault();
                useViewModel.MachineID = maquina.Name;
                useViewModel.Email = item.Email;
                useViewModel.Active = item.Active;
                useViewModel.UpdateDate = item.UpdateDate;
                UserViewModel.Add(useViewModel);
            }
            return View(UserViewModel);
        }

        //
        // GET: /Users/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }

        //
        // GET: /Users/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {

            var maquina = db.Machines.ToList();
            maquina = maquina.OrderBy(c => c.Name).ToList();
            ViewBag.MachineID = new SelectList(maquina, "MachineID", "Name");

            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var maquina = db.Machines.ToList();
                maquina = maquina.OrderBy(c => c.Name).ToList();
                ViewBag.MachineID = new SelectList(maquina, "MachineID", "Name");
                ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");

                if (selectedRoles != null)
                {

                    var user = new ApplicationUser { UserName = userViewModel.UserName, Email = userViewModel.Email, Name = userViewModel.Name, LastName = userViewModel.LastName, Identity = userViewModel.Identity, Active = userViewModel.Active, UpdateDate = DateTime.Now, MachineID = userViewModel.MachineID };
                    var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                    //Add User to the selected Roles 
                    if (adminresult.Succeeded)
                    {
                        if (selectedRoles != null)
                        {
                            var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                            if (!result.Succeeded)
                            {
                                ModelState.AddModelError("", result.Errors.First());
                                ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                                return View();
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", adminresult.Errors.First());
                        ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                        return View();

                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Mensaje = "Debe seleccionar al menos un Rol";
                }


            }
            return View();
        }

        //
        // GET: /Users/Edit/1
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var maquina = db.Machines.ToList();
            maquina = maquina.OrderBy(c => c.Name).ToList();
            ViewBag.MachineID = new SelectList(maquina, "MachineID", "Name", user.MachineID);
            var userRoles = await UserManager.GetRolesAsync(user.Id);

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                Identity = user.Identity,
                Name = user.Name,
                LastName = user.LastName,
                UserName = user.UserName,
                MachineID = user.MachineID,
                Email = user.Email,
                Active = user.Active,
                UpdateDate = DateTime.Now,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Id,MachineID,Identity,Name,LastName,UserName,Password,selectedRole,Active")] EditUserViewModel editUser, params string[] selectedRole)
        {
            var maquina = db.Machines.ToList();
            maquina = maquina.OrderBy(c => c.Name).ToList();
            ViewBag.MachineID = new SelectList(maquina, "MachineID", "Name", editUser.MachineID);

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.Identity = editUser.Identity;
                user.Name = editUser.Name;
                user.LastName = editUser.LastName;
                user.UserName = editUser.UserName;
                user.MachineID = editUser.MachineID;
                user.Email = editUser.Email;
                user.Active = editUser.Active;
                user.UpdateDate = DateTime.Now;
                var userRoles = await UserManager.GetRolesAsync(user.Id);

                if (selectedRole == null)
                {
                    selectedRole = new String[1] { userRoles[0] };
                }
                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }

                UserManager<IdentityUser> userManager =
    new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                userManager.RemovePassword(user.Id);
                userManager.AddPassword(user.Id, editUser.Password);
                return RedirectToAction("Index");
            }
            var userRole = await UserManager.GetRolesAsync(editUser.Id);
            ModelState.AddModelError("", "Algo falló.");
            return View(new EditUserViewModel()
            {
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRole.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // GET: /Users/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
