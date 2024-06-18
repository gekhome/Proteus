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
    public class ErgodotesController : ControllerUnit
    {
        private readonly ProteusDBEntities db;

        private readonly IErgodotesService ergodotesService;
        private readonly IPraktikiService praktikiService;
        private readonly IPraktikiAitisiService praktikiAitisiService;
        private readonly IPraktikiApofasiService praktikiApofasiService;
        private readonly IPraktikiParousiaService praktikiParousiaService;
        private readonly IPraktikiPeratosiService praktikiPeratosiService;
        private readonly IPraktikiElegxosService praktikiElegxosService;
        private readonly IPraktikiRegistryService praktikiRegistryService;
        private readonly IPraktikiExploreService praktikiExploreService;

        public ErgodotesController(ProteusDBEntities entities, IErgodotesService ergodotesService,
            IPraktikiService praktikiService, IPraktikiAitisiService praktikiAitisiService,
            IPraktikiApofasiService praktikiApofasiService, IPraktikiParousiaService praktikiParousiaService,
            IPraktikiPeratosiService praktikiPeratosiService, IPraktikiElegxosService praktikiElegxosService,
            IPraktikiRegistryService praktikiRegistryService, IPraktikiExploreService praktikiExploreService) : base(entities)
        {
            db = entities;

            this.ergodotesService = ergodotesService;
            this.praktikiService = praktikiService;
            this.praktikiAitisiService = praktikiAitisiService;
            this.praktikiApofasiService = praktikiApofasiService;
            this.praktikiParousiaService = praktikiParousiaService;
            this.praktikiPeratosiService = praktikiPeratosiService;
            this.praktikiElegxosService = praktikiElegxosService;
            this.praktikiRegistryService = praktikiRegistryService;
            this.praktikiExploreService = praktikiExploreService;
        }


        #region ΕΡΓΟΔΟΤΕΣ

        #region ΕΡΓΟΔΟΤΕΣ ΚΑΙ ΠΡΑΚΤΙΚΗ ΑΣΚΗΣΗ

        public ActionResult ErgodotesData(string notify = null)
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

            if (!TmimataPraktikiExist() || !StudentsPraktikiExist())
            {
                string errMsg = "Δεν βρέθηκαν τμήματα Γ, Δ, Ε εξαμήνων ή εγγεγραμμένοι σπουδαστές σε αυτά.";
                return RedirectToAction("Index", "School", new { notify = errMsg });
            }

            PopulateTmimataPraktiki(schoolId);
            PopulateStudentsPraktiki(schoolId);
            return View();
        }

        public ActionResult Ergodotes_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            IEnumerable<ErgodotesViewModel> data = ergodotesService.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Ergodotes_Create([DataSourceRequest] DataSourceRequest request, ErgodotesViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            var newdata = new ErgodotesViewModel();

            if (string.IsNullOrEmpty(data.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ)) 
                ModelState.AddModelError("", "Πρέπει να καταχωρηθεί τιμή για το ΑΦΜ. Η καταχώρηση ακυρώθηκε.");

            if (!Common.CheckAFM(data.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ)) 
                ModelState.AddModelError("", "Το ΑΦΜ που δώθηκε δεν είναι έγκυρο. Η καταχώρηση ακυρώθηκε.");

            if (!Kerberos.ValidatePrimaryKeyErgodotis(data.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ, schoolId)) 
                ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε είναι ήδη καταχωρημένο για το σχολείο αυτό. Η καταχώρηση ακυρώθηκε.");

            if (ModelState.IsValid)
            {
                ergodotesService.Create(data, schoolId);
                newdata = ergodotesService.Refresh(data.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Ergodotes_Update([DataSourceRequest] DataSourceRequest request, ErgodotesViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            var newdata = new ErgodotesViewModel();

            if (ModelState.IsValid)
            {
                ergodotesService.Update(data, schoolId);
                newdata = ergodotesService.Refresh(data.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Ergodotes_Destroy([DataSourceRequest] DataSourceRequest request, ErgodotesViewModel data)
        {
            if (data != null)
            {
                if (!Kerberos.CanDeleteErgodotis(data.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ, data.ΙΕΚ))
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί ο εργοδότης διότι υπάρχουν σχετιζόμενες εγγραφές πρακτικής άσκησης.");

                if (ModelState.IsValid)
                {
                    ergodotesService.Destroy(data);
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        #region PRAKTIKI GRID

        public ActionResult PraktikiStudentRead(int tmima)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = db.qrySTUDENT_PRAKTIKI_SELECTOR.AsQueryable().Where(f => f.ΚΩΔ_ΤΜΗΜΑ == tmima).OrderBy(d => d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Praktiki_Read([DataSourceRequest] DataSourceRequest request, int ergodotisId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            IEnumerable<ErgodotesPraktikiViewModel> data = praktikiService.Read(ergodotisId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Praktiki_Create([DataSourceRequest] DataSourceRequest request, ErgodotesPraktikiViewModel data, int ergodotisId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            ErgodotesPraktikiViewModel newdata = new ErgodotesPraktikiViewModel();

            if (ergodotisId > 0)
            {
                var existingData = db.ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ
                                  .Where(s => s.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ == ergodotisId && s.ΜΑΘΗΤΗΣ_ΚΩΔ == data.ΜΑΘΗΤΗΣ_ΚΩΔ && s.ΗΜΝΙΑ_ΑΠΟ == data.ΗΜΝΙΑ_ΑΠΟ && s.ΗΜΝΙΑ_ΕΩΣ == data.ΗΜΝΙΑ_ΕΩΣ)
                                  .Count();
                if (existingData > 0) 
                    ModelState.AddModelError("", "Υπάρχει ήδη καταχώρηση με τα ίδια στοιχεία πρακτικής στον εργοδότη αυτόν. Η καταχώρηση ακυρώθηκε.");

                if (data != null && ModelState.IsValid)
                {
                    praktikiService.Create(data, ergodotisId, schoolId);
                    newdata = praktikiService.Refresh(data.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλεγεί εργοδότης. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Praktiki_Update([DataSourceRequest] DataSourceRequest request, ErgodotesPraktikiViewModel data, int ergodotisId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            ErgodotesPraktikiViewModel newdata = new ErgodotesPraktikiViewModel();

            if (ergodotisId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    praktikiService.Update(data, ergodotisId, schoolId);
                    newdata = praktikiService.Refresh(data.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλεγεί εργοδότης. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Praktiki_Destroy([DataSourceRequest] DataSourceRequest request, ErgodotesPraktikiViewModel data)
        {
            if (data != null)
            {
                praktikiService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion PRAKTIKI GRID


        #region ERGODOTIS DATA FROM

        public ActionResult ErgodotesEdit(int? ergodotisId)
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
            if (!(ergodotisId > 0))
            {
                string msg = "Ο κωδικός εργοδότη δεν είναι έγκυρος. Η εγγραφή πρέπει να έχει αποθηκευτεί πρώτα.";
                return RedirectToAction("ErrorData", "School", new { notify = msg });
            }

            int ergodotisID = (int)ergodotisId;
            ErgodotesViewModel ergodotis = ergodotesService.GetRecord(ergodotisID);
            if (ergodotis == null)
            {
                string msg = "Παρουσιάστηκε πρόβλημα εύρεσης του εργοδότη.";
                return RedirectToAction("ErrorData", "School", new { notify = msg });
            }

            return View(ergodotis);
        }

        [HttpPost]
        public ActionResult ErgodotesEdit(int ergodotisId, ErgodotesViewModel model)
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
                ergodotesService.UpdateRecord(model, ergodotisId, schoolId);

                ErgodotesViewModel newentity = ergodotesService.GetRecord(ergodotisId);
                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");

                return View(newentity);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(model);
        }

        #endregion


        #region PRAKTIKI DATA FORM

        public ActionResult ErgodotesPraktikiEdit(int praktikiId)
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

            if (!(praktikiId > 0))
            {
                string msg = "Ο κωδικός πρακτικής δεν είναι έγκυρος. Η εγγραφή πρέπει να έχει αποθηκευτεί πρώτα.";
                return RedirectToAction("ErgodotesData", "Ergodotes", new { notify = msg });
            }

            ErgodotesPraktikiViewModel praktiki = praktikiService.Refresh(praktikiId);
            if (praktiki == null)
            {
                return HttpNotFound();
            }

            PraktikiInfoViewModel selectedPraktiki = praktikiService.GetInfo(praktikiId);
            if (selectedPraktiki == null)
            {
                string notify = "Προέκυψε σφάλμα εύρεσης της πρακτικής άσκησης.";
                return RedirectToAction("ErgodotesData", "Ergodotes", new { notify });
            }
            else
            {
                ViewBag.PraktikiData = selectedPraktiki;
            }

            return View(praktiki);
        }

        [HttpPost]
        public ActionResult ErgodotesPraktikiEdit(int praktikiId, ErgodotesPraktikiViewModel data)
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

            PraktikiInfoViewModel selectedPraktiki = praktikiService.GetInfo(praktikiId);

            if (selectedPraktiki == null)
            {
                string notify = "Προέκυψε σφάλμα εύρεσης της πρακτικής άσκησης.";
                return RedirectToAction("ErgodotesData", "Ergodotes", new { notify });
            }
            else
            {
                ViewBag.PraktikiData = selectedPraktiki;
            }

            ErgodotesPraktikiViewModel praktiki = praktikiService.Refresh(praktikiId);
            if (praktiki == null)
            {
                string notify = "Η εγγραφή δεν βρέθηκε. Κάνετε επιστροφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErgodotesData", "Ergodotes", new { notify });
            }

            if (data != null)
            {
                ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ entity = db.ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ.Find(praktikiId);

                entity.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ = praktiki.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ;
                entity.ΜΑΘΗΤΗΣ_ΑΜΚ = praktiki.ΜΑΘΗΤΗΣ_ΑΜΚ;
                entity.ΜΑΘΗΤΗΣ_ΚΩΔ = praktiki.ΜΑΘΗΤΗΣ_ΚΩΔ;
                entity.ΙΕΚ = praktiki.ΙΕΚ;
                entity.ΤΜΗΜΑ_ΚΩΔ = praktiki.ΤΜΗΜΑ_ΚΩΔ;
                entity.ΗΜΝΙΑ_ΑΠΟ = praktiki.ΗΜΝΙΑ_ΑΠΟ;
                entity.ΗΜΝΙΑ_ΕΩΣ = praktiki.ΗΜΝΙΑ_ΕΩΣ;
                entity.ΩΡΕΣ = praktiki.ΩΡΕΣ;
                entity.ΑΝΤΙΚΕΙΜΕΝΟ = data.ΑΝΤΙΚΕΙΜΕΝΟ;
                entity.ΠΕΡΙΓΡΑΦΗ = data.ΠΕΡΙΓΡΑΦΗ;
                entity.ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ = data.ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ErgodotesPraktikiViewModel newdata = praktikiService.Refresh(praktikiId);

                return View(newdata);
            }
            else
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
                return View(data);
            }
        }

        #endregion PRAKTIKI DATA FORM

        #endregion


        #region ΜΗΤΡΩΟ ΕΡΓΟΔΟΤΩΝ (ΚΑΙ ΠΡΑΚΤΙΚΗΣ)

        public ActionResult ErgodotesInfoList()
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

            IEnumerable<ErgodotesViewModel> data = ergodotesService.Read(schoolId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένοι εργοδότες για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            ErgodotesViewModel ergodotis = data.First();

            return View(ergodotis);
        }

        public ActionResult ErgodotesInfo_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            IEnumerable<ErgodotesViewModel> data = ergodotesService.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #region PARTIAL VIEW

        public ActionResult PraktikiInfo_Read([DataSourceRequest] DataSourceRequest request, int ergodotisId)
        {
            IEnumerable<PraktikiInfoViewModel> data = praktikiService.ReadInfo(ergodotisId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetErgodotisRecord(int ergodotisId)
        {
            ErgodotesViewModel data = ergodotesService.GetRecord(ergodotisId);

            return PartialView("ErgodotesInfoPartial", data);
        }

        #endregion PARTIAL VIEW

        #endregion


        #endregion ΕΡΓΟΔΟΤΕΣ


        #region ΠΡΑΚΤΙΚΗ - ΑΙΤΗΣΕΙΣ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult PraktikiAitiseis()
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

            PopulateStudentsPraktiki(schoolId);
            PopulateErgodotes(schoolId);

            return View();
        }

        public ActionResult StudentsPraktiki_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            IEnumerable<StudentInPraktikiViewModel> data = praktikiService.ReadStudents(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        #region ΑΙΤΗΣΕΙΣ ΠΡΑΚΤΙΚΗΣ

        public ActionResult PraktikiAitisi_Read([DataSourceRequest] DataSourceRequest request, int ergodotisId = 0, int studentId = 0, int tmimaId = 0)
        {
            IEnumerable<PraktikiAitisiViewModel> data = praktikiAitisiService.Read(ergodotisId, studentId, tmimaId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PraktikiAitisi_Create([DataSourceRequest] DataSourceRequest request, PraktikiAitisiViewModel data, int ergodotisId = 0, int studentId = 0, int tmimaId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            PraktikiAitisiViewModel newdata = new PraktikiAitisiViewModel();

            // only allow if user made selection in parent grid
            if (ergodotisId > 0 && studentId > 0 && tmimaId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    praktikiAitisiService.Create(data, ergodotisId, studentId, tmimaId, schoolId);
                    newdata = praktikiAitisiService.Refresh(data.ΑΙΤΗΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να γίνει επιλογή ενός σπουδαστή σε πρακτική. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PraktikiAitisi_Update([DataSourceRequest] DataSourceRequest request, PraktikiAitisiViewModel data, int ergodotisId = 0, int studentId = 0, int tmimaId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            PraktikiAitisiViewModel newdata = new PraktikiAitisiViewModel();

            // only allow if user made selection in parent grid
            if (ergodotisId > 0 && studentId > 0 && tmimaId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    praktikiAitisiService.Update(data, ergodotisId, studentId, tmimaId, schoolId);
                    newdata = praktikiAitisiService.Refresh(data.ΑΙΤΗΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να γίνει επιλογή ενός σπουδαστή σε πρακτική. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PraktikiAitisi_Destroy([DataSourceRequest] DataSourceRequest request, PraktikiAitisiViewModel data)
        {
            if (data != null)
            {
                if (!Kerberos.CanDeletePraktikiAitisi(data.ΑΙΤΗΣΗ_ΚΩΔ)) 
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή διότι υπάρχει απόφαση για την αίτηση αυτή");
                
                if (ModelState.IsValid)
                {
                    praktikiAitisiService.Destroy(data);
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion ΠΡΑΚΤΙΚΗ - ΑΙΤΗΣΕΙΣ ΣΠΟΥΔΑΣΤΩΝ


        #region ΠΡΑΚΤΙΚΗ - ΑΠΟΦΑΣΕΙΣ ΠΡΑΚΤΙΚΗΣ

        public ActionResult PraktikiApofaseis()
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

            PopulateStudentsPraktiki(schoolId);
            PopulateErgodotes(schoolId);

            return View();
        }

        public ActionResult AitiseisPraktiki_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            IEnumerable<AitiseisPraktikisViewModel> data = praktikiAitisiService.ReadInfo(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #region ΑΠΟΦΑΣΕΙΣ GRID

        public ActionResult PraktikiApofasi_Read([DataSourceRequest] DataSourceRequest request, int aitisiId = 0)
        {
            IEnumerable<PraktikiApofasiViewModel> data = praktikiApofasiService.Read(aitisiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PraktikiApofasi_Create([DataSourceRequest] DataSourceRequest request, PraktikiApofasiViewModel data, int aitisiId = 0)
        {
            PraktikiApofasiViewModel newdata = new PraktikiApofasiViewModel();

            // only allow if user made selection in parent grid
            if (aitisiId > 0)
            {
                AitiseisPraktikisViewModel source = praktikiAitisiService.GetInfo(aitisiId);

                if (data != null && ModelState.IsValid)
                {
                    praktikiApofasiService.Create(data, source);
                    newdata = praktikiApofasiService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να γίνει επιλογή μιας αίτησης. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PraktikiApofasi_Update([DataSourceRequest] DataSourceRequest request, PraktikiApofasiViewModel data, int aitisiId = 0)
        {
            PraktikiApofasiViewModel newdata = new PraktikiApofasiViewModel();

            // only allow if user made selection in parent grid
            if (aitisiId > 0)
            {
                AitiseisPraktikisViewModel source = praktikiAitisiService.GetInfo(aitisiId);

                if (data != null && ModelState.IsValid)
                {
                    praktikiApofasiService.Update(data, source);
                    newdata = praktikiApofasiService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να γίνει επιλογή μιας αίτησης. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PraktikiApofasi_Destroy([DataSourceRequest] DataSourceRequest request, PraktikiApofasiViewModel data)
        {
            if (data != null)
            {
                praktikiApofasiService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion ΑΠΟΦΑΣΕΙΣ GRID


        #endregion


        #region ΠΡΑΚΤΙΚΗ - ΒΕΒΑΙΩΣΕΙΣ ΠΑΡΟΥΣΙΑΣ

        public ActionResult PraktikiParousies()
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

            PopulateStudentsPraktiki(schoolId);
            PopulateErgodotes(schoolId);

            return View();
        }

        #region ΒΕΒΑΙΩΣΕΙΣ ΠΑΡΟΥΣΙΑΣ GRID

        public ActionResult BebeosiParousia_Read([DataSourceRequest] DataSourceRequest request, int ergodotisId = 0, int studentId = 0, int tmimaId = 0)
        {
            IEnumerable<PraktikiParousiaViewModel> data = praktikiParousiaService.Read(ergodotisId, studentId, tmimaId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BebeosiParousia_Create([DataSourceRequest] DataSourceRequest request, PraktikiParousiaViewModel data, int ergodotisId = 0, int studentId = 0, int tmimaId = 0)
        {
            PraktikiParousiaViewModel newdata = new PraktikiParousiaViewModel();

            // only allow if user made selection in parent grid
            if (ergodotisId > 0 && studentId > 0)
            {
                StudentInPraktikiViewModel source = praktikiService.GetStudent(ergodotisId, studentId, tmimaId);

                if (data != null && ModelState.IsValid)
                {
                    praktikiParousiaService.Create(data, source);
                    newdata = praktikiParousiaService.Refresh(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να γίνει επιλογή ενός σπουδαστή σε πρακτική. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BebeosiParousia_Update([DataSourceRequest] DataSourceRequest request, PraktikiParousiaViewModel data, int ergodotisId = 0, int studentId = 0, int tmimaId = 0)
        {
            PraktikiParousiaViewModel newdata = new PraktikiParousiaViewModel();

            // only allow if user made selection in parent grid
            if (ergodotisId > 0 && studentId > 0)
            {
                StudentInPraktikiViewModel source = praktikiService.GetStudent(ergodotisId, studentId, tmimaId);

                if (data != null && ModelState.IsValid)
                {
                    praktikiParousiaService.Update(data, source);
                    newdata = praktikiParousiaService.Refresh(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να γίνει επιλογή ενός σπουδαστή σε πρακτική. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BebeosiParousia_Destroy([DataSourceRequest] DataSourceRequest request, PraktikiParousiaViewModel data)
        {
            if (data != null)
            {
                praktikiParousiaService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion ΒΕΒΑΙΩΣΕΙΣ ΠΑΡΟΥΣΙΑΣ GRID

        #endregion


        #region ΠΡΑΚΤΙΚΗ - ΒΕΒΑΙΩΣΕΙΣ ΠΕΡΑΤΩΣΗΣ

        public ActionResult PraktikiPeratoseis()
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

            PopulateStudentsPraktiki(schoolId);
            PopulateErgodotes(schoolId);

            return View();
        }

        #region ΒΕΒΑΙΩΣΕΙΣ ΠΕΡΑΤΩΣΗΣ GRID

        public ActionResult BebeosiPeratosi_Read([DataSourceRequest] DataSourceRequest request, int ergodotisId = 0, int studentId = 0, int tmimaId = 0)
        {
            IEnumerable<PraktikiPeratosiViewModel> data = praktikiPeratosiService.Read(ergodotisId, studentId, tmimaId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BebeosiPeratosi_Create([DataSourceRequest] DataSourceRequest request, PraktikiPeratosiViewModel data, int ergodotisId = 0, int studentId = 0, int tmimaId = 0)
        {
            PraktikiPeratosiViewModel newdata = new PraktikiPeratosiViewModel();

            // only allow if user made selection in parent grid
            if (ergodotisId > 0 && studentId > 0)
            {
                StudentInPraktikiViewModel source = praktikiService.GetStudent(ergodotisId, studentId, tmimaId);

                if (data != null && ModelState.IsValid)
                {
                    praktikiPeratosiService.Create(data, source);
                    newdata = praktikiPeratosiService.Refresh(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να γίνει επιλογή ενός σπουδαστή σε πρακτική. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BebeosiPeratosi_Update([DataSourceRequest] DataSourceRequest request, PraktikiPeratosiViewModel data, int ergodotisId = 0, int studentId = 0, int tmimaId = 0)
        {
            PraktikiPeratosiViewModel newdata = new PraktikiPeratosiViewModel();

            // only allow if user made selection in parent grid
            if (ergodotisId > 0 && studentId > 0)
            {
                StudentInPraktikiViewModel source = praktikiService.GetStudent(ergodotisId, studentId, tmimaId);

                if (data != null && ModelState.IsValid)
                {
                    praktikiPeratosiService.Update(data, source);
                    newdata = praktikiPeratosiService.Refresh(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να γίνει επιλογή ενός σπουδαστή σε πρακτική. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BebeosiPeratosi_Destroy([DataSourceRequest] DataSourceRequest request, PraktikiPeratosiViewModel data)
        {
            if (data != null)
            {
                praktikiPeratosiService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion ΒΕΒΑΙΩΣΕΙΣ ΠΕΡΑΤΩΣΗΣ GRID

        #endregion


        #region ΠΡΑΚΤΙΚΗ - ΕΛΕΓΧΟΙ ΠΡΑΚΤΙΚΗΣ

        public ActionResult PraktikiElegxoi()
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

            PopulateStudentsPraktiki(schoolId);
            PopulateErgodotes(schoolId);
            PopulateTerms();

            return View();
        }

        #region ΕΛΕΓΧΟΙ GRID

        public ActionResult Elegxos_Read([DataSourceRequest] DataSourceRequest request, int ergodotisId = 0, int studentId = 0, int tmimaId = 0)
        {
            IEnumerable<PraktikiElegxosViewModel> data = praktikiElegxosService.Read(ergodotisId, studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Elegxos_Create([DataSourceRequest] DataSourceRequest request, PraktikiElegxosViewModel data, int ergodotisId = 0, int studentId = 0, int tmimaId = 0)
        {
            PraktikiElegxosViewModel newdata = new PraktikiElegxosViewModel();

            // only allow if user made selection in parent grid
            if (ergodotisId > 0 && studentId > 0 && tmimaId > 0)
            {
                StudentInPraktikiViewModel source = praktikiService.GetStudent(ergodotisId, studentId, tmimaId);

                if (data != null && ModelState.IsValid)
                {
                    praktikiElegxosService.Create(data, source);
                    newdata = praktikiElegxosService.Refresh(data.ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να γίνει επιλογή ενός σπουδαστή σε πρακτική. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Elegxos_Update([DataSourceRequest] DataSourceRequest request, PraktikiElegxosViewModel data, int ergodotisId = 0, int studentId = 0, int tmimaId = 0)
        {
            PraktikiElegxosViewModel newdata = new PraktikiElegxosViewModel();

            // only allow if user made selection in parent grid
            if (ergodotisId > 0 && studentId > 0 && tmimaId > 0)
            {
                StudentInPraktikiViewModel source = praktikiService.GetStudent(ergodotisId, studentId, tmimaId);

                if (data != null && ModelState.IsValid)
                {
                    praktikiElegxosService.Update(data, source);
                    newdata = praktikiElegxosService.Refresh(data.ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να γίνει επιλογή ενός σπουδαστή σε πρακτική. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Elegxos_Destroy([DataSourceRequest] DataSourceRequest request, PraktikiElegxosViewModel data)
        {
            if (data != null)
            {
                praktikiElegxosService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion ΕΛΕΓΧΟΙ GRID

        #endregion


        #region ΜΗΤΡΩΑ ΠΡΑΚΤΙΚΗΣ

        #region ΜΗΤΡΩΟ ΣΠΟΥΔΑΣΤΩΝ ΣΕ ΠΡΑΚΤΙΚΗ

        public ActionResult RegPraktikiStudents()
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

            List<regPraktikiStudentViewModel> data = praktikiRegistryService.ReadStudents(schoolId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένοι σπουδαστές πρακτικής για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            regPraktikiStudentViewModel model = data.First();

            return View(model);
        }

        public ActionResult RegPraktikiStudents_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            List<regPraktikiStudentViewModel> data = praktikiRegistryService.ReadStudents(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPraktikiStudentRecord(int praktikiId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            regPraktikiStudentViewModel data = praktikiRegistryService.ReadStudents(schoolId).Where(e => e.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ == praktikiId).FirstOrDefault();

            return PartialView("RegPraktikiStudentPartial", data);
        }

        #endregion


        #region ΑΙΤΗΣΕΙΣ ΠΡΑΚΤΙΚΗΣ

        public ActionResult RegPraktikiAitiseis()
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

            IEnumerable<regPraktikiAitisiViewModel> data = praktikiRegistryService.ReadAitiseis(schoolId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αιτήσεις πρακτικής για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            regPraktikiAitisiViewModel model = data.First();

            return View(model);
        }

        public ActionResult RegPraktikiAitiseis_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            List<regPraktikiAitisiViewModel> data = praktikiRegistryService.ReadAitiseis(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPraktikiAitisiRecord(int aitisiId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            regPraktikiAitisiViewModel data = praktikiRegistryService.ReadAitiseis(schoolId).Where(e => e.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId).FirstOrDefault();

            return PartialView("RegPraktikiAitisiPartial", data);
        }

        #endregion ΑΙΤΗΣΕΙΣ ΠΡΑΚΤΙΚΗΣ


        #region ΑΠΟΦΑΣΕΙΣ ΠΡΑΚΤΙΚΗΣ

        public ActionResult RegPraktikiApofaseis()
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

            List<regPraktikiApofasiViewModel> data = praktikiRegistryService.ReadApofaseis(schoolId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αποφάσεις πρακτικής για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            regPraktikiApofasiViewModel model = data.First();

            return View(model);
        }

        public ActionResult RegPraktikiApofaseis_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            List<regPraktikiApofasiViewModel> data = praktikiRegistryService.ReadApofaseis(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPraktikiApofasiRecord(int apofasiId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            regPraktikiApofasiViewModel data = praktikiRegistryService.ReadApofaseis(schoolId).Where(e => e.ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ == apofasiId).FirstOrDefault();

            return PartialView("RegPraktikiApofasiPartial", data);
        }


        #endregion ΑΠΟΦΑΣΕΙΣ ΠΡΑΚΤΙΚΗΣ


        #region ΠΑΡΟΥΣΙΕΣ ΠΡΑΚΤΙΚΗΣ

        public ActionResult RegPraktikiParousies()
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

            List<regPraktikiParousiaViewModel> data = praktikiRegistryService.ReadParousies(schoolId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες παρουσίες πρακτικής για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            regPraktikiParousiaViewModel model = data.First();

            return View(model);
        }

        public ActionResult RegPraktikiParousies_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            List<regPraktikiParousiaViewModel> data = praktikiRegistryService.ReadParousies(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPraktikiParousiaRecord(int bebeosiId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            regPraktikiParousiaViewModel data = praktikiRegistryService.ReadParousies(schoolId).Where(e => e.ΒΕΒΑΙΩΣΗ_ΚΩΔ == bebeosiId).FirstOrDefault();

            return PartialView("RegPraktikiParousiaPartial", data);
        }

        #endregion ΠΑΡΟΥΣΙΕΣ ΠΡΑΚΤΙΚΗΣ


        #region ΠΕΡΑΤΩΣΕΙΣ ΠΡΑΚΤΙΚΗΣ

        public ActionResult RegPraktikiPeratoseis()
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

            List<regPraktikiPeratosiViewModel> data = praktikiRegistryService.ReadPeratoseis(schoolId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες παρουσίες πρακτικής για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            regPraktikiPeratosiViewModel model = data.First();

            return View(model);
        }

        public ActionResult RegPraktikiPeratoseis_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            List<regPraktikiPeratosiViewModel> data = praktikiRegistryService.ReadPeratoseis(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPraktikiPeratosiRecord(int bebeosiId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            regPraktikiPeratosiViewModel data = praktikiRegistryService.ReadPeratoseis(schoolId).Where(e => e.ΒΕΒΑΙΩΣΗ_ΚΩΔ == bebeosiId).FirstOrDefault();

            return PartialView("RegPraktikiPeratosiPartial", data);
        }


        #endregion ΠΕΡΑΤΩΣΕΙΣ ΠΡΑΚΤΙΚΗΣ


        #region ΕΛΕΓΧΟΙ ΠΡΑΚΤΙΚΗΣ

        public ActionResult RegPraktikiElegxoi()
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

            List<regPraktikiElegxosViewModel> data = praktikiRegistryService.ReadElegxoi(schoolId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένοι έλεγχοι πρακτικής για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            regPraktikiElegxosViewModel model = data.First();

            return View(model);
        }

        public ActionResult RegPraktikiElegxoi_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            List<regPraktikiElegxosViewModel> data = praktikiRegistryService.ReadElegxoi(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPraktikiElegxosRecord(int elegxosId)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            regPraktikiElegxosViewModel data = praktikiRegistryService.ReadElegxoi(schoolId).Where(e => e.ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ == elegxosId).FirstOrDefault();

            return PartialView("RegPraktikiElegxosPartial", data);
        }


        #endregion ΕΛΕΓΧΟΙ ΠΡΑΚΤΙΚΗΣ

        #endregion


        #region ΕΞΕΡΕΥΝΗΣΗ ΠΡΑΚΤΙΚΗΣ (2018-06-02)

        public ActionResult StudentPraktikiExplore()
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

            List<PraktikiExploreViewModel> data = praktikiExploreService.ReadStudents(schoolId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένοι σπουδαστές πρακτικής για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            PopulatePeriodoi();
            PopulateTerms();
            PopulateIekEidikotites(schoolId);
            return View();
        }

        public ActionResult PraktikiExplore_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            List<PraktikiExploreViewModel> data = praktikiExploreService.ReadStudents(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PraktikiApallagi_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            List<PraktikiApallagiViewModel> data = praktikiExploreService.ReadApallages(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PraktikiDiakopi_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            List<PraktikiDiakopiViewModel> data = praktikiExploreService.ReadDiakopes(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion ΕΞΕΡΕΥΝΗΣΗ ΠΡΑΚΤΙΚΗΣ (2018-06-02)


        #region ΕΚΤΥΠΩΣΕΙΣ ΠΡΑΚΤΙΚΗΣ

        public ActionResult PraktikiBebeosiPrint(int praktikiId)
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

        public ActionResult PraktikiAitisiPrint(int aitisiId)
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

        public ActionResult PraktikiApofasiPrint(int apofasiId)
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

        public ActionResult PraktikiParousiaPrint(int bebeosiId)
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

        public ActionResult PraktikiPeratosiPrint(int bebeosiId)
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

        public ActionResult PraktikiElegxosPrint(int elegxosId)
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

        #endregion

    }
}