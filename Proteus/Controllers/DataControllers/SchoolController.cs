using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using Proteus.Notification;
using Proteus.Services;
using Proteus.Filters;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;


namespace Proteus.Controllers.DataControllers
{
    [ErrorHandlerFilter]
    public class SchoolController : ControllerUnit
    {
        private readonly ProteusDBEntities db;

        private readonly IStudentService studentService;
        private readonly IEgrafesService egrafesService;
        private readonly IStudentInfoService studentInfoService;
        private readonly IAitiseisService aitiseisService;
        private readonly IBebeosiService bebeosiService;
        private readonly IApallagiService apallagiService;

        private readonly ITeacherService teacherService;
        private readonly ITeacherPeriodService teacherPeriodService;
        private readonly ITeacherInfoService teacherInfoService;
        private readonly IAnatheseisService anatheseisService;
        private readonly ITeacherAitisiService teacherAitisiService;
        private readonly ITeacherAnalipsiService teacherAnalipsiService;
        private readonly IApoxorisiService apoxorisiService;

        public SchoolController(ProteusDBEntities entities, IStudentService studentService,
            IEgrafesService egrafesService, IStudentInfoService studentInfoService, IAitiseisService aitiseisService,
            IBebeosiService bebeosiService, IApallagiService apallagiService, ITeacherService teacherService,
            ITeacherPeriodService teacherPeriodService, ITeacherInfoService teacherInfoService,
            IAnatheseisService anatheseisService, ITeacherAitisiService teacherAitisiService,
            ITeacherAnalipsiService teacherAnalipsiService, IApoxorisiService apoxorisiService) : base(entities)
        {
            db = entities;

            this.studentService = studentService;
            this.egrafesService = egrafesService;
            this.studentInfoService = studentInfoService;
            this.aitiseisService = aitiseisService;
            this.bebeosiService = bebeosiService;
            this.apallagiService = apallagiService;

            this.teacherService = teacherService;
            this.teacherPeriodService = teacherPeriodService;
            this.teacherInfoService = teacherInfoService;
            this.anatheseisService = anatheseisService;
            this.teacherAitisiService = teacherAitisiService;
            this.teacherAnalipsiService = teacherAnalipsiService;
            this.apoxorisiService = apoxorisiService;
        }

        public ActionResult Index(string notify = null)
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
            
            if (GetSchoolEnableStatus() == false)
            {
                string msg = GetSchoolEnableMessage();
                return RedirectToAction("Index", "Home", new { notify = msg });
            }

            return View();
        }

