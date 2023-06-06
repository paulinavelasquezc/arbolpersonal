using arboldecisiones.Models;
using arboldecisiones.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace arboldecisiones.Controllers
{
    public class OperatorReportsController : Controller
    {
        private arboldecisionesContext db;

        public OperatorReportsController()
        {
            db = new arboldecisionesContext();
        }

        public string NameUser(string idUser)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(idUser);
            return user.Name + ' ' + user.LastName;
        }
        public ActionResult GeneralReport()
        {
            var ReportGeneral = new List<ReportGeneral>();
            var QueryDet = (from treeDet in db.TreeDecisionDet
                            join treeConfigDet in db.TreeConfigurationDet on treeDet.TreeConfigurationDetID equals treeConfigDet.TreeConfigurationDetID
                            select new
                            {
                                TreeDecisionDetID = treeDet.TreeDecisionDetID,
                                NameConfiguration = treeConfigDet.TreeConfiguration.Defect.Name,
                                //NameLocation = treeConfigDet.TreeConfiguration.Location.Name,
                                idUser = treeConfigDet.IdUser,
                                DefectUpdateDate = treeConfigDet.UpdateDate,
                                NameDecision = treeDet.TreeDecision.Name,
                                DecisionUpdateDate = treeDet.TreeDecision.UpdateDate,
                                Solution = treeDet.Solution
                            }).ToList();

            foreach (var item in QueryDet)
            {
                var report = new ReportGeneral();

                report.TreeDecisionDetID = item.TreeDecisionDetID;
                report.NameConfiguration = item.NameConfiguration;
                //report.NameLocation = item.NameLocation;
                report.NameUser = NameUser(item.idUser);
                report.DefectUpdateDate = item.DefectUpdateDate.ToString();
                report.NameDecision = item.NameDecision;
                report.DecisionUpdateDate = item.DecisionUpdateDate.ToString();
                report.Solution = item.Solution;
                ApplicationUser usu = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(item.idUser);
                var Machine = db.Machines.Where(c => c.MachineID == usu.MachineID).FirstOrDefault();
                report.NameMachine = Machine.Name;
                ReportGeneral.Add(report);
            }

            return View(ReportGeneral);
        }

        public ActionResult DefectsConsulted()
        {

            var Query = (from p in db.TreeConfigurationDet.GroupBy(p => p.TreeConfigurationID)
                         select new
                         {
                             Cantidad = p.Count(),
                             TreeConfigDetID = p.FirstOrDefault().TreeConfigurationDetID,
                             NameConfiguration = p.FirstOrDefault().TreeConfiguration.Defect.Name,
                             //NameLocation = p.FirstOrDefault().TreeConfiguration.Location.Name,
                             DefectUpdateDate = p.Max(t => t.UpdateDate)
                         }).ToList();

            var CoutDefect = (from treeDet in Query
                              select new DefectsConsulted()
                              {
                                  TreeConfigurationDetID = treeDet.TreeConfigDetID,
                                  NameConfiguration = treeDet.NameConfiguration,
                                  //NameLocation = treeDet.NameLocation,
                                  DefectUpdateDate = treeDet.DefectUpdateDate.ToString(),
                                  Cantidad = treeDet.Cantidad
                              }).ToList();

            return View(CoutDefect);
        }

        public ActionResult CorrectSolutions()
        {
            var Query = (from p in db.TreeDecisionDet.Where(p => p.Solution != false).GroupBy(p => p.TreeDecisionID)
                         select new
                         {
                             Cantidad = p.Count(),
                             DecisionID = p.FirstOrDefault().TreeDecisionID,
                             NameDefect = p.FirstOrDefault().TreeConfigurationDet.TreeConfiguration.Defect.Name,
                             NameDecision = p.FirstOrDefault().TreeDecision.Name,
                             DescriptionDecision = p.FirstOrDefault().TreeDecision.Description,
                         }).ToList();

            var CoutSolution = (from treeDet in Query
                                select new CorrectSolutions()
                                {
                                    DecisionID = treeDet.DecisionID,
                                    NameDefect = treeDet.NameDefect,
                                    NameDecision = treeDet.NameDecision,
                                    DescriptionDecision = treeDet.DescriptionDecision,
                                    Cantidad = treeDet.Cantidad
                                }).ToList();


            return View(CoutSolution);
        }

        public ActionResult UserSystem()
        {
            var UserSystem = new List<UserSystem>();
            var Query = (from p in db.TreeConfigurationDet.GroupBy(p => p.IdUser)
                         select new
                         {
                             Cantidad = p.Count(),
                             IdUser = p.FirstOrDefault().IdUser.ToString(),
                             UpdateDate = p.Max(t => t.UpdateDate)
                         }).ToList();


            foreach (var item in Query)
            {
                var userReport = new UserSystem();
                ApplicationUser usu = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(item.IdUser);

                userReport.NameUser = usu.Name;
                userReport.LastNameUser = usu.LastName;
                userReport.UserName = usu.UserName;
                userReport.UpdateDate = item.UpdateDate.ToString();
                userReport.Cantidad = item.Cantidad;
                var Machine = db.Machines.Where(c => c.MachineID == usu.MachineID).FirstOrDefault();
                userReport.NameMachine = Machine.Name;
                UserSystem.Add(userReport);
            }

            return View(UserSystem);
        }

        [HttpPost]
        public JsonResult DetailsCorrectSolutions(int _decisionID)
        {
            bool Status = false;
            var UserSystem = new List<UserSystem>();
            try
            {

                var Query = (from p in db.TreeDecisionDet.Where(p => p.TreeDecisionID == _decisionID && p.Solution != false)
                             select new
                             {
                                 userID = p.TreeConfigurationDet.IdUser,
                                 admissionDate = p.UpdateDate
                             }).ToList();


                //var QueryUser = (from p in Query.GroupBy(p => p.userID)
                //                 select new
                //                 {
                //                     IdUser = p.FirstOrDefault().userID
                //                 }).ToList();

                foreach (var item in Query)
                {
                    var userReport = new UserSystem();
                    ApplicationUser usu = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(item.userID);

                    userReport.NameUser = usu.Name;
                    userReport.LastNameUser = usu.LastName;
                    userReport.UserName = usu.UserName;
                    userReport.UpdateDate = item.admissionDate.ToString();

                    var Machine = db.Machines.Where(c => c.MachineID == usu.MachineID).FirstOrDefault();
                    userReport.NameMachine = Machine.Name;

                    UserSystem.Add(userReport);
                }

                Status = true;
            }
            catch (Exception e)
            {

                Status = false;
                throw e;
            }
            return new JsonResult { Data = new { status = Status, userSystem = UserSystem }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}