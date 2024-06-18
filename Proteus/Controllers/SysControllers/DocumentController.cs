using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Proteus.DAL;
using Proteus.Models;
using Proteus.Notification;
using Proteus.Services;
using Proteus.Filters;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;


namespace Proteus.Controllers.DataControllers
{
    [ErrorHandlerFilter]
    public class DocumentController : Controller
    {
        private USER_SCHOOLS loggedSchool;
        private readonly ProteusDBEntities db;

        private readonly IStudentInfoService studentInfoService;
        private readonly IAtomikoDeltioService atomikoDeltioService;
        private readonly IFoitisiDeltioService foitisiDeltioService;
        private readonly ITableAtomikoDeltioService tableAtomikoDeltioService;
        private readonly ITableDeltioFoitisiService tableDeltioFoitisiService;

        public DocumentController(ProteusDBEntities entities, IStudentInfoService studentInfoService,
            IAtomikoDeltioService atomikoDeltioService, IFoitisiDeltioService foitisiDeltioService,
            ITableAtomikoDeltioService tableAtomikoDeltioService, ITableDeltioFoitisiService tableDeltioFoitisiService)
        {
            db = entities;

            this.studentInfoService = studentInfoService;
            this.atomikoDeltioService = atomikoDeltioService;
            this.foitisiDeltioService = foitisiDeltioService;
            this.tableAtomikoDeltioService = tableAtomikoDeltioService;
            this.tableDeltioFoitisiService = tableDeltioFoitisiService;
        }


        #region --- ΠΙΝΑΚΕΣ ΑΤΟΜΙΚΩΝ ΔΕΛΤΙΩΝ (ΝΕΟ - 13/09/2019) ---

        public ActionResult StudentInfo_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            IEnumerable<StudentInfoViewModel> data = studentInfoService.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AtomikaDeltiaData(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)loggedSchool.USER_SCHOOLID;