        public ActionResult EidikotitaStudentRead(int eidikotita)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = db.qrySTUDENT_GLOBAL_SELECTOR
                .Where(f => f.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ == eidikotita && f.ΙΕΚ == schoolId)
                .OrderBy(d => d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #region ΣΠΟΥΔΑΣΤΕΣ

        #region ΣΠΟΥΔΑΣΤΕΣ ΚΑΙ ΕΓΓΡΑΦΕΣ

        public ActionResult StudentData(string notify = null)
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

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            if (!IekEidikotitesExist(schoolId) || !IekTmimataExist(schoolId))
            {
                string msg = "Για να καταχωρηθούν σπουδαστές πρέπει πρώτα να ορίσετε τις ειδικότητες και τα τμήματα του ΙΕΚ στις Ρυθμίσεις.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            PopulateIekEidikotites(schoolId);
            PopulateIekTmimata(schoolId);
            PopulateRegisterTypes();
            PopulateFoitisi();
            return View();
        }

        public ActionResult Student_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            IEnumerable<StudentGridViewModel> data = studentService.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Student_Create([DataSourceRequest] DataSourceRequest request, StudentGridViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new StudentGridViewModel();

            if (!Kerberos.ValidatePrimaryKeyStudent(data.ΑΜΚ, schoolId)) 
                ModelState.AddModelError("", "Ο αριθμός μητρώου που δόθηκε είναι ήδη καταχωρημένος για το σχολείο αυτό.");

            if (!Common.CheckAFM(data.ΑΦΜ))
                ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο.");

            if (ModelState.IsValid)
            {
                studentService.Create(data, schoolId);
                newdata = studentService.Refresh(data.STUDENT_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Student_Update([DataSourceRequest] DataSourceRequest request, StudentGridViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new StudentGridViewModel();

            if (ModelState.IsValid)
            {
                studentService.Update(data, schoolId);
                newdata = studentService.Refresh(data.STUDENT_ID);
            }

            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Student_Destroy([DataSourceRequest] DataSourceRequest request, StudentGridViewModel data)
        {
            if (data != null)
            {
                if (!Kerberos.CanDeleteStudent(data.STUDENT_ID)) 
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί ο σπουδαστής διότι υπάρχουν εγγραφές του σε τμήματα.");

                if (ModelState.IsValid)
                {
                    studentService.Destroy(data);
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        #region ΕΓΓΡΑΦΕΣ ΣΕ ΤΜΗΜΑΤΑ

        public ActionResult EidikotitaTmima_Read(int? eidikotitaId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            var data = new List<TmimaViewModel>();

            if (eidikotitaId > 0)
            {
                data = (from d in db.ΤΜΗΜΑ
                        where d.ΙΕΚ == schoolId && d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ == eidikotitaId
                        select new TmimaViewModel
                        {
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ
                        }).ToList(); 
            }
            else
            {
                data = (from d in db.ΤΜΗΜΑ
                        where d.ΙΕΚ == schoolId
                        select new TmimaViewModel
                        {
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ
                        }).ToList();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Egrafes_Read([DataSourceRequest] DataSourceRequest request, int studentId = 0)
        {
            IEnumerable<StudentEgrafesViewModel> data = egrafesService.Read(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Egrafes_Create([DataSourceRequest] DataSourceRequest request, StudentEgrafesViewModel data, int studentId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentEgrafesViewModel newdata = new StudentEgrafesViewModel();

            if (studentId > 0)
            {
                var existingData = db.ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ.Where(s => s.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && s.ΚΩΔ_ΤΜΗΜΑ == data.ΚΩΔ_ΤΜΗΜΑ).Count();
                if (existingData > 0) 
                    ModelState.AddModelError("", "Υπάρχει ήδη καταχώρηση του σπουδαστή στο τμήμα αυτό.");

                if (data.ΦΟΙΤΗΣΗ != null)
                {
                    if (!Common.ValidFoitisiPraktiki((int)data.ΚΩΔ_ΤΜΗΜΑ, (int)data.ΦΟΙΤΗΣΗ)) 
                        ModelState.AddModelError("", "Ο χαρακτηρισμός 'Περάτωση ή διακοπή πρακτικής' ισχύει μόνο για τμήματα Ε' εξαμήνου.");
                }
                if (data != null && ModelState.IsValid)
                {
                    egrafesService.Create(data, studentId, schoolId);
                    newdata = egrafesService.Refresh(data.ΜΕ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σπουδαστή για εγγραφή του σε τμήμα.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Egrafes_Update([DataSourceRequest] DataSourceRequest request, StudentEgrafesViewModel data, int studentId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentEgrafesViewModel newdata = new StudentEgrafesViewModel();
            if (studentId > 0)
            {
                if (data.ΦΟΙΤΗΣΗ != null)
                {
                    if (!Common.ValidFoitisiPraktiki((int)data.ΚΩΔ_ΤΜΗΜΑ, (int)data.ΦΟΙΤΗΣΗ))
                        ModelState.AddModelError("", "Οι χαρακτηρισμοί 'Ασκείται κανονικά, περάτωση ή διακοπή πρακτικής' ισχύουν μόνο για τμήματα Ε' εξαμ.");
                }
                if (data != null && ModelState.IsValid)
                {
                    egrafesService.Update(data, studentId, schoolId);
                    newdata = egrafesService.Refresh(data.ΜΕ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να γίνει επιλογή σπουδαστή. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Egrafes_Destroy([DataSourceRequest] DataSourceRequest request, StudentEgrafesViewModel data)
        {
            if (data != null)
            {
                egrafesService.Destroy(data);
            }

            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion REGISTRATIONS


        #region STUDENT DATA FORM

        public ActionResult StudentEdit(int studentId, string notify = null)
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
            if (!(studentId > 0))
            {
                string msg = "Ο κωδικός σπουδαστή δεν είναι έγκυρος. Η εγγραφή πρέπει να έχει αποθηκευτεί πρώτα.";
                return RedirectToAction("ErrorData", "School", new { notify = msg });
            }

            StudentViewModel student = studentService.GetRecord(studentId);
            if (student == null)
            {
                string msg = "Παρουσιάστηκε πρόβλημα εύρεσης του σπουδαστή.";
                return RedirectToAction("ErrorData", "School", new { notify = msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Info, notify);

            return View(student);
        }

        [HttpPost]
        public ActionResult StudentEdit(int studentId, StudentViewModel data)
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

            string ErrorMsg = Common.ValidateStudentFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                studentService.UpdateRecord(data, studentId, schoolId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                StudentViewModel newdata = studentService.GetRecord(studentId);
                return View(newdata);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public ActionResult CopyCandidateData(int studentID, string afm)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            string msg = "Η αντιγραφή των στοιχείων του σπουδαστή από τους υποψήφιους ολοκληρώθηκε";

            ΜΑΘΗΤΕΣ entity = db.ΜΑΘΗΤΕΣ.Find(studentID);

            var source = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ where d.ΙΕΚ1 == schoolId && d.ΑΦΜ == afm select d).FirstOrDefault();
            if (source != null)
            {
                entity.ΑΔΤ = source.ΑΔΤ;
                entity.ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ;
                entity.ΜΗΤΡΩΝΥΜΟ = source.ΜΗΤΡΩΝΥΜΟ;
                entity.ΦΥΛΟ = source.ΦΥΛΟ;
                entity.ΑΜΚΑ = source.ΑΜΚΑ;
                entity.ΔΗΜΟΤΗΣ = source.ΔΗΜΟΣ_ΕΓΓΡΑΦΗΣ;
                entity.ΜΗΤΡΩΟ_ΑΡΡΕΝΩΝ_ΑΡ = source.ΑΜ_ΑΡΡΕΝΩΝ;
                entity.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ = source.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ;
                entity.ΔΙΕΥΘΥΝΣΗ = source.ΚΑΤΟΙΚΙΑ_ΔΝΣΗ + ", " + source.ΚΑΤΟΙΚΙΑ_ΠΟΛΗ;
                entity.ΤΗΛΕΦΩΝΑ = source.ΤΗΛΕΦΩΝΟ;
                entity.EMAIL = source.EMAIL;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                msg = "Δεν βρέθηκαν στοιχεία υποψήφιου για το συγκεκριμένο ΑΦΜ.";
            }
            return RedirectToAction("StudentEdit", "School", new { studentId = studentID, notify = msg });
        }

        #endregion

        #endregion


        #region ΜΗΤΡΩΟ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult StudentInfoList()
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

            IEnumerable<StudentInfoViewModel> data = studentInfoService.Read(schoolId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένοι σπουδαστές για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            StudentInfoViewModel student = data.First();

            return View(student);
        }

        public ActionResult StudentInfo_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            IEnumerable<StudentInfoViewModel> data = studentInfoService.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EgrafesInfo_Read([DataSourceRequest] DataSourceRequest request, int studentId)
        {
            IEnumerable<EgrafesInfoViewModel> data = studentInfoService.GetEgrafes(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetStudentRecord(int studentId)
        {
            StudentInfoViewModel student = studentInfoService.GetRecord(studentId);

            return PartialView("StudentInfoPartial", student);
        }

        #endregion


        #region ΑΙΤΗΣΕΙΣ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult StudentAitiseis(string notify = null)
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

        #region GRID CRUD FUNCTIONS

        public ActionResult StudentAitisi_Read([DataSourceRequest] DataSourceRequest request, int studentId)
        {
            IEnumerable<StudentAitisiViewModel> data = aitiseisService.Read(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentAitisi_Create([DataSourceRequest] DataSourceRequest request, StudentAitisiViewModel data, int studentId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentAitisiViewModel newdata = new StudentAitisiViewModel();

            if (!(studentId > 0)) ModelState.AddModelError("", "Για να γίνει η καταχώρηση πρέπει πρώτα να επιλέξετε σπουδαστή.");

            if (data != null && ModelState.IsValid)
            {
                aitiseisService.Create(data, studentId, schoolId);
                newdata = aitiseisService.Refresh(data.ΑΙΤΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentAitisi_Update([DataSourceRequest] DataSourceRequest request, StudentAitisiViewModel data, int studentId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentAitisiViewModel newdata = new StudentAitisiViewModel();

            if (!(studentId > 0)) ModelState.AddModelError("", "Για να γίνει η καταχώρηση πρέπει πρώτα να επιλέξετε σπουδαστή.");

            if (data != null && ModelState.IsValid)
            {
                aitiseisService.Update(data, studentId, schoolId);
                newdata = aitiseisService.Refresh(data.ΑΙΤΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentAitisi_Destroy([DataSourceRequest] DataSourceRequest request, StudentAitisiViewModel data)
        {
            if (data != null)
            {
                aitiseisService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΑΙΤΗΣΗ DATA FORM

        public ActionResult StudentAitisiEdit(int aitisiId)
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

            if (!(aitisiId > 0))
            {
                string msg = "Ο κωδικός αίτησης δεν είναι έγκυρος. Η εγγραφή πρέπει να έχει αποθηκευτεί πρώτα.";
                return RedirectToAction("ErrorData", "School", new { notify = msg });
            }

            StudentAitisiViewModel aitisi = aitiseisService.GetRecord(aitisiId);
            int studentId = (int)aitisi.ΜΑΘΗΤΗΣ_ΚΩΔ;

            StudentInfoViewModel SelectedStudent = studentInfoService.GetRecord(studentId);
            if (SelectedStudent == null)
            {
                string notify = "Παρουσιάστηκε πρόβλημα εύρεσης του σπουδαστή.";
                return RedirectToAction("ErrorData", "School", new { notify });
            }
            else
            {
                ViewBag.StudentData = SelectedStudent;
            }
            return View(aitisi);
        }

        [HttpPost]
        public ActionResult StudentAitisiEdit(int aitisiId, StudentAitisiViewModel data)
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

            StudentAitisiViewModel aitisi = aitiseisService.GetRecord(aitisiId);

            int studentId = (int)aitisi.ΜΑΘΗΤΗΣ_ΚΩΔ;
            StudentInfoViewModel SelectedStudent = studentInfoService.GetRecord(studentId);
            if (SelectedStudent == null)
            {
                string notify = "Παρουσιάστηκε πρόβλημα εύρεσης του σπουδαστή.";
                return RedirectToAction("ErrorData", "School", new { notify });
            }
            else
            {
                ViewBag.StudentData = SelectedStudent;
            }

            if (data != null)
            {
                ΜΑΘΗΤΕΣ_ΑΙΤΗΣΕΙΣ entity = db.ΜΑΘΗΤΕΣ_ΑΙΤΗΣΕΙΣ.Find(aitisiId);

                entity.ΚΕΙΜΕΝΟ = data.ΚΕΙΜΕΝΟ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                StudentAitisiViewModel newdata = aitiseisService.GetRecord(aitisiId);
                return View(newdata);
            }
            else
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
                return View(data);
            }
        }

        #endregion


        public ActionResult StudentAitisiPrint(int aitisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }

            int schoolId = GetLoginSchool().USER_SCHOOLID ?? 0;

            var aitisi = (from d in db.ΜΑΘΗΤΕΣ_ΑΙΤΗΣΕΙΣ
                          where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                          select new StudentAitisiViewModel
                          {
                              ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                              ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                              ΙΕΚ = d.ΙΕΚ
                          }).FirstOrDefault();

            return View(aitisi);
        }

        #endregion


        #region ΒΕΒΑΙΩΣΕΙΣ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult StudentBebeoseis(string notify = null)
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

        #region GRID CRUD FUNCTIONS

        public ActionResult StudentBebeosi_Read([DataSourceRequest] DataSourceRequest request, int studentId)
        {
            IEnumerable<StudentBebeoseisViewModel> data = bebeosiService.Read(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentBebeosi_Create([DataSourceRequest] DataSourceRequest request, StudentBebeoseisViewModel data, int studentId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentBebeoseisViewModel newdata = new StudentBebeoseisViewModel();
            if (!(studentId > 0)) ModelState.AddModelError("", "Για να γίνει η καταχώρηση πρέπει πρώτα να επιλέξετε σπουδαστή.");

            if (data != null && ModelState.IsValid)
            {
                bebeosiService.Create(data, studentId, schoolId);
                newdata = bebeosiService.Refresh(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentBebeosi_Update([DataSourceRequest] DataSourceRequest request, StudentBebeoseisViewModel data, int studentId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentBebeoseisViewModel newdata = new StudentBebeoseisViewModel();
            if (!(studentId > 0)) ModelState.AddModelError("", "Για να γίνει η καταχώρηση πρέπει πρώτα να επιλέξετε σπουδαστή.");

            if (data != null && ModelState.IsValid)
            {
                bebeosiService.Update(data, studentId, schoolId);
                newdata = bebeosiService.Refresh(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentBebeosi_Destroy([DataSourceRequest] DataSourceRequest request, StudentBebeoseisViewModel data)
        {
            if (data != null)
            {
                bebeosiService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region BEBAIOSI DATA FORM

        public ActionResult StudentBebeosiEdit(int bebeosiId)
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

            if (!(bebeosiId > 0))
            {
                string msg = "Ο κωδικός βεβαίωσης δεν είναι έγκυρος. Η εγγραφή πρέπει να έχει αποθηκευτεί πρώτα.";
                return RedirectToAction("ErrorData", "School", new { notify = msg });
            }

            StudentBebeoseisViewModel bebeosi = bebeosiService.GetRecord(bebeosiId);
            int studentId = (int)bebeosi.ΜΑΘΗΤΗΣ_ΚΩΔ;

            StudentInfoViewModel SelectedStudent = studentInfoService.GetRecord(studentId);
            if (SelectedStudent == null)
            {
                string notify = "Παρουσιάστηκε πρόβλημα εύρεσης του σπουδαστή.";
                return RedirectToAction("ErrorData", "School", new { notify });
            }
            else
            {
                ViewBag.StudentData = SelectedStudent;
            }
            return View(bebeosi);
        }

        [HttpPost]
        public ActionResult StudentBebeosiEdit(int bebeosiId, StudentBebeoseisViewModel data)
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

            StudentBebeoseisViewModel bebeosi = bebeosiService.GetRecord(bebeosiId);
            int studentId = (int)bebeosi.ΜΑΘΗΤΗΣ_ΚΩΔ;

            StudentInfoViewModel SelectedStudent = studentInfoService.GetRecord(studentId);
            if (SelectedStudent == null)
            {
                string notify = "Παρουσιάστηκε πρόβλημα εύρεσης του σπουδαστή.";
                return RedirectToAction("ErrorData", "School", new { notify });
            }
            else
            {
                ViewBag.StudentData = SelectedStudent;
            }

            if (data != null)
            {
                ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ entity = db.ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ.Find(bebeosiId);

                entity.ΓΙΑ_ΧΡΗΣΗ = data.ΓΙΑ_ΧΡΗΣΗ;
                entity.ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ = data.ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                StudentBebeoseisViewModel newdata = bebeosiService.GetRecord(bebeosiId);
                return View(newdata);
            }
            else
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
                return View(data);
            }
        }

        #endregion BEBAIOSI DATA FORM

        #endregion

        #endregion ΣΠΟΥΔΑΣΤΕΣ


        #region ΣΠΟΥΔΑΣΤΕΣ - ΑΠΑΛΛΑΓΕΣ ΜΑΘΗΜΑΤΩΝ

        public ActionResult StudentApallages(string notify = null)
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

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            if (!IekEidikotitesExist(schoolId) || !IekTmimataExist(schoolId))
            {
                string msg = "Για να καταχωρηθούν απαλλαγές μαθημάτων πρέπει πρώτα να ορίσετε τις ειδικότητες και τα τμήματα του ΙΕΚ στις Ρυθμίσεις.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            PopulateIekEidikotites(schoolId);
            PopulateLessonTerms();
            PopulateLessonNames();

            return View();
        }

        public ActionResult adkStudentInfo_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<adkStudentInfoViewModel> data = adkStudentInfoFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<adkStudentInfoViewModel> adkStudentInfoFromDB()
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var students = (from d in db.adk_STUDENT_INFO
                            where d.ΙΕΚ == schoolId
                            orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.EIDIKOTITA_TEXT
                            select new adkStudentInfoViewModel
                            {
                                STUDENT_ID = d.STUDENT_ID,
                                ΑΜΚ = d.ΑΜΚ,
                                ΙΕΚ = d.ΙΕΚ,
                                ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                                EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                                EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT
                            }).ToList();
            return students;
        }

        #region APALLAGES GRID

        // Reader for lesson grid column editor template
        public ActionResult ApallagiLessonsRead(int eidikotita = 0, int term = 0)
        {
            var data = db.adk_LESSON_NAMES.AsQueryable();

            if (eidikotita > 0 && term > 0)
            {
                data = db.adk_LESSON_NAMES.Where(f => f.LESSON_EIDIKOTITA == eidikotita && f.LESSON_TERM == term).OrderBy(d => d.LESSON_TEXT);
            }
            else
            {
                data = db.adk_LESSON_NAMES.OrderBy(d => new { d.LESSON_EIDIKOTITA, d.LESSON_TERM, d.LESSON_TEXT });
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Apallagi_Read([DataSourceRequest] DataSourceRequest request, int studentId = 0, int eidikotitaId = 0)
        {
            IEnumerable<StudentApallagiViewModel> data = apallagiService.Read(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Apallagi_Create([DataSourceRequest] DataSourceRequest request, StudentApallagiViewModel data, int studentId = 0, int eidikotitaId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentApallagiViewModel newdata = new StudentApallagiViewModel();

            if (studentId > 0 && eidikotitaId > 0)
            {
                var existingData = db.ΜΑΘΗΤΕΣ_ΑΠΑΛΛΑΓΕΣ.Where(s => s.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && s.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ == eidikotitaId 
                                            && s.ΕΞΑΜΗΝΟ_ΚΩΔ == data.ΕΞΑΜΗΝΟ_ΚΩΔ && s.ΜΑΘΗΜΑ_ΟΝΟΜΑ == data.ΜΑΘΗΜΑ_ΟΝΟΜΑ).Count();
                if (existingData > 0) 
                    ModelState.AddModelError("", "Υπάρχει ήδη απαλλαγή για τον σπουδαστή αυτόν στο συγκεκριμένο μάθημα.");

                if (data != null && ModelState.IsValid)
                {
                    apallagiService.Create(data, studentId, eidikotitaId, schoolId);
                    newdata = apallagiService.Refersh(data.ΜΑΠ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να επιλέξετε σπουδαστή πρώτα. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Apallagi_Update([DataSourceRequest] DataSourceRequest request, StudentApallagiViewModel data, int studentId = 0, int eidikotitaId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentApallagiViewModel newdata = new StudentApallagiViewModel();

            if (studentId > 0 && eidikotitaId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    apallagiService.Update(data, studentId, eidikotitaId, schoolId);
                    newdata = apallagiService.Refersh(data.ΜΑΠ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλεγεί σπουδαστής. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Apallagi_Destroy([DataSourceRequest] DataSourceRequest request, StudentApallagiViewModel data)
        {
            if (data != null)
            {
                apallagiService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion


        #region ΕΚΠΑΙΔΕΥΤΕΣ

        #region ΕΚΠΑΙΔΕΥΤΕΣ ΚΑΙ ΠΕΡΙΟΔΟΙ ΑΠΑΣΧΟΛΗΣΗΣ

        public ActionResult TeacherData(string notify = null)
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

            PopulateEidikotites();
            PopulatePeriodoi();
            return View();
        }

        public ActionResult EidikotitesTeacherRead()
        {
            var data = (from d in db.VD_EIDIKOTITES
                        orderby d.EIDIKOTITA_KLADOS_ID, d.EIDIKOTITA_DESC
                        select new VD_EIDIKOTITESViewModel
                            {
                                EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                                EIDIKOTITA_DESC = d.EIDIKOTITA_DESC
                            }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Teacher_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            IEnumerable<TeacherGridViewModel> data = teacherService.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Teacher_Create([DataSourceRequest] DataSourceRequest request, TeacherGridViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            var newdata = new TeacherGridViewModel();

            if (!Common.CheckAFM(data.ΑΦΜ)) 
                ModelState.AddModelError("", "Το ΑΦΜ που δώθηκε δεν είναι έγκυρο. Η καταχώρηση ακυρώθηκε.");

            if (!Kerberos.ValidatePrimaryKeyTeacher(data.ΑΦΜ, schoolId, data.ΕΙΔΙΚΟΤΗΤΑ)) 
                ModelState.AddModelError("", "Το ΑΦΜ και η ειδικότητα που δόθηκαν είναι ήδη καταχωρημένα για το σχολείο αυτό. Η αποθήκευση ακυρώθηκε.");

            if (ModelState.IsValid)
            {
                teacherService.Create(data, schoolId);
                newdata = teacherService.Refresh(data.TEACHER_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Teacher_Update([DataSourceRequest] DataSourceRequest request, TeacherGridViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            var newdata = new TeacherGridViewModel();

            if (ModelState.IsValid)
            {
                teacherService.Update(data, schoolId);
                newdata = teacherService.Refresh(data.TEACHER_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Teacher_Destroy([DataSourceRequest] DataSourceRequest request, TeacherGridViewModel data)
        {
            if (data != null)
            {
                if (!Kerberos.CanDeleteTeacher(data.TEACHER_ID)) 
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί ο εκπαιδευτής διότι υπάρχουν σχετιζόμενες περίοδοι απασχόλησης ή/και αναθέσεις.");

                if (ModelState.IsValid)
                {
                    teacherService.Destroy(data);
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        #region TEACHER PERIODOI (ΠΕΡΙΟΔΟΙ ΑΠΑΣΧΟΛΗΣΗΣ)

        public ActionResult TeacherPeriod_Read([DataSourceRequest] DataSourceRequest request, int teacherId = 0)
        {
            IEnumerable<TeacherPeriodsViewModel> data = teacherPeriodService.Read(teacherId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TeacherPeriod_Create([DataSourceRequest] DataSourceRequest request, TeacherPeriodsViewModel data, int teacherId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            var newdata = new TeacherPeriodsViewModel();

            var existingData = db.ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ.Where(s => s.TEACHER_ID == teacherId && s.ΠΕΡΙΟΔΟΣ_ΚΩΔ == data.ΠΕΡΙΟΔΟΣ_ΚΩΔ).Count();
            if (existingData > 0) 
                ModelState.AddModelError("", "Υπάρχει ήδη καταχώρηση του εκπαιδευτή για την περίοδο αυτή.");

            if (teacherId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    teacherPeriodService.Create(data, teacherId, schoolId);
                    newdata = teacherPeriodService.Refresh(data.ΕΠ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε εκπαιδευτή. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TeacherPeriod_Update([DataSourceRequest] DataSourceRequest request, TeacherPeriodsViewModel data, int teacherId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            var newdata = new TeacherPeriodsViewModel();

            if (teacherId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    teacherPeriodService.Update(data, teacherId, schoolId);
                    newdata = teacherPeriodService.Refresh(data.ΕΠ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε εκπαιδευτή. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TeacherPeriod_Destroy([DataSourceRequest] DataSourceRequest request, TeacherPeriodsViewModel data)
        {
            if (data != null)
            {
                teacherPeriodService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion


        #region TEACHER DATA FORM

        public ActionResult TeacherEdit(int teacherId)
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
            if (!(teacherId > 0))
            {
                string msg = "Ο κωδικός εκπαιδευτή δεν είναι έγκυρος. Η εγγραφή πρέπει να έχει αποθηκευτεί πρώτα.";
                return RedirectToAction("ErrorData", "School", new { notify = msg });
            }

            TeacherViewModel teacher = teacherService.GetRecord(teacherId);
            if (teacher == null)
            {
                string msg = "Παρουσιάστηκε πρόβλημε εύρεσης του εκπαιδευτικού.";
                return RedirectToAction("ErrorData", "School", new { notify = msg });
            }
            return View(teacher);
        }

        [HttpPost]
        public ActionResult TeacherEdit(int teacherId, TeacherViewModel data)
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

            int? oldEidikotita = 0;
            bool editEidikotitaOK = true;

            var src = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ where d.TEACHER_ID == teacherId select d).FirstOrDefault();
            if (src != null) oldEidikotita = src.ΕΙΔΙΚΟΤΗΤΑ;

            if (oldEidikotita != data.ΕΙΔΙΚΟΤΗΤΑ) editEidikotitaOK = Kerberos.CanEditTeacherEidikotita(teacherId);
            if (!editEidikotitaOK)
            {
                this.ShowMessage(MessageType.Warning, "Δεν επιτρέπεται αλλαγή της ειδικότητας διότι υπάρχουν περίοδοι απασχόλησης του εκπαιδευτή με αυτή.");
                ModelState.AddModelError("", "");
            }

            string ErrorMsg = Common.ValidateTeacherFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                teacherService.UpdateRecord(data, teacherId, schoolId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                TeacherViewModel newTeacher = teacherService.GetRecord(teacherId);
                return View(newTeacher);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public ActionResult TeacherDataPrint(int teacherId)
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
            var teacher = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ
                          where d.TEACHER_ID == teacherId
                          select new TeacherViewModel
                          {
                              TEACHER_ID = d.TEACHER_ID,
                              ΑΦΜ = d.ΑΦΜ,
                              ΙΕΚ = d.ΙΕΚ
                          }).FirstOrDefault();

            return View(teacher);
        }

        #endregion


        #region ΜΗΤΡΩΟ ΕΚΠΑΙΔΕΥΤΩΝ

        public ActionResult TeacherInfoList()
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

            IEnumerable<TeacherInfoViewModel> data = teacherInfoService.Read(schoolId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένοι εκπαιδευτές για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            TeacherInfoViewModel teacher = data.First();

            return View(teacher);
        }

        public ActionResult TeacherInfo_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            IEnumerable<TeacherInfoViewModel> data = teacherInfoService.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PeriodsInfo_Read([DataSourceRequest] DataSourceRequest request, int teacherId = 0)
        {
            IEnumerable<TeacherPeriodsInfoViewModel> data = teacherInfoService.GetPeriods(teacherId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisInfo_Read([DataSourceRequest] DataSourceRequest request, int teacherId = 0)
        {
            IEnumerable<TeacherAnatheseisInfoViewModel> data = teacherInfoService.GetAnatheseis(teacherId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetTeacherRecord(int teacherId)
        {
            TeacherInfoViewModel teacher = teacherInfoService.GetRecord(teacherId);

            return PartialView("TeacherInfoPartial", teacher);
        }


        #endregion


        #region ΑΝΑΘΕΣΕΙΣ ΜΑΘΗΜΑΤΩΝ

        public ActionResult AnatheseisData(string notify = null)
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

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            // Check if all required collections are not empty
            if (!IekEidikotitesExist(schoolId) || !TeachersExist(schoolId))
            {
                string errmsg = "Δεν βρέθηκαν καταχωρημένες ειδικότητες του ΙΕΚ ή εκπαιδευτικοί!";
                return RedirectToAction("Index", "School", new { notify = errmsg });
            }

            PopulatePeriodoi();
            PopulateIekEidikotites(schoolId);
            PopulateTerms();
            PopulateLessons(schoolId);

            return View();
        }

        public ActionResult AnathesiLessonsRead(int eidikotita = 0, int term = 0)
        {
            int schoolId = GetLoginSchool().USER_SCHOOLID ?? 0;

            var data = db.qryIEK_EIDIKOTITES_LESSONS.AsQueryable();
            if (eidikotita > 0 && term > 0)
            {
                data = db.qryIEK_EIDIKOTITES_LESSONS
                    .Where(f => f.IEK_ID == schoolId && f.LESSON_EIDIKOTITA == eidikotita && f.LESSON_TERM == term)
                    .OrderBy(d => d.LESSON_DESC);
            }
            else
            {
                data = db.qryIEK_EIDIKOTITES_LESSONS.Where(f => f.IEK_ID == schoolId).OrderBy(d => d.LESSON_DESC);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnathesiTermsRead()
        {
            // use the view model to avoid circular reference in editor template
            var data = (from d in db.SYS_TERM
                        where d.TERM_ID < 5
                        select new SYS_TERMViewModel
                        {
                            TERM_ID = d.TERM_ID,
                            TERM = d.TERM
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #region ANATHESEIS GRID

        public ActionResult Anathesi_Read([DataSourceRequest] DataSourceRequest request, int teacherId = 0)
        {
            IEnumerable<TeacherAnatheseisViewModel> data = anatheseisService.Read(teacherId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Anathesi_Create([DataSourceRequest] DataSourceRequest request, TeacherAnatheseisViewModel data, int teacherId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            TeacherAnatheseisViewModel newdata = new TeacherAnatheseisViewModel();

            if (teacherId > 0)
            {
                var existingData = db.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΘΕΣΕΙΣ
                                    .Where(s => s.TEACHER_ID == teacherId && s.ΜΑΘΗΜΑ_ΚΩΔ == data.ΜΑΘΗΜΑ_ΚΩΔ && s.ΠΕΡΙΟΔΟΣ_ΚΩΔ == data.ΠΕΡΙΟΔΟΣ_ΚΩΔ && s.ΕΞΑΜΗΝΟ == data.ΕΞΑΜΗΝΟ)
                                    .Count();
                if (existingData > 0) 
                    ModelState.AddModelError("", "Υπάρχει ήδη ανάθεση για τον εκπαιδευτή αυτόν στο συγκεκριμένο μάθημα.");

                if (ModelState.IsValid)
                {
                    anatheseisService.Create(data, teacherId, schoolId);
                    newdata = anatheseisService.Refresh(data.ΕΑ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να επιλέξετε εκπαιδευτή πρώτα. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Anathesi_Update([DataSourceRequest] DataSourceRequest request, TeacherAnatheseisViewModel data, int teacherId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            TeacherAnatheseisViewModel newdata = new TeacherAnatheseisViewModel();

            if (teacherId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    anatheseisService.Update(data, teacherId, schoolId);
                    newdata = anatheseisService.Refresh(data.ΕΑ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλεγεί εκπαιδευτής. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Anathesi_Destroy([DataSourceRequest] DataSourceRequest request, TeacherAnatheseisViewModel data)
        {
            if (data != null)
            {
                anatheseisService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion ANATHESEIS GRID

        #endregion


        #region ΜΗΤΡΩΟ ΑΝΑΘΕΣΕΩΝ

        public ActionResult AnatheseisView()
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
            return View();
        }

        public ActionResult AnatheseisView_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = GetLoginSchool().USER_SCHOOLID ?? 0;

            IEnumerable<QueryAnatheseisViewModel> data = anatheseisService.View(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            int schoolId = GetLoginSchool().USER_SCHOOLID ?? 0;

            var data = (from d in db.sqlANATHESEIS_VIEW
                          where d.ΙΕΚ == schoolId
                          select new QueryAnatheseisViewModel
                          {
                              ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ,
                              TEACHER_ID = d.TEACHER_ID,
                              ΑΦΜ = d.ΑΦΜ,
                              ΙΕΚ = d.ΙΕΚ,
                              ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                          }).FirstOrDefault();
            return View(data);
        }


        #endregion


        #region ΑΙΤΗΣΕΙΣ ΕΚΠΑΙΔΕΥΤΩΝ

        public ActionResult TeacherAitiseis()
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

            if (!TeachersExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένοι εκπαιδευτές για το ΙΕΚ αυτό.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            return View();
        }

        #region GRID CRUD FUNCTIONS

        public ActionResult TeacherAitisi_Read([DataSourceRequest] DataSourceRequest request, int teacherId)
        {
            List<TeacherAitiseisViewModel> data = teacherAitisiService.Read(teacherId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TeacherAitisi_Create([DataSourceRequest] DataSourceRequest request, TeacherAitiseisViewModel data, int teacherId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            TeacherAitiseisViewModel newdata = new TeacherAitiseisViewModel();

            if (!(teacherId > 0)) ModelState.AddModelError("", "Για να γίνει η καταχώρηση πρέπει πρώτα να επιλέξετε εκπαιδευτικό.");

            if (data != null && ModelState.IsValid)
            {
                teacherAitisiService.Create(data, teacherId, schoolId);
                newdata = teacherAitisiService.Refresh(data.ΑΙΤΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TeacherAitisi_Update([DataSourceRequest] DataSourceRequest request, TeacherAitiseisViewModel data, int teacherId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            TeacherAitiseisViewModel newdata = new TeacherAitiseisViewModel();

            if (!(teacherId > 0)) ModelState.AddModelError("", "Για να γίνει η καταχώρηση πρέπει πρώτα να επιλέξετε εκπαιδευτικό.");

            if (data != null && ModelState.IsValid)
            {
                teacherAitisiService.Update(data, teacherId, schoolId);
                newdata = teacherAitisiService.Refresh(data.ΑΙΤΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TeacherAitisi_Destroy([DataSourceRequest] DataSourceRequest request, TeacherAitiseisViewModel data)
        {
            if (data != null)
            {
                teacherAitisiService.Destroy(data);
            }

            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        public ActionResult TeacherAitisiPrint(int aitisiId)
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
            var aitisi = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΙΤΗΣΕΙΣ
                          where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                          select new TeacherAitiseisViewModel
                          {
                              ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                              TEACHER_ID= d.TEACHER_ID,
                              ΑΦΜ = d.ΑΦΜ,
                              ΙΕΚ = d.ΙΕΚ
                          }).FirstOrDefault();

            return View(aitisi);
        }

        #endregion


        #region ΒΕΒΑΙΩΣΕΙΣ ΑΝΑΛΗΨΗΣ ΚΑΘΗΚΟΝΤΩΝ

        public ActionResult TeacherAnalipseis(string notify = null)
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

            if (!TeachersExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένοι εκπαιδευτικοί για το ΙΕΚ αυτό.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateTeachersInPeriods(schoolId);
            PopulatePeriodoi();

            return View();
        }

        public ActionResult TeacherAnalipsi_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            List<TeacherAnalipsiViewModel> data = teacherAnalipsiService.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TeacherAnalipsi_Create([DataSourceRequest] DataSourceRequest request, TeacherAnalipsiViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            TeacherAnalipsiViewModel newdata = new TeacherAnalipsiViewModel();

            if (data != null && ModelState.IsValid)
            {
                teacherAnalipsiService.Create(data, schoolId);
                newdata = teacherAnalipsiService.Refresh(data.ΑΝΑΛΗΨΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TeacherAnalipsi_Update([DataSourceRequest] DataSourceRequest request, TeacherAnalipsiViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            TeacherAnalipsiViewModel newdata = new TeacherAnalipsiViewModel();

            if (data != null && ModelState.IsValid)
            {
                teacherAnalipsiService.Update(data, schoolId);
                newdata = teacherAnalipsiService.Refresh(data.ΑΝΑΛΗΨΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TeacherAnalipsi_Destroy([DataSourceRequest] DataSourceRequest request, TeacherAnalipsiViewModel data)
        {
            if (data != null)
            {
                teacherAnalipsiService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Periods_Read()
        {
            var data = (from d in db.ΠΕΡΙΟΔΟΙ
                        orderby d.ΠΕΡΙΟΔΟΣ
                        select new PeriodosViewModel
                        {
                            PERIOD_ID = d.PERIOD_ID,
                            ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PeriodTeachers_Read(int periodId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = db.sqlTEACHERS_IN_PERIODS.AsQueryable().Where(f => f.PERIOD_ID == periodId && f.ΙΕΚ == schoolId).OrderBy(d => d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TeacherAnalipsiPrint(int analipsiId)
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

            var bebeosi = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΛΗΨΕΙΣ
                           where d.ΑΝΑΛΗΨΗ_ΚΩΔ == analipsiId
                           select new TeacherAnalipsiViewModel
                           {
                               ΑΝΑΛΗΨΗ_ΚΩΔ = d.ΑΝΑΛΗΨΗ_ΚΩΔ,
                               TEACHER_ID = d.TEACHER_ID,
                               ΙΕΚ = d.ΙΕΚ,
                               PERIOD_ID = d.PERIOD_ID
                           }).FirstOrDefault();

            return View(bebeosi);
        }


        #endregion


        #region ΟΙΚΕΙΟΘΕΛΕΙΣ ΑΠΟΧΩΡΗΣΕΙΣ

        public ActionResult TeacherWithdrawals(string notify = null)
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

            if (!TeachersExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένοι εκπαιδευτικοί για το ΙΕΚ αυτό.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateTeachers(schoolId);
            PopulateSchoolYears();
            PopulateAitiesApoxorisi();

            return View();
        }

        public ActionResult Apoxorisi_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            List<TeacherWithdrawalViewModel> data = apoxorisiService.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Apoxorisi_Create([DataSourceRequest] DataSourceRequest request, TeacherWithdrawalViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            TeacherWithdrawalViewModel newdata = new TeacherWithdrawalViewModel();

            if (data != null && ModelState.IsValid)
            {
                apoxorisiService.Create(data, schoolId);
                newdata = apoxorisiService.Refresh(data.ΑΠΟΧΩΡΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Apoxorisi_Update([DataSourceRequest] DataSourceRequest request, TeacherWithdrawalViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            TeacherWithdrawalViewModel newdata = new TeacherWithdrawalViewModel();

            if (data != null && ModelState.IsValid)
            {
                apoxorisiService.Update(data, schoolId);
                newdata = apoxorisiService.Refresh(data.ΑΠΟΧΩΡΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Apoxorisi_Destroy([DataSourceRequest] DataSourceRequest request, TeacherWithdrawalViewModel data)
        {
            if (data != null)
            {
                apoxorisiService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult WithdrawalsPrint()
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

            var data = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΠΟΧΩΡΗΣΕΙΣ
                        where d.ΙΕΚ == schoolId
                        select new TeacherWithdrawalViewModel
                        {
                            ΑΠΟΧΩΡΗΣΗ_ΚΩΔ = d.ΑΠΟΧΩΡΗΣΗ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        #endregion

        #endregion ΕΚΠΑΙΔΕΥΤΕΣ


        #region ΕΚΤΥΠΩΣΕΙΣ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult BebeosiSpoudesPrint(int bebeosiId)
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
            var data = (from d in db.ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ
                        where d.ΒΕΒΑΙΩΣΗ_ΚΩΔ == bebeosiId
                        select new StudentBebeoseisViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΑΜΚ = d.ΑΜΚ
                        }).FirstOrDefault();

            return View(data);

        }

        public ActionResult BebeosiArmyPrint(int bebeosiId)
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
            var data = (from d in db.ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ
                        where d.ΒΕΒΑΙΩΣΗ_ΚΩΔ == bebeosiId
                        select new StudentBebeoseisViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΑΜΚ = d.ΑΜΚ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult BebeosiAPSPrint(int bebeosiId)
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
            var data = (from d in db.ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ
                        where d.ΒΕΒΑΙΩΣΗ_ΚΩΔ == bebeosiId
                        select new StudentBebeoseisViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΑΜΚ = d.ΑΜΚ
                        }).FirstOrDefault();

            return View(data);
        }
       
        #endregion


        #region STATISTICS

        #region ELSTAT

        public ActionResult elstatTeacherGenderIekPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult elstatTeacherGenderAllPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult elstatStudentsEntrantsPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult elstatStudentsGraduatesPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult elstatStudentsRegisteredPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult elstatStudentsEntrants2Print()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult elstatStudentsGraduates2Print()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult elstatStudentsRegistered2Print()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        // ΓΕΝΙΚΑ ΣΥΝΟΛΑ (ΑΝΑ ΣΧΟΛΙΚΟ ΈΤΟΣ)

        public ActionResult elstatSumStudentsEntrantsPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult elstatSumStudentsGraduatesPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult elstatSumStudentsRegisteredPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult elstatSumStudentsEntrants2Print()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult elstatSumStudentsGraduates2Print()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult elstatSumStudentsRegistered2Print()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult elstatISCEDEidikotitesPrint()
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
            return View();
        }

        public ActionResult elstatISCEDSector0000Print()
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
            return View();
        }

        public ActionResult elstatISCEDSector000Print()
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
            return View();
        }

        public ActionResult elstatISCEDSector00Print()
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
            return View();
        }

        public ActionResult elstatRegionRegisteredPrint()
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
            return View();
        }

        #endregion ELSTAT


        #region ΑΛΛΕΣ ΣΤΑΤΙΣΤΙΚΕΣ

        public ActionResult statStudentsEidikotitaMenPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult statStudentsEidikotitaWomenPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult statStudentsEidikotitaAgePrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult statStudentsAgeGroupPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult statSummaryAgeGroupPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult statSummaryEidikotitaIekPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult statSummaryGenderIekPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult statSummaryGenderPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();
           
            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult statSummaryIekEidikotitesPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult TmimataDetailPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult TmimataSumPrint()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        #endregion ΑΛΛΕΣ ΣΤΑΤΙΣΤΙΚΕΣ

        #endregion


        #region ΝΕΕΣ ΣΤΑΤΙΣΤΙΚΕΣ ΟΘΟΝΕΣ (26/5/2018)

        #region ΕΛΣΤΑΤ

        public ActionResult xReportsElstatList(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            List<SysReportViewModel> reports = GetReportsElstatFromDB();
            return View(reports);
        }

        public ActionResult ReportSelectorElstat(int reportId = 0)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            // logic of report selection here
            if (reportId == 1)
            {
                return RedirectToAction("elstatStudentsEntrantsPrint", "School");
            }
            else if (reportId == 2)
            {
                return RedirectToAction("elstatStudentsRegisteredPrint", "School");
            }
            else if (reportId == 3)
            {
                return RedirectToAction("elstatStudentsGraduatesPrint", "School");
            }
            else if (reportId == 4)
            {
                return RedirectToAction("elstatStudentsEntrants2Print", "School");
            }
            else if (reportId == 5)
            {
                return RedirectToAction("elstatStudentsRegistered2Print", "School");
            }
            else if (reportId == 6)
            {
                return RedirectToAction("elstatStudentsGraduates2Print", "School");
            }
            else if (reportId == 7)
            {
                return RedirectToAction("elstatSumStudentsEntrantsPrint", "School");
            }
            else if (reportId == 8)
            {
                return RedirectToAction("elstatSumStudentsRegisteredPrint", "School");
            }
            else if (reportId == 9)
            {
                return RedirectToAction("elstatSumStudentsGraduatesPrint", "School");
            }
            else if (reportId == 10)
            {
                return RedirectToAction("elstatSumStudentsEntrants2Print", "School");
            }
            else if (reportId == 11)
            {
                return RedirectToAction("elstatSumStudentsRegistered2Print", "School");
            }
            else if (reportId == 12)
            {
                return RedirectToAction("elstatSumStudentsGraduates2Print", "School");
            }
            else if (reportId == 13)
            {
                return RedirectToAction("elstatTeacherGenderIekPrint", "School");
            }
            else if (reportId == 14)
            {
                return RedirectToAction("elstatTeacherGenderAllPrint", "School");
            }
            else if (reportId == 31)
            {
                return RedirectToAction("elstatRegionRegisteredPrint", "School");
            }
            else if (reportId == 32)
            {
                return RedirectToAction("elstatISCEDSector0000Print", "School");
            }
            else if (reportId == 33)
            {
                return RedirectToAction("elstatISCEDSector000Print", "School");
            }
            else if (reportId == 34)
            {
                return RedirectToAction("elstatISCEDSector00Print", "School");
            }
            else if (reportId == 35)
            {
                return RedirectToAction("elstatISCEDEidikotitesPrint", "School");
            }
            else
            {
                string msg = "Η έκθεση αυτή δεν υπάρχει καταχωρημένη.";
                return RedirectToAction("xReportsElstatList", "School", new { notify = msg });
            }
        }

        public ActionResult ReportsElstat_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<SysReportViewModel> data = GetReportsElstatFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SysReportViewModel> GetReportsElstatFromDB()
        {
            var data = (from d in db.ΣΥΣ_ΕΝΤΥΠΑ_ΙΕΚ
                        where d.DOC_CLASS == "ELSTAT"
                        select new SysReportViewModel
                        {
                            DOC_ID = d.DOC_ID,
                            DOC_NAME = d.DOC_NAME,
                            DOC_DESCRIPTION = d.DOC_DESCRIPTION,
                            DOC_CLASS = d.DOC_CLASS
                        }).ToList();
            return data;
        }

        #endregion ΕΛΣΤΑΤ


        #region ΣΥΓΚΕΝΤΡΩΤΙΚΑ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult xReportsSummaryList(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View();
        }

        public ActionResult ReportSelectorSummary(int reportId = 0)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            // logic of report selection here
            if (reportId == 15)
            {
                return RedirectToAction("statSummaryAgeGroupPrint", "School");
            }
            else if (reportId == 16)
            {
                return RedirectToAction("statSummaryGenderPrint", "School");
            }
            else if (reportId == 17)
            {
                return RedirectToAction("statSummaryGenderIekPrint", "School");
            }
            else if (reportId == 18)
            {
                return RedirectToAction("statSummaryEidikotitaIekPrint", "School");
            }
            else if (reportId == 19)
            {
                return RedirectToAction("statSummaryIekEidikotitesPrint", "School");
            }
            else
            {
                string msg = "Η έκθεση αυτή δεν υπάρχει καταχωρημένη.";
                return RedirectToAction("xReportsSummaryList", "School", new { notify = msg });
            }
        }

        public ActionResult ReportsSummary_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<SysReportViewModel> data = GetReportsSummaryFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SysReportViewModel> GetReportsSummaryFromDB()
        {
            var data = (from d in db.ΣΥΣ_ΕΝΤΥΠΑ_ΙΕΚ
                        where d.DOC_CLASS == "SUMMARY"
                        select new SysReportViewModel
                        {
                            DOC_ID = d.DOC_ID,
                            DOC_NAME = d.DOC_NAME,
                            DOC_DESCRIPTION = d.DOC_DESCRIPTION,
                            DOC_CLASS = d.DOC_CLASS
                        }).ToList();
            return data;
        }

        #endregion ΣΥΓΚΕΝΤΡΩΤΙΚΑ ΣΠΟΥΔΑΣΤΩΝ


        #region ΔΥΝΑΜΗ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult xReportsManpowerList(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View();
        }

        public ActionResult ReportSelectorManpower(int reportId = 0)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            // logic of report selection here
            if (reportId == 20)
            {
                return RedirectToAction("TmimataDetailPrint", "School");
            }
            else if (reportId == 21)
            {
                return RedirectToAction("TmimataSumPrint", "School");
            }
            else if (reportId == 22)
            {
                return RedirectToAction("statStudentsAgeGroupPrint", "School");
            }
            else if (reportId == 23)
            {
                return RedirectToAction("statStudentsEidikotitaMenPrint", "School");
            }
            else if (reportId == 24)
            {
                return RedirectToAction("statStudentsEidikotitaWomenPrint", "School");
            }
            else if (reportId == 25)
            {
                return RedirectToAction("statStudentsEidikotitaAgePrint", "School");
            }
            else
            {
                string msg = "Η έκθεση αυτή δεν υπάρχει καταχωρημένη.";
                return RedirectToAction("xReportsManpowerList", "School", new { notify = msg });
            }
        }

        public ActionResult ReportsManpower_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<SysReportViewModel> data = GetReportsManpowerFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SysReportViewModel> GetReportsManpowerFromDB()
        {
            var data = (from d in db.ΣΥΣ_ΕΝΤΥΠΑ_ΙΕΚ
                        where d.DOC_CLASS == "MANPOWER"
                        select new SysReportViewModel
                        {
                            DOC_ID = d.DOC_ID,
                            DOC_NAME = d.DOC_NAME,
                            DOC_DESCRIPTION = d.DOC_DESCRIPTION,
                            DOC_CLASS = d.DOC_CLASS
                        }).ToList();
            return data;
        }

        #endregion ΔΥΝΑΜΗ ΣΠΟΥΔΑΣΤΩΝ


        #region ΣΤΟΙΧΕΙΑ ΓΙΑ ΦΟΡΕΙΣ

        public ActionResult xReportsExternalList(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View();
        }

        public ActionResult ReportSelectorExternal(int reportId = 0)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            // logic of report selection here
            if (reportId == 26)
            {
                return RedirectToAction("HdikaTable", "School");
            }
            else if (reportId == 27)
            {
                return RedirectToAction("HdikaTable2", "School");
            }
            else if (reportId == 28)
            {
                return RedirectToAction("HdikaPraktiki", "School");
            }
            else if (reportId == 30)
            {
                return RedirectToAction("OpenDataReport", "School");
            }
            else
            {
                string msg = "Η έκθεση αυτή δεν υπάρχει καταχωρημένη.";
                return RedirectToAction("xReportsExternalList", "School", new { notify = msg });
            }
        }

        public ActionResult ReportsExternal_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<SysReportViewModel> data = GetReportsExternalFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SysReportViewModel> GetReportsExternalFromDB()
        {
            var data = (from d in db.ΣΥΣ_ΕΝΤΥΠΑ_ΙΕΚ
                        where d.DOC_CLASS == "EXTERNAL"
                        select new SysReportViewModel
                        {
                            DOC_ID = d.DOC_ID,
                            DOC_NAME = d.DOC_NAME,
                            DOC_DESCRIPTION = d.DOC_DESCRIPTION,
                            DOC_CLASS = d.DOC_CLASS
                        }).ToList();
            return data;
        }

        #endregion ΣΤΟΙΧΕΙΑ ΓΙΑ ΦΟΡΕΙΣ


        #region ΣΤΟΙΧΕΙΑ ΓΙΑ ΕΙΔΙΚΕΣ ΕΚΘΕΣΕΙΣ

        public ActionResult xReportsCustomList(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View();
        }

        public ActionResult ReportSelectorCustom(int reportId = 0)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            // logic of report selection here
            if (reportId == 29)
            {
                return RedirectToAction("xReportDemoPrint", "School");
            }
            else if (reportId == 36)
            {
                return RedirectToAction("xActiveEidikotitesPrint", "School");
            }
            else if (reportId == 37)
            {
                return RedirectToAction("xActiveEidikotitesYearPrint", "School");
            }
            else
            {
                string msg = "Η έκθεση αυτή δεν υπάρχει καταχωρημένη.";
                return RedirectToAction("xReportsCustomList", "School", new { notify = msg });
            }
        }

        public ActionResult ReportsCustom_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<SysReportViewModel> data = GetReportsCustomFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SysReportViewModel> GetReportsCustomFromDB()
        {
            var data = (from d in db.ΣΥΣ_ΕΝΤΥΠΑ_ΙΕΚ
                        where d.DOC_CLASS == "CUSTOM"
                        select new SysReportViewModel
                        {
                            DOC_ID = d.DOC_ID,
                            DOC_NAME = d.DOC_NAME,
                            DOC_DESCRIPTION = d.DOC_DESCRIPTION,
                            DOC_CLASS = d.DOC_CLASS
                        }).ToList();
            return data;
        }

        #endregion ΣΤΟΙΧΕΙΑ ΓΙΑ ΕΙΔΙΚΕΣ ΕΚΘΕΣΕΙΣ

        #endregion


        #region ΕΚΥΠΩΣΕΙΣ ΓΕΝΙΚΕΣ

        public ActionResult StudentPhoneListPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = loggedSchool.USER_SCHOOLID ?? 0;

            GeneralReportParameters rp = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId
            };
            return View(rp);
        }

        public ActionResult xReportDemoPrint()
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

            return View();
        }

        public ActionResult HdikaTable()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult HdikaTable2()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult HdikaPraktiki()
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

            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID == schoolId select d).FirstOrDefault();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = schoolId,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult OpenDataReport()
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
            return View();
        }

        public ActionResult xActiveEidikotitesPrint()
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
            return View();
        }

        public ActionResult xActiveEidikotitesYearPrint()
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
            return View();
        }

        #endregion


        #region LOCAL FUNCTIONS

        // Getter for schools
        public JsonResult GetIekEidikotites()
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = db.viewIEK_EIDIKOTITES.Where(e => e.IEK_ID == schoolId).OrderBy(e => e.EIDIKOTITA_TEXT).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public bool GetSchoolEnableStatus()
        {
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();
            bool status = data.SCHOOL_ENABLE ?? false;
            return status;
        }

        public string GetSchoolEnableMessage()
        {
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();
            string msg = data.SCHOOL_MESSAGE;
            return msg;
        }

        #endregion

    }
}