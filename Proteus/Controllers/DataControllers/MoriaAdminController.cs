using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using Proteus.Notification;
using Proteus.ServicesMoria;
using Proteus.Filters;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Proteus.Controllers.DataControllers
{
    [ErrorHandlerFilter]
    public class MoriaAdminController : ControllerMoria
    {
        private readonly ProteusDBEntities db;

        private readonly IAitisiService aitisiService;
        private readonly IEgyklioiService egyklioiService;
        private readonly IEidikotitesService eidikotitesService;
        private readonly IEidikotitaEgykliosService eidikotitaEgykliosService;
        private readonly IExperienceService experienceService;
        private readonly IUploadService uploadService;

        public MoriaAdminController(ProteusDBEntities entities, IAitisiService aitisiService, IEgyklioiService egyklioiService,
            IEidikotitesService eidikotitesService, IEidikotitaEgykliosService eidikotitaEgykliosService,
            IExperienceService experienceService, IUploadService uploadService) : base(entities)
        {
            db = entities;

            this.aitisiService = aitisiService;
            this.egyklioiService = egyklioiService;
            this.eidikotitesService = eidikotitesService;
            this.eidikotitaEgykliosService = eidikotitaEgykliosService;
            this.experienceService = experienceService;
            this.uploadService = uploadService;
        }


        #region ΕΓΚΥΚΛΙΟΙ ΥΠΟΨΗΦΙΩΝ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult xEgykliosList()
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
            PopulateSchoolYears();
            PopulateStatus();

            return View();
        }

        public ActionResult Egyklios_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<XmEgykliosViewModel> data = egyklioiService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Egyklios_Create([DataSourceRequest] DataSourceRequest request, XmEgykliosViewModel data)
        {
            var newdata = new XmEgykliosViewModel();

            if (!ValidEgykliosStatus(data.ΚΑΤΑΣΤΑΣΗ))
                ModelState.AddModelError("", "Μόνο μία προκήρυξη μπορεί να είναι ανοικτή κάθε φορά. Η αποθήκευση ακυρώθηκε.");

            if (ModelState.IsValid)
            {
                egyklioiService.Create(data);
                newdata = egyklioiService.Refresh(data.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Egyklios_Update([DataSourceRequest] DataSourceRequest request, XmEgykliosViewModel data)
        {
            var newdata = new XmEgykliosViewModel();

            if (!ValidEgykliosStatus(data.ΚΑΤΑΣΤΑΣΗ))
                ModelState.AddModelError("", "Μόνο μία προκήρυξη μπορεί να είναι ανοικτή κάθε φορά. Η αποθήκευση ακυρώθηκε.");

            if (ModelState.IsValid)
            {
                egyklioiService.Update(data);
                newdata = egyklioiService.Refresh(data.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Egyklios_Destroy([DataSourceRequest] DataSourceRequest request, XmEgykliosViewModel data)
        {
            if (data != null)
            {
                if (CanDeleteEgyklios(data.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ))
                {
                    egyklioiService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί η προκήρυξη διότι υπάρχουν υποψήφιοι σε αυτή.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public bool CanDeleteEgyklios(int egykliosID)
        {
            var data = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ where d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosID select d).Count();
            if (data > 0) return false;
            else return true;
        }

        public bool ValidEgykliosStatus(int status)
        {
            if (status == 1)
            {
                int data = (from d in db.ΧΜ_ΕΓΚΥΚΛΙΟΣ where d.ΚΑΤΑΣΤΑΣΗ == status select d).Count();
                if (data > 0) return false;
                else return true;
            }
            return true;
        }

        #endregion


        #region ΕΙΔΙΚΟΤΗΤΕΣ ΚΑΤΑΡΤΙΣΗΣ ΓΙΑ ΕΓΚΥΚΛΙΟΥΣ

        public ActionResult xEidikotitesList()
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

        public ActionResult xmEidikotita_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<XmEidikotitesViewModel> data = eidikotitesService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult xmEidikotita_Create([DataSourceRequest] DataSourceRequest request, XmEidikotitesViewModel data)
        {
            var newdata = new XmEidikotitesViewModel();

            var existingData = db.ΧΜ_ΕΙΔΙΚΟΤΗΤΕΣ.Where(s => s.EIDIKOTITA_TEXT == data.EIDIKOTITA_TEXT).Count();
            if (existingData > 0) 
                ModelState.AddModelError("", "Η ειδικότητα αυτή υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                eidikotitesService.Create(data);
                newdata = eidikotitesService.Refresh(data.EIDIKOTITA_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult xmEidikotita_Update([DataSourceRequest] DataSourceRequest request, XmEidikotitesViewModel data)
        {
            var newdata = new XmEidikotitesViewModel();

            if (data != null && ModelState.IsValid)
            {
                eidikotitesService.Update(data);
                newdata = eidikotitesService.Refresh(data.EIDIKOTITA_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult xmEidikotita_Destroy([DataSourceRequest] DataSourceRequest request, XmEidikotitesViewModel data)
        {
            if (data != null)
            {
                if (CanDeleteEidikotita(data.EIDIKOTITA_ID))
                {
                    eidikotitesService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί η ειδικότητα αυτή διότι είναι σε χρήση.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public bool CanDeleteEidikotita(int eidikotitaId)
        {
            int count = (from d in db.ΧΜ_ΕΓΚΥΚΛΙΟΣ_ΕΙΔΙΚΟΤΗΤΕΣ where d.EIDIKOTITA_ID == eidikotitaId select d).Count();
            if (count > 0) return false;
            else return true;
        }

        public ActionResult xEidikotitesIekPrint()
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


        #region ΕΓΚΕΚΡΙΜΕΝΕΣ ΕΙΔΙΚΟΤΗΤΕΣ ΑΝΑ ΕΓΚΥΚΛΙΟ ΚΑΙ ΙΕΚ

        public ActionResult xEidikotitesInEgyklios(string notify = null)
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

            XmEgykliosViewModel egyklios = Common.GetAdminEgyklios();

            int schoolYearId = (int)egyklios.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            ViewData["egykliosProtocol"] = egyklios.ΕΓΚΥΚΛΙΟΣ_ΑΠ;
            ViewData["schoolYearText"] = Common.GetSchoolYearText(schoolYearId);

            PopulateXmEidikotites();
            PopulateTerms();
            return View();
        }

        public ActionResult EidikotitesEgykliosRead([DataSourceRequest] DataSourceRequest request, int egykliosId, int schoolId)
        {
            IEnumerable<XmEgykliosEidikotitesViewModel> data = eidikotitaEgykliosService.Read(egykliosId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitesEgykliosCreate([DataSourceRequest] DataSourceRequest request,
                            [Bind(Prefix = "models")] IEnumerable<XmEgykliosEidikotitesViewModel> data, int egykliosId, int schoolId)
        {
            if (egykliosId > 0 && schoolId > 0)
            {
                foreach (var item in data)
                {
                    XmEgykliosEidikotitesViewModel newData = new XmEgykliosEidikotitesViewModel()
                    {
                        EGYKLIOS_ID = egykliosId,
                        SCHOOL_ID = schoolId,
                        EIDIKOTITA_ID = item.EIDIKOTITA_ID,
                        TERM_ID = item.TERM_ID
                    };
                    if (EidikotitaEgykliosExists(newData))
                    {
                        ModelState.AddModelError("", "Έγινε απόπειρα δημιουργίας διπλοεγγραφής. Η διπλή καταχώρηση ακυρώθηκε.");
                        return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                    }
                    if (ModelState.IsValid)
                    {
                        eidikotitaEgykliosService.Create(item, egykliosId, schoolId);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Δεν έχει γίνει επιλογή εγκυκλίου και σχολείου. Η αποθήκευση ακυρώθηκε.");
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitesEgykliosUpdate([DataSourceRequest] DataSourceRequest request,
                            [Bind(Prefix = "models")] IEnumerable<XmEgykliosEidikotitesViewModel> data, int egykliosId, int schoolId)
        {
            if (egykliosId > 0 && schoolId > 0)
            {
                foreach (var item in data)
                {
                    if (item != null && ModelState.IsValid)
                    {
                        eidikotitaEgykliosService.Update(item, egykliosId, schoolId);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Δεν έχει γίνει επιλογή προκήρυξης και σχολείου. Η αποθήκευση ακυρώθηκε.");
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitesEgykliosDestroy([DataSourceRequest] DataSourceRequest request,
                            [Bind(Prefix = "models")] IEnumerable<XmEgykliosEidikotitesViewModel> data)
        {
            if (data.Any())
            {
                foreach (var item in data)
                {
                    if (CanDeleteEidikotitaInEgyklios(item))
                    {
                        eidikotitaEgykliosService.Destroy(item);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί ειδικότητα προηγούμενης προκήρυξης.");
                    }
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public bool CanDeleteEidikotitaInEgyklios(XmEgykliosEidikotitesViewModel data)
        {
            int currentEgyklios = Common.GetAdminEgykliosID();
            if (data.EGYKLIOS_ID != currentEgyklios) return false;

            return true;
        }

        public bool EidikotitaEgykliosExists(XmEgykliosEidikotitesViewModel item)
        {
            int data = (from d in db.ΧΜ_ΕΓΚΥΚΛΙΟΣ_ΕΙΔΙΚΟΤΗΤΕΣ
                        where d.EGYKLIOS_ID == item.EGYKLIOS_ID && d.SCHOOL_ID == item.SCHOOL_ID && d.EIDIKOTITA_ID == item.EIDIKOTITA_ID && d.TERM_ID == item.TERM_ID
                        select d).Count();
            if (data > 0)
                return true;
            else
                return false;
        }

        public ActionResult xEidikotitesEgykliosPrint()
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


        #region ΑΙΤΗΣΕΙΣ ΥΠΟΨΗΦΙΩΝ ΣΠΟΥΔΑΣΤΩΝ

        #region ΠΛΕΓΜΑ ΑΙΤΗΣΕΩΝ

        public ActionResult xAitiseisList()
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

            if (Common.GetAdminEgykliosID() == 0)
            {
                string msg = "Δεν βρέθηκε διαχειριστική προκήρυξη για προβολή υποψηφίων σπουδαστών.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            PopulateSchools();

            return View();
        }

        public ActionResult XmAitiseis_Read([DataSourceRequest] DataSourceRequest request)
        {
            int egykliosId = Common.GetAdminEgykliosID();

            IEnumerable<XmAitisiGridViewModel> data = aitisiService.Read(egykliosId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult XmAitisi_Create([DataSourceRequest] DataSourceRequest request, XmAitisiGridViewModel data)
        {
            int egykliosId = Common.GetAdminEgykliosID();

            XmAitisiGridViewModel newdata = new XmAitisiGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                aitisiService.Create(data, egykliosId);
                newdata = aitisiService.Refresh(data.ΑΙΤΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult XmAitisi_Update([DataSourceRequest] DataSourceRequest request, XmAitisiGridViewModel data)
        {
            int egykliosId = Common.GetAdminEgykliosID();

            var newdata = new XmAitisiGridViewModel();

            if (data != null & ModelState.IsValid)
            {
                aitisiService.Update(data, egykliosId);
                newdata = aitisiService.Refresh(data.ΑΙΤΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult XmAitisi_Destroy([DataSourceRequest] DataSourceRequest request, XmAitisiGridViewModel data)
        {
            if (data != null)
            {
                if (CanDeleteXmAitisi(data.ΑΙΤΗΣΗ_ΚΩΔ))
                {
                    aitisiService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί η αίτηση διότι υπάρχουν εργασιακές εμπειρίες ή συνημμένα αρχεία σε αυτή.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public XmAitisiGridViewModel XmRefreshAitisiFromDB(int recordId)
        {
            var data = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ
                        where d.ΑΙΤΗΣΗ_ΚΩΔ == recordId
                        select new XmAitisiGridViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                            ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ = d.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΑΦΜ = d.ΑΦΜ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΙΕΚ1 = d.ΙΕΚ1,
                            ΙΕΚ2 = d.ΙΕΚ2,
                            ΜΟΡΙΑ = d.ΜΟΡΙΑ
                        }).FirstOrDefault();
            return data;
        }

        public List<XmAitisiGridViewModel> XmGetAitiseisFromDB()
        {
            int egykliosId = Common.GetAdminEgykliosID();

            var data = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ
                        where d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId
                        orderby d.ΕΠΩΝΥΜΟ, d.ΟΝΟΜΑ
                        select new XmAitisiGridViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                            ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ = d.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΑΦΜ = d.ΑΦΜ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΙΕΚ1 = d.ΙΕΚ1,
                            ΙΕΚ2 = d.ΙΕΚ2,
                            ΜΟΡΙΑ = d.ΜΟΡΙΑ
                        }).ToList();

            return data;
        }

        public bool CanDeleteXmAitisi(int aitisiID)
        {
            var data1 = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ_ΕΜΠΕΙΡΙΑ where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiID select d).Count();
            var data2 = (from d in db.ΧΜ_UPLOADS where d.AITISI_ID == aitisiID select d).Count();

            if (data1 > 0 || data2 > 0) return false;
            else return true;
        }

        #endregion ΠΛΕΓΜΑ ΑΙΤΗΣΕΩΝ


        #region ΠΛΕΓΜΑ ΠΡΟΫΠΗΡΕΣΙΩΝ ΥΠΟΨΗΦΙΟΥ

        public ActionResult Experience_Read([DataSourceRequest] DataSourceRequest request, int aitisiId = 0)
        {
            IEnumerable<XmExperienceViewModel> data = experienceService.Read(aitisiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Experience_Create([DataSourceRequest] DataSourceRequest request, XmExperienceViewModel data, int aitisiId = 0)
        {
            int egykliosId = Common.GetAdminEgykliosID();

            var newdata = new XmExperienceViewModel();

            if (!(aitisiId > 0)) ModelState.AddModelError("", "Πρέπει να επιλεγεί μία αίτηση πρώτα. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                experienceService.Create(data, aitisiId, egykliosId);
                newdata = experienceService.Refresh(data.ΕΜΠΕΙΡΙΑ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Experience_Update([DataSourceRequest] DataSourceRequest request, XmExperienceViewModel data, int aitisiId = 0)
        {
            int egykliosId = Common.GetAdminEgykliosID();

            var newdata = new XmExperienceViewModel();

            if (!(aitisiId > 0)) ModelState.AddModelError("", "Πρέπει να επιλεγεί μία αίτηση πρώτα. Η καταχώρηση ακυρώθηκε.");

            if (data != null & ModelState.IsValid)
            {
                experienceService.Update(data, aitisiId, egykliosId);
                newdata = experienceService.Refresh(data.ΕΜΠΕΙΡΙΑ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Experience_Destroy([DataSourceRequest] DataSourceRequest request, XmExperienceViewModel data)
        {
            if (data != null)
            {
                experienceService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΚΑΡΤΕΛΑ ΑΙΤΗΣΕΩΝ

        public ActionResult xAitisiEdit(int aitisiID)
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
            if (!(aitisiID > 0))
            {
                string msg = "Ο κωδικός αίτησης δεν είναι έγκυρος. Η εγγραφή πρέπει να έχει αποθηκευτεί πρώτα στο πλέγμα. Κλείστε την καρτέλα.";
                XmAitisiViewModel blankAitisi = new XmAitisiViewModel();
                this.ShowMessage(MessageType.Error, msg);
                return View(blankAitisi);
            }

            XmAitisiViewModel aitisi = aitisiService.GetRecord(aitisiID);
            return View(aitisi);
        }

        [HttpPost]
        public ActionResult xAitisiEdit(XmAitisiViewModel data, int aitisiID)
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

            int egykliosID = Common.GetAdminEgykliosID();

            string errorMessage = Kerberos.ValidAitisiData(data);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                this.ShowMessage(MessageType.Error, errorMessage);
                return View(data);
            }

            if (aitisiID > 0 && ModelState.IsValid)
            {
                aitisiService.UpdateRecord(data, aitisiID, egykliosID);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                XmAitisiViewModel newData = aitisiService.GetRecord(aitisiID);
                return View(newData);
            }
            else
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης ή άκυρου κωδικού αίτησης.");
                return View(data);
            }
        }

        public ActionResult xAitisiMoriaPrint(int aitisiID = 0)
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
                var data = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ
                            where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiID
                            select new XmAitisiViewModel
                            {
                                ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                                ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ = d.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ,
                                ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ
                            }).FirstOrDefault();

                return View(data);
            }
        }

        #endregion


        #region ΑΝΕΒΑΣΜΕΝΑ ΑΡΧΕΙΑ ΑΙΤΗΣΗΣ

        public ActionResult xAitisiUploadedFiles(int aitisiId = 0)
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

            var data = (from d in db.ΧΜ_sqlAITISEIS_HEADERS where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId select d).FirstOrDefault();

            ViewBag.AitisiHeader = data;
            ViewData["aitisi_id"] = data.ΑΙΤΗΣΗ_ΚΩΔ;

            return View();
        }

        public ActionResult AitisiUploads_Read([DataSourceRequest] DataSourceRequest request, int aitisiId = 0)
        {
            var data = GetUploadedFiles(aitisiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<sqlUploadedFilesViewModel> GetUploadedFiles(int aitisiId = 0)
        {
            var data = (from d in db.ΧΜ_sqlUPLOADED_FILES
                        where d.AITISI_ID == aitisiId
                        orderby d.FILENAME
                        select new sqlUploadedFilesViewModel
                        {
                            FILE_ID = d.FILE_ID,
                            ID = d.ID,
                            AITISI_ID = d.AITISI_ID,
                            EGYKLIOS_ID = d.EGYKLIOS_ID,
                            FILENAME = d.FILENAME,
                            EXTENSION = d.EXTENSION,
                            SCHOOL_ID = d.SCHOOL_ID,
                            STUDENT_AFM = d.STUDENT_AFM,
                            UPLOAD_NAME = d.UPLOAD_NAME,
                            UPLOAD_SUMMARY = d.UPLOAD_SUMMARY,
                            SCHOOL_USER = d.SCHOOL_USER,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT
                        }).ToList();

            return (data);
        }

        #endregion


        #region ΔΙΚΑΙΟΛΟΓΗΤΙΚΑ ΑΙΤΗΣΕΩΝ

        public ActionResult xDownloadData(string notify = null)
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
            int egykliosId = Common.GetAdminEgykliosID();
            if (egykliosId == 0)
            {
                string Msg = "Δεν βρέθηκε διαχειριστική Προκήρυξη.";
                return RedirectToAction("Index", "Admin", new { notify = Msg });
            }
            int schoolYearId = (int)Common.GetAdminEgyklios().ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            ViewData["egykliosProtocol"] = Common.GetAdminEgyklios().ΕΓΚΥΚΛΙΟΣ_ΑΠ;
            ViewData["schoolYearText"] = Common.GetSchoolYearText(schoolYearId);

            if (notify != null)
            {
                this.ShowMessage(MessageType.Warning, notify);
            }
            if (!AitisεisExist())
            {
                string msg = "Δεν βρέθηκαν αιτήσεις για την εγκύκλιο αυτή. Η προβολή μεταφορτωμένων αρχείων είναι αδύνατη.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }

            PopulateSchoolYears();
            PopulateAitiseis();

            return View();
        }

        #region MASTER GRID CRUD FUNCTIONS

        public ActionResult Upload_Read([DataSourceRequest] DataSourceRequest request, int schoolId)
        {
            int egykliosId = Common.GetAdminEgykliosID();

            IEnumerable<UploadsViewModel> data = uploadService.Read(egykliosId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CHILD GRID (UPLOADED FILEDETAILS)

        public ActionResult UploadFiles_Read([DataSourceRequest] DataSourceRequest request, int uploadId = 0)
        {
            IEnumerable<UploadsFilesViewModel> data = uploadService.GetFiles(uploadId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public FileResult Download(string file_id)
        {
            string p = "";
            string f = "";
            string the_path = UPLOAD_PATH;

            var fileinfo = (from d in db.ΧΜ_UPLOADS_FILES where d.ID == file_id select d).FirstOrDefault();
            if (fileinfo != null)
            {
                the_path += fileinfo.SCHOOL_USER + "/" + fileinfo.SCHOOLYEAR_TEXT + "/";
                p = fileinfo.ID.ToString() + fileinfo.EXTENSION;
                f = fileinfo.FILENAME;
            }
            return File(Path.Combine(Server.MapPath(the_path), p), System.Net.Mime.MediaTypeNames.Application.Octet, f);
        }

        #endregion

        #endregion

        #endregion


        #region ΜΟΡΙΟΔΟΤΗΣΗ ΚΑΙ ΕΚΚΑΘΑΡΙΣΗ ΑΙΤΗΣΕΩΝ

        public ActionResult BatchMoriodotisi()
        {
            string message = "";
            int egykliosId = Common.GetActiveEgykliosID();

            var aitiseis = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ where d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId orderby d.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ select d).ToList();
            if (aitiseis.Count == 0)
            {
                message = "Δεν βρέθηκαν αιτήσεις στην Προκήρυξη αυτή για μοριοδότηση.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var item in aitiseis)
            {
                ΧΜ_ΥΠΟΨΗΦΙΟΣ entity = db.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Find(item.ΑΙΤΗΣΗ_ΚΩΔ);

                entity.ΜΟΡΙΑ_ΑΠΟΦΟΙΤΗΣΗ = Kerberos.MoriaApofitisi(entity);
                entity.ΜΟΡΙΑ_ΒΑΘΜΟΣ = Kerberos.MoriaGrade(entity);
                entity.ΜΟΡΙΑ_ΕΜΠΕΙΡΙΑ = Kerberos.MoriaWork(entity);
                entity.ΜΟΡΙΑ_ΜΟΝΟΓΟΝΕΙΚΟΣ = Kerberos.MoriaMonogoneikos(entity);
                entity.ΜΟΡΙΑ_ΠΟΛΥΤΕΚΝΟΣ = Kerberos.MoriaPolyteknos(entity);
                entity.ΜΟΡΙΑ_ΤΡΙΤΕΚΝΟΣ = Kerberos.MoriaTriteknos(entity);
                entity.ΜΟΡΙΑ = Kerberos.MoriaTotal(entity);

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
            message = "Η μοριοδότηση των αιτήσεων ολοκληρώθηκε.";
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CleanDuplicateEidikotites()
        {
            string message = "";
            int egykliosId = Common.GetActiveEgykliosID();

            var aitiseis = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ where d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId orderby d.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ select d).ToList();
            if (aitiseis.Count == 0)
            {
                message = "Δεν βρέθηκαν αιτήσεις στην Προκήρυξη αυτή για μοριοδότηση.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var item in aitiseis)
            {
                ΧΜ_ΥΠΟΨΗΦΙΟΣ entity = db.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Find(item.ΑΙΤΗΣΗ_ΚΩΔ);

                if (entity.ΕΙΔΙΚΟΤΗΤΑ2 == entity.ΕΙΔΙΚΟΤΗΤΑ1)
                    entity.ΕΙΔΙΚΟΤΗΤΑ2 = null;
                if (entity.ΕΙΔΙΚΟΤΗΤΑ3 == entity.ΕΙΔΙΚΟΤΗΤΑ1 || entity.ΕΙΔΙΚΟΤΗΤΑ3 == entity.ΕΙΔΙΚΟΤΗΤΑ2)
                    entity.ΕΙΔΙΚΟΤΗΤΑ3 = null;

                if (entity.ΕΙΔΙΚΟΤΗΤΑ5 == entity.ΕΙΔΙΚΟΤΗΤΑ4)
                    entity.ΕΙΔΙΚΟΤΗΤΑ5 = null;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            message = "Η εκκαθάριση διπλότυπων ειδικοτήτων των αιτήσεων ολοκληρώθηκε.";
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Ε-ΑΙΤΗΣΕΙΣ ΕΚΤΥΠΩΣΕΙΣ

        public ActionResult xCandidatesDataPrint()
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

            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult xEidikotitaAitiseisPrint()
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
            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult xSchoolEidikotitaAitiseisPrint()
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
            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult xSchoolAitiseisPrint()
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
            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult xAitiseisDailyPrint()
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
            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult xAitiseisSpoudesPrint()
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
            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult xAitiseisGradesPrint()
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
            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult xPinakasIek1Print()
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
            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult xPinakasIek2Print()
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
            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult xPinakasPostIek1Print()
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
            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult xPinakasPostIek2Print()
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
            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        #endregion


    }
}