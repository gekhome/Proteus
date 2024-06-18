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
    public class SetupController : ControllerUnit
    {
        private readonly ProteusDBEntities db;

        private readonly IUserSchoolService userSchoolService;
        private readonly ISchoolYearService schoolYearService;
        private readonly IEidikotitaService eidikotitaService;
        private readonly IKladosUnifiedService kladosUnifiedService;
        private readonly IApoxorisiAitiaService apoxorisiAitiaService;

        public SetupController(ProteusDBEntities entities, IUserSchoolService userSchoolService,
            ISchoolYearService schoolYearService, IEidikotitaService eidikotitaService,
            IKladosUnifiedService kladosUnifiedService, IApoxorisiAitiaService apoxorisiAitiaService) : base(entities)
        {
            db = entities;

            this.userSchoolService = userSchoolService;
            this.schoolYearService = schoolYearService;
            this.eidikotitaService = eidikotitaService;
            this.kladosUnifiedService = kladosUnifiedService;
            this.apoxorisiAitiaService = apoxorisiAitiaService;
        }


        #region SCHOOL ACCOUNTS (USER_SCHOOL)

        public ActionResult UserSchools(string notify = null)
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
            if (notify != null) this.ShowMessage(MessageType.Info, notify);

            PopulateSchools();

            return View();
        }

        public List<UserSchoolViewModel> GetUserSchools()
        {
            var data = (from a in db.USER_SCHOOLS
                        orderby a.USER_SCHOOLID
                        select new UserSchoolViewModel
                        {
                            USER_ID = a.USER_ID,
                            USERNAME = a.USERNAME,
                            PASSWORD = a.PASSWORD,
                            USER_SCHOOLID = a.USER_SCHOOLID ?? 0,
                            ISACTIVE = a.ISACTIVE ?? false
                        }).ToList();
            return data;
        }

        public ActionResult CreatePasswords()
        {
            var schools = (from s in db.USER_SCHOOLS where s.USER_SCHOOLID >= 100 select s).ToList();

            Random rnd = new Random();
            foreach (var school in schools)
            {
                school.PASSWORD = Common.GeneratePassword(rnd);
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
            }

            string notify = "Η δημιουργία νέων κωδικών σχολείων ολοκληρώθηκε.";
            return RedirectToAction("ListSchool", "Setup", new { notify });
        }


        #region Grid CRUD Functions

        public ActionResult UserSchool_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<UserSchoolViewModel> data = userSchoolService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserSchool_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserSchoolViewModel> userSchools)
        {
            var results = new List<UserSchoolViewModel>();
            foreach (var userSchool in userSchools)
            {
                if (userSchool != null && ModelState.IsValid)
                {
                    userSchoolService.Create(userSchool);
                    results.Add(userSchool);
                }
            }
            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserSchool_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserSchoolViewModel> userSchools)
        {
            if (userSchools != null)
            {
                foreach (var userSchool in userSchools)
                {
                    userSchoolService.Update(userSchool);
                }
            }
            return Json(userSchools.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserSchool_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserSchoolViewModel> userSchools)
        {
            if (userSchools.Any())
            {
                foreach (var userSchool in userSchools)
                {
                    userSchoolService.Destroy(userSchool);
                }
            }
            return Json(userSchools.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #endregion


        #region ΣΧΟΛΙΚΑ ΕΤΗ

        public ActionResult SchoolYearsList()
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

        //[HttpPost]
        public ActionResult SchoolYear_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<SchoolYearsViewModel> data = schoolYearService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolYear_Create([DataSourceRequest] DataSourceRequest request, SchoolYearsViewModel data)
        {
            var newdata = new SchoolYearsViewModel();

            var existingData = db.SYS_SCHOOLYEARS.Where(s => s.SY_TEXT == data.SY_TEXT).Count();
            if (existingData > 0) 
                ModelState.AddModelError("", "Το σχολικό αυτό έτος υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                schoolYearService.Create(data);
                newdata = schoolYearService.Refresh(data.SY_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolYear_Update([DataSourceRequest] DataSourceRequest request, SchoolYearsViewModel data)
        {
            var newdata = new SchoolYearsViewModel();

            if (data != null & ModelState.IsValid)
            {
                schoolYearService.Update(data);
                newdata = schoolYearService.Refresh(data.SY_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolYear_Destroy([DataSourceRequest] DataSourceRequest request, SchoolYearsViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteSchoolYear(data.SY_ID))
                {
                    schoolYearService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί το σχολικό έτος διότι είναι σε χρήση.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΕΙΔΙΚΟΤΗΤΕΣ ΕΚΠΑΙΔΕΥΤΩΝ ΝΕΕΣ 2018

        public ActionResult EidikotitesList()
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
            PopulateKladoi();
            PopulateKladoiUnified();

            return View();
        }

        [HttpPost]
        public ActionResult Eidikotita_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<EidikotitesViewModel> data = eidikotitaService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eidikotita_Create([DataSourceRequest] DataSourceRequest request, EidikotitesViewModel data)
        {
            var newdata = new EidikotitesViewModel();

            var existingData = db.SYS_EIDIKOTITES.Where(s => s.EIDIKOTITA_NAME == data.EIDIKOTITA_NAME && s.EIDIKOTITA_CODE == data.EIDIKOTITA_CODE).Count();
            if (existingData > 0) 
                ModelState.AddModelError("", "Η ειδικότητα αυτή υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                eidikotitaService.Create(data);
                newdata = eidikotitaService.Refresh(data.EIDIKOTITA_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eidikotita_Update([DataSourceRequest] DataSourceRequest request, EidikotitesViewModel data)
        {
            var newdata = new EidikotitesViewModel();

            if (data != null)
            {
                eidikotitaService.Update(data);
                newdata = eidikotitaService.Refresh(data.EIDIKOTITA_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eidikotita_Destroy([DataSourceRequest] DataSourceRequest request, EidikotitesViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteTeacherEidikotita(data.EIDIKOTITA_ID))
                {
                    eidikotitaService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί η ειδικότητα αυτή διότι είναι σε χρήση.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΚΛΑΔΟΙ ΕΝΟΠΟΙΗΜΕΝΟΙ ΝΕΟ  (05-08-2019)

        public ActionResult KladosUnifiedList()
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

            PopulateKlados();
            PopulateSqlEidikotites2();
            return View();
        }

        [HttpPost]
        public ActionResult KladosUnified_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<KladosUnifiedViewModel> data = kladosUnifiedService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KladosUnified_Create([DataSourceRequest] DataSourceRequest request, KladosUnifiedViewModel data)
        {
            var newdata = new KladosUnifiedViewModel();

            var existingData = db.SYS_KLADOS_ENIAIOS.Where(s => s.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ == data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ).Count();
            if (existingData > 0) 
                ModelState.AddModelError("", "Η κλάδος αυτός υπάρχει ήδη καταχωρημένος. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                kladosUnifiedService.Create(data);
                newdata = kladosUnifiedService.Refresh(data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KladosUnified_Update([DataSourceRequest] DataSourceRequest request, KladosUnifiedViewModel data)
        {
            var newdata = new KladosUnifiedViewModel();

            if (data != null && ModelState.IsValid)
            {
                kladosUnifiedService.Update(data);
                newdata = kladosUnifiedService.Refresh(data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KladosUnified_Destroy([DataSourceRequest] DataSourceRequest request, KladosUnifiedViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteKladosUnified(data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ) )
                {
                    kladosUnifiedService.Destroy(data);
                }
                else 
                {
                    ModelState.AddModelError("", "Υπάρχουν ειδικότητες σ' αυτό τον ενοποιημένο κλάδο. Η διαγραφή ακυρώθηκε.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        #region ΠΛΕΓΜΑ ΕΙΔΙΚΟΤΗΤΩΝ ΕΝΙΑΙΩΝ ΚΛΑΔΩΝ

        public ActionResult sqlEidikotitaKU_Read([DataSourceRequest] DataSourceRequest request, int kladosunifiedId = 0)
        {
            IEnumerable<sqlEidikotitesKUViewModel> data = kladosUnifiedService.GetEidikotites(kladosunifiedId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult sqlEidikotitaKU_Set([DataSourceRequest] DataSourceRequest request, sqlEidikotitesKUViewModel data, int kladosunifiedId = 0)
        {
            var newdata = new sqlEidikotitesKUViewModel();

            if (kladosunifiedId > 0)
            {
                if (data != null)
                {
                    kladosUnifiedService.SetEidikotita(data, kladosunifiedId);
                    newdata = kladosUnifiedService.RefreshEidikotita(data.EIDIKOTITA_ID);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε έναν κλάδο ενοποίησης. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult sqlEidikotitaKU_Reset([DataSourceRequest] DataSourceRequest request, sqlEidikotitesKUViewModel data, int kladosunifiedId = 0)
        {
            var newdata = new sqlEidikotitesKUViewModel();

            if (kladosunifiedId > 0)
            {
                if (data != null)
                {
                    kladosUnifiedService.ResetEidikotita(data, kladosunifiedId);
                    newdata = kladosUnifiedService.RefreshEidikotita(data.EIDIKOTITA_ID);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε έναν κλάδο ενοποίησης. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion


        #region ΩΡΑΡΙΑ ΚΛΑΔΩΝ

        public ActionResult XKladosList()
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

        public ActionResult Klados_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = KladosViewModelFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Klados_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<SYS_KLADOSViewModel> data)
        {
            var results = new List<SYS_KLADOSViewModel>();
            foreach (var item in data)
            {
                if (item != null & ModelState.IsValid)
                {
                    SYS_KLADOS modKlados = db.SYS_KLADOS.Find(item.KLADOS_ID);

                    modKlados.KLADOS_NAME = item.KLADOS_NAME;
                    modKlados.KLADOS_CATEGORY = item.KLADOS_CATEGORY;
                    modKlados.KLADOS_HOURS = item.KLADOS_HOURS;

                    db.Entry(modKlados).State = EntityState.Modified;
                    db.SaveChanges();
                    results.Add(item);
                }
            }
            return Json(results.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
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

        public ActionResult XApoxorisiAities()
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

        public ActionResult Aities_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<ApoxorisiAitiaViewModel> data = apoxorisiAitiaService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aities_Create([DataSourceRequest] DataSourceRequest request, ApoxorisiAitiaViewModel data)
        {
            var newdata = new ApoxorisiAitiaViewModel();

            var existingData = db.ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΕΣ.Where(s => s.ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΑ == data.ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΑ).Count();
            if (existingData > 0) 
                ModelState.AddModelError("", "Αυτή η αιτία αποχώρησης υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                apoxorisiAitiaService.Create(data);
                newdata = apoxorisiAitiaService.Refresh(data.ΑΠΟΧΩΡΗΣΗ_ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aities_Update([DataSourceRequest] DataSourceRequest request, ApoxorisiAitiaViewModel data)
        {
            var newdata = new ApoxorisiAitiaViewModel();

            if (data != null && ModelState.IsValid)
            {
                apoxorisiAitiaService.Update(data);
                newdata = apoxorisiAitiaService.Refresh(data.ΑΠΟΧΩΡΗΣΗ_ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aities_Destroy([DataSourceRequest] DataSourceRequest request, ApoxorisiAitiaViewModel data)
        {
            if (data != null)
            {
                apoxorisiAitiaService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region CONVERTERS

        public ActionResult XUtilityHoursConverter()
        {
            HoursConverterViewModel data = GetConverterData();

            return View(data);
        }

        [HttpPost]
        public ActionResult XUtilityHoursConverter(HoursConverterViewModel data)
        {
            HoursConverterViewModel result = new HoursConverterViewModel();

            if (data.Hours > 99999)
            {
                HoursConverterViewModel data0 = GetConverterData();
                data0.HoursWord = HumanFriendlyInteger.IntegerToWritten(data.Hours);
                return View(data0);
            }

            result.Hours = data.Hours;
            result.HoursWord = HumanFriendlyInteger.IntegerToWritten(data.Hours);

            result.PE_Months = Common.HoursToMonths(1, data.Hours);
            result.PE_Days = Common.HoursToDays(1, data.Hours);

            result.TE_Months = Common.HoursToMonths(2, data.Hours);
            result.TE_Days = Common.HoursToDays(2, data.Hours);

            result.DE_Months = Common.HoursToMonths(3, data.Hours);
            result.DE_Days = Common.HoursToDays(3, data.Hours);

            result.ET_Months = Common.HoursToMonths(4, data.Hours);
            result.ET_Days = Common.HoursToDays(4, data.Hours);

            return View(result);
        }

        public HoursConverterViewModel GetConverterData()
        {
            var data = new HoursConverterViewModel();
            data.Hours = 0;
            data.PE_Months = 0;
            data.PE_Days = 0;
            data.TE_Months = 0;
            data.TE_Days = 0;
            data.DE_Months = 0;
            data.DE_Days = 0;
            data.ET_Months = 0;
            data.ET_Days = 0;
            data.HoursWord = "";

            return (data);
        }

        public ActionResult XUtilityGradeConverter()
        {
            GradeConverterViewModel data = GetGradeData();

            return View(data);
        }

        [HttpPost]
        public ActionResult XUtilityGradeConverter(GradeConverterViewModel data)
        {
            GradeConverterViewModel result = new GradeConverterViewModel();

            result.GradeSum = data.GradeSum;
            result.NumberOfLessons = data.NumberOfLessons;

            result.GradeDecimal = Common.GradeDecimal(data.NumberOfLessons, data.GradeSum);
            result.GradeFractional = Common.DecimalToFractional(data.NumberOfLessons, data.GradeSum);

            return View(result);
        }


        public GradeConverterViewModel GetGradeData()
        {
            var data = new GradeConverterViewModel();
            data.NumberOfLessons = 0;
            data.GradeSum = 0;
            data.GradeDecimal = 0.00;
            data.GradeFractional = "00 0/00";

            return (data);
        }

        #endregion


        #region ΠΕΡΙΦΕΡΕΙΕΣ-ΔΗΜΟΙ

        public ActionResult PeriferiesDimoi()
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

        public ActionResult Periferies([DataSourceRequest] DataSourceRequest request)
        {
            var periferies = db.SYS_PERIFERIES.Select(p => new PeriferiaViewModel()
            {
                PERIFERIA_ID = p.PERIFERIA_ID,
                PERIFERIA_NAME = p.PERIFERIA_NAME
            });
            return Json(periferies.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dimoi([DataSourceRequest] DataSourceRequest request, int periferiaId)
        {
            var dimoi = db.SYS_DIMOS.Select(p => new DimosViewModel()
            {
                DIMOS_ID = p.DIMOS_ID,
                DIMOS = p.DIMOS,
                DIMOS_PERIFERIA = p.DIMOS_PERIFERIA
            }).Where(o => o.DIMOS_PERIFERIA == periferiaId).OrderBy(s => s.DIMOS);
            return Json(dimoi.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PeriferiesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
                return View();
            }
        }

        #endregion


        #region ΧΑΡΤΕΣ GOOGLE

        public ActionResult xGoogleMaps()
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

        public ActionResult GoogleMapsTest()
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


        #region ΤΟΜΕΙΣ ΣΠΟΥΔΩΝ ISCED_2013

        public ActionResult TomeisISCEDPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        #endregion


        #region ΕΙΣΟΔΟΙ ΣΧΟΛΕΙΩΝ

        public ActionResult SchoolLogins()
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

        public ActionResult Logins_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetSchoolLoginsFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SchoolLoginsViewModel> GetSchoolLoginsFromDB()
        {
            var data = (from d in db.sqlSCHOOL_LOGINS orderby d.SCHOOL_NAME
                        orderby d.LOGIN_DATETIME descending, d.SCHOOL_NAME
                        select new SchoolLoginsViewModel 
                        {
                            LOGIN_ID = d.LOGIN_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            LOGIN_DATETIME = d.LOGIN_DATETIME
                        }).ToList();
            return data;
        }

        #endregion


        #region ΕΚΤΥΠΩΣΕΙΣ

        public ActionResult SchoolAccountsPrint()
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
                return View();
            }
        }

        public ActionResult EidikotitesPrint()
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
                return View();
            }
        }

        public ActionResult EidikotitesOldNewPrint()
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
                return View();
            }
        }

        #endregion

    }
}