            if (!IekEidikotitesExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν ειδικότητες που υλοποιεί το ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        #region DOCUMENT GRID CRUD FUNCTIONS

        public ActionResult StudentAdk_Read([DataSourceRequest] DataSourceRequest request, int studentId)
        {
            IEnumerable<StudentAtomikoDeltioViewModel> data = atomikoDeltioService.Read(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentAdk_Create([DataSourceRequest] DataSourceRequest request, StudentAtomikoDeltioViewModel data, int studentId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentAtomikoDeltioViewModel newdata = new StudentAtomikoDeltioViewModel();
            if (!(studentId > 0)) ModelState.AddModelError("", "Για να γίνει η καταχώρηση πρέπει πρώτα να επιλέξετε σπουδαστή.");

            if (data != null && ModelState.IsValid)
            {
                atomikoDeltioService.Create(data, studentId, schoolId);
                newdata = atomikoDeltioService.Refresh(data.ΑΔΚ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentAdk_Update([DataSourceRequest] DataSourceRequest request, StudentAtomikoDeltioViewModel data, int studentId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentAtomikoDeltioViewModel newdata = new StudentAtomikoDeltioViewModel();
            if (!(studentId > 0)) ModelState.AddModelError("", "Για να γίνει η καταχώρηση πρέπει πρώτα να επιλέξετε σπουδαστή.");

            if (data != null && ModelState.IsValid)
            {
                atomikoDeltioService.Update(data, studentId, schoolId);
                newdata = atomikoDeltioService.Refresh(data.ΑΔΚ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentAdk_Destroy([DataSourceRequest] DataSourceRequest request, StudentAtomikoDeltioViewModel data)
        {
            if (data != null)
            {
                atomikoDeltioService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult AtomikoDeltioPrint(int recordId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            var adk = (from d in db.ΜΑΘΗΤΕΣ_ΑΤΟΜΙΚΑΔΕΛΤΙΑ
                       where d.ΑΔΚ_ΚΩΔ == recordId
                       select new StudentAtomikoDeltioViewModel
                       {
                           ΑΔΚ_ΚΩΔ = d.ΑΔΚ_ΚΩΔ,
                           ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                           ΙΕΚ = d.ΙΕΚ,
                       }).FirstOrDefault();

            return View(adk);
        }

        #region ΛΕΙΤΟΥΡΓΙΕΣ ΔΗΜΙΟΥΡΓΙΑΣ-ΕΝΗΜΕΡΩΣΗΣ-ΠΡΟΒΟΛΗΣ ΠΙΝΑΚΑ ΑΤΟΜΙΚΟΥ ΔΕΛΤΙΟΥ

        public ActionResult CreateAtomikoDeltio(int studentId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            string msg = tableAtomikoDeltioService.Create(studentId, schoolId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateAtomikoDeltio(int studentId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            string msg = tableAtomikoDeltioService.Update(studentId, schoolId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DestroyAtomikoDeltio(int studentId)
        {
            string msg = tableAtomikoDeltioService.Destroy(studentId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewAtomikoDeltio(int studentId, string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            adkStudentDataViewModel data = atomikoDeltioService.GetStudentData(studentId);

            return View(data);
        }

        #endregion

        public ActionResult GradesApousies_Read([DataSourceRequest] DataSourceRequest request, int studentId, int termId)
        {
            List<AtomikoDeltioViewModel> data = atomikoDeltioService.GetGradesApousies(studentId, termId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PraktikiData_Read([DataSourceRequest] DataSourceRequest request, int studentId)
        {
            List<adkStudentPraktikiViewModel> data = atomikoDeltioService.GetPraktikiData(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region --- ΠΙΝΑΚΕΣ ΔΕΛΤΙΩΝ ΦΟΙΤΗΣΗΣ (ΝΕΟ - 10/10/2019) ---

        public ActionResult FoitisiDeltiaData(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = loggedSchool.USER_SCHOOLID ?? 0;

            if (!IekEidikotitesExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν ειδικότητες που υλοποιεί το ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        #region DOCUMENT GRID CRUD FUNCTIONS

        public ActionResult StudentFdk_Read([DataSourceRequest] DataSourceRequest request, int studentId)
        {
            IEnumerable<StudentFoitisiDeltioViewModel> data = foitisiDeltioService.Read(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentFdk_Create([DataSourceRequest] DataSourceRequest request, StudentFoitisiDeltioViewModel data, int studentId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentFoitisiDeltioViewModel newdata = new StudentFoitisiDeltioViewModel();
            if (!(studentId > 0)) ModelState.AddModelError("", "Για να γίνει η καταχώρηση πρέπει πρώτα να επιλέξετε σπουδαστή.");

            if (data != null && ModelState.IsValid)
            {
                foitisiDeltioService.Create(data, studentId, schoolId);
                newdata = foitisiDeltioService.Refresh(data.ΑΔΚ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentFdk_Update([DataSourceRequest] DataSourceRequest request, StudentFoitisiDeltioViewModel data, int studentId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentFoitisiDeltioViewModel newdata = new StudentFoitisiDeltioViewModel();
            if (!(studentId > 0)) ModelState.AddModelError("", "Για να γίνει η καταχώρηση πρέπει πρώτα να επιλέξετε σπουδαστή.");

            if (data != null && ModelState.IsValid)
            {
                foitisiDeltioService.Update(data, studentId, schoolId);
                newdata = foitisiDeltioService.Refresh(data.ΑΔΚ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentFdk_Destroy([DataSourceRequest] DataSourceRequest request, StudentFoitisiDeltioViewModel data)
        {
            if (data != null)
            {
                foitisiDeltioService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult FoitisiDeltioPrint(int recordId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            var data = (from d in db.ΜΑΘΗΤΕΣ_ΦΟΙΤΗΣΗΔΕΛΤΙΑ
                        where d.ΑΔΚ_ΚΩΔ == recordId
                        select new StudentFoitisiDeltioViewModel
                        {
                            ΑΔΚ_ΚΩΔ = d.ΑΔΚ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                        }).FirstOrDefault();

            return View(data);
        }


        #region ΛΕΙΤΟΥΡΓΙΕΣ ΔΗΜΙΟΥΡΓΙΑΣ-ΕΝΗΜΕΡΩΣΗΣ-ΠΡΟΒΟΛΗΣ ΠΙΝΑΚΑ ΔΕΛΤΙΟΥ ΦΟΙΤΗΣΗΣ

        public ActionResult CreateFoitisiDeltio(int studentId)
        {
            int schoolId = GetLoginSchool().USER_SCHOOLID ?? 0;

            string msg = tableDeltioFoitisiService.Create(studentId, schoolId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateFoitisiDeltio(int studentId)
        {
            int schoolId = GetLoginSchool().USER_SCHOOLID ?? 0;

            string msg = tableDeltioFoitisiService.Update(studentId, schoolId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DestroyFoitisiDeltio(int studentId)
        {
            string msg = tableDeltioFoitisiService.Destroy(studentId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewFoitisiDeltio(int studentId, string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            adkStudentDataViewModel vmData = atomikoDeltioService.GetStudentData(studentId);

            return View(vmData);
        }

        #endregion

        public ActionResult FoitisiGradesApousies_Read([DataSourceRequest] DataSourceRequest request, int studentId, int termId)
        {
            List<FoitisiDeltioViewModel> data = foitisiDeltioService.GetGradesApousies(studentId, termId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region VALIDATIONS (required in case everything is empty)

        public bool StudentsExist(int schoolId)
        {
            int students = (from d in db.ΜΑΘΗΤΕΣ where d.ΙΕΚ == schoolId select d).Count();

            if (students > 0) return true;
            else return false;
        }

        public bool IekEidikotitesExist(int schoolId)
        {
            int eidikotites = (from d in db.IEK_EIDIKOTITES where d.IEK_ID == schoolId select d).Count();

            if (eidikotites > 0) return true;
            else return false;
        }

        #endregion


        public USER_SCHOOLS GetLoginSchool()
        {
            loggedSchool = db.USER_SCHOOLS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            int SchoolID = loggedSchool.USER_SCHOOLID ?? 0;
            var _school = (from s in db.sqlUSER_SCHOOL
                           where s.USER_SCHOOLID == SchoolID
                           select new { s.SCHOOL_NAME }).FirstOrDefault();

            ViewBag.loggedUser = _school.SCHOOL_NAME;
            return loggedSchool;
        }

        public ActionResult PageInProgress(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        public ActionResult ErrorData(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

    }
}