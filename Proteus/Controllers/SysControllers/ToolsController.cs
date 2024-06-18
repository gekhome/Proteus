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
    public class ToolsController : ControllerUnit
    {
        private readonly ProteusDBEntities db;

        private readonly ISchoolYearService schoolYearService;
        private readonly IPeriodoiService periodoiService;
        private readonly ITmimaService tmimaService;
        private readonly ISpoudesService spoudesService;
        private readonly IApolytiriaService apolytiriaService;
        private readonly ILessonService lessonService;
        private readonly IEidikotitaService eidikotitaService;
        private readonly IEidikotitesIekService eidikotitesIekService;
        private readonly IEidikotitesKatartisiService eidikotitesKatartisiService;
        private readonly IApoxorisiAitiaService apoxorisiAitiaService;

        public ToolsController(ProteusDBEntities entities, ISchoolYearService schoolYearService,
            IPeriodoiService periodoiService, ITmimaService tmimaService, ISpoudesService spoudesService,
            IApolytiriaService apolytiriaService, ILessonService lessonService, IEidikotitaService eidikotitaService,
            IEidikotitesIekService eidikotitesIekService,  IEidikotitesKatartisiService eidikotitesKatartisiService, 
            IApoxorisiAitiaService apoxorisiAitiaService) : base(entities)
        {
            db = entities;

            this.schoolYearService = schoolYearService;
            this.periodoiService = periodoiService;
            this.tmimaService = tmimaService;
            this.spoudesService = spoudesService;
            this.apolytiriaService = apolytiriaService;
            this.lessonService = lessonService;
            this.eidikotitaService = eidikotitaService;
            this.eidikotitesIekService = eidikotitesIekService;
            this.eidikotitesKatartisiService = eidikotitesKatartisiService;
            this.apoxorisiAitiaService = apoxorisiAitiaService;
        }


        #region ΚΑΡΤΕΛΑ ΣΤΟΙΧΕΙΩΝ ΣΧΟΛΕΙΟΥ

        public ActionResult SchoolEdit()
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

            SYS_SCHOOLSViewModel schoolData = SchoolViewModelFromDB(schoolId);
            return View(schoolData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SchoolEdit(SYS_SCHOOLSViewModel data)
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

            if (ModelState.IsValid)
            {
                SYS_SCHOOLS entity = db.SYS_SCHOOLS.Find(schoolId);

                entity.SCHOOL_NAME = data.SCHOOL_NAME.Trim();
                entity.SCHOOL_PERIFERIAKI_ID = data.SCHOOL_PERIFERIAKI_ID;
                entity.SCHOOL_PERIFERIA_ID = data.SCHOOL_PERIFERIA_ID;
                entity.SCHOOL_DIMOS = data.SCHOOL_DIMOS;
                entity.SCHOOL_ADDRESS = data.SCHOOL_ADDRESS.Trim();
                entity.SCHOOL_TK_CITY = data.SCHOOL_TK_CITY.Trim();
                entity.SCHOOL_EMAIL = data.SCHOOL_EMAIL.Trim();
                entity.SCHOOL_PHONE = data.SCHOOL_PHONE.Trim();
                entity.SCHOOL_FAX = data.SCHOOL_FAX.Trim();
                entity.SCHOOL_DIRECTOR = data.SCHOOL_DIRECTOR.Trim();
                entity.SCHOOL_DEPUTY = entity.SCHOOL_DEPUTY.HasValue() ? data.SCHOOL_DEPUTY.Trim() : data.SCHOOL_DEPUTY;
                entity.SCHOOL_INFO = data.SCHOOL_INFO.Trim();
                entity.DIRECTOR_GENDER = data.DIRECTOR_GENDER;
                entity.DEPUTY_GENDER = data.DEPUTY_GENDER;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                // Notify here
                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                return View(data);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public SYS_SCHOOLSViewModel SchoolViewModelFromDB(int schoolId)
        {
            var data = (from s in db.SYS_SCHOOLS
                        where s.SCHOOL_ID == schoolId
                        select new SYS_SCHOOLSViewModel
                        {
                            SCHOOL_ID = s.SCHOOL_ID,
                            SCHOOL_NAME = s.SCHOOL_NAME,
                            SCHOOL_PERIFERIAKI_ID = s.SCHOOL_PERIFERIAKI_ID,
                            SCHOOL_PERIFERIA_ID = s.SCHOOL_PERIFERIA_ID,
                            SCHOOL_DIMOS = s.SCHOOL_DIMOS,
                            SCHOOL_ADDRESS = s.SCHOOL_ADDRESS,
                            SCHOOL_TK_CITY = s.SCHOOL_TK_CITY,
                            SCHOOL_EMAIL = s.SCHOOL_EMAIL,
                            SCHOOL_PHONE = s.SCHOOL_PHONE,
                            SCHOOL_FAX = s.SCHOOL_FAX,
                            SCHOOL_DIRECTOR = s.SCHOOL_DIRECTOR,
                            SCHOOL_DEPUTY = s.SCHOOL_DEPUTY,
                            SCHOOL_INFO = s.SCHOOL_INFO,
                            DIRECTOR_GENDER = s.DIRECTOR_GENDER,
                            DEPUTY_GENDER = s.DEPUTY_GENDER
                        }).FirstOrDefault();
            return data;
        }

        #endregion


        #region ΣΧΟΛΙΚΑ ΕΤΗ

        public ActionResult SchoolYearsList()
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

        public ActionResult SchoolYear_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<SchoolYearsViewModel> data = schoolYearService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΠΕΡΙΟΔΟΙ

        public ActionResult PeriodosList()
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

        #endregion


        #region ΒΑΣΙΚΕΣ ΣΠΟΥΔΕΣ

        public ActionResult SpoudesList()
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

        public ActionResult Spoudes_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<SpoudesViewModel> data = spoudesService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΑΠΟΛΥΤΗΡΙΑ

        public ActionResult ApolytiriaList()
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

        public ActionResult Apolytiria_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<ApolytiriaViewModel> data = apolytiriaService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΕΙΔΙΚΟΤΗΤΕΣ ΚΑΤΑΡΤΙΣΗΣ (READ-ONLY)

        public ActionResult EidikotitesKatartisiList()
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

        public ActionResult EidikotitaKatartisi_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<SYS_EIDIKOTITES_IEKViewModel> data = eidikotitesKatartisiService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EidikotitesKatartisiPrint()
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


        #region ΕΙΔΙΚΟΤΗΤΕΣ ΙΕΚ

        public ActionResult EidikotitesIekList()
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
            PopulateEidikotitesKatartisi();

            return View();
        }

        public ActionResult EidikotitaIek_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = GetLoginSchool().USER_SCHOOLID ?? 0;

            IEnumerable<IekEidikotitesViewModel> data = eidikotitesIekService.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaIek_Create([DataSourceRequest] DataSourceRequest request, IekEidikotitesViewModel data)
        {
            int schoolId = GetLoginSchool().USER_SCHOOLID ?? 0;
            var newdata = new IekEidikotitesViewModel();

            var existingData = db.IEK_EIDIKOTITES.Where(s => s.IEK_ID == schoolId && s.EIDIKOTITA_ID == data.EIDIKOTITA_ID).Count();
            if (existingData > 0)
                ModelState.AddModelError("", "Η ειδικότητα αυτή είναι ήδη καταχωρημένη για το ΙΕΚ αυτό.");

            if (data != null && ModelState.IsValid)
            {
                eidikotitesIekService.Create(data, schoolId);
                newdata = eidikotitesIekService.Refresh(data.IE_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaIek_Update([DataSourceRequest] DataSourceRequest request, IekEidikotitesViewModel data)
        {
            int schoolId = GetLoginSchool().USER_SCHOOLID ?? 0;
            var newdata = new IekEidikotitesViewModel();

            if (data != null && ModelState.IsValid)
            {
                eidikotitesIekService.Update(data, schoolId);
                newdata = eidikotitesIekService.Refresh(data.IE_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaIek_Destroy([DataSourceRequest] DataSourceRequest request, IekEidikotitesViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteEidikotita(data.EIDIKOTITA_ID, data.IEK_ID))
                {
                    eidikotitesIekService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί η ειδικότητα διότι υπάρχουν συσχετισμένα τμήματα.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΤΜΗΜΑΤΑ ΙΕΚ

        public ActionResult IekTmimaList()
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

            if (!IekEidikotitesExist(schoolId))
            {
                string msg = "Για να καταχωρήσετε τμήματα πρέπει πρώτα να ορίσετε τις ειδικότητες που υλοποιεί το ΙΕΚ.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            PopulateIekEidikotites(schoolId);
            PopulatePeriodoi();
            PopulateTerms();
            PopulatePA();

            return View();
        }

        public ActionResult Tmima_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            IEnumerable<TmimaViewModel> data = tmimaService.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Tmima_Create([DataSourceRequest] DataSourceRequest request, TmimaViewModel data)
        {
            int schoolId = GetLoginSchool().USER_SCHOOLID ?? 0;
            var newdata = new TmimaViewModel();

            var existingData = db.ΤΜΗΜΑ.Where(s => s.ΙΕΚ == schoolId && s.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ == data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ && s.ΠΕΡΙΟΔΟΣ_ΚΩΔ == data.ΠΕΡΙΟΔΟΣ_ΚΩΔ
                                              && s.ΕΞΑΜΗΝΟ == data.ΕΞΑΜΗΝΟ && s.ΠΑ_ΚΩΔ == data.ΠΑ_ΚΩΔ && s.ΤΜΗΜΑ_ΟΝΟΜΑ == data.ΤΜΗΜΑ_ΟΝΟΜΑ).Count();
            if (existingData > 0)
                ModelState.AddModelError("", "Υπάρχει ήδη τμήμα καταχωρημένο με αυτά τα χαρακτηριστικά.");

            if (data != null && ModelState.IsValid)
            {
                tmimaService.Create(data, schoolId);
                newdata = tmimaService.Refresh(data.ΤΜΗΜΑ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Tmima_Update([DataSourceRequest] DataSourceRequest request, TmimaViewModel data)
        {
            int schoolId = GetLoginSchool().USER_SCHOOLID ?? 0;
            var newdata = new TmimaViewModel();

            if (data != null && ModelState.IsValid)
            {
                tmimaService.Update(data, schoolId);
                newdata = tmimaService.Refresh(data.ΤΜΗΜΑ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Tmima_Destroy([DataSourceRequest] DataSourceRequest request, TmimaViewModel data)
        {
            ModelState.Clear();
            if (data != null)
            {
                if (!Kerberos.CanDeleteTmima(data.ΤΜΗΜΑ_ΚΩΔ, (int)data.ΙΕΚ))
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί το τμήμα διότι υπάρχει σε εγγραφές σπουδαστών ή σε ωρολόγιο πρόγραμμα.");

                if (ModelState.IsValid)
                {
                    tmimaService.Destroy(data);
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΜΑΘΗΜΑΤΑ ΕΙΔΙΚΟΤΗΤΩΝ ΚΑΤΑΡΤΙΣΗΣ

        public ActionResult EidikotitesLessons()
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
            PopulateTerms();
            PopulateLessonTypes();

            return View();
        }

        // Read action is EidikotitesKatartisi_Read in section #region Eidikotites Katartisis

        public ActionResult Lesson_Read([DataSourceRequest] DataSourceRequest request, int eidikotitaID = 0)
        {
            IEnumerable<LessonsIekViewModel> data = lessonService.Read(eidikotitaID);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // ΕΚΤΥΠΩΣΗ ΩΡΟΛΟΓΙΩΝ ΠΡΟΓΡΑΜΜΑΤΩΝ ΕΙΔΙΚΟΤΗΤΩΝ (ΝΕΟ 17-02-2019)
        public ActionResult EidikotitesLessonsPrint()
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


        #region ΕΙΔΙΚΟΤΗΤΕΣ ΕΚΠΑΙΔΕΥΤΩΝ ΝΕΕΣ - 2018

        public ActionResult EidikotitesList()
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
            PopulateKladoi();
            PopulateKladoiUnified();

            return View();
        }

        public ActionResult Eidikotita_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<EidikotitesViewModel> data = eidikotitaService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EidikotitesPrint()
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


        #region ΣΥΣΧΕΤΙΣΗ ΠΑΛΑΙΩΝ-ΝΕΩΝ ΕΙΔΙΚΟΤΗΤΩΝ

        public ActionResult EidikotitesOldNewPrint()
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


        #region ΚΛΑΔΟΙ ΩΡΑΡΙΑ

        public ActionResult KladosList()
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

        public ActionResult Klados_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<SYS_KLADOSViewModel> data = KladosViewModelFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SYS_KLADOSViewModel> KladosViewModelFromDB()
        {
            var data = (from d in db.SYS_KLADOS
                        orderby d.KLADOS_ID
                        select new SYS_KLADOSViewModel
                        {
                            KLADOS_ID = d.KLADOS_ID,
                            KLADOS_NAME = d.KLADOS_NAME,
                            KLADOS_CATEGORY = d.KLADOS_CATEGORY,
                            KLADOS_HOURS = d.KLADOS_HOURS
                        }).ToList();
            return data;
        }


        #endregion


        #region ΑΙΤΙΕΣ ΑΠΟΧΩΡΗΣΗΣ

        public ActionResult ApoxorisiAities()
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

        public ActionResult ApoxorisiAities_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<ApoxorisiAitiaViewModel> data = apoxorisiAitiaService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΠΕΡΙΦΕΡΕΙΕΣ-ΔΗΜΟΙ

        public ActionResult PeriferiesDimoi()
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

        public ActionResult Periferies([DataSourceRequest] DataSourceRequest request)
        {
            var periferies = db.SYS_PERIFERIES.Select(p => new PeriferiaViewModel()
            {
                PERIFERIA_ID = p.PERIFERIA_ID,
                PERIFERIA_NAME = p.PERIFERIA_NAME
            });
            return Json(periferies.ToDataSourceResult(request));
        }

        public ActionResult Dimoi([DataSourceRequest] DataSourceRequest request, int periferiaId)
        {
            var dimoi = db.SYS_DIMOS.Select(p => new DimosViewModel()
            {
                DIMOS_ID = p.DIMOS_ID,
                DIMOS = p.DIMOS,
                DIMOS_PERIFERIA = p.DIMOS_PERIFERIA
            }).Where(o => o.DIMOS_PERIFERIA == periferiaId).OrderBy(s => s.DIMOS);
            return Json(dimoi.ToDataSourceResult(request));
        }

        public ActionResult PeriferiesPrint()
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


        #region ΧΑΡΤΕΣ GOOGLE

        public ActionResult GoogleMaps()
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


        #region CONVERTERS

        public ActionResult UtilityHoursConverter()
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

            HoursConverterViewModel data = GetConverterData();

            return View(data);
        }

        [HttpPost]
        public ActionResult UtilityHoursConverter(HoursConverterViewModel data)
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

            if (data.Hours > 99999)
            {
                HoursConverterViewModel data0 = GetConverterData();
                data0.HoursWord = HumanFriendlyInteger.IntegerToWritten(data.Hours);
                return View(data0);
            }

            HoursConverterViewModel result = new HoursConverterViewModel
            {
                Hours = data.Hours,
                HoursWord = HumanFriendlyInteger.IntegerToWritten(data.Hours),

                PE_Months = Common.HoursToMonths(1, data.Hours),
                PE_Days = Common.HoursToDays(1, data.Hours),

                TE_Months = Common.HoursToMonths(2, data.Hours),
                TE_Days = Common.HoursToDays(2, data.Hours),

                DE_Months = Common.HoursToMonths(3, data.Hours),
                DE_Days = Common.HoursToDays(3, data.Hours),

                ET_Months = Common.HoursToMonths(4, data.Hours),
                ET_Days = Common.HoursToDays(4, data.Hours)
            };

            return View(result);
        }

        public HoursConverterViewModel GetConverterData()
        {
            var data = new HoursConverterViewModel
            {
                Hours = 0,
                PE_Months = 0,
                PE_Days = 0,
                TE_Months = 0,
                TE_Days = 0,
                DE_Months = 0,
                DE_Days = 0,
                ET_Months = 0,
                ET_Days = 0,
                HoursWord = ""
            };

            return (data);
        }

        public ActionResult UtilityGradeConverter()
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

            GradeConverterViewModel data = GetGradeData();

            return View(data);
        }

        [HttpPost]
        public ActionResult UtilityGradeConverter(GradeConverterViewModel data)
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

            GradeConverterViewModel result = new GradeConverterViewModel
            {
                GradeSum = data.GradeSum,
                NumberOfLessons = data.NumberOfLessons,
                GradeDecimal = Common.GradeDecimal(data.NumberOfLessons, data.GradeSum),
                GradeFractional = Common.DecimalToFractional(data.NumberOfLessons, data.GradeSum)
            };

            return View(result);
        }


        public GradeConverterViewModel GetGradeData()
        {
            var data = new GradeConverterViewModel
            {
                NumberOfLessons = 0,
                GradeSum = 0,
                GradeDecimal = 0.00,
                GradeFractional = "00 0/00"
            };

            return (data);
        }

        #endregion

    }
}