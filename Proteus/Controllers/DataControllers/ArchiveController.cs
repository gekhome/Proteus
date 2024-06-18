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
    public class ArchiveController : ControllerUnit
    {
        private readonly ProteusDBEntities db;

        private readonly IBebeosiTeacherService bebeosiTeacherService;
        private readonly ITeacherInfoService teacherInfoService;

        public ArchiveController(ProteusDBEntities entities, IBebeosiTeacherService bebeosiTeacherService,
            ITeacherInfoService teacherInfoService) : base(entities)
        {
            db = entities;
            this.bebeosiTeacherService = bebeosiTeacherService;
            this.teacherInfoService = teacherInfoService;
        }

        // Master grid data source in aTeacherBebaioseis
        public ActionResult TeacherInfo_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            IEnumerable<TeacherInfoViewModel> data = teacherInfoService.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // Used by editor template in aProgrammaDay
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

        // Used by editor template in aProgrammaDay
        public ActionResult FilteredLessonsRead(int eidikotitaId, int termId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = db.qryIEK_EIDIKOTITES_LESSONS
                .Where(f => f.LESSON_EIDIKOTITA == eidikotitaId && f.LESSON_TERM == termId && f.IEK_ID == schoolId)
                .OrderBy(d => d.LESSON_DESC);

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #region ΑΡΧΕΙΟ ΠΡΟΓΡΑΜΜΑΤΟΣ (28-09-2019)

        public ActionResult aTmimata_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<sqlTmimataViewModel> data = aGetTmimataFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<sqlTmimataViewModel> aGetTmimataFromDB()
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = (from d in db.sqlΤΜΗΜΑΤΑ
                        where d.ΙΕΚ == schoolId && d.PERIOD_ID < PERIOD_ARCHIVE
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


        #region ΒΕΒΑΙΩΣΕΙΣ ΕΚΠΑΙΔΕΥΤΩΝ (ΑΡΧΕΙΟ)

        public ActionResult aTeacherBebeoseis(string notify = null)
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
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateSchoolYears();

            return View();
        }

        #region GRID CRUD FUNCTIONS

        public ActionResult aTeacherBebeosi_Read([DataSourceRequest] DataSourceRequest request, int teacherId)
        {
            List<TeacherBebeoseisViewModel> data = bebeosiTeacherService.ReadArchive(teacherId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult aTeacherBebeosi_Create([DataSourceRequest] DataSourceRequest request, TeacherBebeoseisViewModel data, int teacherId)
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

        public ActionResult aTeacherBebeosi_Update([DataSourceRequest] DataSourceRequest request, TeacherBebeoseisViewModel data, int teacherId)
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

        public ActionResult aTeacherBebeosi_Destroy([DataSourceRequest] DataSourceRequest request, TeacherBebeoseisViewModel data)
        {
            if (data != null)
            {
                bebeosiTeacherService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #region DATA FORM

        public ActionResult aTeacherBebeosiEdit(int bebeosiId)
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
                return RedirectToAction("ErrorData", "Archive", new { notify = msg });
            }

            TeacherBebeoseisViewModel bebeosi = bebeosiTeacherService.Refresh(bebeosiId);
            if (bebeosi == null)
            {
                return HttpNotFound();
            }
            int teacherId = (int)bebeosi.TEACHER_ID;

            sqlTEACHER_INFO SelectedTeacher = Common.GetTeacherInfo(teacherId);
            if (SelectedTeacher == null)
            {
                string notify = "Παρουσιάστηκε σφάλμα εύρεσης του εκπαιδευτικού.";
                return RedirectToAction("Index", "School", new { notify });
            }
            else
            {
                ViewBag.TeacherData = SelectedTeacher;
            }
            return View(bebeosi);
        }

        [HttpPost]
        public ActionResult aTeacherBebeosiEdit(int bebeosiId, TeacherBebeoseisViewModel data)
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

        public ActionResult aBebeosiUnifiedPrint(int bebeosiId, int schoolyearId)
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

        public ActionResult aBebeosiUnifiedTotalPrint(int bebeosiId)
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

        #endregion


        #region ΒΙΒΛΙΟ ΠΑΡΟΥΣΙΩΝ ΕΚΠΑΙΔΕΥΤΩΝ (ΑΡΧΕΙΟ)

        public ActionResult aTmimaTeacherParousies()
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

        public ActionResult aLessonsTmima_Read([DataSourceRequest] DataSourceRequest request, int tmimaId)
        {
            var data = aGetLessonHoursMonitorFromDB(tmimaId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<LessonHoursMonitorViewModel> aGetLessonHoursMonitorFromDB(int tmimaId)
        {
            var data = (from d in db.C_sqlΜΑΘΗΜΑ_ΠΑΡΑΚΟΛΟΥΘΗΣΗ
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
            return data;
        }

        public ActionResult aDocTeacherParousiesPrint(int periodId, int tmimaId, string theDate1, string TheDate2)
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


        #region ΩΡΟΛΟΓΙΟ ΠΡΟΓΡΑΜΜΑ (ΑΡΧΕΙΟ)

        public ActionResult aProgrammaDay()
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

        public ActionResult aProgramma_Read([DataSourceRequest] DataSourceRequest request, int tmimaId = 0, DateTime? theDate = null)
        {
            List<ProgrammaDayViewModel> data = aGetProgrammaDayFromDB(tmimaId, theDate);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<ProgrammaDayViewModel> aGetProgrammaDayFromDB(int tmimaId, DateTime? theDate)
        {
            var data = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΑΡΧΕΙΟ
                        where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.ΗΜΕΡΟΜΗΝΙΑ == theDate
                        select new ProgrammaDayViewModel
                        {
                            ΠΡΟΓΡΑΜΜΑ_ΚΩΔ = d.ΠΡΟΓΡΑΜΜΑ_ΚΩΔ,
                            ΕΒΔΟΜΑΔΑ = d.ΕΒΔΟΜΑΔΑ ?? 0,
                            ΕΚΠΑΙΔΕΥΤΗΣ1 = d.ΕΚΠΑΙΔΕΥΤΗΣ1 ?? 0,
                            ΕΚΠΑΙΔΕΥΤΗΣ2 = d.ΕΚΠΑΙΔΕΥΤΗΣ2 ?? 0,
                            ΕΚΠΑΙΔΕΥΤΗΣ3 = d.ΕΚΠΑΙΔΕΥΤΗΣ3 ?? 0,
                            Π1 = d.Π1 ?? false,
                            Π2 = d.Π2 ?? false,
                            Π3 = d.Π3 ?? false,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΚΩΔ_ΕΡΓΑΣΙΑ = d.ΚΩΔ_ΕΡΓΑΣΙΑ ?? 0,
                            ΚΩΔ_ΜΑΘΗΜΑ = d.ΚΩΔ_ΜΑΘΗΜΑ ?? 0,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ,
                            ΩΡΑ = d.ΩΡΑ
                        }).ToList();
            return data;
        }

        public ActionResult aGetWeekOfDate(ProgrammaParameters parameters)
        {
            string lastweek = "";

            int tmimaId = parameters.tmimaId;
            DateTime theDate = parameters.theDate;

            var data = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΑΡΧΕΙΟ
                        where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.ΗΜΕΡΟΜΗΝΙΑ == theDate
                        select d).ToList();

            if (data.Count > 0) lastweek = data.First().ΕΒΔΟΜΑΔΑ.ToString();

            return Json(lastweek, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΠΑΡΑΚΟΛΟΥΘΗΣΗ ΩΡΩΝ ΜΑΘΗΜΑΤΩΝ (ΑΡΧΕΙΟ)

        public ActionResult aTmimaLessonHoursMonitor()
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

        public ActionResult aDocLessonHoursMonitorPrint(int tmimaId)
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

        #endregion


        #region ΜΗΝΙΑΙΕΣ ΚΑΤΑΣΤΑΣΕΙΣ ΩΡΩΝ (ΑΡΧΕΙΟ)

        public ActionResult aDocTeacherMonthHoursPrint()
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

        public ActionResult aDocTeacherMonthAnapliroseisPrint()
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

        #endregion

    }
}