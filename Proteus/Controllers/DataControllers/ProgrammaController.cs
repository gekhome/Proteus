using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using Proteus.Notification;
using Proteus.Services;
using Proteus.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Proteus.Controllers.DataControllers
{
    [ErrorHandlerFilter]
    public class ProgrammaController : ControllerUnit
    {        
        private readonly ProteusDBEntities db;

        private readonly IBekService bekService;
        private readonly IBebeosiTeacherService bebeosiTeacherService;
        private readonly IApousies2Service apousies2Service;
        private readonly IApousiesService apousiesService;
        private readonly IGradesService gradesService;
        private readonly IGradesTransferService gradesTransferService;
        private readonly IProgrammaService programmaService;
        private readonly IErgasiaGradeService ergasiaGradeService;
        private readonly ITableGradesTermService tableGradesTermService;

        public ProgrammaController(ProteusDBEntities entities, IBekService bekService,
            IBebeosiTeacherService bebeosiTeacherService, IApousies2Service apousies2Service,
            IApousiesService apousiesService, IGradesService gradesService, 
            IGradesTransferService gradesTransferService, IProgrammaService programmaService, 
            IErgasiaGradeService ergasiaGradeService, ITableGradesTermService tableGradesTermService) : base(entities)
        {
            db = entities;

            this.bekService = bekService;
            this.bebeosiTeacherService = bebeosiTeacherService;
            this.apousies2Service = apousies2Service;
            this.apousiesService = apousiesService;
            this.gradesService = gradesService;
            this.gradesTransferService = gradesTransferService;
            this.programmaService = programmaService;
            this.ergasiaGradeService = ergasiaGradeService;
            this.tableGradesTermService = tableGradesTermService;
        }


        #region TMIMATA DATA SOURCES

        // Used by Editor Templates in Apousies2, Apousies, Grades pages
        public ActionResult TmimaStudentsRead(int tmimaId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = db.qrySTUDENT_TMIMA_SELECTOR
                .Where(f => f.ΚΩΔ_ΤΜΗΜΑ == tmimaId && f.ΙΕΚ == schoolId)
                .OrderBy(d => d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ);

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        // Tmimata data source used in grades and programma
        public ActionResult Tmimata_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetTmimataFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<sqlTmimataViewModel> GetTmimataFromDB()
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = (from d in db.sqlΤΜΗΜΑΤΑ
                        where d.ΙΕΚ == schoolId
                        orderby d.ΠΕΡΙΟΔΟΣ descending, d.TERM_ID, d.EIDIKOTITA_TEXT
                        select new sqlTmimataViewModel
                        {
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            TERM = d.TERM,
                            ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ,
                            PERIOD_ID = d.PERIOD_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            TERM_ID = d.TERM_ID,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID
                        }).ToList();
            return data;
        }

        // Tmimata data source for programma, Hours monitor, teacher parousies
        public ActionResult TmimataNew_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<sqlTmimataViewModel> data = GetTmimataNewFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<sqlTmimataViewModel> GetTmimataNewFromDB()
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = (from d in db.sqlΤΜΗΜΑΤΑ
                        where d.ΙΕΚ == schoolId && d.PERIOD_ID >= PERIOD_ARCHIVE
                        orderby d.ΠΕΡΙΟΔΟΣ descending, d.TERM_ID, d.EIDIKOTITA_TEXT
                        select new sqlTmimataViewModel
                        {
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            TERM = d.TERM,
                            ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ,
                            PERIOD_ID = d.PERIOD_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            TERM_ID = d.TERM_ID,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID
                        }).ToList();
            return data;
        }

        #endregion


        #region ΜΗ ΚΑΤΑΧΩΡΗΜΕΝΕΣ ΑΠΟΥΣΙΕΣ

        public ActionResult StudentApousies2()
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

            if (!IekTmimataExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            PopulateIekTmimata(schoolId);
            PopulateStudents(schoolId);

            return View();
        }

        public ActionResult Apousia2_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            IEnumerable<StudentApousies2ViewModel> data = apousies2Service.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Apousia2_Create([DataSourceRequest] DataSourceRequest request, StudentApousies2ViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentApousies2ViewModel newdata = new StudentApousies2ViewModel();

            if (data != null && ModelState.IsValid)
            {
                apousies2Service.Create(data, schoolId);
                newdata = apousies2Service.Refresh(data.ΜΑ2_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Apousia2_Update([DataSourceRequest] DataSourceRequest request, StudentApousies2ViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentApousies2ViewModel newdata = new StudentApousies2ViewModel();

            if (data != null && ModelState.IsValid)
            {
                apousies2Service.Update(data, schoolId);
                newdata = apousies2Service.Refresh(data.ΜΑ2_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Apousia2_Destroy([DataSourceRequest] DataSourceRequest request, StudentApousies2ViewModel data)
        {
            if (data != null)
            {
                apousies2Service.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion ΜΗ ΚΑΤΑΧΩΡΗΜΕΝΕΣ ΑΠΟΥΣΙΕΣ


        #region ΑΠΟΥΣΙΕΣ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult StudentApousies()
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

            if (!IekTmimataExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            int count = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ where d.ΙΕΚ == schoolId select d).Count();
            if (count == 0) 
            {
                string msg = "Δεν βρέθηκε καταχωρημένο ωρολόγιο πρόγραμμα μαθημάτων. Η καταχώρηση απουσιών είναι αδύνατη χωρίς αυτό.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            PopulateStudents(schoolId);
            PopulateHours();
            PopulateApousiesLessons(schoolId);

            return View();
        }

        public ActionResult ApousiesLessonsRead(int eidikotitaId, int termId, string valueDate)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            DateTime theDate = Convert.ToDateTime(valueDate);

            var data1 = db.qryIEK_LESSONS_IN_PROGRAMMADAY.AsQueryable();
            var data2 = db.qryIEK_EIDIKOTITES_LESSONS.AsQueryable();

            if (theDate != null)
            {
                data1 = db.qryIEK_LESSONS_IN_PROGRAMMADAY
                        .Where(f => f.LESSON_EIDIKOTITA == eidikotitaId && f.LESSON_TERM == termId && f.IEK_ID == schoolId && f.ΗΜΕΡΟΜΗΝΙΑ == theDate)
                        .OrderBy(d => d.LESSON_DESC);
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                data2 = db.qryIEK_EIDIKOTITES_LESSONS
                        .Where(f => f.LESSON_EIDIKOTITA == eidikotitaId && f.LESSON_TERM == termId && f.IEK_ID == schoolId)
                        .OrderBy(d => d.LESSON_DESC);
                return Json(data2, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Apousia_Read([DataSourceRequest] DataSourceRequest request, int tmimaId = 0, DateTime? theDate = null)
        {
            IEnumerable<StudentApousiesViewModel> data = apousiesService.Read(tmimaId, theDate);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Apousia_Create([DataSourceRequest] DataSourceRequest request, StudentApousiesViewModel data, int tmimaId = 0, DateTime? theDate = null)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentApousiesViewModel newdata = new StudentApousiesViewModel();

            if (tmimaId == 0 || theDate == null)
                ModelState.AddModelError("", "Για καταχώρηση απουσιών πρέπει πρώτα να επιλέξετε τμήμα και ημερομηνία.");

            if (data != null && ModelState.IsValid)
            {
                apousiesService.Create(data, tmimaId, theDate, schoolId);
                newdata = apousiesService.Refresh(data.ΜΑ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Apousia_Update([DataSourceRequest] DataSourceRequest request, StudentApousiesViewModel data, int tmimaId = 0, DateTime? theDate = null)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentApousiesViewModel newdata = new StudentApousiesViewModel();

            if (tmimaId == 0 || theDate == null)
                ModelState.AddModelError("", "Για καταχώρηση απουσιών πρέπει πρώτα να επιλέξετε τμήμα και ημερομηνία.");

            if (data != null && ModelState.IsValid)
            {
                apousiesService.Update(data, tmimaId, theDate, schoolId);
                newdata = apousiesService.Refresh(data.ΜΑ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Apousia_Destroy([DataSourceRequest] DataSourceRequest request, StudentApousiesViewModel data)
        {
            if (data != null)
            {
                apousiesService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΒΑΘΜΟΛΟΓΙΕΣ ΣΠΟΥΔΑΣΤΩΝ ΝΕΟ (27/5/2018)

        public ActionResult StudentGrades()
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

            if (!IekTmimataExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            PopulateStudents(schoolId);
            PopulateLessonTags();
            return View();
        }

        public ActionResult Grades_Read([DataSourceRequest] DataSourceRequest request, int tmimaId = 0, int lessonId = 0)
        {
            IEnumerable<StudentGradesViewModel> data = gradesService.Read(tmimaId, lessonId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Grades_Create([DataSourceRequest] DataSourceRequest request, 
                            [Bind(Prefix = "models")]IEnumerable<StudentGradesViewModel> data, int tmimaId = 0, int lessonId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            if (tmimaId == 0 || lessonId == 0)
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε τμήμα και μάθημα. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    string errmsg = Common.ValidateGrades(item);
                    if (!string.IsNullOrEmpty(errmsg))
                    {
                        ModelState.AddModelError("", "Βρέθηκαν βαθμολογίες που δεν είναι έγυρες (< 0 ή > 10). Η καταχώρηση ακυρώθηκε.");
                        return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                    }
                    if (Common.CanCreateStudentGrade((int)item.ΜΑΘΗΤΗΣ_ΚΩΔ, tmimaId, lessonId))
                    {
                        gradesService.Create(item, tmimaId, lessonId, schoolId);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Υπάρχει ήδη καταχωρημένη βαθμολογία για αυτό το σπουδαστή και μάθημα. Η καταχώρηση ακυρώθηκε");
                        return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Grades_Update([DataSourceRequest] DataSourceRequest request, 
                            [Bind(Prefix = "models")]IEnumerable<StudentGradesViewModel> data, int tmimaId = 0, int lessonId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            if (tmimaId == 0 || lessonId == 0)
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε τμήμα και μάθημα. Η ενημέρωση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    string errmsg = Common.ValidateGrades(item);
                    if (!string.IsNullOrEmpty(errmsg))
                    {
                        ModelState.AddModelError("", "Βρέθηκαν βαθμολογίες που δεν είναι έγυρες (< 0 ή > 10). Η καταχώρηση ακυρώθηκε.");
                        return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                    }
                    gradesService.Update(item, tmimaId, lessonId, schoolId);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Grades_Destroy([DataSourceRequest] DataSourceRequest request, 
                            [Bind(Prefix = "models")]IEnumerable<StudentGradesViewModel> data)
        {
            if (data.Any())
            {
                foreach (var item in data)
                {
                    gradesService.Destroy(item);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertStudents(int tmimaId = 0, int lessonId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            string msg = "";

            var students = (from d in db.sqlSTUDENTS_TMIMA_ACTIVE where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ select d).ToList();
            if (students.Count > 0)
            {
                foreach (var item in students)
                {
                    if (Common.CanCreateStudentGrade(item.STUDENT_ID, tmimaId, lessonId))
                    {
                        ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ entity = new ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ()
                        {
                            ΜΑΘΗΤΗΣ_ΚΩΔ = item.STUDENT_ID,
                            ΚΩΔ_ΜΑΘΗΜΑ = lessonId,
                            ΙΕΚ = schoolId,
                            ΚΩΔ_ΤΜΗΜΑ = tmimaId,
                            ΑΜΚ = Common.GetStudentAmk(item.STUDENT_ID),
                            ΧΑΡΑΚΤΗΡΙΣΜΟΣ = 1,
                            ΒΑΘΜΟΣ_ΠΡΟΟΔΟΥ = 0,
                            ΒΑΘΜΟΣ_ΤΕ = 0,
                            ΒΑΘΜΟΣ_ΕΠ = 0,
                            ΒΑΘΜΟΣ_ΜΟ = 0
                        };
                        db.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ.Add(entity);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                msg = "Δεν βρέθηκαν σπουδαστές για το τμήμα αυτό.";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteGrades(int tmimaId = 0, int lessonId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            string msg = "";

            var data = (from d in db.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.ΚΩΔ_ΜΑΘΗΜΑ == lessonId select d).ToList();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ entity = db.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ.Find(item.ΜΒ_ΚΩΔ);
                    if (entity != null)
                    {
                        db.Entry(entity).State = EntityState.Deleted;
                        db.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ.Remove(entity);
                        db.SaveChanges();
                    }
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        // Combo data source
        public ActionResult GradesLessonsRead(int eidikotitaId, int termId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            var data = db.qryIEK_EIDIKOTITES_LESSONS.AsQueryable();

            if (eidikotitaId > 0 && termId > 0)
            {
                data = db.qryIEK_EIDIKOTITES_LESSONS.Where(f => f.LESSON_EIDIKOTITA == eidikotitaId && f.LESSON_TERM == termId && f.IEK_ID == schoolId).OrderBy(d => d.LESSON_DESC);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΜΕΤΑΦΟΡΑ ΚΑΙ ΚΑΤΑΧΩΡΗΣΗ ΒΑΘΜΩΝ (ΝΕΟ 8-10-2019)

        public ActionResult StudentGradesTransfer()
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

            if (!IekTmimataExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            PopulateStudents(schoolId);
            PopulateLessonTags();
            PopulateTransferLessons(schoolId);

            return View();
        }

        public ActionResult IekLessonsRead(int eidikotitaId, int termId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var lessons = (from d in db.sqlIEK_LESSONS
                           where d.EIDIKOTITA_ID == eidikotitaId && d.LESSON_TERM == termId && d.IEK_ID == schoolId
                           orderby d.LESSON_DESC
                           select new sqlIekLessonsViewModel
                           {
                               LESSON_ID = d.LESSON_ID,
                               LESSON_DESC = d.LESSON_DESC
                           }).ToList();
            return Json(lessons, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GradesTransfer_Read([DataSourceRequest] DataSourceRequest request, int tmimaId = 0, int studentId = 0)
        {
            IEnumerable<StudentGradesViewModel> data = gradesTransferService.Read(tmimaId, studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GradesTransfer_Create([DataSourceRequest] DataSourceRequest request,
                    [Bind(Prefix = "models")]IEnumerable<StudentGradesViewModel> data, int tmimaId = 0, int studentId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            if (tmimaId == 0 || studentId == 0)
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε τμήμα και σπουδαστή. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    string errmsg = Common.ValidateGrades(item);
                    if (!string.IsNullOrEmpty(errmsg))
                    {
                        ModelState.AddModelError("", "Βρέθηκαν βαθμολογίες που δεν είναι έγυρες (< 0 ή > 10). Η καταχώρηση ακυρώθηκε.");
                        return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                    }
                    if (Common.CanCreateStudentGrade(studentId, tmimaId, item.ΚΩΔ_ΜΑΘΗΜΑ))
                    {
                        gradesTransferService.Create(item, tmimaId, studentId, schoolId);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Υπάρχει ήδη καταχωρημένη βαθμολογία για αυτό το σπουδαστή και μάθημα. Η καταχώρηση ακυρώθηκε");
                        return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GradesTransfer_Update([DataSourceRequest] DataSourceRequest request,
                            [Bind(Prefix = "models")]IEnumerable<StudentGradesViewModel> data, int tmimaId = 0, int studentId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            if (tmimaId == 0 || studentId == 0)
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε τμήμα και σπουδαστή. Η ενημέρωση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    string errmsg = Common.ValidateGrades(item);
                    if (!string.IsNullOrEmpty(errmsg))
                    {
                        ModelState.AddModelError("", "Βρέθηκαν βαθμολογίες που δεν είναι έγυρες (< 0 ή > 10). Η καταχώρηση ακυρώθηκε.");
                        return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                    }
                    gradesTransferService.Update(item, tmimaId, studentId, schoolId);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GradesTransfer_Destroy([DataSourceRequest] DataSourceRequest request,
                            [Bind(Prefix = "models")]IEnumerable<StudentGradesViewModel> data)
        {
            if (data.Any())
            {
                foreach (var item in data)
                {
                    gradesTransferService.Destroy(item);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TransferGrades(int tmimaId, int studentId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            string msg = "";
            int source_tmima = 0;
            int target_term = 0;
            int target_eidikotita = 0;

            var data1 = (from d in db.ΤΜΗΜΑ where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId select d).FirstOrDefault();
            if (data1 != null)
            {
                target_term = (int)data1.ΕΞΑΜΗΝΟ;
                target_eidikotita = (int)data1.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
            }

            var data2 = (from d in db.sqlΤΜΗΜΑΤΑ where d.ΙΕΚ == schoolId && d.EIDIKOTITA_ID == target_eidikotita && d.TERM_ID == target_term && d.ΤΜΗΜΑ_ΚΩΔ != tmimaId
                         orderby d.PERIOD_ID descending select d).First();
            if (data2 != null) source_tmima = data2.ΤΜΗΜΑ_ΚΩΔ;
            if (!(source_tmima > 0))
            {
                msg = "Δεν βρέθηκε τμήμα ίδιας ειδικότητας και εξαμήνου για μεταφορά βαθμολογιών";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

            var source = (from d in db.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ where d.ΚΩΔ_ΤΜΗΜΑ == source_tmima && d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId 
                          orderby d.LESSONS_IEK.LESSON_TEXT select d).ToList();

            if (source.Count > 0)
            {
                foreach (var item in source)
                {
                    if (Common.CanCreateStudentGrade(studentId, tmimaId, (int)item.ΚΩΔ_ΜΑΘΗΜΑ))
                    {
                        ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ entity = new ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ()
                        {
                            ΜΑΘΗΤΗΣ_ΚΩΔ = studentId,
                            ΙΕΚ = schoolId,
                            ΚΩΔ_ΤΜΗΜΑ = tmimaId,
                            ΑΜΚ = Common.GetStudentAmk(studentId),
                            ΚΩΔ_ΜΑΘΗΜΑ = item.ΚΩΔ_ΜΑΘΗΜΑ,
                            ΧΑΡΑΚΤΗΡΙΣΜΟΣ = item.ΧΑΡΑΚΤΗΡΙΣΜΟΣ,
                            ΒΑΘΜΟΣ_ΠΡΟΟΔΟΥ = item.ΒΑΘΜΟΣ_ΠΡΟΟΔΟΥ,
                            ΒΑΘΜΟΣ_ΤΕ = item.ΒΑΘΜΟΣ_ΤΕ,
                            ΒΑΘΜΟΣ_ΕΠ = item.ΒΑΘΜΟΣ_ΕΠ,
                            ΒΑΘΜΟΣ_ΜΟ = item.ΒΑΘΜΟΣ_ΜΟ
                        };
                        db.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ.Add(entity);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                msg = "Δεν βρέθηκαν βαθμολογίες του σπουδαστή για τμήμα ίδιου εξαμήνου με το τμήμα προορισμού";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveGrades(int tmimaId = 0, int studentId = 0)
        {
            string msg = "";

            var data = (from d in db.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId select d).ToList();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ entity = db.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ.Find(item.ΜΒ_ΚΩΔ);
                    if (entity != null)
                    {
                        db.Entry(entity).State = EntityState.Deleted;
                        db.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ.Remove(entity);
                        db.SaveChanges();
                    }
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertLessons(int tmimaId = 0, int studentId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            string msg = "";
            int target_term = 0;
            int target_eidikotita = 0;

            var data1 = (from d in db.ΤΜΗΜΑ where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId select d).FirstOrDefault();
            if (data1 != null)
            {
                target_term = (int)data1.ΕΞΑΜΗΝΟ;
                target_eidikotita = (int)data1.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
            }

            var source = (from d in db.sqlIEK_LESSONS
                          where d.IEK_ID == schoolId && d.EIDIKOTITA_ID == target_eidikotita && d.LESSON_TERM == target_term
                          orderby d.LESSON_DESC
                          select d).ToList();

            if (source.Count > 0)
            {
                foreach (var item in source)
                {
                    if (Common.CanCreateStudentGrade(studentId, tmimaId, item.LESSON_ID))
                    {
                        ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ entity = new ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ()
                        {
                            ΜΑΘΗΤΗΣ_ΚΩΔ = studentId,
                            ΙΕΚ = schoolId,
                            ΚΩΔ_ΤΜΗΜΑ = tmimaId,
                            ΑΜΚ = Common.GetStudentAmk(studentId),
                            ΚΩΔ_ΜΑΘΗΜΑ = item.LESSON_ID,
                            ΧΑΡΑΚΤΗΡΙΣΜΟΣ = 1,
                            ΒΑΘΜΟΣ_ΠΡΟΟΔΟΥ = 0,
                            ΒΑΘΜΟΣ_ΤΕ = 0,
                            ΒΑΘΜΟΣ_ΕΠ = 0,
                            ΒΑΘΜΟΣ_ΜΟ = 0
                        };
                        db.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ.Add(entity);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                msg = "Δεν βρέθηκαν μαθήματα για την ειδικότητα και εξάμηνο του τμήματος προορισμού.";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΒΑΘΜΟΛΟΓΙΕΣ ΕΡΓΑΣΙΩΝ (ΝΕΟ 29-11-2022)

        public ActionResult ErgasiesGrades()
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

            if (!IekTmimataExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            PopulateStudents(schoolId);
            PopulateErgasiesTypes();
            return View();
        }

        public ActionResult Ergasies_Read([DataSourceRequest] DataSourceRequest request, int tmimaId = 0, int lessonId = 0)
        {
            string lessonText = Common.GetLessonNameFromRowId(lessonId);

            IEnumerable<ErgasiaGradeViewModel> data = ergasiaGradeService.Read(tmimaId, lessonText);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ergasies_Create([DataSourceRequest] DataSourceRequest request,
                    [Bind(Prefix = "models")] IEnumerable<ErgasiaGradeViewModel> data, int tmimaId = 0, int lessonId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            if (tmimaId == 0 || lessonId == 0)
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε τμήμα και μάθημα. Η καταχώρηση ακυρώθηκε.");

            string lessonText = Common.GetLessonNameFromRowId(lessonId);

            if (data != null && ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    string errmsg = Common.ValidateErgasiaGrade(item);
                    if (!string.IsNullOrEmpty(errmsg))
                    {
                        ModelState.AddModelError("", "Βρέθηκαν βαθμολογίες που δεν είναι έγυρες (< 0 ή > 10). Η καταχώρηση ακυρώθηκε.");
                        return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                    }
                    if (Common.CanCreateErgasiaGrade((int)item.STUDENT_ID, tmimaId, lessonText))
                    {
                        ergasiaGradeService.Create(item, tmimaId, lessonText, schoolId);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Υπάρχει ήδη καταχωρημένη βαθμολογία για αυτό το σπουδαστή και μάθημα. Η καταχώρηση ακυρώθηκε");
                        return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ergasies_Update([DataSourceRequest] DataSourceRequest request,
                    [Bind(Prefix = "models")] IEnumerable<ErgasiaGradeViewModel> data, int tmimaId = 0, int lessonId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            if (tmimaId == 0 || lessonId == 0)
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε τμήμα και μάθημα. Η ενημέρωση ακυρώθηκε.");

            string lessonText = Common.GetLessonNameFromRowId(lessonId);

            if (data != null && ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    string errmsg = Common.ValidateErgasiaGrade(item);
                    if (!string.IsNullOrEmpty(errmsg))
                    {
                        ModelState.AddModelError("", "Βρέθηκαν βαθμολογίες που δεν είναι έγυρες (< 0 ή > 10). Η καταχώρηση ακυρώθηκε.");
                        return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                    }
                    ergasiaGradeService.Update(item, tmimaId, lessonText, schoolId);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ergasies_Destroy([DataSourceRequest] DataSourceRequest request,
                    [Bind(Prefix = "models")] IEnumerable<ErgasiaGradeViewModel> data)
        {
            if (data.Any())
            {
                foreach (var item in data)
                {
                    ergasiaGradeService.Destroy(item);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        public ActionResult InsertErgasies(int tmimaId = 0, int lessonId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            string msg = "";

            if (tmimaId == 0 || lessonId == 0)
            {
                msg = "Πρέπει πρώτα να επιλέξετε τμήμα και μάθημα";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

            string lessonText = Common.GetLessonNameFromRowId(lessonId);
            var students = (from d in db.sqlSTUDENTS_TMIMA_ACTIVE where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ select d).ToList();

            if (students.Count > 0)
            {
                foreach (var item in students)
                {
                    if (Common.CanCreateErgasiaGrade(item.STUDENT_ID, tmimaId, lessonText))
                    {
                        STUDENT_ERGASIES entity = new STUDENT_ERGASIES()
                        {
                            LESSON_TEXT = lessonText,
                            IEK = schoolId,
                            TMIMA_ID = tmimaId,
                            STUDENT_ID = item.STUDENT_ID,
                            ERGASIA_TYPE = 1,
                            GRADE = 0,
                            EIDIKOTITA_ID = Common.GetEidikotitaFromTmima(tmimaId),
                            TERM_ID = Common.GetTermFromTmima(tmimaId)
                        };
                        db.STUDENT_ERGASIES.Add(entity);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                msg = "Δεν βρέθηκαν σπουδαστές για το τμήμα αυτό.";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteErgasies(int tmimaId = 0, int lessonId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            string msg = "";

            string lessonText = Common.GetLessonNameFromRowId(lessonId);
            var data = (from d in db.STUDENT_ERGASIES where d.TMIMA_ID == tmimaId && d.LESSON_TEXT == lessonText select d).ToList();

            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    STUDENT_ERGASIES entity = db.STUDENT_ERGASIES.Find(item.ERGASIA_ID);
                    if (entity != null)
                    {
                        db.Entry(entity).State = EntityState.Deleted;
                        db.STUDENT_ERGASIES.Remove(entity);
                        db.SaveChanges();
                    }
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        // Lesson Combo data source
        public ActionResult ErgasiesLessons_Read(int eidikotitaId, int termId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            var data = db.qryIEK_EIDIKOTITA_LESSON_TEXT.AsQueryable();

            if (eidikotitaId > 0 && termId > 0)
            {
                data = db.qryIEK_EIDIKOTITA_LESSON_TEXT
                        .Where(f => f.LESSON_EIDIKOTITA == eidikotitaId && f.LESSON_TERM == termId && f.IEK_ID == schoolId)
                        .OrderBy(d => d.LESSON_TEXT);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // Editor Template for Ergasia type combo
        public ActionResult ErgasiaType_Read()
        {
            var data = (from d in db.ΘΕΜΑΤΙΚΕΣ_ΕΙΔΗ select d).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΩΡΟΛΟΓΙΟ ΠΡΟΓΡΑΜΜΑ ΝΕΟ (18/11/2018)

        public ActionResult GetWeekNumbers()
        {
            var data = (from d in db.NUMBERS
                        select new NumbersViewModel
                        {
                            NUMBER = d.NUMBER
                        }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FilteredLessonsRead(int eidikotitaId, int termId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = db.qryIEK_EIDIKOTITES_LESSONS
                .Where(f => f.LESSON_EIDIKOTITA == eidikotitaId && f.LESSON_TERM == termId && f.IEK_ID == schoolId)
                .OrderBy(d => d.LESSON_DESC);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PeriodTeachersRead(int periodId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = (from d in db.sqlTEACHERS_IN_PERIODS
                        where d.ΙΕΚ == schoolId && d.PERIOD_ID == periodId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new sqlTeacherInPeriodViewModel
                        {
                            TEACHER_ID = d.TEACHER_ID,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProgrammaDay()
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

            if (!IekTmimataExist(schoolId) || !TeachersExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και εκπαιδευτικοί.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            if (!TeacherWithPeriodsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένες περίοδοι απασχόλησης των εκπαιδευτικών.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            PopulateErgasies();
            PopulateWeeks();
            PopulateHours();
            PopulateLessons(schoolId);
            PopulateTeachersInPeriods(schoolId);

            return View();
        }


        #region GRID CRUD FUNCTIONS

        public ActionResult Programma_Read([DataSourceRequest] DataSourceRequest request, int tmimaId = 0, DateTime? theDate = null)
        {
            IEnumerable<ProgrammaDayViewModel> data = programmaService.Read(tmimaId, theDate);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Programma_Create([DataSourceRequest] DataSourceRequest request, 
                           [Bind(Prefix = "models")]IEnumerable<ProgrammaDayViewModel> data, int tmimaId = 0, DateTime? theDate = null, int week = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            if (tmimaId == 0 || theDate == null || week == 0)
                ModelState.AddModelError("", "Για να γίνει η καταχώρηση πρέπει πρώτα να έχετε επιλέξει τμήμα, ημερομηνία και εβδομάδα. Η διαδικασία ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    if (Common.ProgrammaItemExists(item))
                    {
                        ModelState.AddModelError("", "Έγινε απόπειρα δημιουργίας διπλοεγγραφής. Η διπλή καταχώρηση ακυρώθηκε.");
                        return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                    }
                    string err_msg = Common.ValidateLesson(item.ΚΩΔ_ΜΑΘΗΜΑ, item.ΚΩΔ_ΕΡΓΑΣΙΑ);
                    if (err_msg != "")
                    {
                        ModelState.AddModelError("", err_msg);
                        return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                    }
                    programmaService.Create(item, tmimaId, theDate, week, schoolId);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Programma_Update([DataSourceRequest] DataSourceRequest request, 
                            [Bind(Prefix = "models")]IEnumerable<ProgrammaDayViewModel> data, int tmimaId = 0, DateTime? theDate = null, int week = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            if (tmimaId == 0 || theDate == null || week == 0)
                ModelState.AddModelError("", "Για να γίνει η καταχώρηση πρέπει πρώτα να έχετε επιλέξει τμήμα, ημερομηνία και εβδομάδα. Η διαδικασία ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    string err_msg = Common.ValidateLesson(item.ΚΩΔ_ΜΑΘΗΜΑ, item.ΚΩΔ_ΕΡΓΑΣΙΑ);
                    if (err_msg != "")
                    {
                        ModelState.AddModelError("", err_msg);
                        return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                    }
                    programmaService.Update(item, tmimaId, theDate, week, schoolId);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Programma_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProgrammaDayViewModel> data)
        {
            if (data.Any())
            {
                foreach (var item in data)
                {
                    programmaService.Destroy(item);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS


        #region AUTOMATION

        public ActionResult GetWeekOfDate(ProgrammaParameters parameters)
        {
            string lastweek = "";

            int tmimaId = parameters.tmimaId;
            DateTime theDate = parameters.theDate;

            var data = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ
                        where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.ΗΜΕΡΟΜΗΝΙΑ == theDate
                        select d).ToList();

            if (data.Count > 0) lastweek = data.First().ΕΒΔΟΜΑΔΑ.ToString();

            return Json(lastweek, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLastSavedWeek(int tmimaId = 0)
        {
            string lastweek = "";

            var data = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId
                        orderby d.ΕΒΔΟΜΑΔΑ descending
                        select d).ToList();

            if (data.Count > 0) lastweek = data.First().ΕΒΔΟΜΑΔΑ.ToString();

            return Json(lastweek, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TransferHour(ProgrammaParameters parameters)
        {
            string msg = "";
            int tmimaId = parameters.tmimaId;
            DateTime theDate = parameters.theDate;

            IEnumerable<ProgrammaDayViewModel> data = programmaService.Read(tmimaId, theDate);

            if (data.Count() == 0)
            {
                msg = "Δεν βρέθηκαν καταχωρήσεις για μεταφορά της τελευταίας ώρας στην επόμενη.";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            // get the last saved hour and increment
            var item = data.OrderByDescending(x => x.ΩΡΑ).First();
            int nextHour = item.ΩΡΑ + 1;

            ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ target = new ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ()
            {
                ΕΒΔΟΜΑΔΑ = item.ΕΒΔΟΜΑΔΑ,
                ΕΚΠΑΙΔΕΥΤΗΣ1 = item.ΕΚΠΑΙΔΕΥΤΗΣ1,
                ΕΚΠΑΙΔΕΥΤΗΣ2 = item.ΕΚΠΑΙΔΕΥΤΗΣ2,
                ΕΚΠΑΙΔΕΥΤΗΣ3 = item.ΕΚΠΑΙΔΕΥΤΗΣ3,
                ΗΜΕΡΟΜΗΝΙΑ = item.ΗΜΕΡΟΜΗΝΙΑ,
                ΙΕΚ = item.ΙΕΚ,
                ΚΩΔ_ΕΡΓΑΣΙΑ = item.ΚΩΔ_ΕΡΓΑΣΙΑ,
                ΚΩΔ_ΜΑΘΗΜΑ = item.ΚΩΔ_ΜΑΘΗΜΑ,
                ΚΩΔ_ΤΜΗΜΑ = item.ΚΩΔ_ΤΜΗΜΑ,
                Π1 = item.Π1,
                Π2 = item.Π2,
                Π3 = item.Π3,
                ΩΡΑ = nextHour
            };
            db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ.Add(target);
            db.SaveChanges();

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TransferDay(ProgrammaParameters parameters)
        {
            string msg = "";

            int tmimaId = parameters.tmimaId;
            DateTime srcDate = parameters.theDate.AddDays(-7);
            DateTime targetDate = parameters.theDate;

            var targetData = programmaService.Read(tmimaId, targetDate);
            if (targetData.Count() > 0)
            {
                msg = "Βρέθηκε ήδη καταχωρημένο ωρολόγιο πρόγραμμα του τμήματος στην " + targetDate.ToString("dd/MM/yyyy") + ". Η μεταφορά ακυρώθηκε.";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

            var sourceData = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ
                              where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.ΗΜΕΡΟΜΗΝΙΑ == srcDate
                              orderby d.ΩΡΑ ascending
                              select d).ToList();

            if (sourceData.Count() == 0)
            {
                msg = "Δεν βρέθηκε καταχωρημένο ωρολόγιο πρόγραμμα του τμήματος στην ημερομηνία προέλευσης. Δεν υπάρχουν δεδομένα για μεταφορά.";
                return Json(msg, JsonRequestBehavior.AllowGet);
            };

            foreach (var d in sourceData)
            {
                ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ target = new ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ()
                {
                    ΕΒΔΟΜΑΔΑ = d.ΕΒΔΟΜΑΔΑ + 1,
                    ΗΜΕΡΟΜΗΝΙΑ = targetDate,
                    ΕΚΠΑΙΔΕΥΤΗΣ1 = d.ΕΚΠΑΙΔΕΥΤΗΣ1,
                    ΕΚΠΑΙΔΕΥΤΗΣ2 = d.ΕΚΠΑΙΔΕΥΤΗΣ2,
                    ΕΚΠΑΙΔΕΥΤΗΣ3 = d.ΕΚΠΑΙΔΕΥΤΗΣ3,
                    ΙΕΚ = d.ΙΕΚ,
                    ΚΩΔ_ΕΡΓΑΣΙΑ = d.ΚΩΔ_ΕΡΓΑΣΙΑ,
                    ΚΩΔ_ΜΑΘΗΜΑ = d.ΚΩΔ_ΜΑΘΗΜΑ,
                    ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ,
                    Π1 = d.Π1,
                    Π2 = d.Π2,
                    Π3 = d.Π3,
                    ΩΡΑ = d.ΩΡΑ
                };
                db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ.Add(target);
                db.SaveChanges();
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TransferWeek(ProgrammaParameters parameters)
        {
            string msg = "";
            int tmimaId = parameters.tmimaId;

            int count = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId orderby d.ΕΒΔΟΜΑΔΑ descending, d.ΗΜΕΡΟΜΗΝΙΑ ascending select d).Count();
            // no data to transfer
            if (count == 0)
            {
                msg = "Δεν βρέθηκε καταχωρημένο ωρολόγιο πρόγραμμα του τμήματος για να γίνει η μεταφορά εβδομάδας.";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            // data exist for transfer - get last saved week
            var source = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ
                          where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId
                          orderby d.ΕΒΔΟΜΑΔΑ descending, d.ΗΜΕΡΟΜΗΝΙΑ ascending
                          select d).First();

            int sourceWeek = (int)source.ΕΒΔΟΜΑΔΑ;
            DateTime initialDate = source.ΗΜΕΡΟΜΗΝΙΑ;
            DateTime targetStartDate = initialDate.AddDays(7);
            DateTime targetFinalDate = targetStartDate.AddDays(4);

            // Ελέγχουμε αν υπερβαίνει η αρχική ημερομηνία-στόχος τη λήξη του εξαμήνου.
            var tmimaInfo = (from d in db.sqlΤΜΗΜΑΤΑ_ΧΕ where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId select d).FirstOrDefault();
            DateTime dateLimit = (DateTime)tmimaInfo.ΗΜΝΙΑ_ΛΗΞΗΣ;
            if (targetFinalDate > dateLimit)
            {
                msg = "Η ημερομηνία πέρατος της επόμενης εβδομάδας [" + targetFinalDate.ToString("dd/MM/yyyy") +
                      "] είναι μεγαλύτερη της ημ/νίας λήξης της περιόδου.<br/>Η τελευταία εβδομάδα πρέπει να καταχωρηθεί με το χέρι ή με μεταφορά ημέρας όπου εφικτό.";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

            msg = HolidayDatesStopper(initialDate, targetStartDate, targetFinalDate);
            if (msg != "")
                return Json(msg, JsonRequestBehavior.AllowGet);

            // Στο σημείο αυτό όλα είναι εντάξει για τη μεταφορά της εβδομάδας προέλευσης στην επόμενη.
            var sourceData = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ
                              where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.ΕΒΔΟΜΑΔΑ == sourceWeek
                              orderby d.ΗΜΕΡΟΜΗΝΙΑ ascending, d.ΩΡΑ ascending
                              select d).ToList();

            foreach (var d in sourceData)
            {
                ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ target = new ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ()
                {
                    ΕΒΔΟΜΑΔΑ = d.ΕΒΔΟΜΑΔΑ + 1,
                    ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ.AddDays(7),
                    ΕΚΠΑΙΔΕΥΤΗΣ1 = d.ΕΚΠΑΙΔΕΥΤΗΣ1,
                    ΕΚΠΑΙΔΕΥΤΗΣ2 = d.ΕΚΠΑΙΔΕΥΤΗΣ2,
                    ΕΚΠΑΙΔΕΥΤΗΣ3 = d.ΕΚΠΑΙΔΕΥΤΗΣ3,
                    ΙΕΚ = d.ΙΕΚ,
                    ΚΩΔ_ΕΡΓΑΣΙΑ = d.ΚΩΔ_ΕΡΓΑΣΙΑ,
                    ΚΩΔ_ΜΑΘΗΜΑ = d.ΚΩΔ_ΜΑΘΗΜΑ,
                    ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ,
                    Π1 = d.Π1,
                    Π2 = d.Π2,
                    Π3 = d.Π3,
                    ΩΡΑ = d.ΩΡΑ
                };
                db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ.Add(target);
                db.SaveChanges();
            }
            msg = "Η μεταφορά του προγράμματος της τελευταίας καταχωρημένης εβδόμάδας [" + sourceWeek + "]<br/>με ημ/νία έναρξης [" +
                   initialDate.ToString("dd/MM/yyyy") + "] στην επόμενη με ημ/νία έναρξης [" + targetStartDate.ToString("dd/MM/yyyy") + "] ολοκληρώθηκε.";

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public string HolidayDatesStopper(DateTime initialDate, DateTime targetStartDate, DateTime targetFinalDate)
        {
            string msg = "";

            bool checkForChristmas = false;
            bool checkForEaster = false;

            DateTime easterStart;
            DateTime easterEnd;
            DateTime christmasStart;
            DateTime christmasEnd;

            int year = initialDate.Year;
            // Βρίσκουμε τις ημερομηνίες διακοπών Χριστουγέννων και Πάσχα.
            DateTime EasterDate = Common.GetOrthodoxEaster(year);
            Common.GetChristmasDates(year, out christmasStart, out christmasEnd);
            Common.GetEasterDates(EasterDate, out easterStart, out easterEnd);

            if (initialDate > easterEnd && initialDate <= christmasStart) checkForChristmas = true;
            else if (initialDate <= easterStart) checkForEaster = true;

            // Ελέγχουμε εάν η εβδομάδα προορισμού πέφτει μέσα στις διακοπές Χριστουγέννων ή Πάσχα.
            if (checkForChristmas)
            {
                bool p1 = targetFinalDate >= christmasStart && targetFinalDate <= christmasEnd;     // intersects holidays (final date in christmas)
                bool p2 = targetStartDate >= christmasStart && targetStartDate <= christmasEnd;     // intersects holidays (initial date in christmas)
                bool p3 = targetStartDate >= christmasStart && targetFinalDate <= christmasEnd;     // inside holidays
                bool p4 = targetStartDate <= christmasStart && targetFinalDate >= christmasEnd;     // encloses holidays

                if (p1 || p2 || p3 || p4)
                {
                    msg = "Οι ημερομηνίες της επόμενης εβδομάδας με έναρξη [" + targetStartDate.ToString("dd/MM/yyyy") +
                          "] βρίσκονται μέσα στις διακοπές Χριστουγέννων.<br/>Η εβδομάδα πριν και μετά τις διακοπές πρέπει να καταχωρηθεί με το χέρι ή με μεταφορά ημέρας όπου εφικτό.";
                }
            }
            if (checkForEaster)
            {
                bool p1 = targetFinalDate >= easterStart && targetFinalDate <= easterEnd;     // intersects holidays (final date in easter)
                bool p2 = targetStartDate >= easterStart && targetStartDate <= easterEnd;     // intersects holidays (initial date in easter)
                bool p3 = targetStartDate >= easterStart && targetFinalDate <= easterEnd;     // inside holidays
                bool p4 = targetStartDate <= easterStart && targetFinalDate >= easterEnd;     // encloses holidays

                if (p1 || p2 || p3 || p4)
                {
                    msg = "Οι ημερομηνίες της επόμενης εβδομάδας με έναρξη [" + targetStartDate.ToString("dd/MM/yyyy") +
                          "] βρίσκονται μέσα στις διακοπές Πάσχα.<br/>Η εβδομάδα πριν και μετά τις διακοπές πρέπει να καταχωρηθεί με το χέρι ή με μεταφορά ημέρας όπου εφικτό.";
                }
            }
            return (msg);
        }


        #region TEST - 08/12/2018

        public ActionResult TransferWeekCustom(int tmimaId, int weekId, DateTime theDate)
        {
            string msg = "";

            if (!Common.ProgrammaTmimaExists(tmimaId))
            {
                msg = "Δεν βρέθηκε καταχωρημένο ωρολόγιο πρόγραμμα του τμήματος για να γίνει η μεταφορά εβδομάδας.";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            // Βρίσκουμε το πρόγραμμα της εβδομάδας προέλευσης
            var source = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ
                          where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.ΕΒΔΟΜΑΔΑ == weekId
                          orderby d.ΗΜΕΡΟΜΗΝΙΑ ascending, d.ΩΡΑ ascending
                          select d).ToList();

            if (source.Count == 0)
            {
                msg = "Δεν βρέθηκε καταχωρημένο ωρολόγιο πρόγραμμα του τμήματος στην [" + weekId + "η] εβδομάδα για να γίνει η μεταφορά.";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

            int targetWeek = 0;
            int lastWeek = LastSavedWeekNumber(tmimaId);
            if (lastWeek > 0) targetWeek = lastWeek + 1; 
            
            DateTime initialDate = source.First().ΗΜΕΡΟΜΗΝΙΑ;
            string DayString = Common.WeekdayToString((int)initialDate.DayOfWeek);

            if ((int)initialDate.DayOfWeek > 1)
            {
                msg = "Η εβδομάδα προέλευσης πρέπει να αρχίζει από Δευτέρα ασχέτως αν η αρχική ημερομηνία προορισμού δεν είναι Δευτέρα.";
                msg += "Η εβδομάδα [" + weekId + "] που επιλέξατε αρχίζει από [" + DayString + "]";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

            DateTime targetStartDate = theDate;
            DateTime targetFinalDate = theDate.AddDays(4);

            // Η εδβομάδα προορισμού πρέπει να είναι κενή, αλλιώς δεν μπορεί να γίνει μεταφορά.
            var target = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ
                          where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && (d.ΗΜΕΡΟΜΗΝΙΑ >= targetStartDate && d.ΗΜΕΡΟΜΗΝΙΑ <= targetFinalDate)
                          select d).ToList();
            if (target.Count > 0)
            {
                msg = "Υπάρχει ήδη καταχωρημένο ωρολόγιο πρόγραμμα του τμήματος στην [" + targetStartDate.ToString("dd/MM/yyy") + " - " + 
                       targetFinalDate.ToString("dd/MM/yyy") + "] εβδομάδα. Η μεταφορά ακυρώθηκε.";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

            int weekDay = (int)targetStartDate.DayOfWeek;
            int daysToSkip = 0;
            int daysOffset = (int)(targetStartDate - initialDate).TotalDays;

            // Εάν η εβδομάδα προορισμού δεν αρχίζει από Δευτέρα προσαρμόζουμε την προέλευση
            // ώστε να αρχίζει από τη ίδια μέρα της εβδομάδας.
            if (weekDay > 1)
            {
                daysToSkip = weekDay - 1;
                initialDate = initialDate.AddDays(daysToSkip);
                daysOffset = (int)(targetStartDate - initialDate).TotalDays;

                source = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ
                          where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.ΕΒΔΟΜΑΔΑ == weekId && d.ΗΜΕΡΟΜΗΝΙΑ >= initialDate
                          orderby d.ΗΜΕΡΟΜΗΝΙΑ ascending, d.ΩΡΑ ascending
                          select d).ToList();

                if (source.Count == 0)
                {
                    msg = "Η διαμορφωμένη εβδομάδα προέλευσης με ημερομηνία έναρξης " + initialDate.ToString("dd/MM/yyy") + " δεν περιέχει δεδομένα για μεταφορά.";
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }

            // Στο σημείο αυτό όλα είναι εντάξει για τη μεταφορά της εβδομάδας προέλευσης στην επόμενη.
            // ΜΕΤΑΦΕΡΕΙ ΚΑΘΕ ΩΡΑ 2 ΦΟΡΕΣ ???? ΣΥΝΕΒΗ ΜΙΑ ΦΟΡΑ
            foreach (var d in source)
            {
                DateTime currentDate = d.ΗΜΕΡΟΜΗΝΙΑ.AddDays(daysOffset);
                if ((int)currentDate.DayOfWeek <= 5)
                {
                    ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ entity = new ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ()
                    {
                        ΕΒΔΟΜΑΔΑ = targetWeek,
                        ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ.AddDays(daysOffset),
                        ΕΚΠΑΙΔΕΥΤΗΣ1 = d.ΕΚΠΑΙΔΕΥΤΗΣ1,
                        ΕΚΠΑΙΔΕΥΤΗΣ2 = d.ΕΚΠΑΙΔΕΥΤΗΣ2,
                        ΕΚΠΑΙΔΕΥΤΗΣ3 = d.ΕΚΠΑΙΔΕΥΤΗΣ3,
                        ΙΕΚ = d.ΙΕΚ,
                        ΚΩΔ_ΕΡΓΑΣΙΑ = d.ΚΩΔ_ΕΡΓΑΣΙΑ,
                        ΚΩΔ_ΜΑΘΗΜΑ = d.ΚΩΔ_ΜΑΘΗΜΑ,
                        ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ,
                        Π1 = d.Π1,
                        Π2 = d.Π2,
                        Π3 = d.Π3,
                        ΩΡΑ = d.ΩΡΑ
                    };
                    db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ.Add(entity);
                    db.SaveChanges();
                }
            }
            msg = "Η μεταφορά του προγράμματος της επιλεγμένης εβδόμάδας [" + weekId + "] με ημ/νία έναρξης [" +
                   initialDate.ToString("dd/MM/yyyy") + "] στην [" + targetWeek + "η] εβδομάδα με ημ/νία έναρξης [" + targetStartDate.ToString("dd/MM/yyyy") + "] ολοκληρώθηκε.";

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public int LastSavedWeekNumber(int tmimaId)
        {
            int weekNumber = 0;
            var data = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ
                        where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId
                        orderby d.ΕΒΔΟΜΑΔΑ descending, d.ΗΜΕΡΟΜΗΝΙΑ ascending, d.ΩΡΑ ascending
                        select d).First();

            if (data != null)
            {
                weekNumber = (int)data.ΕΒΔΟΜΑΔΑ;
            }
            return (weekNumber);
        }

        #endregion TEST


        #endregion AUTOMATION

        #endregion


        #region ΕΛΕΓΧΟΣ ΩΡΟΛΟΓΙΟΥ ΠΡΟΓΡΑΜΜΑΤΟΣ

        public ActionResult GetTmimata()
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = (from d in db.sqlΤΜΗΜΑΤΑ
                        where d.ΙΕΚ == schoolId && d.TERM_ID < 5 && d.PERIOD_ID >= PERIOD_ARCHIVE
                        orderby d.ΠΕΡΙΟΔΟΣ descending, d.ΤΜΗΜΑ_ΟΝΟΜΑ
                        select new sqlTmimataViewModel
                        {
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TmimaLessonsRead(int tmimaId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            int? eidikotita = 0;
            int? term = 0;

            var data = (from d in db.ΤΜΗΜΑ where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId select d).FirstOrDefault();
            if (data != null)
            {
                eidikotita = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
                term = data.ΕΞΑΜΗΝΟ;
            }
            var lessons = (from d in db.qryIEK_EIDIKOTITES_LESSONS where d.LESSON_EIDIKOTITA == eidikotita && d.LESSON_TERM == term && d.IEK_ID == schoolId select d).ToList();
            return Json(lessons, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TmimaTeachersRead(int tmimaId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            int? periodId = 0;

            var data1 = (from d in db.ΤΜΗΜΑ where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId select d).FirstOrDefault();
            if (data1 != null)
            {
                periodId = data1.ΠΕΡΙΟΔΟΣ_ΚΩΔ;
            }

            var data = (from d in db.sqlTEACHERS_IN_PERIODS
                        where d.ΙΕΚ == schoolId && d.PERIOD_ID == periodId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new sqlTeacherInPeriodViewModel
                        {
                            TEACHER_ID = d.TEACHER_ID,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProgramma(int tmimaId = 0, string theDate1 = null, string theDate2 = null)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            string msg = "";
            DateTime dtDate1, dtDate2;

            bool result1 = DateTime.TryParse(theDate1, out dtDate1);
            bool result2 = DateTime.TryParse(theDate2, out dtDate2);

            if (tmimaId > 0 && result1 && result2)
            {
                var data = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.ΗΜΕΡΟΜΗΝΙΑ >= dtDate1 && d.ΗΜΕΡΟΜΗΝΙΑ <= dtDate2 select d).ToList();
                if (data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ entity = db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ.Find(item.ΠΡΟΓΡΑΜΜΑ_ΚΩΔ);
                        if (entity != null)
                        {
                            db.Entry(entity).State = EntityState.Deleted;
                            db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ.Remove(entity);
                            db.SaveChanges();
                        }
                    }
                    msg = "Η διαγραφή του επιλεγμένου προγράμματος ολοκληρώθηκε με επιτυχία.";
                }
                else
                {
                    msg = "Δεν βρέθηκε καταχωρημένο πρόγραμμα για το επιλεγμένο τμήμα στις ημερομηνίες αυτές.";
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProgrammaDayCheck()
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

            if (!IekTmimataExist(schoolId) || !TeachersExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και εκπαιδευτικοί.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            if (!TeacherWithPeriodsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένες περίοδοι απασχόλησης των εκπαιδευτικών.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            PopulateTeachersWithPeriods(schoolId);
            PopulateErgasies();
            PopulateWeeks();
            PopulateHours();
            PopulateLessons(schoolId);

            return View();
        }


        #region GRID CRUD FUNCTIONS

        public ActionResult Programma2_Read([DataSourceRequest] DataSourceRequest request, int tmimaId = 0, DateTime? theDate1 = null, DateTime? theDate2 = null)
        {
            IEnumerable<ProgrammaDayViewModel> data = programmaService.Read(tmimaId, theDate1, theDate2);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Programma2_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProgrammaDayViewModel> data, int tmimaId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            if (tmimaId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    foreach (var item in data)
                    {
                        string err_msg = Common.ValidateLesson(item.ΚΩΔ_ΜΑΘΗΜΑ, item.ΚΩΔ_ΕΡΓΑΣΙΑ);
                        if (err_msg != "")
                        {
                            ModelState.AddModelError("", err_msg);
                            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                        }
                        programmaService.Create(item, tmimaId, schoolId);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να γίνει επιλογή τμήματος. Η αποθήκευση ακυρώθηκε.");
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Programma2_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProgrammaDayViewModel> data, int tmimaId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            if (tmimaId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    foreach (var item in data)
                    {
                        string err_msg = Common.ValidateLesson(item.ΚΩΔ_ΜΑΘΗΜΑ, item.ΚΩΔ_ΕΡΓΑΣΙΑ);
                        if (err_msg != "")
                        {
                            ModelState.AddModelError("", err_msg);
                            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                        }
                        programmaService.Update(item, tmimaId, schoolId);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να γίνει επιλογή τμήματος. Η αποθήκευση ακυρώθηκε.");
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Programma2_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProgrammaDayViewModel> data)
        {
            if (data.Any())
            {
                foreach (var item in data)
                {
                    programmaService.Destroy(item);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        #endregion GRID CRUD FUNCTIONS


        #endregion


        #region ΒΕΚ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult BekStudents_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetBekStudentsTable();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<bekStudentsTableViewModel> GetBekStudentsTable()
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = (from d in db.bek_STUDENTS_TABLE
                        where d.ΙΕΚ == schoolId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new bekStudentsTableViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            ΑΜΚ = d.ΑΜΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ ?? 0,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            TERM = d.TERM,
                            ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ
                        }).ToList();
            return data;
        }

        public ActionResult StudentBek(string notify = null)
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

            if (!IekTmimataBekExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα Δ' εξαμήνου ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        #region GRID CRUD FUNCTIONS

        public ActionResult Bek_Read([DataSourceRequest] DataSourceRequest request, int studentId)
        {
            IEnumerable<StudentBekViewModel> data = bekService.Read(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Bek_Create([DataSourceRequest] DataSourceRequest request, StudentBekViewModel data, int studentId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentBekViewModel newdata = new StudentBekViewModel();
            if (!(studentId > 0)) ModelState.AddModelError("", "Για να γίνει η καταχώρηση πρέπει πρώτα να επιλέξετε σπουδαστή.");

            if (data != null && ModelState.IsValid)
            {
                bekService.Create(data, studentId, schoolId);
                newdata = bekService.Refresh(data.ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Bek_Update([DataSourceRequest] DataSourceRequest request, StudentBekViewModel data, int studentId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            StudentBekViewModel newdata = new StudentBekViewModel();
            if (!(studentId > 0)) ModelState.AddModelError("", "Για να γίνει η καταχώρηση πρέπει πρώτα να επιλέξετε σπουδαστή.");

            if (data != null && ModelState.IsValid)
            {
                bekService.Update(data, studentId, schoolId);
                newdata = bekService.Refresh(data.ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Bek_Destroy([DataSourceRequest] DataSourceRequest request, StudentBekViewModel data)
        {
            if (data != null)
            {
                bekService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #region BEK EDIT FORM

        public ActionResult StudentBekEdit(int bekId)
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

            if (!(bekId > 0))
            {
                string msg = "Ο κωδικός της εγγραφής δεν είναι έγκυρος. Η εγγραφή πρέπει να έχει αποθηκευτεί πρώτα.";
                return RedirectToAction("ErrorData", "Programma", new { notify = msg });
            }

            StudentBekViewModel bek = bekService.Refresh(bekId);
            if (bek == null)
            {
                string msg = "Παρουσιάστηκε πρόβλημα εύρεσης των στοιχείων ΒΕΚ.";
                return RedirectToAction("ErrorData", "Programma", new { notify = msg });
            }
            return View(bek);
        }

        [HttpPost]
        public ActionResult StudentBekEdit(int bekId, StudentBekViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            if (data != null)
            {
                bekService.UpdateRecord(data, bekId, schoolId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                StudentBekViewModel newdata = bekService.Refresh(bekId);
                return View(newdata);
            }
            else
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
                return View(data);
            }
        }

        #endregion BEK EDIT FORM


        // NEW ADDITION 14-11-2018

        public ActionResult StudentBekData(string notify = null)
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

            if (!IekTmimataBekExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα Δ' εξαμήνου ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateIekTmimataBek(schoolId);
            return View();
        }

        public ActionResult BekRegistry_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            IEnumerable<StudentBekViewModel> data = bekService.ReadAll(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΒΕΒΑΙΩΣΕΙΣ ΕΚΠΑΙΔΕΥΤΩΝ

        public ActionResult TeacherBebeoseis(string notify = null)
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
            int teachers = (from d in db.sqlTEACHERS_WITH_PERIODS where d.ΙΕΚ == schoolId orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ select d).Count();
            if (teachers == 0)
            {
                string msg = "Δεν βρέθηκαν καταχωρημένες περίοδοι απασχόλησης εκπαιδευτών. Χωρίς αυτές δεν μπορούν να βγουν βεβαιώσεις.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            // ok to populate combos
            PopulateSchoolYears();
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        #region GRID CRUD FUNCTIONS

        public ActionResult TeacherBebeosi_Read([DataSourceRequest] DataSourceRequest request, int teacherId)
        {
            IEnumerable<TeacherBebeoseisViewModel> data = bebeosiTeacherService.Read(teacherId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TeacherBebeosi_Create([DataSourceRequest] DataSourceRequest request, TeacherBebeoseisViewModel data, int teacherId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            TeacherBebeoseisViewModel newdata = new TeacherBebeoseisViewModel();

            if (!(teacherId > 0)) ModelState.AddModelError("", "Για να γίνει η καταχώρηση πρέπει πρώτα να επιλέξετε εκπαιδευτικό.");

            if (data != null && ModelState.IsValid)
            {
                bebeosiTeacherService.Create(data, teacherId, schoolId);
                newdata = bebeosiTeacherService.Refresh(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TeacherBebeosi_Update([DataSourceRequest] DataSourceRequest request, TeacherBebeoseisViewModel data, int teacherId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            TeacherBebeoseisViewModel newdata = new TeacherBebeoseisViewModel();

            if (!(teacherId > 0)) ModelState.AddModelError("", "Για να γίνει η καταχώρηση πρέπει πρώτα να επιλέξετε εκπαιδευτικό.");

            if (data != null && ModelState.IsValid)
            {
                bebeosiTeacherService.Update(data, teacherId, schoolId);
                newdata = bebeosiTeacherService.Refresh(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TeacherBebeosi_Destroy([DataSourceRequest] DataSourceRequest request, TeacherBebeoseisViewModel data)
        {
            if (data != null)
            {
                bebeosiTeacherService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #region DATA FORM

        public ActionResult TeacherBebeosiEdit(int bebeosiId)
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
                return RedirectToAction("ErrorData", "Programma", new { notify = msg });
            }

            TeacherBebeoseisViewModel bebeosi = bebeosiTeacherService.Refresh(bebeosiId);
            int teacherId = (int)bebeosi.TEACHER_ID;

            sqlTEACHER_INFO selectedTeacher = Common.GetTeacherInfo(teacherId);
            if (selectedTeacher == null)
            {
                string notify = "Παρουσιάστηκε σφάλμα εύρεσης του εκπαιδευτικού.";
                return RedirectToAction("Index", "School", new { notify });
            }
            else
            {
                ViewBag.TeacherData = selectedTeacher;
            }
            return View(bebeosi);
        }

        [HttpPost]
        public ActionResult TeacherBebeosiEdit(int bebeosiId, TeacherBebeoseisViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }

            TeacherBebeoseisViewModel bebeosi = bebeosiTeacherService.Refresh(bebeosiId);
            int teacherId = (int)bebeosi.TEACHER_ID;

            sqlTEACHER_INFO selectedTeacher = Common.GetTeacherInfo(teacherId);
            if (selectedTeacher == null)
            {
                string notify = "Παρουσιάστηκε σφάλμα εύρεσης του εκπαιδευτικού.";
                return RedirectToAction("Index", "School", new { notify });
            }
            else
            {
                ViewBag.TeacherData = selectedTeacher;
            }
            if (data != null)
            {
                ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ entity = db.ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ.Find(bebeosiId);

                entity.ΓΙΑ_ΧΡΗΣΗ = data.ΓΙΑ_ΧΡΗΣΗ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                TeacherBebeoseisViewModel newdata = bebeosiTeacherService.Refresh(bebeosiId);
                return View(newdata);
            }
            else
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
                return View(data);
            }
        }

        #endregion DATA FORM

        #endregion


        #region ΠΑΡΟΥΣΙΟΛΟΓΙΑ

        public ActionResult TmimaParousiologio()
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

            if (!IekTmimataExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            return View();
        }

        public ActionResult StudentsTmima_Read([DataSourceRequest] DataSourceRequest request, int tmimaId)
        {
            var data = GetStudentTmimaFromDB(tmimaId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<StudentTmimaActiveViewModel> GetStudentTmimaFromDB(int tmimaId)
        {
            var data = (from d in db.sqlΜΑΘΗΤΕΣ_ΤΜΗΜΑ_ΕΝΕΡΓΟΙ
                        where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.ΠΑΤΡΩΝΥΜΟ
                        select new StudentTmimaActiveViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ ?? 0,
                            ΑΜΚ = d.ΑΜΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                            ΠΡΑΞΗ_ΕΞΟΔΟΥ = d.ΠΡΑΞΗ_ΕΞΟΔΟΥ,
                            ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΕΙΔΟΣ = d.ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΕΙΔΟΣ,
                        }).ToList();
            return data;
        }

        #endregion


        #region ΟΝΟΜΑΣΤΙΚΕΣ ΚΑΤΑΣΤΑΣΕΙΣ ΤΜΗΜΑΤΩΝ

        public ActionResult TmimaKatastasiGeniki()
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

            if (!IekTmimataExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            return View();
        }

        #endregion


        #region ΕΝΤΥΠΑ ΒΑΘΜΩΝ ΠΡΟΟΔΟΥ

        public ActionResult TmimaDocGradesProodos()
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

            if (!IekTmimataExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            return View();
        }

        #endregion


        #region ΕΝΤΥΠΑ ΒΑΘΜΩΝ ΤΕΛΙΚΗΣ ΕΞΕΤΑΣΗΣ

        public ActionResult TmimaDocGradesExam()
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

            if (!IekTmimataExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            return View();
        }

        #endregion


        #region ΕΝΤΥΠΟ ΣΥΓΚΕΝΤΡΩΤΙΚΩΝ ΑΠΟΥΣΙΩΝ

        public ActionResult StudentDocApousiesLesson()
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

            if (!IekTmimataExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            return View();
        }

        public ActionResult ApousiesLesson_Read([DataSourceRequest] DataSourceRequest request, int tmimaId)
        {
            var data = GetApousiesLessonFromDB(tmimaId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<StudentApousiesLessonViewModel> GetApousiesLessonFromDB(int tmimaId)
        {
            var data = (from d in db.sqlSTUDENTS_APOUSIES_LESSON
                        where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.LESSON_DESC
                        select new StudentApousiesLessonViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            LESSON_DESC = d.LESSON_DESC,
                            ΚΩΔ_ΜΑΘΗΜΑ = d.ΚΩΔ_ΜΑΘΗΜΑ,
                            ΑΠΟΥΣΙΕΣ = d.ΑΠΟΥΣΙΕΣ,
                            ΟΡΙΟ_15 = d.ΟΡΙΟ_15,
                            ΟΡΙΟ_20 = d.ΟΡΙΟ_20
                        }).ToList();
            return data;
        }

        #endregion


        #region ΕΝΤΥΠΟ ΑΝΑΛΥΤΙΚΩΝ ΑΠΟΥΣΙΩΝ

        public ActionResult StudentDocApousiesDetail()
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

            if (!IekTmimataExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            return View();
        }

        public ActionResult StudentApousiesDetail_Read([DataSourceRequest] DataSourceRequest request, int tmimaId)
        {
            var data = GetStudentApousiesFromDB(tmimaId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<StudentApousiesDetailViewModel> GetStudentApousiesFromDB(int tmimaId)
        {
            var data = (from d in db.sqlΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ_ΑΝΑΛΥΤΙΚΟ
                        where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.LESSON_DESC
                        select new StudentApousiesDetailViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            LESSON_DESC = d.LESSON_DESC,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            TERM_TEXT = d.TERM_TEXT,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΚΩΔ_ΜΑΘΗΜΑ = d.ΚΩΔ_ΜΑΘΗΜΑ,
                            ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ,
                            ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ,
                            ΩΡΕΣ_ΑΠΟΥΣΙΑ = d.ΩΡΕΣ_ΑΠΟΥΣΙΑ
                        }).ToList();
            return data;
        }


        #endregion


        #region ΕΝΤΥΠΟ ΒΑΘΜΩΝ ΑΝΑ ΜΑΘΗΜΑ (ΜΙΚΤΑ ΜΑΘΗΜΑΤΑ)

        public ActionResult StudentDocGradesMikta()
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

            if (!IekTmimataExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            return View();
        }

        public ActionResult StudentGradesMikta_Read([DataSourceRequest] DataSourceRequest request, int tmimaId)
        {
            var data = GetStudentGradesMiktaFromDB(tmimaId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<StudentGradesMiktaViewModel> GetStudentGradesMiktaFromDB(int tmimaId)
        {
            var data = (from d in db.sqlΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ_ΜΙΚΤΑ
                        where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.ΜΑΘΗΜΑ
                        select new StudentGradesMiktaViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            TERM_ID = d.TERM_ID,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΜΑΘΗΜΑ = d.ΜΑΘΗΜΑ,
                            TERM_TEXT = d.TERM_TEXT,
                            ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ,
                            ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ,
                            ΓΜΟ = d.ΓΜΟ,
                            ΜΟΒΠ = d.ΜΟΒΠ,
                            ΒΕ = d.ΒΕ,
                            ΜΟΕΠ = d.ΜΟΕΠ,
                            ΜΟΤΕ = d.ΜΟΤΕ
                        }).ToList();
            return data;
        }


        #endregion


        #region ΕΝΤΥΠΟ ΤΕΛΙΚΩΝ ΒΑΘΜΩΝ ΑΝΑ ΣΠΟΥΔΑΣΤΗ

        public ActionResult StudentDocGradesFinal()
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

            if (!IekTmimataExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            return View();
        }

        #endregion


        #region ΤΕΛΙΚΟΙ ΒΑΘΜΟΙ ΚΑΙ ΑΠΟΥΣΙΕΣ (ΝΕΟ-22/01/2019)

        public ActionResult StudentDocGradesApousies()
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

            if (!IekTmimataExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            return View();
        }

        public ActionResult DocGradesApousiesPrint(int tmimaId, int studentId = 0)
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

            var data = (from d in db.sqlΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ_ΜΙΚΤΑ
                        where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.STUDENT_ID == studentId
                        select new StudentGradesMiktaViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΜΑΘΗΜΑ = d.ΜΑΘΗΜΑ
                        }).FirstOrDefault();

            return View(data);
        }

        #endregion


        #region ΕΝΤΥΠΟ ΒΑΘΜΟΛΟΓΙΩΝ ΕΞΑΜΗΝΟΥ (ΝΕΟ - 26/10/2018)

        public ActionResult AllGradesReport()
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

            if (!IekTmimataExist(schoolId) || !StudentsExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            return View();
        }

        public ActionResult GradesReport_Read([DataSourceRequest] DataSourceRequest request, int tmimaId)
        {
            List<StudentGradesReportViewModel> data = GetGradesReportFromDB(tmimaId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<StudentGradesReportViewModel> GetGradesReportFromDB(int tmimaId)
        {
            var data = (from d in db.STUDENT_GRADES
                        where d.TMIMA_ID == tmimaId
                        orderby d.FULLNAME, d.LESSON_TEXT
                        select new StudentGradesReportViewModel
                        {
                            GRADES_ID = d.GRADES_ID,
                            IEK = d.IEK,
                            STUDENT_ID = d.STUDENT_ID,
                            AMK = d.AMK,
                            FULLNAME = d.FULLNAME,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            LESSON_TEXT = d.LESSON_TEXT,
                            PERIOD_TEXT = d.PERIOD_TEXT,
                            TERM_TEXT = d.TERM_TEXT,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            TERM_ID = d.TERM_ID,
                            TMIMA_ID = d.TMIMA_ID,
                            ΠΡΘ = d.ΠΡΘ,
                            ΠΡΕ = d.ΠΡΕ,
                            ΤΕΘ = d.ΤΕΘ,
                            ΤΕΕ = d.ΤΕΕ,
                            ΕΠ = d.ΕΠ,
                            ΠΡΟΟΔΟΣ = d.ΠΡΟΟΔΟΣ,
                            ΕΡΓΑΣΙΑ = d.ΕΡΓΑΣΙΑ,
                            ΕΞΕΤΑΣΗ = d.ΕΞΕΤΑΣΗ,
                            ΤΕΛΙΚΟΣ = d.ΤΕΛΙΚΟΣ
                        }).ToList();
            return data;
        }

        #region ΛΕΙΤΟΥΡΓΙΕΣ ΔΗΜΙΟΥΡΓΙΑΣ-ΕΝΗΜΕΡΩΣΗΣ-ΕΚΤΥΠΩΣΗΣ ΠΙΝΑΚΑ ΒΑΘΜΟΛΟΓΙΩΝ

        public ActionResult CreateGradesTable(int tmimaId)
        {
            string msg = tableGradesTermService.Create(tmimaId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateGradesTable(int tmimaId)
        {
            string msg = tableGradesTermService.Update(tmimaId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DestroyGradesTable(int tmimaId)
        {
            string msg = tableGradesTermService.Destroy(tmimaId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult AllGradesEidikotitaPrint(int eidikotitaId, string period)
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

            var data = (from d in db.STUDENT_GRADES
                        where d.IEK == schoolId && d.EIDIKOTITA_ID == eidikotitaId && d.PERIOD_TEXT == period
                        select new StudentGradesReportViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            TMIMA_ID = d.TMIMA_ID,
                            IEK = d.IEK,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            PERIOD_TEXT = d.PERIOD_TEXT
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult AllGradesReportPrint(int tmimaId, int studentId = 0)
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

            var data = (from d in db.STUDENT_GRADES
                        where d.TMIMA_ID == tmimaId && d.STUDENT_ID == studentId
                        select new StudentGradesReportViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            TMIMA_ID = d.TMIMA_ID,
                            IEK = d.IEK,
                            FULLNAME = d.FULLNAME,
                            LESSON_TEXT = d.LESSON_TEXT
                        }).FirstOrDefault();

            return View(data);
        }

        #endregion


        #region ΕΝΤΥΠΟ ΠΑΡΑΚΟΛΟΥΘΗΣΗΣ ΩΡΩΝ ΜΑΘΗΜΑΤΩΝ

        public ActionResult TmimaLessonHoursMonitor()
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

            if (!IekTmimataExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            return View();
        }

        public ActionResult LessonsTmima_Read([DataSourceRequest] DataSourceRequest request, int tmimaId)
        {
            var data = GetLessonHoursMonitorFromDB(tmimaId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<LessonHoursMonitorViewModel> GetLessonHoursMonitorFromDB(int tmimaId)
        {
            var data = (from d in db.sqlΜΑΘΗΜΑ_ΠΑΡΑΚΟΛΟΥΘΗΣΗ
                        where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId
                        orderby d.LESSON_TEXT
                        select new LessonHoursMonitorViewModel
                        {
                            LESSON_ID = d.LESSON_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ,
                            TERM_ID = d.TERM_ID,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            LESSON_TEXT = d.LESSON_TEXT,
                            TERM_TEXT = d.TERM_TEXT,
                            ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ,
                            ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ,
                            ΚΩΔ_ΕΡΓΑΣΙΑ = d.ΚΩΔ_ΕΡΓΑΣΙΑ,
                            ΕΡΓΑΣΙΑ = d.ΕΡΓΑΣΙΑ,
                            ΩΡΕΣ_ΜΑΘΗΜΑ = d.ΩΡΕΣ_ΜΑΘΗΜΑ,
                            ΩΡΕΣ_ΟΡΙΟ = d.ΩΡΕΣ_ΟΡΙΟ,
                            ΥΠΟΛΟΙΠΟ = d.ΥΠΟΛΟΙΠΟ
                        }).ToList();

            return (data);
        }

        #endregion


        #region ΒΙΒΛΙΟ ΠΑΡΟΥΣΙΩΝ ΕΚΠΑΙΔΕΥΤΩΝ

        public ActionResult TmimaTeacherParousies()
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

            if (!IekTmimataExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα του ΙΕΚ.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            PopulatePeriodoi();
            return View();
        }

        public ActionResult DocTeacherParousiesPrint(int periodId, int tmimaId, string theDate1, string TheDate2)
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

            TeacherParousiesParams data = new TeacherParousiesParams
            {
                schoolId = schoolId,
                periodId = periodId,
                tmimaId = tmimaId,
                theDate1 = theDate1,
                theDate2 = TheDate2
            };
            return View(data);
        }

        #endregion


        #region ΕΚΤΥΠΩΣΕΙΣ

        public ActionResult BebeosiUnifiedPrint(int bebeosiId, int schoolyearId)
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
            var data = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ
                        where d.ΒΕΒΑΙΩΣΗ_ΚΩΔ == bebeosiId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId
                        select new TeacherBebeoseisViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult BebeosiUnifiedTotalPrint(int bebeosiId)
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
            var data = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ
                        where d.ΒΕΒΑΙΩΣΗ_ΚΩΔ == bebeosiId
                        select new TeacherBebeoseisViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult BekNewPrint(int bekId)
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
            var bek = (from d in db.ΜΑΘΗΤΕΣ_ΒΕΚ
                       where d.ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ == bekId
                       select new StudentBekViewModel
                       {
                           ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ,
                           ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                           ΙΕΚ = d.ΙΕΚ ?? 0,
                           ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ = d.ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ
                       }).FirstOrDefault();

            return View(bek);
        }

        public ActionResult DocParousiologioPrint(int tmimaId)
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
            var data = (from d in db.sqlΤΜΗΜΑΤΑ
                        where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId
                        select new sqlTmimataViewModel
                        {
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            TERM_ID = d.TERM_ID
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult DocKatastasiGenikiPrint(int tmimaId)
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
            var data = (from d in db.sqlΤΜΗΜΑΤΑ
                        where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId
                        select new sqlTmimataViewModel
                        {
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            TERM_ID = d.TERM_ID
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult DocGradesExamPrint(int tmimaId)
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
            var data = (from d in db.sqlΤΜΗΜΑΤΑ
                        where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId
                        select new sqlTmimataViewModel
                        {
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            TERM_ID = d.TERM_ID
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult DocGradesProodosPrint(int tmimaId)
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
            var data = (from d in db.sqlΤΜΗΜΑΤΑ
                        where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId
                        select new sqlTmimataViewModel
                        {
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            TERM_ID = d.TERM_ID
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult DocApousiesLessonPrint(int tmimaId, int studentId = 0)
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
            var data = (from d in db.sqlΜΑΘΗΤΕΣ_ΤΜΗΜΑ_ΕΝΕΡΓΟΙ
                        where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.STUDENT_ID == studentId
                        select new StudentTmimaActiveViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult DocApousiesDetailPrint(int tmimaId, int studentId = 0)
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
            var data = (from d in db.sqlΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ_ΑΝΑΛΥΤΙΚΟ
                        where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId && d.STUDENT_ID == studentId
                        select new StudentApousiesDetailViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult DocGradesMiktaPrint(int tmimaId, int studentId = 0)
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
            var data = (from d in db.sqlΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ_ΜΙΚΤΑ
                        where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.STUDENT_ID == studentId
                        select new StudentGradesMiktaViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΜΑΘΗΜΑ = d.ΜΑΘΗΜΑ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult DocGradesFinalPrint(int tmimaId, int studentId = 0)
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
            var data = (from d in db.sqlΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ_ΜΙΚΤΑ
                        where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.STUDENT_ID == studentId
                        select new StudentGradesMiktaViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΜΑΘΗΜΑ = d.ΜΑΘΗΜΑ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult DocLessonHoursMonitorPrint(int tmimaId)
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
            var data = (from d in db.sqlΤΜΗΜΑΤΑ
                        where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId
                        select new sqlTmimataViewModel
                        {
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            TERM_ID = d.TERM_ID
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult DocTeacherMonthHoursPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
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

        public ActionResult DocTeacherMonthAnapliroseisPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_SCHOOLS");
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

        #endregion

    }
}