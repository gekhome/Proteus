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
    public class AdminController : ControllerUnit
    {
        private readonly ProteusDBEntities db;

        private readonly ITmimaService tmimaService;
        private readonly ILessonService lessonService;
        private readonly IPeriodoiService periodoiService;
        private readonly ISpoudesService spoudesService;
        private readonly IApolytiriaService apolytiriaService;
        private readonly IEidikotitesIekService eidikotitesIekService;
        private readonly IEidikotitesKatartisiService eidikotitesKatartisiService;

        private readonly IStudentService studentService;
        private readonly ITeacherService teacherService;
        private readonly IErgodotesService ergodotesService;
        private readonly IIekInfoService iekInfoService;
        private readonly IStudentInfoService studentInfoService;
        private readonly ITeacherInfoService teacherInfoService;
        private readonly IErgodotisInfoService ergodotisInfoService;
        private readonly IPraktikiRegistryService praktikiRegistryService;

        private readonly IAtomikoDeltioService atomikoDeltioService;
        private readonly IFoitisiDeltioService foitisiDeltioService;
        private readonly ITableAtomikoDeltioService tableAtomikoDeltioService;
        private readonly ITableDeltioFoitisiService tableDeltioFoitisiService;
        private readonly ITableGradesTermService tableGradesTermService;

        public AdminController(ProteusDBEntities entities, ITmimaService tmimaService, ILessonService lessonService,
            IPeriodoiService periodoiService, ISpoudesService spoudesService, IApolytiriaService apolytiriaService,
            IEidikotitesIekService eidikotitesIekService, IEidikotitesKatartisiService eidikotitesKatartisiService,
            IStudentService studentService, ITeacherService teacherService, IErgodotesService ergodotesService,
            IIekInfoService iekInfoService, IStudentInfoService studentInfoService, ITeacherInfoService teacherInfoService,
            IErgodotisInfoService ergodotisInfoService, IPraktikiRegistryService praktikiRegistryService,
            IAtomikoDeltioService atomikoDeltioService, IFoitisiDeltioService foitisiDeltioService,
            ITableAtomikoDeltioService tableAtomikoDeltioService, ITableDeltioFoitisiService tableDeltioFoitisiService,
            ITableGradesTermService tableGradesTermService) : base(entities)
        {
            db = entities;

            this.tmimaService = tmimaService;
            this.lessonService = lessonService;
            this.periodoiService = periodoiService;
            this.spoudesService = spoudesService;
            this.apolytiriaService = apolytiriaService;
            this.eidikotitesIekService = eidikotitesIekService;
            this.eidikotitesKatartisiService = eidikotitesKatartisiService;

            this.studentService = studentService;
            this.teacherService = teacherService;
            this.ergodotesService = ergodotesService;
            this.iekInfoService = iekInfoService;
            this.studentInfoService = studentInfoService;
            this.teacherInfoService = teacherInfoService;
            this.ergodotisInfoService = ergodotisInfoService;
            this.praktikiRegistryService = praktikiRegistryService;

            this.atomikoDeltioService = atomikoDeltioService;
            this.foitisiDeltioService = foitisiDeltioService;
            this.tableAtomikoDeltioService = tableAtomikoDeltioService;
            this.tableDeltioFoitisiService = tableDeltioFoitisiService;
            this.tableGradesTermService = tableGradesTermService;
        }

        public ActionResult Index(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View();
        }

        #region SETUP (ΡΥΘΜΙΣΕΙΣ)

        #region ΠΕΡΙΟΔΟΙ

        public ActionResult XPeriodosList()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            PopulateXE();
            PopulateShoolYears();
            PopulateGenders();

            return View();
        }

        public ActionResult Periodos_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<PeriodosViewModel> data = periodoiService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Periodos_Create([DataSourceRequest] DataSourceRequest request, PeriodosViewModel data)
        {
            var newdata = new PeriodosViewModel();

            var existingData = db.ΠΕΡΙΟΔΟΙ.Where(p => p.ΠΕΡΙΟΔΟΣ == data.ΠΕΡΙΟΔΟΣ && p.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ).Count();
            if (existingData > 0) 
                ModelState.AddModelError("", "Η περίοδος αυτή είναι ήδη καταχωρημένη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                periodoiService.Create(data);
                newdata = periodoiService.Refresh(data.PERIOD_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Periodos_Update([DataSourceRequest] DataSourceRequest request, PeriodosViewModel data)
        {
            var newdata = new PeriodosViewModel();

            if (data != null && ModelState.IsValid)
            {
                periodoiService.Update(data);
                newdata = periodoiService.Refresh(data.PERIOD_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Periodos_Destroy([DataSourceRequest] DataSourceRequest request, PeriodosViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeletePeriodos(data.PERIOD_ID))
                {
                    periodoiService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί η περίοδος διότι είναι σε χρήση.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΒΑΣΙΚΕΣ ΣΠΟΥΔΕΣ

        public ActionResult XSpoudesList()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult Spoudes_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<SpoudesViewModel> data = spoudesService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Spoudes_Create([DataSourceRequest] DataSourceRequest request, SpoudesViewModel data)
        {
            var newdata = new SpoudesViewModel();

            var existingData = db.ΣΠΟΥΔΕΣ.Where(s => s.ΒΑΘΜΙΔΑ == data.ΒΑΘΜΙΔΑ).Count();
            if (existingData > 0) 
                ModelState.AddModelError("", "Η τμή αυτή είναι ήδη καταχωρημένη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                spoudesService.Create(data);
                newdata = spoudesService.Refresh(data.ΒΑΘΜΙΔΑ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Spoudes_Update([DataSourceRequest] DataSourceRequest request, SpoudesViewModel data)
        {
            var newdata = new SpoudesViewModel();

            if (data != null && ModelState.IsValid)
            {
                spoudesService.Update(data);
                newdata = spoudesService.Refresh(data.ΒΑΘΜΙΔΑ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Spoudes_Destroy([DataSourceRequest] DataSourceRequest request, SpoudesViewModel data)
        {
            if (data != null)
            {
                spoudesService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΕΙΔΙΚΟΤΗΤΕΣ ΚΑΤΑΡΤΙΣΗΣ

        public ActionResult XEidikotitesKatartisiList()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult EidikotitaKatartisi_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = eidikotitesKatartisiService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaKatartisi_Create([DataSourceRequest] DataSourceRequest request, SYS_EIDIKOTITES_IEKViewModel data)
        {
            var newdata = new SYS_EIDIKOTITES_IEKViewModel();

            var existingData = db.SYS_EIDIKOTITES_IEK.Where(s => s.EIDIKOTITA_TEXT == data.EIDIKOTITA_TEXT).Count();
            if (existingData > 0) 
                ModelState.AddModelError("", "Η ειδικότητα αυτή υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                eidikotitesKatartisiService.Create(data);
                newdata = eidikotitesKatartisiService.Refresh(data.EIDIKOTITA_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaKatartisi_Update([DataSourceRequest] DataSourceRequest request, SYS_EIDIKOTITES_IEKViewModel data)
        {
            var newdata = new SYS_EIDIKOTITES_IEKViewModel();

            if (data != null && ModelState.IsValid)
            {
                eidikotitesKatartisiService.Update(data);
                newdata = eidikotitesKatartisiService.Refresh(data.EIDIKOTITA_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaKatartisi_Destroy([DataSourceRequest] DataSourceRequest request, SYS_EIDIKOTITES_IEKViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteEidikotitaKatartisi(data.EIDIKOTITA_ID))
                {
                    eidikotitesKatartisiService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί η ειδικότητα διότι είναι σε χρήση.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΕΙΔΙΚΟΤΗΤΕΣ ΤΩΝ ΙΕΚ

        public ActionResult XEidikotitesIekList()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            PopulateSchools();
            PopulateAllEidikotitesKatartisi();

            return View();
        }

        public ActionResult EidikotitaIek_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<IekEidikotitesViewModel> data = eidikotitesIekService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaIek_Create([DataSourceRequest] DataSourceRequest request, IekEidikotitesViewModel data)
        {
            var newdata = new IekEidikotitesViewModel();

            var existingData = db.IEK_EIDIKOTITES.Where(s => s.IEK_ID == data.IEK_ID && s.EIDIKOTITA_ID == data.EIDIKOTITA_ID).Count();
            if (existingData > 0) 
                ModelState.AddModelError("", "Η ειδικότητα είναι ήδη καταχωρημένη για το ΙΕΚ αυτό. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                eidikotitesIekService.Create(data);
                newdata = eidikotitesIekService.Refresh(data.IE_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaIek_Update([DataSourceRequest] DataSourceRequest request, IekEidikotitesViewModel data)
        {
            var newdata = new IekEidikotitesViewModel();

            if (data != null && ModelState.IsValid)
            {
                eidikotitesIekService.Update(data);
                newdata = eidikotitesIekService.Refresh(data.IE_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaIek_Destroy([DataSourceRequest] DataSourceRequest request, IekEidikotitesViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteIekEidikotita(data.IEK_ID, data.EIDIKOTITA_ID))
                {
                    eidikotitesIekService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί η εγγραφή αυτή διότι είναι σε χρήση.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΤΜΗΜΑΤΑ ΙΕΚ

        public ActionResult XIekTmimaList()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (!IekEidikotitesExist())
            {
                string msg = "Δεν βρέθηκαν καταχωρημένες ειδικότητες που υλοποιούν τα ΙΕΚ.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            PopulateSchools();
            PopulateIekEidikotites();
            PopulatePeriodoi();
            PopulateTerms();
            PopulatePA();

            return View();
        }

        public ActionResult Tmima_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<TmimaViewModel> data = tmimaService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Tmima_Create([DataSourceRequest] DataSourceRequest request, TmimaViewModel data)
        {
            var newdata = new TmimaViewModel();

            var existingData = db.ΤΜΗΜΑ.Where(s => s.ΙΕΚ == data.ΙΕΚ && s.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ == data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ && s.ΠΕΡΙΟΔΟΣ_ΚΩΔ == data.ΠΕΡΙΟΔΟΣ_ΚΩΔ
                                              && s.ΕΞΑΜΗΝΟ == data.ΕΞΑΜΗΝΟ && s.ΠΑ_ΚΩΔ == data.ΠΑ_ΚΩΔ).Count();
            if (existingData > 0) 
                ModelState.AddModelError("", "Υπάρχει ήδη τμήμα καταχωρημένο με αυτά τα χαρακτηριστικά.");

            if (data != null && ModelState.IsValid)
            {
                tmimaService.Create(data);
                newdata = tmimaService.Refresh(data.ΤΜΗΜΑ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Tmima_Update([DataSourceRequest] DataSourceRequest request, TmimaViewModel data)
        {
            var newdata = new TmimaViewModel();

            if (data != null && ModelState.IsValid)
            {
                tmimaService.Update(data);
                newdata = tmimaService.Refresh(data.ΤΜΗΜΑ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Tmima_Destroy([DataSourceRequest] DataSourceRequest request, TmimaViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteTmima(data.ΤΜΗΜΑ_ΚΩΔ))
                {
                    tmimaService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί το τμήμα διότι είναι σε χρήση.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΜΑΘΗΜΑΤΑ ΕΙΔΙΚΟΤΗΤΩΝ (ΩΡΟΛΟΓΙΑ ΠΡΟΓΡΑΜΜΑΤΑ)

        public ActionResult XEidikotitesLessons()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            PopulateTerms();
            PopulateLessonTypes();
            return View();
        }

        // Read action is EidikotiteaKatartisi_Read in section #region Eidikotites Katartisis

        public ActionResult Lesson_Read([DataSourceRequest] DataSourceRequest request, int eidikotitaId = 0)
        {
            IEnumerable<LessonsIekViewModel> data = lessonService.Read(eidikotitaId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Lesson_Create([DataSourceRequest] DataSourceRequest request, LessonsIekViewModel data, int eidikotitaId)
        {
            var newdata = new LessonsIekViewModel();

            if (eidikotitaId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    lessonService.Create(data, eidikotitaId);
                    newdata = lessonService.Refresh(data.LESSON_ID);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε μια ειδικότητα. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Lesson_Update([DataSourceRequest] DataSourceRequest request, LessonsIekViewModel data, int eidikotitaId)
        {
            var newdata = new LessonsIekViewModel();

            if (eidikotitaId > 0)
            {
                if (data != null & ModelState.IsValid)
                {
                    lessonService.Update(data, eidikotitaId);
                    newdata = lessonService.Refresh(data.LESSON_ID);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλεχθεί μια ειδικότητα. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Lesson_Destroy([DataSourceRequest] DataSourceRequest request, LessonsIekViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteLesson(data.LESSON_ID))
                {
                    lessonService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί το μάθημα αυτό διότι είναι σε χρήση.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public LessonsIekViewModel RefreshLessonIekFromDB(int recordId)
        {
            var data = (from d in db.LESSONS_IEK
                        where d.LESSON_ID == recordId
                        select new LessonsIekViewModel
                        {
                            LESSON_ID = d.LESSON_ID,
                            LESSON_TEXT = d.LESSON_TEXT,
                            LESSON_TERM = d.LESSON_TERM,
                            LESSON_TYPE = d.LESSON_TYPE,
                            LESSON_HOURS_WEEK = d.LESSON_HOURS_WEEK,
                            LESSON_HOURS = d.LESSON_HOURS,
                            LESSON_EIDIKOTITA = d.LESSON_EIDIKOTITA
                        }).FirstOrDefault();
            return (data);
        }

        public List<LessonsIekViewModel> GetLessonsIekViewModelFromDB(int eidikotitaId)
        {
            var data = (from d in db.LESSONS_IEK
                        where d.LESSON_EIDIKOTITA == eidikotitaId
                        orderby d.LESSON_TERM, d.LESSON_TEXT, d.LESSON_TYPE
                        select new LessonsIekViewModel
                            {
                                LESSON_ID = d.LESSON_ID,
                                LESSON_TEXT = d.LESSON_TEXT,
                                LESSON_TERM = d.LESSON_TERM,
                                LESSON_TYPE = d.LESSON_TYPE,
                                LESSON_HOURS_WEEK = d.LESSON_HOURS_WEEK,
                                LESSON_HOURS = d.LESSON_HOURS,
                                LESSON_EIDIKOTITA = d.LESSON_EIDIKOTITA
                            }).ToList();
            return (data);
        }

        #endregion


        #region ΑΠΟΛΥΤΗΡΙΑ

        public ActionResult XApolytiriaList()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            PopulateApolytiriaKlaseis();
            return View();
        }

        public ActionResult Apolytiria_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<ApolytiriaViewModel> data = apolytiriaService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Apolytiria_Create([DataSourceRequest] DataSourceRequest request, ApolytiriaViewModel data)
        {
            var newdata = new ApolytiriaViewModel();

            var existingData = db.ΑΠΟΛΥΤΗΡΙΑ.Where(a => a.ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ == data.ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ).Count();
            if (existingData > 0) 
                ModelState.AddModelError("", "Η τιμή αυτή είναι ήδη καταχωρημένη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                apolytiriaService.Create(data);
                newdata = apolytiriaService.Refresh(data.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Apolytiria_Update([DataSourceRequest] DataSourceRequest request, ApolytiriaViewModel data)
        {
            var newdata = new ApolytiriaViewModel();

            if (data != null && ModelState.IsValid)
            {
                apolytiriaService.Update(data);
                newdata = apolytiriaService.Refresh(data.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Apolytiria_Destroy([DataSourceRequest] DataSourceRequest request, ApolytiriaViewModel data)
        {
            if (data != null)
            {
                apolytiriaService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion SETUP


        #region IEK DATA GRID

        public ActionResult XIekList(int schoolId = 0, string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            SYS_SCHOOLSViewModel school = iekInfoService.Read().First();
            PopulatePeriferiakes();

            return View(school);
        }

        public ActionResult Iek_Read([DataSourceRequest] DataSourceRequest request, int schoolId = 0)
        {
            IEnumerable<SYS_SCHOOLSViewModel> data = iekInfoService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult IekEidikotites_Read(int schoolId, [DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<qryIekEidikotitesViewModel> data = iekInfoService.GetEidikotites(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetIekRecord(int schoolId)
        {
            SYS_SCHOOLSViewModel data = iekInfoService.GetRecord(schoolId);

            return PartialView("XIekPartial", data);
        }

        #endregion


        #region IEK DATA FORM

        public ActionResult XSchoolEdit(int schoolId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            SYS_SCHOOLSViewModel school = iekInfoService.GetRecord(schoolId);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        [HttpPost]
        public ActionResult XSchoolEdit(int schoolId, SYS_SCHOOLSViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (ModelState.IsValid)
            {
                SYS_SCHOOLS modschool = db.SYS_SCHOOLS.Find(schoolId);

                modschool.SCHOOL_NAME = data.SCHOOL_NAME.Trim();
                modschool.SCHOOL_PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID;
                modschool.SCHOOL_PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID;
                modschool.SCHOOL_DIMOS = data.SCHOOL_DIMOS;
                modschool.SCHOOL_ADDRESS = data.SCHOOL_ADDRESS.Trim();
                modschool.SCHOOL_TK_CITY = data.SCHOOL_TK_CITY.Trim();
                modschool.SCHOOL_EMAIL = data.SCHOOL_EMAIL.Trim();
                modschool.SCHOOL_PHONE = data.SCHOOL_PHONE.Trim();
                modschool.SCHOOL_FAX = data.SCHOOL_FAX.Trim();
                modschool.SCHOOL_DIRECTOR = data.SCHOOL_DIRECTOR.Trim();
                modschool.SCHOOL_DEPUTY = modschool.SCHOOL_DEPUTY.HasValue() ? data.SCHOOL_DEPUTY.Trim() : data.SCHOOL_DEPUTY;
                modschool.SCHOOL_INFO = data.SCHOOL_INFO.Trim();
                modschool.DIRECTOR_GENDER = data.DIRECTOR_GENDER;
                modschool.DEPUTY_GENDER = data.DEPUTY_GENDER;

                db.Entry(modschool).State = EntityState.Modified;
                db.SaveChanges();
                // Notify here
                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                return View(data);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }


        #endregion


        #region ΜΗΤΡΩΟ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult XStudentInfoList(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            IEnumerable<StudentInfoViewModel> data = studentInfoService.Read();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένοι σπουδαστές για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            StudentInfoViewModel student = data.First();

            if (notify != null) this.ShowMessage(MessageType.Info, notify);

            PopulateSchools();
            return View(student);
        }

        public ActionResult StudentInfo_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<StudentInfoViewModel> data = studentInfoService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EgrafesInfo_Read([DataSourceRequest] DataSourceRequest request, int studentId)
        {
            IEnumerable<EgrafesInfoViewModel> data = studentInfoService.GetEgrafes(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetStudentRecord(int studentId)
        {
            StudentInfoViewModel data = studentInfoService.GetRecord(studentId);

            return PartialView("XStudentInfoPartial", data);
        }

        public ActionResult XStudentInfoEdit(int studentId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            StudentViewModel student = studentService.GetRecord(studentId);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult XStudentInfoEdit(int studentId, StudentViewModel model)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            string ErrorMsg = Common.ValidateStudentFields(model);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(model);
            }

            model.ΙΕΚ = Common.GetStudentSchool(studentId);

            if (ModelState.IsValid)
            {
                studentService.UpdateRecord(model, studentId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                StudentViewModel newStudent = studentService.GetRecord(studentId);
                return View(newStudent);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(model);
        }

        #endregion


        #region ΜΗΤΡΩΟ ΒΕΒΑΙΩΣΕΩΝ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult XStudentBebeoseis(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (!IekEidikotitesExist() || !StudentsExist())
            {
                string msg = "Δεν βρέθηκαν ειδικότητες που υλοποιούν τα ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            // ok to populate combos
            PopulateSchools();
            PopulateStudents();

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        public ActionResult StudentBebeosi_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<StudentBebeoseisViewModel> data = GetBebeoseisFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<StudentBebeoseisViewModel> GetBebeoseisFromDB()
        {
            var data = (from d in db.ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ
                        orderby d.ΙΕΚ, d.ΜΑΘΗΤΕΣ.ΕΠΩΝΥΜΟ, d.ΜΑΘΗΤΕΣ.ΟΝΟΜΑ, d.ΗΜΕΡΟΜΗΝΙΑ descending
                        select new StudentBebeoseisViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ = d.ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ,
                            ΓΙΑ_ΧΡΗΣΗ = d.ΓΙΑ_ΧΡΗΣΗ,
                            ΠΑΡΑΔΟΔΗΚΕ = d.ΠΑΡΑΔΟΔΗΚΕ ?? true
                        }).ToList();
            return data;
        }

        public StudentBebeoseisViewModel GetBebeoseisFromDB(int bebeosiId)
        {
            var data = (from d in db.ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ
                        where d.ΒΕΒΑΙΩΣΗ_ΚΩΔ == bebeosiId
                        select new StudentBebeoseisViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ = d.ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ,
                            ΓΙΑ_ΧΡΗΣΗ = d.ΓΙΑ_ΧΡΗΣΗ,
                            ΠΑΡΑΔΟΔΗΚΕ = d.ΠΑΡΑΔΟΔΗΚΕ ?? true
                        }).FirstOrDefault();
            return data;
        }

        #region BEBAIOSI DATA FORM

        public ActionResult XStudentBebeosiEdit(int bebeosiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            if (!(bebeosiId > 0))
            {
                string msg = "Ο κωδικός βεβαίωσης δεν είναι έγκυρος. Η εγγραφή πρέπει να έχει αποθηκευτεί πρώτα.";
                return RedirectToAction("ErrorData", "Admin", new { notify = msg });
            }
            StudentBebeoseisViewModel bebeosi = GetBebeoseisFromDB(bebeosiId);
            if (bebeosi == null)
            {
                return HttpNotFound();
            }
            int studentId = (int)bebeosi.ΜΑΘΗΤΗΣ_ΚΩΔ;

            StudentInfoViewModel SelectedStudent = studentInfoService.GetRecord(studentId);
            if (SelectedStudent == null)
            {
                string notify = "Παρουσιάστηκε σφάλμα εύρεσης του σπουδαστή.";
                return RedirectToAction("ErrorData", "Admin", new { notify });
            }
            else
            {
                ViewBag.StudentData = SelectedStudent;
            }
            return View(bebeosi);
        }

        [HttpPost]
        public ActionResult XStudentBebeosiEdit(int bebeosiId, StudentBebeoseisViewModel data)
        {
            StudentBebeoseisViewModel bebeosi = GetBebeoseisFromDB(bebeosiId);

            int studentId = (int)bebeosi.ΜΑΘΗΤΗΣ_ΚΩΔ;
            StudentInfoViewModel SelectedStudent = studentInfoService.GetRecord(studentId);

            if (SelectedStudent == null)
            {
                string notify = "Παρουσιάστηκε σφάλμα εύρεσης του σπουδαστή.";
                return RedirectToAction("ErrorData", "Admin", new { notify });
            }
            else
            {
                ViewBag.StudentData = SelectedStudent;
            }

            if (data != null)
            {
                ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ entity = db.ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ.Find(bebeosiId);

                entity.ΑΜΚ = bebeosi.ΑΜΚ;
                entity.ΠΡΩΤΟΚΟΛΛΟ = bebeosi.ΠΡΩΤΟΚΟΛΛΟ;
                entity.ΗΜΕΡΟΜΗΝΙΑ = bebeosi.ΗΜΕΡΟΜΗΝΙΑ;
                entity.ΜΑΘΗΤΗΣ_ΚΩΔ = bebeosi.ΜΑΘΗΤΗΣ_ΚΩΔ;
                entity.ΠΑΡΑΔΟΔΗΚΕ = bebeosi.ΠΑΡΑΔΟΔΗΚΕ;
                entity.ΓΙΑ_ΧΡΗΣΗ = data.ΓΙΑ_ΧΡΗΣΗ;
                entity.ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ = data.ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                StudentBebeoseisViewModel newdata = GetBebeoseisFromDB(bebeosiId);
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


        #region ΜΗΤΡΩΟ ΑΤΟΜΙΚΩΝ ΔΕΛΤΙΩΝ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult XStudentAtomikaDeltia(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (!IekEidikotitesExist() || !StudentsExist())
            {
                string msg = "Δεν βρέθηκαν ειδικότητες που υλοποιούν τα ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateSchools();
            PopulateStudents();

            return View();
        }

        public ActionResult ReadStudentInfo([DataSourceRequest] DataSourceRequest request, int schoolId)
        {
            if (schoolId > 0)
            {
                IEnumerable<StudentInfoViewModel> data = studentInfoService.Read(schoolId);
                return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            else
            {
                IEnumerable<StudentInfoViewModel> data = studentInfoService.Read();
                return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AtomikaDeltia_Read([DataSourceRequest] DataSourceRequest request, int studentId = 0, int schoolId = 0)
        {
            IEnumerable<StudentAtomikoDeltioViewModel> data = atomikoDeltioService.Read(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AtomikaDeltia_Create([DataSourceRequest] DataSourceRequest request, StudentAtomikoDeltioViewModel data, int studentId, int schoolId)
        {
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
        public ActionResult AtomikaDeltia_Update([DataSourceRequest] DataSourceRequest request, StudentAtomikoDeltioViewModel data, int studentId, int schoolId)
        {
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
        public ActionResult AtomikaDeltia_Destroy([DataSourceRequest] DataSourceRequest request, StudentAtomikoDeltioViewModel data)
        {
            if (data != null)
            {
                atomikoDeltioService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        #region ΔΗΜΙΟΥΡΓΙΑ-ΕΝΗΜΕΡΩΣΗ-ΔΙΑΓΡΑΦΗ ΠΙΝΑΚΑ ΑΤΟΜΙΚΟΥ ΔΕΛΤΙΟΥ

        public ActionResult CreateAtomikoDeltio(int studentId, int schoolId)
        {
            string msg = tableAtomikoDeltioService.Create(studentId, schoolId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateAtomikoDeltio(int studentId, int schoolId)
        {
            string msg = tableAtomikoDeltioService.Update(studentId, schoolId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DestroyAtomikoDeltio(int studentId)
        {
            string msg = tableAtomikoDeltioService.Destroy(studentId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult XViewAtomikoDeltio(int studentId, string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            adkStudentDataViewModel data = atomikoDeltioService.GetStudentData(studentId);

            return View(data);
        }

        #endregion

        public ActionResult XStudentADKPrint(int recordId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            var data = (from d in db.ΜΑΘΗΤΕΣ_ΑΤΟΜΙΚΑΔΕΛΤΙΑ
                        where d.ΑΔΚ_ΚΩΔ == recordId
                        select new StudentAtomikoDeltioViewModel
                        {
                            ΑΔΚ_ΚΩΔ = d.ΑΔΚ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                        }).FirstOrDefault();

            return View(data);
        }

        #endregion


        #region ΜΗΤΡΩΟ ΔΕΛΤΙΩΝ ΦΟΙΤΗΣΗΣ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult XStudentFoitisiDeltia(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (!IekEidikotitesExist() || !StudentsExist())
            {
                string msg = "Δεν βρέθηκαν ειδικότητες που υλοποιούν τα ΙΕΚ ή/και σπουδαστές.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateSchools();
            PopulateStudents();

            return View();
        }

        public ActionResult FoitisiDeltia_Read([DataSourceRequest] DataSourceRequest request, int studentId, int schoolId = 0)
        {
            IEnumerable<StudentFoitisiDeltioViewModel> data = foitisiDeltioService.Read(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FoitisiDeltia_Create([DataSourceRequest] DataSourceRequest request, StudentFoitisiDeltioViewModel data, int studentId, int schoolId = 0)
        {
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
        public ActionResult FoitisiDeltia_Update([DataSourceRequest] DataSourceRequest request, StudentFoitisiDeltioViewModel data, int studentId, int schoolId = 0)
        {
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
        public ActionResult FoitisiDeltia_Destroy([DataSourceRequest] DataSourceRequest request, StudentFoitisiDeltioViewModel data)
        {
            if (data != null)
            {
                foitisiDeltioService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        #region ΔΗΜΙΟΥΡΓΙΑ-ΕΝΗΜΕΡΩΣΗ-ΔΙΑΓΡΑΦΗ ΠΙΝΑΚΑ ΔΕΛΤΙΟΥ ΦΟΙΤΗΣΗΣ

        public ActionResult CreateFoitisiDeltio(int studentId, int schoolId)
        {
            string msg = tableDeltioFoitisiService.Create(studentId, schoolId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateFoitisiDeltio(int studentId, int schoolId)
        {
            string msg = tableDeltioFoitisiService.Update(studentId, schoolId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DestroyFoitisiDeltio(int studentId)
        {
            string msg = tableDeltioFoitisiService.Destroy(studentId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult XViewFoitisiDeltio(int studentId, string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            adkStudentDataViewModel vmData = atomikoDeltioService.GetStudentData(studentId);

            return View(vmData);
        }

        #endregion

        public ActionResult XStudentFDKPrint(int recordId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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

        #endregion


        #region ΜΗΤΡΩΟ ΒΕΚ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult XStudentBek(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (!IekTmimataBekExist() || !StudentsExist())
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα Δ' εξαμήνου ή/και σπουδαστές.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateSchools();
            PopulateIekTmimataBek();
            PopulateAllEidikotitesKatartisi();
            PopulateBekStudents();

            return View();
        }

        public ActionResult Bek_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<StudentBekViewModel> data = GetStudentBekFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<StudentBekViewModel> GetStudentBekFromDB()
        {
            var data = (from d in db.ΜΑΘΗΤΕΣ_ΒΕΚ
                        orderby d.ΙΕΚ, d.ΜΑΘΗΤΕΣ.ΕΠΩΝΥΜΟ, d.ΜΑΘΗΤΕΣ.ΟΝΟΜΑ, d.ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ descending
                        select new StudentBekViewModel
                        {
                            ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ ?? 0,
                            ΙΕΚ = d.ΙΕΚ ?? 0,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ ?? 0,
                            ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ = d.ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ ?? false,
                            ΑΠΟ_ΚΑΤΑΤΑΞΗ = d.ΑΠΟ_ΚΑΤΑΤΑΞΗ ?? false,
                            ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ = d.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ ?? false,
                            ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ = d.ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ,
                            ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ = d.ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ,
                            ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ = d.ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ,
                            ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ = d.ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΕΚΔΟΣΗ = d.ΕΚΔΟΣΗ ?? true,
                            ΕΞΑΜΗΝΑ = d.ΕΞΑΜΗΝΑ,
                            ΕΠΩΝΥΜΟ_ΟΝΟΜΑ = d.ΕΠΩΝΥΜΟ_ΟΝΟΜΑ,
                            ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ = d.ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ,
                            ΜΗΤΡΩΝΥΜΟ = d.ΜΗΤΡΩΝΥΜΟ,
                            ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ
                        }).ToList();

            return (data);
        }

        public ActionResult XStudentBekListPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        #region BEK EDIT FORM

        public ActionResult XStudentBekEdit(int bekId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            if (!(bekId > 0))
            {
                string msg = "Ο κωδικός της εγγραφής δεν είναι έγκυρος. Η εγγραφή πρέπει να έχει αποθηκευτεί πρώτα.";
                return RedirectToAction("ErrorData", "Admin", new { notify = msg });
            }

            StudentBekViewModel bek = RefreshBekViewModel(bekId);
            if (bek == null)
            {
                string msg = "Προέκυψε πρόβλημα εύρεσης των στοιχείων ΒΕΚ.";
                return RedirectToAction("ErrorData", "Admin", new { notify = msg });
            }

            return View(bek);
        }

        [HttpPost]
        public ActionResult XStudentBekEdit(int bekId, StudentBekViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            StudentBekViewModel bek = RefreshBekViewModel(bekId);

            if (data != null)
            {
                ΜΑΘΗΤΕΣ_ΒΕΚ entity = db.ΜΑΘΗΤΕΣ_ΒΕΚ.Find(bekId);

                entity.ΜΑΘΗΤΗΣ_ΚΩΔ = bek.ΜΑΘΗΤΗΣ_ΚΩΔ;
                entity.ΙΕΚ = bek.ΙΕΚ;
                entity.ΑΜΚ = bek.ΑΜΚ;
                entity.ΕΙΔΙΚΟΤΗΤΑ = bek.ΕΙΔΙΚΟΤΗΤΑ;
                entity.EIDIKOTITA_TEXT = bek.EIDIKOTITA_TEXT;
                entity.ΚΩΔ_ΤΜΗΜΑ = bek.ΚΩΔ_ΤΜΗΜΑ;
                entity.ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ = bek.ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ;
                entity.ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ = bek.ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ;
                entity.ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ = bek.ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ;
                entity.ΕΚΔΟΣΗ = bek.ΕΚΔΟΣΗ;
                entity.ΕΞΑΜΗΝΑ = data.ΕΞΑΜΗΝΑ;

                entity.ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ = data.ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ;
                entity.ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ = data.ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ;
                entity.ΜΗΤΡΩΝΥΜΟ = data.ΜΗΤΡΩΝΥΜΟ;
                entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ;
                entity.ΕΠΩΝΥΜΟ_ΟΝΟΜΑ = data.ΕΠΩΝΥΜΟ_ΟΝΟΜΑ;
                entity.ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ = data.ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ;
                entity.ΑΠΟ_ΚΑΤΑΤΑΞΗ = data.ΑΠΟ_ΚΑΤΑΤΑΞΗ;
                entity.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ = data.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ;


                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                StudentBekViewModel newdata = RefreshBekViewModel(bekId);
                return View(newdata);
            }
            else
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
                return View(data);
            }
        }

        public StudentBekViewModel RefreshBekViewModel(int recordId)
        {
            var data = (from d in db.ΜΑΘΗΤΕΣ_ΒΕΚ
                        where d.ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ == recordId
                        select new StudentBekViewModel
                        {
                            ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ ?? 0,
                            ΙΕΚ = d.ΙΕΚ ?? 0,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ ?? 0,
                            ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ = d.ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ ?? false,
                            ΑΠΟ_ΚΑΤΑΤΑΞΗ = d.ΑΠΟ_ΚΑΤΑΤΑΞΗ ?? false,
                            ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ = d.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ ?? false,
                            ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ = d.ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ,
                            ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ = d.ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ,
                            ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ = d.ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ,
                            ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ = d.ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΕΚΔΟΣΗ = d.ΕΚΔΟΣΗ ?? true,
                            ΕΞΑΜΗΝΑ = d.ΕΞΑΜΗΝΑ,
                            ΕΠΩΝΥΜΟ_ΟΝΟΜΑ = d.ΕΠΩΝΥΜΟ_ΟΝΟΜΑ,
                            ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ = d.ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ,
                            ΜΗΤΡΩΝΥΜΟ = d.ΜΗΤΡΩΝΥΜΟ,
                            ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ
                        }).FirstOrDefault();

            return (data);
        }

        #endregion BEK EDIT FORM

        #endregion


        #region ΕΚΠΑΙΔΕΥΤΙΚΟΙ

        #region ΜΗΤΡΩΟ ΕΚΠΑΙΔΕΥΤΩΝ

        public ActionResult XTeacherInfoList()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            IEnumerable<TeacherInfoViewModel> data = teacherInfoService.Read();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένοι εκπαιδευτές για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            TeacherInfoViewModel teacher = data.First();

            PopulateSchools();
            return View(teacher);
        }

        public ActionResult TeacherInfo_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<TeacherInfoViewModel> data = teacherInfoService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PeriodsInfo_Read(int teacherId, [DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<TeacherPeriodsInfoViewModel> data = teacherInfoService.GetPeriods(teacherId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisInfo_Read(int teacherId, [DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<TeacherAnatheseisInfoViewModel> data = teacherInfoService.GetAnatheseis(teacherId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult XTeacherInfoEdit(int teacherId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            if (!(teacherId > 0))
            {
                string msg = "Ο κωδικός του εκπαιδευτικού δεν είναι έγκυρος. Η εγγραφή πρέπει να έχει αποθηκευτεί πρώτα.";
                return RedirectToAction("ErrorData", "Admin", new { notify = msg });
            }

            TeacherViewModel teacher = teacherService.GetRecord(teacherId);
            if (teacher == null)
            {
                string msg = "Προέκυψε πρόβλημα εύρεσης του εκπαιδευτικού.";
                return RedirectToAction("ErrorData", "Admin", new { notify = msg });
            }
            return View(teacher);
        }

        [HttpPost]
        public ActionResult XTeacherInfoEdit(int teacherId, TeacherViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            data.ΙΕΚ = Common.GetTeacherSchool(teacherId);

            string ErrorMsg = Common.ValidateTeacherFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                teacherService.UpdateRecord(data, teacherId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                TeacherViewModel newTeacher = teacherService.GetRecord(teacherId);
                return View(newTeacher);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public PartialViewResult GetTeacherRecord(int teacherId)
        {
            TeacherInfoViewModel teacher = teacherInfoService.GetRecord(teacherId);

            return PartialView("XTeacherInfoPartial", teacher);
        }

        #endregion

        #region ΜΗΤΡΩΟ ΑΝΑΘΕΣΕΩΝ ΕΚΠΑΙΔΕΥΤΩΝ

        public ActionResult XAnatheseisView()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            return View();
        }

        public ActionResult AnatheseisView_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = AnatheseisViewFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<QueryAnatheseisViewModel> AnatheseisViewFromDB()
        {
            var data = (from d in db.sqlANATHESEIS_VIEW
                        orderby d.SCHOOL_NAME, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.ΠΕΡΙΟΔΟΣ, d.EIDIKOTITA_TEXT, d.LESSON_DESC
                        select new QueryAnatheseisViewModel
                        {
                            ΕΑ_ΚΩΔ = d.ΕΑ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = (int)d.ΙΕΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_DESC = d.EIDIKOTITA_DESC,
                            ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            LESSON_DESC = d.LESSON_DESC,
                            ΩΡΕΣ_ΘΕΩΡΙΑ = d.ΩΡΕΣ_ΘΕΩΡΙΑ ?? 0,
                            ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ = d.ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ ?? 0,
                            SCHOOL_NAME = d.SCHOOL_NAME
                        }).ToList();

            return (data);
        }

        public ActionResult XAnatheseisPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            var data = (from d in db.sqlANATHESEIS_VIEW
                        orderby d.SCHOOL_NAME
                        select new QueryAnatheseisViewModel
                        {
                            ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        }).First();

            return View(data);
        }

        #endregion

        #region ΜΗΤΡΩΟ ΒΕΒΑΙΩΣΕΩΝ ΑΝΑΛΗΨΗΣ ΚΑΘΗΚΟΝΤΩΝ

        public ActionResult XTeacherAnalipseis(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (!TeachersExist())
            {
                string msg = "Δεν βρέθηκαν καταχωρημένοι εκπαιδευτικοί.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            // ok to populate combos
            PopulateSchools();
            PopulateTeachersInPeriods();
            PopulatePeriodoi();

            return View();
        }

        public ActionResult TeacherAnalipsi_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetTeacherAnalipseisFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<TeacherAnalipsiViewModel> GetTeacherAnalipseisFromDB()
        {
            var data = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΛΗΨΕΙΣ
                        orderby d.ΙΕΚ, d.TEACHER_ID, d.ΗΜΕΡΟΜΗΝΙΑ descending
                        select new TeacherAnalipsiViewModel
                        {
                            ΑΝΑΛΗΨΗ_ΚΩΔ = d.ΑΝΑΛΗΨΗ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            PERIOD_ID = d.PERIOD_ID,
                            ΥΠΕΓΡΑΦΗ = d.ΥΠΕΓΡΑΦΗ ?? true
                        }).ToList();

            return (data);
        }

        public ActionResult XTeacherAnalipsiPrint(int analipsiId)
        {
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

        #region ΒΕΒΑΙΩΣΕΙΣ ΠΡΟΫΠΗΡΕΣΙΑΣ ΕΚΠΑΙΔΕΥΤΩΝ

        public ActionResult xTeacherBebeoseis(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (!TeachersExist())
            {
                string msg = "Δεν βρέθηκαν καταχωρημένοι εκπαιδευτικοί.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            // ok to populate combos
            PopulateSchools();
            PopulateTeachers();
            PopulateSchoolYears();

            return View();
        }

        public ActionResult TeacherBebeosi_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetTeacherBebeoseisFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #region DATA FORM

        public ActionResult XTeacherBebeosiEdit(int bebeosiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            if (!(bebeosiId > 0))
            {
                string msg = "Ο κωδικός βεβαίωσης δεν είναι έγκυρος. Η εγγραφή πρέπει να έχει αποθηκευτεί πρώτα.";
                return RedirectToAction("Errordata", "Admin", new { notify = msg });
            }

            TeacherBebeoseisViewModel bebeosi = GetTeacherBebeosiFromDB(bebeosiId);
            int teacherId = (int)bebeosi.TEACHER_ID;

            TeacherInfoViewModel SelectedTeacher = teacherInfoService.GetRecord(teacherId);
            if (SelectedTeacher == null)
            {
                string notify = "Παρουσιάστηκε σφάλμα εύρεσης του εκπαιδευτικού.";
                return RedirectToAction("ErrorData", "Admin", new { notify });
            }
            else
            {
                ViewBag.TeacherData = SelectedTeacher;
            }

            return View(bebeosi);
        }

        [HttpPost]
        public ActionResult XTeacherBebeosiEdit(int bebeosiId, TeacherBebeoseisViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            TeacherBebeoseisViewModel bebeosi = GetTeacherBebeosiFromDB(bebeosiId);

            int teacherId = (int)bebeosi.TEACHER_ID;
            TeacherInfoViewModel SelectedTeacher = teacherInfoService.GetRecord(teacherId);

            if (SelectedTeacher == null)
            {
                string notify = "Παρουσιάστηκε σφάλμα.";
                return RedirectToAction("Index", "Admin", new { notify });
            }
            else
            {
                ViewBag.TeacherData = SelectedTeacher;
            }

            if (data != null)
            {
                ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ entity = db.ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ.Find(bebeosiId);

                entity.ΒΕΒΑΙΩΣΗ_ΚΩΔ = bebeosi.ΒΕΒΑΙΩΣΗ_ΚΩΔ;
                entity.ΙΕΚ = bebeosi.ΙΕΚ;
                entity.ΑΦΜ = bebeosi.ΑΦΜ;
                entity.TEACHER_ID = bebeosi.TEACHER_ID;
                entity.ΠΡΩΤΟΚΟΛΛΟ = bebeosi.ΠΡΩΤΟΚΟΛΛΟ;
                entity.ΗΜΕΡΟΜΗΝΙΑ = bebeosi.ΗΜΕΡΟΜΗΝΙΑ;
                entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = bebeosi.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
                entity.ΠΑΡΑΔΟΔΗΚΕ = bebeosi.ΠΑΡΑΔΟΔΗΚΕ;
                entity.ΓΙΑ_ΧΡΗΣΗ = data.ΓΙΑ_ΧΡΗΣΗ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                TeacherBebeoseisViewModel newdata = GetTeacherBebeosiFromDB(bebeosiId);
                return View(newdata);
            }
            else
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
                return View(data);
            }
        }

        #endregion DATA FORM

        public List<TeacherBebeoseisViewModel> GetTeacherBebeoseisFromDB()
        {
            var data = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ
                        orderby d.ΙΕΚ, d.ΕΚΠΑΙΔΕΥΤΕΣ.ΕΠΩΝΥΜΟ, d.ΕΚΠΑΙΔΕΥΤΕΣ.ΟΝΟΜΑ, d.ΗΜΕΡΟΜΗΝΙΑ descending
                        select new TeacherBebeoseisViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΓΙΑ_ΧΡΗΣΗ = d.ΓΙΑ_ΧΡΗΣΗ,
                            ΠΑΡΑΔΟΔΗΚΕ = d.ΠΑΡΑΔΟΔΗΚΕ ?? true
                        }).ToList();

            return (data);
        }

        public TeacherBebeoseisViewModel GetTeacherBebeosiFromDB(int bebeosiId)
        {
            var data = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ
                        where d.ΒΕΒΑΙΩΣΗ_ΚΩΔ == bebeosiId
                        select new TeacherBebeoseisViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΓΙΑ_ΧΡΗΣΗ = d.ΓΙΑ_ΧΡΗΣΗ,
                            ΠΑΡΑΔΟΔΗΚΕ = d.ΠΑΡΑΔΟΔΗΚΕ ?? true
                        }).FirstOrDefault();

            return (data);
        }

        #endregion

        #region ΟΙΚΕΙΟΘΕΛΕΙΣ ΑΠΟΧΩΡΗΣΕΙΣ

        public ActionResult XTeacherWithdrawals(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (!TeachersExist())
            {
                string msg = "Δεν βρέθηκαν καταχωρημένοι εκπαιδευτικοί.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            // ok to populate combos
            PopulateSchools();
            PopulateTeachers();
            PopulateSchoolYears();
            PopulateAitiesApoxorisi();

            return View();
        }

        public ActionResult Apoxorisi_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetTeacherWithdrawalsFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<TeacherWithdrawalViewModel> GetTeacherWithdrawalsFromDB()
        {
            var data = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΠΟΧΩΡΗΣΕΙΣ
                        orderby d.ΙΕΚ, d.ΣΧΟΛΙΚΟ_ΕΤΟΣ, d.ΗΜΕΡΟΜΗΝΙΑ descending
                        select new TeacherWithdrawalViewModel
                        {
                            ΑΠΟΧΩΡΗΣΗ_ΚΩΔ = d.ΑΠΟΧΩΡΗΣΗ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΑΙΤΙΟΛΟΓΙΑ = d.ΑΙΤΙΟΛΟΓΙΑ
                        }).ToList();

            return (data);
        }

        public ActionResult XWithdrawalsPrint()
        {
            var data = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΠΟΧΩΡΗΣΕΙΣ
                        select new TeacherWithdrawalViewModel
                        {
                            ΑΠΟΧΩΡΗΣΗ_ΚΩΔ = d.ΑΠΟΧΩΡΗΣΗ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).First();

            return View(data);
        }

        #endregion

        public ActionResult XTeacherDataPrint(int teacherId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ
                        where d.TEACHER_ID == teacherId
                        select new TeacherViewModel
                        {
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ
                        }).FirstOrDefault();

            return View(data);
        }


        #endregion ΕΚΠΑΙΔΕΥΤΙΚΟΙ


        #region ΜΗΤΡΩΑ ΠΡΑΚΤΙΚΗΣ

        #region ΜΗΤΡΩΟ ΕΡΓΟΔΟΤΩΝ (ΚΑΙ ΠΡΑΚΤΙΚΗΣ)

        public ActionResult XErgodotesInfoList()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            IEnumerable<ErgodotesViewModel> data = ergodotisInfoService.Read();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένοι εργοδότες για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            ErgodotesViewModel ergodotis = data.First();
            PopulateSchools();
            return View(ergodotis);
        }

        public ActionResult ErgodotesInfo_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<ErgodotesViewModel> data = ergodotisInfoService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        #region ERGODOTES INFO DATA FORM

        public ActionResult XErgodotesInfoEdit(int ergodotisId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            ErgodotesViewModel ergodotis = ergodotesService.GetRecord(ergodotisId);
            if (ergodotis == null)
            {
                string msg = "Προέκυψε πρόβλημα εύρεσης των στοιχείων εργοδότη.";
                return RedirectToAction("ErrorData", "Admin", new { notify = msg });
            }

            return View(ergodotis);
        }

        [HttpPost]
        public ActionResult XErgodotesInfoEdit(int ergodotisId, ErgodotesViewModel model)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            model.ΙΕΚ = Common.GetErgodotisSchool(ergodotisId);

            if (ModelState.IsValid)
            {
                ergodotesService.UpdateRecord(model, ergodotisId);

                ErgodotesViewModel newentity = ergodotesService.GetRecord(ergodotisId);
                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");

                return View(newentity);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(model);
        }

        #endregion ERGODOTES INFO DATA FORM


        #region PARTIAL VIEW

        public ActionResult PraktikiInfo_Read(int ergodotisId, [DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<PraktikiInfoViewModel> data = ergodotisInfoService.GetPraktikes(ergodotisId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetErgodotisRecord(int ergodotisId)
        {
            ErgodotesViewModel data = ergodotisInfoService.GetRecord(ergodotisId);

            return PartialView("XErgodotesInfoPartial", data);
        }

        #endregion PARTIAL VIEW

        #endregion


        #region ΜΗΤΡΩΟ ΣΠΟΥΔΑΣΤΩΝ ΣΕ ΠΡΑΚΤΙΚΗ

        public ActionResult XRegPraktikiStudents()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            IEnumerable<regPraktikiStudentViewModel> data = praktikiRegistryService.ReadStudents();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένοι σπουδαστές πρακτικής για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            regPraktikiStudentViewModel model = data.First();

            return View(model);
        }

        public ActionResult RegPraktikiStudents_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<regPraktikiStudentViewModel> data = praktikiRegistryService.ReadStudents();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPraktikiStudentRecord(int praktikiId)
        {
            regPraktikiStudentViewModel data = praktikiRegistryService.ReadStudents()
                .Where(e => e.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ == praktikiId)
                .FirstOrDefault();

            return PartialView("XRegPraktikiStudentPartial", data);
        }


        #endregion


        #region ΑΙΤΗΣΕΙΣ ΠΡΑΚΤΙΚΗΣ

        public ActionResult XRegPraktikiAitiseis()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            List<regPraktikiAitisiViewModel> data = praktikiRegistryService.ReadAitiseis();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αιτήσεις πρακτικής για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            regPraktikiAitisiViewModel model = data.First();

            return View(model);
        }

        public ActionResult RegPraktikiAitiseis_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<regPraktikiAitisiViewModel> data = praktikiRegistryService.ReadAitiseis();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPraktikiAitisiRecord(int aitisiId)
        {
            regPraktikiAitisiViewModel data = praktikiRegistryService.ReadAitiseis().Where(e => e.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId).FirstOrDefault();

            return PartialView("XRegPraktikiAitisiPartial", data);
        }


        #endregion ΑΙΤΗΣΕΙΣ ΠΡΑΚΤΙΚΗΣ


        #region ΑΠΟΦΑΣΕΙΣ ΠΡΑΚΤΙΚΗΣ

        public ActionResult XRegPraktikiApofaseis()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            List<regPraktikiApofasiViewModel> data = praktikiRegistryService.ReadApofaseis();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αποφάσεις πρακτικής για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            regPraktikiApofasiViewModel model = data.First();

            return View(model);
        }

        public ActionResult RegPraktikiApofaseis_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<regPraktikiApofasiViewModel> data = praktikiRegistryService.ReadApofaseis();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPraktikiApofasiRecord(int apofasiId)
        {
            regPraktikiApofasiViewModel data = praktikiRegistryService.ReadApofaseis().Where(e => e.ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ == apofasiId).FirstOrDefault();

            return PartialView("XRegPraktikiApofasiPartial", data);
        }


        #endregion ΑΠΟΦΑΣΕΙΣ ΠΡΑΚΤΙΚΗΣ


        #region ΠΑΡΟΥΣΙΕΣ ΠΡΑΚΤΙΚΗΣ

        public ActionResult XRegPraktikiParousies()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            List<regPraktikiParousiaViewModel> data = praktikiRegistryService.ReadParousies();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες παρουσίες πρακτικής για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            regPraktikiParousiaViewModel model = data.First();

            return View(model);
        }

        public ActionResult RegPraktikiParousies_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<regPraktikiParousiaViewModel> data = praktikiRegistryService.ReadParousies();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPraktikiParousiaRecord(int bebeosiId)
        {
            regPraktikiParousiaViewModel data = praktikiRegistryService.ReadParousies().Where(e => e.ΒΕΒΑΙΩΣΗ_ΚΩΔ == bebeosiId).FirstOrDefault();

            return PartialView("XRegPraktikiParousiaPartial", data);
        }


        #endregion ΠΑΡΟΥΣΙΕΣ ΠΡΑΚΤΙΚΗΣ


        #region ΠΕΡΑΤΩΣΕΙΣ ΠΡΑΚΤΙΚΗΣ

        public ActionResult XRegPraktikiPeratoseis()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            List<regPraktikiPeratosiViewModel> data = praktikiRegistryService.ReadPeratoseis();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες παρουσίες πρακτικής για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            regPraktikiPeratosiViewModel model = data.First();

            return View(model);
        }

        public ActionResult RegPraktikiPeratoseis_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<regPraktikiPeratosiViewModel> data = praktikiRegistryService.ReadPeratoseis();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPraktikiPeratosiRecord(int bebeosiId)
        {
            regPraktikiPeratosiViewModel data = praktikiRegistryService.ReadPeratoseis().Where(e => e.ΒΕΒΑΙΩΣΗ_ΚΩΔ == bebeosiId).FirstOrDefault();

            return PartialView("XRegPraktikiPeratosiPartial", data);
        }


        #endregion ΠΕΡΑΤΩΣΕΙΣ ΠΡΑΚΤΙΚΗΣ


        #region ΕΛΕΓΧΟΙ ΠΡΑΚΤΙΚΗΣ

        public ActionResult XRegPraktikiElegxoi()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            List<regPraktikiElegxosViewModel> data = praktikiRegistryService.ReadElegxoi();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένοι έλεγχοι πρακτικής για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            regPraktikiElegxosViewModel model = data.First();

            return View(model);
        }

        public ActionResult RegPraktikiElegxoi_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<regPraktikiElegxosViewModel> data = praktikiRegistryService.ReadElegxoi();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPraktikiElegxosRecord(int elegxosId)
        {
            regPraktikiElegxosViewModel data = praktikiRegistryService.ReadElegxoi().Where(e => e.ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ == elegxosId).FirstOrDefault();

            return PartialView("XRegPraktikiElegxosPartial", data);
        }


        #endregion ΑΠΟΦΑΣΕΙΣ ΠΡΑΚΤΙΚΗΣ

        #endregion


        #region ΠΡΟΓΡΑΜΜΑ - ΑΠΟΥΣΙΕΣ ΚΑΙ ΒΑΘΜΟΙ

        // Tmimata data source used in grades and programma
        public List<sqlTmimataViewModel> GetTmimataFromDB(int schoolId)
        {
            var data = (from d in db.sqlΤΜΗΜΑΤΑ
                        where d.ΙΕΚ == schoolId
                        orderby d.EIDIKOTITA_TEXT, d.PERIOD_ID descending, d.TERM_ID
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

        // Tmimata data source used in grades and programma
        public ActionResult Tmimata_Read([DataSourceRequest] DataSourceRequest request, int schoolId = 0)
        {
            var data = GetTmimataFromDB(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        #region ΣΥΓΚΕΝΤΡΩΤΙΚΕΣ ΑΠΟΥΣΙΕΣ

        public ActionResult AdminApousiesLesson()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (!IekTmimataExist() || !StudentsExist())
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα ή/και σπουδαστές.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
           
            PopulateSchools();
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

        public ActionResult AdminApousiesLessonPrint(int tmimaId, int studentId = 0)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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

        #endregion


        #region ΑΝΑΛΥΤΙΚΕΣ ΑΠΟΥΣΙΕΣ

        public ActionResult AdminApousiesDetail()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (!IekTmimataExist() || !StudentsExist())
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα ή/και σπουδαστές.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }

            PopulateSchools();
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

        public ActionResult AdminApousiesDetailPrint(int tmimaId, int studentId = 0)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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

        #endregion


        #region ΤΕΛΙΚΟΙ ΒΑΘΜΟΙ ΑΝΑ ΣΠΟΥΔΑΣΤΗ

        public ActionResult AdminGradesFinal()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (!IekTmimataExist() || !StudentsExist())
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα ή/και σπουδαστές.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }

            PopulateSchools();
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

        public ActionResult AdminGradesFinalPrint(int tmimaId, int studentId = 0)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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


        #region ΤΕΛΙΚΟΙ ΒΑΘΜΟΙ ΚΑΙ ΑΠΟΥΣΙΕΣ

        public ActionResult AdminGradesApousies()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (!IekTmimataExist() || !StudentsExist())
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα ή/και σπουδαστές.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }

            PopulateSchools();
            return View();
        }

        public ActionResult AdminGradesApousiesPrint(int tmimaId, int studentId = 0)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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


        #region ΕΝΤΥΠΟ ΒΑΘΜΟΛΟΓΙΩΝ ΕΞΑΜΗΝΟΥ

        public ActionResult AdminAllGradesReport()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (!IekTmimataExist() || !StudentsExist())
            {
                string msg = "Δεν βρέθηκαν καταχωρημένα τμήματα ή/και σπουδαστές.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }

            PopulateSchools();
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

        public ActionResult AdminAllGradesEidikotitaPrint(int eidikotitaId, string period, int schoolId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

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

        public ActionResult AdminAllGradesReportPrint(int tmimaId, int studentId = 0)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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

        #endregion

        #endregion


        #region STATISTICS

        #region ΕΛΣΤΑΤ

        public ActionResult XelstatTeacherGenderIekPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XelstatTeacherGenderAllPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }
        
        public ActionResult XelstatStudentsEntrantsPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XelstatStudentsGraduatesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XelstatStudentsRegisteredPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XelstatStudentsEntrants2Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XelstatStudentsGraduates2Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XelstatStudentsRegistered2Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }


        // ΓΕΝΙΚΑ ΣΥΝΟΛΑ (ΑΝΑ ΣΧΟΛΙΚΟ ΈΤΟΣ)

        public ActionResult XelstatSumStudentsEntrantsPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XelstatSumStudentsGraduatesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XelstatSumStudentsRegisteredPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XelstatSumStudentsEntrants2Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XelstatSumStudentsGraduates2Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XelstatSumStudentsRegistered2Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XelstatISCEDEidikotitesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult XelstatISCEDSector0000Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult XelstatISCEDSector000Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult XelstatISCEDSector00Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult XelstatRegionRegisteredPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        #endregion ELSTAT


        #region ΑΛΛΕΣ ΣΤΑΤΙΣΤΙΚΕΣ
        // ΕΙΔΙΚΕΣ ΣΤΑΤΙΣΤΙΚΕΣ

        public ActionResult XstatStudentsEidikotitaMenPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XstatStudentsEidikotitaWomenPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XstatStudentsEidikotitaAgePrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XstatStudentsAgeGroupPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XstatSummaryAgeGroupPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XstatSummaryEidikotitaIekPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XstatSummaryGenderIekPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XstatSummaryGenderPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        public ActionResult XstatSummaryIekEidikotitesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.SYS_SCHOOLS select d).First();

            GeneralReportParameters parameters = new GeneralReportParameters
            {
                SCHOOL_ID = data.SCHOOL_ID,
                PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID,
                PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID
            };
            return View(parameters);
        }

        #endregion ΑΛΛΕΣ ΣΤΑΤΙΣΤΙΚΕΣ

        #endregion


        #region ΝΕΕΣ ΣΤΑΤΙΣΤΙΚΕΣ ΟΘΟΝΕΣ (26/5/2018)

        #region ΕΛΣΤΑΤ

        public ActionResult xxReportsElstatList(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }

            return View();
        }

        public ActionResult ReportSelectorElstat(int reportId = 0)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            // logic of report selection here
            if (reportId == 1)
            {
                return RedirectToAction("XelstatStudentsEntrantsPrint", "Admin");
            }
            else if (reportId == 2)
            {
                return RedirectToAction("XelstatStudentsRegisteredPrint", "Admin");
            }
            else if (reportId == 3)
            {
                return RedirectToAction("XelstatStudentsGraduatesPrint", "Admin");
            }
            else if (reportId == 4)
            {
                return RedirectToAction("XelstatStudentsEntrants2Print", "Admin");
            }
            else if (reportId == 5)
            {
                return RedirectToAction("XelstatStudentsRegistered2Print", "Admin");
            }
            else if (reportId == 6)
            {
                return RedirectToAction("XelstatStudentsGraduates2Print", "Admin");
            }
            else if (reportId == 7)
            {
                return RedirectToAction("XelstatSumStudentsEntrantsPrint", "Admin");
            }
            else if (reportId == 8)
            {
                return RedirectToAction("XelstatSumStudentsRegisteredPrint", "Admin");
            }
            else if (reportId == 9)
            {
                return RedirectToAction("XelstatSumStudentsGraduatesPrint", "Admin");
            }
            else if (reportId == 10)
            {
                return RedirectToAction("XelstatSumStudentsEntrants2Print", "Admin");
            }
            else if (reportId == 11)
            {
                return RedirectToAction("XelstatSumStudentsRegistered2Print", "Admin");
            }
            else if (reportId == 12)
            {
                return RedirectToAction("XelstatSumStudentsGraduates2Print", "Admin");
            }
            else if (reportId == 13)
            {
                return RedirectToAction("XelstatTeacherGenderIekPrint", "Admin");
            }
            else if (reportId == 14)
            {
                return RedirectToAction("XelstatTeacherGenderAllPrint", "Admin");
            }
            else if (reportId == 31)
            {
                return RedirectToAction("XelstatRegionRegisteredPrint", "Admin");
            }
            else if (reportId == 32)
            {
                return RedirectToAction("XelstatISCEDSector0000Print", "Admin");
            }
            else if (reportId == 33)
            {
                return RedirectToAction("XelstatISCEDSector000Print", "Admin");
            }
            else if (reportId == 34)
            {
                return RedirectToAction("XelstatISCEDSector00Print", "Admin");
            }
            else if (reportId == 35)
            {
                return RedirectToAction("XelstatISCEDEidikotitesPrint", "Admin");
            }
            else
            {
                string msg = "Η έκθεση αυτή δεν υπάρχει καταχωρημένη.";
                return RedirectToAction("xxReportsElstatList", "Admin", new { notify = msg });
            }
        }

        public ActionResult ReportsElstat_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetReportsElstatFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SysReportViewModel> GetReportsElstatFromDB()
        {
            var data = (from d in db.ΣΥΣ_ΕΝΤΥΠΑ
                        orderby d.DOC_ID
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

        public ActionResult xxReportsSummaryList(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            // logic of report selection here
            if (reportId == 15)
            {
                return RedirectToAction("XstatSummaryAgeGroupPrint", "Admin");
            }
            else if (reportId == 16)
            {
                return RedirectToAction("XstatSummaryGenderPrint", "Admin");
            }
            else if (reportId == 17)
            {
                return RedirectToAction("XstatSummaryGenderIekPrint", "Admin");
            }
            else if (reportId == 18)
            {
                return RedirectToAction("XstatSummaryEidikotitaIekPrint", "Admin");
            }
            else if (reportId == 19)
            {
                return RedirectToAction("XstatSummaryIekEidikotitesPrint", "Admin");
            }
            else
            {
                string msg = "Η έκθεση αυτή δεν υπάρχει καταχωρημένη.";
                return RedirectToAction("xxReportDemoPrint", "Admin", new { notify = msg });
            }
        }

        public ActionResult ReportsSummary_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<SysReportViewModel> data = GetReportsSummaryFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SysReportViewModel> GetReportsSummaryFromDB()
        {
            var data = (from d in db.ΣΥΣ_ΕΝΤΥΠΑ
                        where d.DOC_CLASS == "SUMMARY"
                        orderby d.DOC_ID
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

        public ActionResult xxReportsManpowerList(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            // logic of report selection here
            if (reportId == 20)
            {
                return RedirectToAction("XTmimataDetailPrint", "Admin");
            }
            else if (reportId == 21)
            {
                return RedirectToAction("XTmimataSumPrint", "Admin");
            }
            else if (reportId == 22)
            {
                return RedirectToAction("XstatStudentsAgeGroupPrint", "Admin");
            }
            else if (reportId == 23)
            {
                return RedirectToAction("XstatStudentsEidikotitaMenPrint", "Admin");
            }
            else if (reportId == 24)
            {
                return RedirectToAction("XstatStudentsEidikotitaWomenPrint", "Admin");
            }
            else if (reportId == 25)
            {
                return RedirectToAction("XstatStudentsEidikotitaAgePrint", "Admin");
            }
            else if (reportId == 45)
            {
                return RedirectToAction("XStudentAddressesPrint", "Admin");
            }
            else if (reportId == 46)
            {
                return RedirectToAction("XNumberStudentsTerms13Print", "Admin");
            }
            else if (reportId == 47)
            {
                return RedirectToAction("XNumberStudentsTerms24Print", "Admin");
            }
            else
            {
                string msg = "Η έκθεση αυτή δεν υπάρχει καταχωρημένη.";
                return RedirectToAction("xxReportDemoPrint", "Admin", new { notify = msg });
            }
        }

        public ActionResult ReportsManpower_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<SysReportViewModel> data = GetReportsManpowerFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SysReportViewModel> GetReportsManpowerFromDB()
        {
            var data = (from d in db.ΣΥΣ_ΕΝΤΥΠΑ
                        where d.DOC_CLASS == "MANPOWER"
                        orderby d.DOC_ID
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

        public ActionResult xxReportsExternalList(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            // logic of report selection here
            if (reportId == 26)
            {
                return RedirectToAction("XHdikaTable", "Admin");
            }
            else if (reportId == 27)
            {
                return RedirectToAction("XHdikaTable2", "Admin");
            }
            else if (reportId == 28)
            {
                return RedirectToAction("XHdikaPraktiki", "Admin");
            }
            else if (reportId == 30)
            {
                return RedirectToAction("XOpenDataReport", "Admin");
            }
            else if (reportId == 40)
            {
                return RedirectToAction("xxEfkaStudentListPrint", "Admin");
            }
            else if (reportId == 49)
            {
                return RedirectToAction("ypethStudentsGraduatesPrint", "Admin");
            }
            else if (reportId == 50)
            {
                return RedirectToAction("ypethStudentsTablePrint", "Admin");
            }
            else if (reportId == 51)
            {
                return RedirectToAction("KartaNeonStudentsPrint", "Admin");
            }
            else
            {
                string msg = "Η έκθεση αυτή δεν υπάρχει καταχωρημένη.";
                return RedirectToAction("xxReportDemoPrint", "Admin", new { notify = msg });
            }
        }

        public ActionResult ReportsExternal_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<SysReportViewModel> data = GetReportsExternalFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SysReportViewModel> GetReportsExternalFromDB()
        {
            var data = (from d in db.ΣΥΣ_ΕΝΤΥΠΑ
                        where d.DOC_CLASS == "EXTERNAL"
                        orderby d.DOC_ID
                        select new SysReportViewModel
                        {
                            DOC_ID = d.DOC_ID,
                            DOC_NAME = d.DOC_NAME,
                            DOC_DESCRIPTION = d.DOC_DESCRIPTION,
                            DOC_CLASS = d.DOC_CLASS
                        }).ToList();
            return data;
        }

        public ActionResult KartaNeonStudentsPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult ypethStudentsTablePrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult ypethStudentsGraduatesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult XHdikaTable()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult XHdikaTable2()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult XHdikaPraktiki()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult XOpenDataReport()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult xxEfkaStudentListPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }


        #endregion ΣΤΟΙΧΕΙΑ ΓΙΑ ΦΟΡΕΙΣ


        #region ΣΤΟΙΧΕΙΑ ΓΙΑ ΕΙΔΙΚΕΣ ΕΚΘΕΣΕΙΣ

        public ActionResult xxReportsCustomList(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            // logic of report selection here
            if (reportId == 29)
            {
                return RedirectToAction("xxReportDemoPrint", "Admin");
            }
            else if (reportId == 36)
            {
                return RedirectToAction("xxActiveEidikotitesPrint", "Admin");
            }
            else if (reportId == 37)
            {
                return RedirectToAction("xxActiveEidikotitesYearPrint", "Admin");
            }
            else if (reportId == 38)
            {
                return RedirectToAction("xxTmimaTeachersLabPrint", "Admin");
            }
            else if (reportId == 39)
            {
                return RedirectToAction("xxTmimaTeachersLab2Print", "Admin");
            }
            else if (reportId == 41)
            {
                return RedirectToAction("xx_TmimaTeachersLabPrint", "Admin");
            }
            else if (reportId == 42)
            {
                return RedirectToAction("xx_TmimaTeachersLab2Print", "Admin");
            }
            else if (reportId == 43)
            {
                return RedirectToAction("XStudentsBekPeriferiaPrint", "Admin");
            }
            else if (reportId == 44)
            {
                return RedirectToAction("NewStudentsEidikotitaPrint", "Admin");
            }
            else if (reportId == 48)
            {
                return RedirectToAction("statStudentsEidikotitaTerm1Print", "Admin");
            }
            else if (reportId == 52)
            {
                return RedirectToAction("XApousiesDetailPrint", "Admin");
            }
            else if (reportId == 53)
            {
                return RedirectToAction("StudentsTerms13AgePrint", "Admin");
            }
            else
            {
                string msg = "Η έκθεση αυτή δεν υπάρχει καταχωρημένη.";
                return RedirectToAction("xxReportDemoPrint", "Admin", new { notify = msg });
            }
        }

        public ActionResult ReportsCustom_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<SysReportViewModel> data = GetReportsCustomFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SysReportViewModel> GetReportsCustomFromDB()
        {
            var data = (from d in db.ΣΥΣ_ΕΝΤΥΠΑ
                        where d.DOC_CLASS == "CUSTOM"
                        orderby d.DOC_ID
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

        public ActionResult StudentsTerms13AgePrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult XApousiesDetailPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult XStudentsBekPeriferiaPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult statStudentsEidikotitaTerm1Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult NewStudentsEidikotitaPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult xxReportDemoPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        #endregion


        #region LOCAL FUNCTIONS GETTERS

        // Getter for admins
        public JsonResult GetIekEidikotites()
        {
            var data = (from d in db.SYS_EIDIKOTITES_IEK
                        where d.APPROVED == true
                        orderby d.EIDIKOTITA_TEXT
                        select new SYS_EIDIKOTITES_IEKViewModel
                        {
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region REPORTS

        public ActionResult XEidikotitesKatartisiPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult XNumberStudentsTerms13Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult XNumberStudentsTerms24Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult XStudentAddressesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        // ΕΚΤΥΠΩΣΗ ΩΡΟΛΟΓΙΩΝ ΠΡΟΓΡΑΜΜΑΤΩΝ ΕΙΔΙΚΟΤΗΤΩΝ (ΝΕΟ 17-02-2019)
        public ActionResult XEidikotitesLessonsPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        // ΝΕΑ ΒΕΒΑΙΩΣΗ ΕΚΠΑΙΔΕΥΤΩΝ ΕΝΟΠΟΙΗΜΕΝΗ
        public ActionResult XBebeosiUnifiedPrint(int bebeosiId, int schoolyearId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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

        public ActionResult XBebeosiUnifiedTotalPrint(int bebeosiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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

        public ActionResult XBebeosiAPSPrint(int bebeosiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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

        public ActionResult XPraktikiBebeosiPrint(int praktikiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ
                        where d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ == praktikiId
                        select new ErgodotesPraktikiViewModel
                        {
                            ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ = d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                            ΙΕΚ = d.ΙΕΚ,
                            ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ = d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult XBebeosiSpoudesPrint(int bebeosiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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

        public ActionResult XBebeosiArmyPrint(int bebeosiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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

        public ActionResult XBekNewPrint(int bekId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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

        public ActionResult XPraktikiAitisiPrint(int aitisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.ΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΗ
                        where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                        select new PraktikiAitisiViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                            ΙΕΚ = d.ΙΕΚ,
                            ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult XPraktikiApofasiPrint(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.ΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΗ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ == apofasiId
                        select new PraktikiApofasiViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ = d.ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                            ΙΕΚ = d.ΙΕΚ,
                            ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult XPraktikiParousiaPrint(int bebeosiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.ΠΡΑΚΤΙΚΗ_ΠΑΡΟΥΣΙΑ
                        where d.ΒΕΒΑΙΩΣΗ_ΚΩΔ == bebeosiId
                        select new PraktikiParousiaViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                            ΙΕΚ = d.ΙΕΚ,
                            ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult XPraktikiPeratosiPrint(int bebeosiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ
                        where d.ΒΕΒΑΙΩΣΗ_ΚΩΔ == bebeosiId
                        select new PraktikiPeratosiViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                            ΙΕΚ = d.ΙΕΚ,
                            ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult XPraktikiElegxosPrint(int elegxosId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.ΠΡΑΚΤΙΚΗ_ΕΛΕΓΧΟΣ
                        where d.ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ == elegxosId
                        select new PraktikiElegxosViewModel
                        {
                            ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ = d.ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                            ΙΕΚ = d.ΙΕΚ,
                            ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult XTmimataDetailPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult XTmimataSumPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult xxActiveEidikotitesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult xxActiveEidikotitesYearPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        // Νέα έκθεση 23-02-2019
        public ActionResult xxTmimaTeachersLabPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult xxTmimaTeachersLab2Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult xx_TmimaTeachersLabPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult xx_TmimaTeachersLab2Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        #endregion


        #region MAINTENANCE

        public ActionResult UpdateEgrafesStudentId()
        {
            var egrafes = (from d in db.ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ select d).ToList();

            foreach(var e in egrafes)
            {
                ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ student = db.ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ.Find(e.ΜΕ_ΚΩΔ);

                int amk = (int)student.ΑΜΚ;
                int iek = (int)student.ΙΕΚ;

                student.ΜΑΘΗΤΗΣ_ΚΩΔ = Common.GetStudentID(amk, iek);
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
            }
            db.SaveChanges();

            string msg = "Η διαδικασία ενημέρωσης κωδικών σπουδαστών ολοκληρώθηκε.";

            return RedirectToAction("StudentInfoList", "Admin", new { notify = msg });
        }

        public ActionResult UpdatePraktikiStudentId()
        {
            var egrafes = (from d in db.ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ select d).ToList();

            foreach (var e in egrafes)
            {
                ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ student = db.ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ.Find(e.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ);

                int amk = (int)student.ΜΑΘΗΤΗΣ_ΑΜΚ;
                int iek = (int)student.ΙΕΚ;

                student.ΜΑΘΗΤΗΣ_ΚΩΔ = Common.GetStudentID(amk, iek);
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
            }
            db.SaveChanges();

            string msg = "Η διαδικασία ενημέρωσης κωδικών σπουδαστών ολοκληρώθηκε.";

            return RedirectToAction("XStudentInfoList", "Admin", new { notify = msg });
        }

        public ActionResult UpdateTeacherEidikotitaCode()
        {
            var egrafes = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ select d).ToList();

            foreach (var e in egrafes)
            {
                ΕΚΠΑΙΔΕΥΤΕΣ teacher = db.ΕΚΠΑΙΔΕΥΤΕΣ.Find(e.TEACHER_ID);
                if (e.ΕΙΔΙΚΟΤΗΤΑ != null)
                {
                    int eidikotita = (int)e.ΕΙΔΙΚΟΤΗΤΑ;
                    string code = db.SYS_EIDIKOTITES.Find(eidikotita).EIDIKOTITA_CODE;
                    if (teacher.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ == null) teacher.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = code;
                }
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
            }
            string msg = "Η διαδικασία ενημέρωσης κωδικών ειδικοτήτων ολοκληρώθηκε.";

            return RedirectToAction("XTeacherInfoList", "Admin", new { notify = msg });
        }

        public ActionResult UpdateTeacherPeriods()
        {
            DateTime hire_date = Convert.ToDateTime("10/10/2016");
            string apofasi = "9999/01-10-2016";
            string ada = "000011111-ABCD";
            int period = 5;
            
            var teachers = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ select d).ToList();

            foreach(var t in teachers)
            {
                var periodoi = (from p in db.ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ where p.TEACHER_ID == t.TEACHER_ID select p).ToList();
                if (periodoi.Count == 0)
                {
                    ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ newData = new ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ()
                    {
                        ΑΦΜ = t.ΑΦΜ,
                        ΙΕΚ = t.ΙΕΚ,
                        TEACHER_ID = t.TEACHER_ID,
                        ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗΣ = hire_date,
                        ΑΠΟΦΑΣΗ = apofasi,
                        ΑΔΑ = ada,
                        ΠΕΡΙΟΔΟΣ_ΚΩΔ = period
                    };
                    db.ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ.Add(newData);
                    db.SaveChanges();
                }
            }
            string msg = "Η διαδικασία ενημέρωσης περιόδων απασχόλησης ολοκληρώθηκε.";
            return RedirectToAction("XTeacherInfoList", "Admin", new { notify = msg });
        }

        // ΝΑ ΓΙΝΕΙ ΣΤΗ ΒΑΣΗ ΤΟΥ ΣΕΡΒΕΡ
        public ActionResult UpdateTmimaPraktikiAitiseis()
        {
            var source = (from d in db.sqlSTUDENTS_IN_PRAKTIKI select d).ToList();

            foreach (var item in source)
            {
                var aitisiID = 0;
                var search = (from d in db.ΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΗ where d.ΜΑΘΗΤΗΣ_ΚΩΔ == item.STUDENT_ID && d.ΕΡΓΟΔΟΤΗΣ == item.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ select d).FirstOrDefault();
                if (search != null)
                {
                    aitisiID = search.ΑΙΤΗΣΗ_ΚΩΔ;
                }
                ΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΗ entity = db.ΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΗ.Find(aitisiID);
                if (entity != null)
                {
                    entity.ΤΜΗΜΑ_ΚΩΔ = item.ΤΜΗΜΑ_ΚΩΔ;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            string msg = "Η διαδικασία ενημέρωσης των κωδικών τμημάτων ολοκληρώθηκε.";
            return RedirectToAction("XRegPraktikiAitiseis", "Admin", new { notify = msg });
        }

        public ActionResult UpdateTmimaPraktikiApofaseis()
        {
            var source = (from d in db.ΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΗ select d).ToList();

            foreach (var item in source)
            {
                var aitisiID = item.ΑΙΤΗΣΗ_ΚΩΔ;
                var target = (from d in db.ΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΗ where d.ΑΙΤΗΣΗ_ΚΩΔΙΚΟΣ == aitisiID select d).ToList();

                foreach (var apofasi in target) 
                {
                    ΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΗ entity = db.ΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΗ.Find(apofasi.ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ);
                    if (entity != null)
                    {
                        entity.ΤΜΗΜΑ_ΚΩΔ = item.ΤΜΗΜΑ_ΚΩΔ;
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }

            string msg = "Η διαδικασία ενημέρωσης των κωδικών τμημάτων ολοκληρώθηκε.";
            return RedirectToAction("XRegPraktikiApofaseis", "Admin", new { notify = msg });
        }

        public ActionResult UpdateTmimaPraktikiPeratoseis()
        {
            var source = (from d in db.sqlSTUDENTS_IN_PRAKTIKI orderby d.STUDENT_ID, d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ select d).ToList();

            foreach (var item in source)
            {
                ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ entity = db.ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ
                                           .Where(d => d.ΜΑΘΗΤΗΣ_ΚΩΔ == item.STUDENT_ID && d.ΕΡΓΟΔΟΤΗΣ == item.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ && d.ΤΜΗΜΑ_ΚΩΔ == null)
                                           .FirstOrDefault();
                if (entity != null)
                {
                    entity.ΤΜΗΜΑ_ΚΩΔ = item.ΤΜΗΜΑ_ΚΩΔ;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            string msg = "Η διαδικασία ενημέρωσης των κωδικών τμημάτων ολοκληρώθηκε.";
            return RedirectToAction("XRegPraktikiPeratoseis", "Admin", new { notify = msg });
        }

        public ActionResult UpdateTmimaPraktikiParousies()
        {
            var source = (from d in db.sqlSTUDENTS_IN_PRAKTIKI orderby d.STUDENT_ID, d.ΤΜΗΜΑ_ΚΩΔ, d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ select d).ToList();

            foreach (var item in source)
            {
                ΠΡΑΚΤΙΚΗ_ΠΑΡΟΥΣΙΑ entity = db.ΠΡΑΚΤΙΚΗ_ΠΑΡΟΥΣΙΑ
                                           .Where(d => d.ΜΑΘΗΤΗΣ_ΚΩΔ == item.STUDENT_ID && d.ΕΡΓΟΔΟΤΗΣ == item.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ && d.ΤΜΗΜΑ_ΚΩΔ == null)
                                           .FirstOrDefault();
                if (entity != null)
                {
                    entity.ΤΜΗΜΑ_ΚΩΔ = item.ΤΜΗΜΑ_ΚΩΔ;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            string msg = "Η διαδικασία ενημέρωσης των κωδικών τμημάτων ολοκληρώθηκε.";
            return RedirectToAction("XRegPraktikiParousies", "Admin", new { notify = msg });
        }

        #endregion

    }
}