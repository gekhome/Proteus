using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using Proteus.Notification;
using Proteus.ServicesMoria;
using Proteus.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proteus.Controllers.DataControllers
{
    [ErrorHandlerFilter]
    public class CandidatesController : ControllerMoria
    {
        private USER_STUDENTS loggedStudent;
        private ΧΜ_ΥΠΟΨΗΦΙΟΣ loggedStudentData;

        private readonly ProteusDBEntities db;
        private readonly IExperienceService experienceService;
        private readonly IUploadService uploadService;

        public CandidatesController(ProteusDBEntities entities,
            IExperienceService experienceService, IUploadService uploadService) : base(entities)
        {
            db = entities;

            this.experienceService = experienceService;
            this.uploadService = uploadService;
        }

        public ActionResult Index(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_STUDENTS");
            }
            else
            {
                loggedStudent = GetLoginStudent();
                if (loggedStudent == null)
                    return RedirectToAction("Error", "Candidates", new { notify = "Δεν βρέθηκε εξουσιοδοτημένος χρήστης για το αίτημα." });
            }

            int egykliosId = Common.GetOpenEgykliosID();
            if (egykliosId == 0)
            {
                string Msg = "Δεν βρέθηκε ανοικτή Προκήρυξη. Όλες οι ενέργειες δημιουργίας και επεξεργασίας δεδομένων είναι απενεργοποιημένες.";
                this.ShowMessage(MessageType.Warning, Msg);
            }
            if (notify != null)
            {
                this.ShowMessage(MessageType.Warning, notify);
            }
            return View();
        }


        #region ΦΟΡΜΑ ΗΛΕΚΤΡΟΝΙΚΗΣ ΑΙΤΗΣΗΣ

        public ActionResult CandidateCreate()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_STUDENTS");
            }
            else
            {
                loggedStudent = GetLoginStudent();
                if (loggedStudent == null)
                    return RedirectToAction("Error", "Candidates", new { notify = "Δεν βρέθηκε εξουσιοδοτημένος χρήστης για το αίτημα." });
            }

            int egykliosId = Common.GetOpenEgykliosID();
            if (egykliosId == 0)
            {
                return RedirectToAction("Index", "Candidates");
            }

            var data = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ where d.ΑΦΜ == loggedStudent.USER_AFM && d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId select d).FirstOrDefault();
            if (data == null)
            {
                return View(new XmAitisiViewModel() { ΑΦΜ = loggedStudent.USER_AFM });
            }
            else
            {
                return RedirectToAction("CandidateEdit", "Candidates");
            }
        }

        [HttpPost]
        public ActionResult CandidateCreate(XmAitisiViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_STUDENTS");
            }
            else
            {
                loggedStudent = GetLoginStudent();
            }
            int egykliosId = Common.GetOpenEgykliosID();

            string ErrorMsg = Kerberos.ValidAitisiData(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                data.ΑΦΜ = loggedStudent.USER_AFM;
                return View(data);
            }
            if (Kerberos.CandidateExists(loggedStudent.USER_AFM))
            {
                data.ΑΦΜ = loggedStudent.USER_AFM;
                return View(data);
            }

            if (ModelState.IsValid)
            {
                ΧΜ_ΥΠΟΨΗΦΙΟΣ entity = new ΧΜ_ΥΠΟΨΗΦΙΟΣ()
                {
                    ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = egykliosId,
                    ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GenerateProtocol(),
                    ΗΜΕΡΟΜΗΝΙΑ = DateTime.Now.Date,
                    ΑΦΜ = loggedStudent.USER_AFM,
                    ΑΜΚΑ = data.ΑΜΚΑ,
                    ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = data.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ,
                    ΜΗΤΡΩΝΥΜΟ = data.ΜΗΤΡΩΝΥΜΟ,
                    ΑΔΤ = data.ΑΔΤ,
                    ΑΔΤ_ΑΡΧΗ = data.ΑΔΤ_ΑΡΧΗ,
                    ΑΔΤ_ΗΜΝΙΑ = data.ΑΔΤ_ΗΜΝΙΑ,
                    ΑΜ_ΑΡΡΕΝΩΝ = data.ΑΜ_ΑΡΡΕΝΩΝ,
                    ΑΡ_ΔΗΜΟΤΟΛΟΓΙΟ = data.ΑΡ_ΔΗΜΟΤΟΛΟΓΙΟ,
                    ΔΗΜΟΣ_ΕΓΓΡΑΦΗΣ = data.ΔΗΜΟΣ_ΕΓΓΡΑΦΗΣ,
                    ΚΑΤΟΙΚΙΑ_ΔΝΣΗ = data.ΚΑΤΟΙΚΙΑ_ΔΝΣΗ,
                    ΚΑΤΟΙΚΙΑ_ΠΟΛΗ = data.ΚΑΤΟΙΚΙΑ_ΠΟΛΗ,
                    ΤΗΛΕΦΩΝΟ = data.ΤΗΛΕΦΩΝΟ,
                    EMAIL = data.EMAIL,
                    ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ = data.ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ,
                    ΒΑΘΜΟΣ = data.ΒΑΘΜΟΣ,
                    ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ = data.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ,
                    ΤΡΙΤΕΚΝΟΣ = data.ΤΡΙΤΕΚΝΟΣ,
                    ΠΟΛΥΤΕΚΝΟΣ = data.ΠΟΛΥΤΕΚΝΟΣ,
                    ΜΟΝΟΓΟΝΕΙΚΟΣ = data.ΜΟΝΟΓΟΝΕΙΚΟΣ,
                    ΦΥΛΟ = data.ΦΥΛΟ,
                    ΙΕΚ1 = data.ΙΕΚ1,
                    ΕΙΔΙΚΟΤΗΤΑ1 = data.ΕΙΔΙΚΟΤΗΤΑ1,
                    ΕΙΔΙΚΟΤΗΤΑ2 = data.ΕΙΔΙΚΟΤΗΤΑ2,
                    ΕΙΔΙΚΟΤΗΤΑ3 = data.ΕΙΔΙΚΟΤΗΤΑ3,
                    TERM1 = data.TERM1,
                    TERM2 = data.TERM2,
                    TERM3 = data.TERM3,
                    ΙΕΚ2 = data.ΙΕΚ2,
                    ΕΙΔΙΚΟΤΗΤΑ4 = data.ΕΙΔΙΚΟΤΗΤΑ4,
                    ΕΙΔΙΚΟΤΗΤΑ5 = data.ΕΙΔΙΚΟΤΗΤΑ5,
                    TERM4 = data.TERM4,
                    TERM5 = data.TERM5,
                    ΔΙΑΒΑΤΗΡΙΟ = data.ΔΙΑΒΑΤΗΡΙΟ,
                    ΕΘΝΙΚΟΤΗΤΑ = data.ΕΘΝΙΚΟΤΗΤΑ,
                    ΠΑΡΑΤΗΡΗΣΕΙΣ = data.ΠΑΡΑΤΗΡΗΣΕΙΣ
                };
                db.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Add(entity);
                db.SaveChanges();

                string msg = "Η αποθήκευση ολοκληρώθηκε με επιτυχία.";
                return RedirectToAction("CandidateEdit", "Candidates", new { notify = msg });
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης.");
            data.ΑΦΜ = loggedStudent.USER_AFM;
            return View(data);
        }

        public ActionResult CandidateEdit(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_STUDENTS");
            }
            else
            {
                loggedStudent = GetLoginStudent();
                if (loggedStudent == null)
                    return RedirectToAction("Error", "Candidates", new { notify = "Δεν βρέθηκε εξουσιοδοτημένος χρήστης για το αίτημα." });
            }

            int egykliosId = Common.GetOpenEgykliosID();
            if (egykliosId == 0)
            {
                string Msg = "Δεν βρέθηκε ανοικτή Προκήρυξη. Όλες οι ενέργειες δημιουργίας και επεξεργασίας δεδομένων είναι απενεργοποιημένες.";
                return RedirectToAction("Index", "Candidates", new { notify = Msg });
            }

            if (notify != null) this.ShowMessage(MessageType.Success, notify);

            XmAitisiViewModel data = GetCandidateDataFromDB(loggedStudent);
            if (data == null)
            {
                return RedirectToAction("CandidateCreate", "Candidates");
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult CandidateEdit(XmAitisiViewModel data)
        {
            loggedStudent = GetLoginStudent();          
            int egykliosId = Common.GetOpenEgykliosID();
            int aitisiID = 0;

            var aitisi = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ where d.ΑΦΜ == loggedStudent.USER_AFM && d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId select d).FirstOrDefault();
            if (aitisi != null)
                aitisiID = aitisi.ΑΙΤΗΣΗ_ΚΩΔ;

            string errorMessage = Kerberos.ValidAitisiData(data);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + errorMessage);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                ΧΜ_ΥΠΟΨΗΦΙΟΣ entity = db.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Find(aitisiID);

                entity.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = egykliosId;
                entity.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ = aitisi.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ;
                entity.ΗΜΕΡΟΜΗΝΙΑ = aitisi.ΗΜΕΡΟΜΗΝΙΑ;
                entity.ΑΦΜ = loggedStudent.USER_AFM;
                entity.ΑΜΚΑ = data.ΑΜΚΑ;
                entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ;
                entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ;
                entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ;
                entity.ΜΗΤΡΩΝΥΜΟ = data.ΜΗΤΡΩΝΥΜΟ;
                entity.ΑΔΤ = data.ΑΔΤ;
                entity.ΑΔΤ_ΑΡΧΗ = data.ΑΔΤ_ΑΡΧΗ;
                entity.ΑΔΤ_ΗΜΝΙΑ = data.ΑΔΤ_ΗΜΝΙΑ;
                entity.ΑΜ_ΑΡΡΕΝΩΝ = data.ΑΜ_ΑΡΡΕΝΩΝ;
                entity.ΑΡ_ΔΗΜΟΤΟΛΟΓΙΟ = data.ΑΡ_ΔΗΜΟΤΟΛΟΓΙΟ;
                entity.ΔΗΜΟΣ_ΕΓΓΡΑΦΗΣ = data.ΔΗΜΟΣ_ΕΓΓΡΑΦΗΣ;
                entity.ΚΑΤΟΙΚΙΑ_ΔΝΣΗ = data.ΚΑΤΟΙΚΙΑ_ΔΝΣΗ;
                entity.ΚΑΤΟΙΚΙΑ_ΠΟΛΗ = data.ΚΑΤΟΙΚΙΑ_ΠΟΛΗ;
                entity.ΤΗΛΕΦΩΝΟ = data.ΤΗΛΕΦΩΝΟ;
                entity.EMAIL = data.EMAIL;
                entity.ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ = data.ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ;
                entity.ΒΑΘΜΟΣ = data.ΒΑΘΜΟΣ;
                entity.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ = data.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ;
                entity.ΤΡΙΤΕΚΝΟΣ = data.ΤΡΙΤΕΚΝΟΣ;
                entity.ΠΟΛΥΤΕΚΝΟΣ = data.ΠΟΛΥΤΕΚΝΟΣ;
                entity.ΜΟΝΟΓΟΝΕΙΚΟΣ = data.ΜΟΝΟΓΟΝΕΙΚΟΣ;
                entity.ΦΥΛΟ = data.ΦΥΛΟ;
                entity.ΕΜΠΕΙΡΙΑ_ΜΗΝΕΣ = Kerberos.GetMonthsWork(entity);
                entity.ΙΕΚ1 = data.ΙΕΚ1;
                entity.ΕΙΔΙΚΟΤΗΤΑ1 = data.ΕΙΔΙΚΟΤΗΤΑ1;
                entity.ΕΙΔΙΚΟΤΗΤΑ2 = data.ΕΙΔΙΚΟΤΗΤΑ2;
                entity.ΕΙΔΙΚΟΤΗΤΑ3 = data.ΕΙΔΙΚΟΤΗΤΑ3;
                entity.TERM1 = data.TERM1;
                entity.TERM2 = data.TERM2;
                entity.TERM3 = data.TERM3;
                entity.ΙΕΚ2 = data.ΙΕΚ2;
                entity.ΕΙΔΙΚΟΤΗΤΑ4 = data.ΕΙΔΙΚΟΤΗΤΑ4;
                entity.ΕΙΔΙΚΟΤΗΤΑ5 = data.ΕΙΔΙΚΟΤΗΤΑ5;
                entity.TERM4 = data.TERM4;
                entity.TERM5 = data.TERM5;
                entity.ΑΠΟΤΕΛΕΣΜΑ = data.ΑΠΟΤΕΛΕΣΜΑ;
                entity.ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ;
                entity.ΔΙΑΒΑΤΗΡΙΟ = data.ΔΙΑΒΑΤΗΡΙΟ;
                entity.ΕΘΝΙΚΟΤΗΤΑ = data.ΕΘΝΙΚΟΤΗΤΑ;
                entity.ΠΑΡΑΤΗΡΗΣΕΙΣ = data.ΠΑΡΑΤΗΡΗΣΕΙΣ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                XmAitisiViewModel newData = GetCandidateDataFromDB(loggedStudent);
                return View(newData);
            }
            else
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης ή άκυρου κωδικού αίτησης.");
                return View(data);
            }
        }

        public XmAitisiViewModel GetCandidateDataFromDB(USER_STUDENTS loggedStudent)
        {
            XmAitisiViewModel data = new XmAitisiViewModel();
            int egykliosId = Common.GetOpenEgykliosID();

            data = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ
                    where d.ΑΦΜ == loggedStudent.USER_AFM && d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId
                    select new XmAitisiViewModel
                    {
                        ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                        ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                        ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ = d.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ,
                        ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                        ΑΦΜ = d.ΑΦΜ,
                        ΑΜΚΑ = d.ΑΜΚΑ,
                        ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                        ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                        ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                        ΜΗΤΡΩΝΥΜΟ = d.ΜΗΤΡΩΝΥΜΟ,
                        EMAIL = d.EMAIL,
                        ΑΔΤ = d.ΑΔΤ,
                        ΑΔΤ_ΑΡΧΗ = d.ΑΔΤ_ΑΡΧΗ,
                        ΑΔΤ_ΗΜΝΙΑ = d.ΑΔΤ_ΗΜΝΙΑ,
                        ΑΜ_ΑΡΡΕΝΩΝ = d.ΑΜ_ΑΡΡΕΝΩΝ,
                        ΑΡ_ΔΗΜΟΤΟΛΟΓΙΟ = d.ΑΡ_ΔΗΜΟΤΟΛΟΓΙΟ,
                        ΔΗΜΟΣ_ΕΓΓΡΑΦΗΣ = d.ΔΗΜΟΣ_ΕΓΓΡΑΦΗΣ,
                        ΚΑΤΟΙΚΙΑ_ΔΝΣΗ = d.ΚΑΤΟΙΚΙΑ_ΔΝΣΗ,
                        ΚΑΤΟΙΚΙΑ_ΠΟΛΗ = d.ΚΑΤΟΙΚΙΑ_ΠΟΛΗ,
                        ΤΗΛΕΦΩΝΟ = d.ΤΗΛΕΦΩΝΟ,
                        ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ,
                        ΒΑΘΜΟΣ = d.ΒΑΘΜΟΣ,
                        ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ = d.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ,
                        ΤΡΙΤΕΚΝΟΣ = d.ΤΡΙΤΕΚΝΟΣ ?? false,
                        ΠΟΛΥΤΕΚΝΟΣ = d.ΠΟΛΥΤΕΚΝΟΣ ?? false,
                        ΜΟΝΟΓΟΝΕΙΚΟΣ = d.ΜΟΝΟΓΟΝΕΙΚΟΣ ?? false,
                        ΦΥΛΟ = d.ΦΥΛΟ,
                        ΙΕΚ1 = d.ΙΕΚ1,
                        ΕΙΔΙΚΟΤΗΤΑ1 = d.ΕΙΔΙΚΟΤΗΤΑ1,
                        ΕΙΔΙΚΟΤΗΤΑ2 = d.ΕΙΔΙΚΟΤΗΤΑ2,
                        ΕΙΔΙΚΟΤΗΤΑ3 = d.ΕΙΔΙΚΟΤΗΤΑ3,
                        TERM1 = d.TERM1,
                        TERM2 = d.TERM2,
                        TERM3 = d.TERM3,
                        ΙΕΚ2 = d.ΙΕΚ2,
                        ΕΙΔΙΚΟΤΗΤΑ4 = d.ΕΙΔΙΚΟΤΗΤΑ4,
                        ΕΙΔΙΚΟΤΗΤΑ5 = d.ΕΙΔΙΚΟΤΗΤΑ5,
                        TERM4 = d.TERM4,
                        TERM5 = d.TERM5,
                        ΕΘΝΙΚΟΤΗΤΑ = d.ΕΘΝΙΚΟΤΗΤΑ,
                        ΔΙΑΒΑΤΗΡΙΟ = d.ΔΙΑΒΑΤΗΡΙΟ,
                        ΠΑΡΑΤΗΡΗΣΕΙΣ = d.ΠΑΡΑΤΗΡΗΣΕΙΣ

                    }).FirstOrDefault();

            return (data);
        }

        public ActionResult CandidateAitisiPrint(int aitisiID = 0)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_STUDENTS");
            }
            else
            {
                loggedStudent = GetLoginStudent();
            }
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

        #endregion


        #region ΕΠΑΓΓΕΛΜΑΤΙΚΗ ΕΜΠΕΙΡΙΑ

        public ActionResult WorkExperience()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_STUDENTS");
            }
            else
            {
                loggedStudent = GetLoginStudent();
                if (loggedStudent == null)
                    return RedirectToAction("Error", "Candidates", new { notify = "Δεν βρέθηκε εξουσιοδοτημένος χρήστης για το αίτημα." });
            }

            if (Common.GetOpenEgykliosID() == 0)
            {
                string msg = "Δεν βρέθηκε ανοικτή Προκήρυξη για προβολή εργασιακών εμπειριών.";
                return RedirectToAction("Index", "Candidates", new { notify = msg });
            }

            int aitisiId = Common.GetAitisiIDFromAFM(loggedStudent.USER_AFM);
            if (aitisiId == 0)
            {
                string msg = "Για καταχώρηση εργασιακών εμπειριών πρέπει πρώτα να υποβάλετε μια αίτηση.";
                return RedirectToAction("Index", "Candidates", new { notify = msg });
            }

            AitisiHeaderToViewBag();
            return View();
        }

        public void AitisiHeaderToViewBag()
        {
            int aitisiId = Common.GetAitisiIDFromAFM(GetLoginStudent().USER_AFM);

            var data = (from d in db.ΧΜ_sqlAITISEIS_HEADERS where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId select d).FirstOrDefault();
            if (data != null)
                ViewBag.AitisiHeader = data;
        }


        #region ΠΛΕΓΜΑ ΠΡΟΫΠΗΡΕΣΙΩΝ ΥΠΟΨΗΦΙΟΥ

        public ActionResult Experience_Read([DataSourceRequest] DataSourceRequest request)
        {
            int aitisiId = Common.GetAitisiIDFromAFM(GetLoginStudent().USER_AFM);

            IEnumerable<XmExperienceViewModel> data = experienceService.Read(aitisiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Experience_Create([DataSourceRequest] DataSourceRequest request, XmExperienceViewModel data)
        {
            int egykliosId = Common.GetOpenEgykliosID();
            int aitisiId = Common.GetAitisiIDFromAFM(GetLoginStudent().USER_AFM);

            var newdata = new XmExperienceViewModel();

            if (!(aitisiId > 0)) ModelState.AddModelError("", "Πρέπει να υπάρχει μία αίτηση πρώτα. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                experienceService.Create(data, aitisiId, egykliosId);
                newdata = experienceService.Refresh(data.ΕΜΠΕΙΡΙΑ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Experience_Update([DataSourceRequest] DataSourceRequest request, XmExperienceViewModel data)
        {
            int egykliosId = Common.GetOpenEgykliosID();
            int aitisiId = Common.GetAitisiIDFromAFM(GetLoginStudent().USER_AFM);

            var newdata = new XmExperienceViewModel();

            if (!(aitisiId > 0)) ModelState.AddModelError("", "Πρέπει να υπάρχει μία αίτηση πρώτα. Η καταχώρηση ακυρώθηκε.");

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

        #endregion


        #region ΜΕΤΑΦΟΡΤΩΣΗ ΔΙΚΑΙΟΛΟΓΗΤΙΚΩΝ

        public ActionResult UploadData(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_STUDENTS");
            }
            else
            {
                loggedStudent = GetLoginStudent();
                if (loggedStudent == null)
                    return RedirectToAction("Error", "Candidates", new { notify = "Δεν βρέθηκε εξουσιοδοτημένος χρήστης για το αίτημα." });
            }
            int egykliosId = Common.GetOpenEgykliosID();
            if (egykliosId == 0)
            {
                string Msg = "Δεν βρέθηκε ανοικτή Προκήρυξη. Όλες οι ενέργειες δημιουργίας και επεξεργασίας δεδομένων είναι απενεργοποιημένες.";
                return RedirectToAction("Index", "Candidates", new { notify = Msg });
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            if (!AitisisExist())
            {
                string msg = "Για να γίνει μεταφόρτωση πρέπει πρώτα να καταχωρήσετε αίτηση.";
                return RedirectToAction("Index", "Candidates", new { notify = msg });
            }

            if (!Common.VerifyUploadIntegrity(egykliosId, loggedStudent))
            {
                notify = "Το σχολείο της αίτησης δεν συμφωνεί με το σχολείο για το οποίο έχουν ανέβει τα αρχεία. ";
                notify += "Διαγράψτε τα ανεβασμένα αρχεία και μεταφορτώστε τα πάλι, αλλιώς δεν θα μπορούν να βρεθούν.";
                this.ShowMessage(MessageType.Error, notify);
            }

            return View();
        }

        #region MASTER GRID CRUD FUNCTIONS

        public ActionResult Upload_Read([DataSourceRequest] DataSourceRequest request)
        {
            int egykliosId = Common.GetOpenEgykliosID();
            string studentAfm = GetLoginStudent().USER_AFM;

            IEnumerable<UploadsViewModel> data = uploadService.Read(egykliosId, studentAfm);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload_Create([DataSourceRequest] DataSourceRequest request, UploadsViewModel data)
        {
            loggedStudent = GetLoginStudent();
            int egykliosId = Common.GetOpenEgykliosID();

            var newdata = new UploadsViewModel();

            if (data != null && ModelState.IsValid)
            {
                uploadService.Create(data, egykliosId, loggedStudent.USER_AFM);
                newdata = uploadService.Refresh(data.UPLOAD_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload_Update([DataSourceRequest] DataSourceRequest request, UploadsViewModel data)
        {
            loggedStudent = GetLoginStudent();
            int egykliosId = Common.GetOpenEgykliosID();

            var newdata = new UploadsViewModel();

            if (data != null && ModelState.IsValid)
            {
                uploadService.Update(data, egykliosId, loggedStudent.USER_AFM);
                newdata = uploadService.Refresh(data.UPLOAD_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload_Destroy([DataSourceRequest] DataSourceRequest request, UploadsViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteUpload(data.UPLOAD_ID))
                {
                    uploadService.Destroy(data);
                }
                else ModelState.AddModelError("", "Για να γίνει διαγραφή πρέπει πρώτα να διαγραφούν τα σχετικά αρχεία μεταφόρτωσης");
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        // --------------------------
        // Αντικαθιστά τη συνηθισμένη action Destroy διότι όταν χρειάζεται
        // error message η ModelState.AddModelError έχει το σύμπτωμα να
        // καλεί ξανά την Destroy μετά από μια Create/Update Action!
        // Χρησιμοποείται σε συνδυασμό με την jQuery deleteRow() στον client.
        // Ημερομηνία : 26/03/2020
        // --------------------------
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload_Delete(int uploadId = 0)
        {
            string msg = uploadService.Delete(uploadId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region CHILD GRID (UPLOADED FILEDETAILS)

        public ActionResult UploadFiles_Read([DataSourceRequest] DataSourceRequest request, int uploadId = 0)
        {
            IEnumerable<UploadsFilesViewModel> data = uploadService.GetFiles(uploadId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadFiles_Destroy([DataSourceRequest] DataSourceRequest request, UploadsFilesViewModel data)
        {
            // TODO: ALSO REMOVE UPLOADED FILES FROM SERVER
            if (data != null)
            {
                // First delete the physical file and then the info record. Important!
                DeleteUploadedFile(data.ID);
                uploadService.DestroyFile(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region UPLOAD FORM WITH SAVE-REMOVE ACTIONS

        public ActionResult UploadForm(int uploadId, string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_STUDENTS");
            }
            else
            {
                loggedStudent = GetLoginStudent();
                if (loggedStudent == null)
                    return RedirectToAction("Error", "Candidates", new { notify = "Δεν βρέθηκε εξουσιοδοτημένος χρήστης για το αίτημα." });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);
            if (!(uploadId > 0))
            {
                string msg = "Άκυρος κωδικός μεταφόρτωσης. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή μεταφόρτωσης.";
                return RedirectToAction("ErrorData", "Candidates", new { notify = msg });
            }
            ViewData["uploadId"] = uploadId;

            return View();
        }

        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files, int uploadId = 0)
        {
            string uploadPath = UPLOAD_PATH;
            loggedStudent = GetLoginStudent();

            List<UploadsFilesViewModel> fileDetails = new List<UploadsFilesViewModel>();

            // returns tuple with Item1=school_id, Item2=prokirixi_id, Item3=aitisi_id
            var upload_info = Common.GetUploadInfo(uploadId);
            int schoolyearId = (int)db.ΧΜ_ΕΓΚΥΚΛΙΟΣ.Find(upload_info.Item2).ΣΧΟΛΙΚΟ_ΕΤΟΣ;

            string folder = Common.GetUserSchoolFromSchoolId(upload_info.Item1);
            string subfolder = Common.GetSchoolYearText(schoolyearId);
            if (!string.IsNullOrEmpty(folder) && !string.IsNullOrEmpty(subfolder))
                uploadPath += folder + "/" + subfolder + "/";

            try
            {
                bool exists = Directory.Exists(Server.MapPath(uploadPath));
                if (!exists)
                    Directory.CreateDirectory(Server.MapPath(uploadPath));

                if (files != null)
                {
                    foreach (var file in files)
                    {
                        // Some browsers send file names with full path.
                        // We are only interested in the file name.
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var fileExtension = Path.GetExtension(fileName);
                            if (!Kerberos.ValidFileExtension(fileExtension))
                            {
                                string msg = "Επιτρέπονται μόνο αρχεία τύπου PDF, JPG, JPEG. Δοκιμάστε πάλι.";
                                return Content(msg);
                            }
                            ΧΜ_UPLOADS_FILES fileDetail = new ΧΜ_UPLOADS_FILES()
                            {
                                FILENAME = fileName.Length > 120 ? fileName.Substring(0, 120) : fileName,
                                EXTENSION = fileExtension,
                                SCHOOL_USER = folder,
                                SCHOOLYEAR_TEXT = subfolder,
                                UPLOAD_ID = uploadId,
                                ID = loggedStudent.USER_AFM + "_" + Guid.NewGuid()
                            };
                            db.ΧΜ_UPLOADS_FILES.Add(fileDetail);
                            db.SaveChanges();

                            var physicalPath = Path.Combine(Server.MapPath(uploadPath), fileDetail.ID + fileDetail.EXTENSION);
                            file.SaveAs(physicalPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = "Παρουσιάστηκε σφάλμα στη μεταφόρτωση:<br/>" + ex.Message;
                return Content(msg);
            }
            // Return an empty string to signify success
            return Content("");
        }


        // NOT USED - 26.10.2022 (replaced by function in Kerberos.cs)
        public bool ValidFileExtension(string extension)
        {
            string[] extensions = { ".EXE", ".COM", "BAT", ".MSI", ".BIN", ".CMD", ".JSE", ".REG", ".VBS", ".VBE", ".WS", ".WSF" };

            List<string> forbidden_extensions = new List<string>(extensions);

            if (forbidden_extensions.Contains(extension.ToUpper()))
                return false;
            return true;
        }

        public ActionResult Remove(string[] fileNames, int uploadId)
        {
            // The parameter of the Remove action must be called "fileNames"
            string folder = "";
            string uploadPath = UPLOAD_PATH;
            string subfolder = "";

            // returns tuple with Item1=school_id, Item2=prokirixi_id, Item3=aitisi_id
            var upload_info = Common.GetUploadInfo(uploadId);
            int schoolyearId = (int)db.ΧΜ_ΕΓΚΥΚΛΙΟΣ.Find(upload_info.Item2).ΣΧΟΛΙΚΟ_ΕΤΟΣ;

            folder = Common.GetUserSchoolFromSchoolId(upload_info.Item1);
            subfolder = Common.GetSchoolYearText(schoolyearId);

            if (!String.IsNullOrEmpty(folder) && !String.IsNullOrEmpty(subfolder))
                uploadPath += folder + "/" + subfolder + "/";

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var extension = Path.GetExtension(fileName);

                    string file_guid = Common.GetFileGuidFromName(fileName, uploadId);

                    string fileToDelete = file_guid + extension;
                    var physicalPath = Path.Combine(Server.MapPath(uploadPath), fileToDelete);

                    if (System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                        DeleteUploadFileRecord(file_guid);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public FileResult Download(string file_id)
        {
            String p = "";
            String f = "";
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

        public ActionResult DeleteUploadFileRecord(string file_guid)
        {
            ΧΜ_UPLOADS_FILES entity = db.ΧΜ_UPLOADS_FILES.Find(file_guid);
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.ΧΜ_UPLOADS_FILES.Remove(entity);
                db.SaveChanges();
            }
            return Content("");
        }

        public ActionResult DeleteUploadedFile(string file_guid)
        {
            string folder = "";
            string uploadPath = UPLOAD_PATH;
            string subfolder = "";
            string extension = "";

            var data = (from d in db.ΧΜ_UPLOADS_FILES where d.ID == file_guid select d).FirstOrDefault();
            if (data != null)
            {
                folder = data.SCHOOL_USER;
                subfolder = data.SCHOOLYEAR_TEXT;
                extension = data.EXTENSION;

                if (!String.IsNullOrEmpty(folder) && !String.IsNullOrEmpty(subfolder))
                    uploadPath += folder + "/" + subfolder + "/";

                string fileToDelete = file_guid + extension;
                var physicalPath = Path.Combine(Server.MapPath(uploadPath), fileToDelete);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Content("");
        }

        #endregion

        public bool AitisisExist()
        {
            int egykliosId = Common.GetOpenEgykliosID();
            string studentAfm = GetLoginStudent().USER_AFM;

            if (studentAfm != null)
            {
                var aitiseis = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ where d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId && d.ΑΦΜ == studentAfm select d).ToList();
                if (aitiseis.Count > 0)
                    return true;
            }
            return false;
        }

        #endregion


        #region ΦΟΡΜΑ ΑΙΤΗΣΗΣ ΜΕ ΜΟΡΙΑ

        public ActionResult CandidateMoria(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_STUDENTS");
            }
            else
            {
                loggedStudent = GetLoginStudent();
            }

            int egykliosId  = Common.GetOpenEgykliosID();
            if (egykliosId == 0)
            {
                string Msg = "Δεν βρέθηκε ανοικτή Προκήρυξη. Όλες οι ενέργειες δημιουργίας και επεξεργασίας δεδομένων είναι απενεργοποιημένες.";
                return RedirectToAction("Index", "Candidates", new { notify = Msg });
            }

            if (notify != null) this.ShowMessage(MessageType.Success, notify);

            XmAitisiViewModel data = GetCandidateMoriaFromDB(loggedStudent);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε αίτηση για προβολή της καρτέλας αξιολόγησης.";
                return RedirectToAction("Index", "Candidates", new { notify = msg });
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult CandidateMoria(XmAitisiViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_STUDENTS");
            }
            else
            {
                loggedStudent = GetLoginStudent();
            }
            int egykliosId = Common.GetOpenEgykliosID();
            int aitisiID = 0;

            var aitisi = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ where d.ΑΦΜ == loggedStudent.USER_AFM && d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId select d).FirstOrDefault();
            if (aitisi != null)
                aitisiID = aitisi.ΑΙΤΗΣΗ_ΚΩΔ;

            string errorMessage = Kerberos.ValidAitisiData(data);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + errorMessage);
                return View(data);
            }

            if (aitisiID > 0 && ModelState.IsValid)
            {
                ΧΜ_ΥΠΟΨΗΦΙΟΣ entity = db.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Find(aitisiID);

                entity.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ = aitisi.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ;
                entity.ΗΜΕΡΟΜΗΝΙΑ = aitisi.ΗΜΕΡΟΜΗΝΙΑ;
                entity.ΑΦΜ = loggedStudent.USER_AFM;
                entity.ΑΜΚΑ = data.ΑΜΚΑ;
                entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ;
                entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ;
                entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ;
                entity.ΜΗΤΡΩΝΥΜΟ = data.ΜΗΤΡΩΝΥΜΟ;
                entity.ΑΔΤ = data.ΑΔΤ;
                entity.ΑΔΤ_ΑΡΧΗ = data.ΑΔΤ_ΑΡΧΗ;
                entity.ΑΔΤ_ΗΜΝΙΑ = data.ΑΔΤ_ΗΜΝΙΑ;
                entity.ΑΜ_ΑΡΡΕΝΩΝ = data.ΑΜ_ΑΡΡΕΝΩΝ;
                entity.ΑΡ_ΔΗΜΟΤΟΛΟΓΙΟ = data.ΑΡ_ΔΗΜΟΤΟΛΟΓΙΟ;
                entity.ΔΗΜΟΣ_ΕΓΓΡΑΦΗΣ = data.ΔΗΜΟΣ_ΕΓΓΡΑΦΗΣ;
                entity.ΚΑΤΟΙΚΙΑ_ΔΝΣΗ = data.ΚΑΤΟΙΚΙΑ_ΔΝΣΗ;
                entity.ΚΑΤΟΙΚΙΑ_ΠΟΛΗ = data.ΚΑΤΟΙΚΙΑ_ΠΟΛΗ;
                entity.ΤΗΛΕΦΩΝΟ = data.ΤΗΛΕΦΩΝΟ;
                entity.EMAIL = data.EMAIL;
                entity.ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ = data.ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ;
                entity.ΒΑΘΜΟΣ = data.ΒΑΘΜΟΣ;
                entity.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ = data.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ;
                entity.ΤΡΙΤΕΚΝΟΣ = data.ΤΡΙΤΕΚΝΟΣ;
                entity.ΠΟΛΥΤΕΚΝΟΣ = data.ΠΟΛΥΤΕΚΝΟΣ;
                entity.ΜΟΝΟΓΟΝΕΙΚΟΣ = data.ΜΟΝΟΓΟΝΕΙΚΟΣ;
                entity.ΦΥΛΟ = data.ΦΥΛΟ;
                entity.ΕΜΠΕΙΡΙΑ_ΜΗΝΕΣ = Kerberos.GetMonthsWork(entity);
                entity.ΙΕΚ1 = data.ΙΕΚ1;
                entity.ΕΙΔΙΚΟΤΗΤΑ1 = data.ΕΙΔΙΚΟΤΗΤΑ1;
                entity.ΕΙΔΙΚΟΤΗΤΑ2 = data.ΕΙΔΙΚΟΤΗΤΑ2;
                entity.ΕΙΔΙΚΟΤΗΤΑ3 = data.ΕΙΔΙΚΟΤΗΤΑ3;
                entity.ΙΕΚ2 = data.ΙΕΚ2;
                entity.ΕΙΔΙΚΟΤΗΤΑ4 = data.ΕΙΔΙΚΟΤΗΤΑ4;
                entity.ΕΙΔΙΚΟΤΗΤΑ5 = data.ΕΙΔΙΚΟΤΗΤΑ5;
                entity.ΑΠΟΤΕΛΕΣΜΑ = data.ΑΠΟΤΕΛΕΣΜΑ;
                entity.ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ;
                entity.ΔΙΑΒΑΤΗΡΙΟ = data.ΔΙΑΒΑΤΗΡΙΟ;
                entity.ΕΘΝΙΚΟΤΗΤΑ = data.ΕΘΝΙΚΟΤΗΤΑ;
                entity.ΠΑΡΑΤΗΡΗΣΕΙΣ = data.ΠΑΡΑΤΗΡΗΣΕΙΣ;
                entity.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = egykliosId;
                // Moria calculators here
                entity.ΜΟΡΙΑ_ΒΑΘΜΟΣ = Kerberos.MoriaGrade(entity);
                entity.ΜΟΡΙΑ_ΕΜΠΕΙΡΙΑ = Kerberos.MoriaWork(entity);
                entity.ΜΟΡΙΑ_ΑΠΟΦΟΙΤΗΣΗ = Kerberos.MoriaApofitisi(entity);
                entity.ΜΟΡΙΑ_ΤΡΙΤΕΚΝΟΣ = Kerberos.MoriaTriteknos(entity);
                entity.ΜΟΡΙΑ_ΠΟΛΥΤΕΚΝΟΣ = Kerberos.MoriaPolyteknos(entity);
                entity.ΜΟΡΙΑ_ΜΟΝΟΓΟΝΕΙΚΟΣ = Kerberos.MoriaMonogoneikos(entity);
                entity.ΜΟΡΙΑ = entity.ΜΟΡΙΑ_ΒΑΘΜΟΣ + entity.ΜΟΡΙΑ_ΕΜΠΕΙΡΙΑ + entity.ΜΟΡΙΑ_ΑΠΟΦΟΙΤΗΣΗ + entity.ΜΟΡΙΑ_ΤΡΙΤΕΚΝΟΣ + entity.ΜΟΡΙΑ_ΠΟΛΥΤΕΚΝΟΣ + entity.ΜΟΡΙΑ_ΜΟΝΟΓΟΝΕΙΚΟΣ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                XmAitisiViewModel newData = GetCandidateMoriaFromDB(loggedStudent);
                return View(newData);
            }
            else
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης ή άκυρου κωδικού αίτησης.");
                return View(data);
            }
        }

        public XmAitisiViewModel GetCandidateMoriaFromDB(USER_STUDENTS loggedStudent)
        {
            XmAitisiViewModel data = new XmAitisiViewModel();
            int egykliosId = Common.GetOpenEgykliosID();

            data = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ
                    where d.ΑΦΜ == loggedStudent.USER_AFM && d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId
                    select new XmAitisiViewModel
                    {
                        ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                        ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                        ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ = d.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ,
                        ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                        ΑΦΜ = d.ΑΦΜ,
                        ΑΜΚΑ = d.ΑΜΚΑ,
                        ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                        ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                        ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                        ΜΗΤΡΩΝΥΜΟ = d.ΜΗΤΡΩΝΥΜΟ,
                        EMAIL = d.EMAIL,
                        ΑΔΤ = d.ΑΔΤ,
                        ΑΔΤ_ΑΡΧΗ = d.ΑΔΤ_ΑΡΧΗ,
                        ΑΔΤ_ΗΜΝΙΑ = d.ΑΔΤ_ΗΜΝΙΑ,
                        ΑΜ_ΑΡΡΕΝΩΝ = d.ΑΜ_ΑΡΡΕΝΩΝ,
                        ΑΡ_ΔΗΜΟΤΟΛΟΓΙΟ = d.ΑΡ_ΔΗΜΟΤΟΛΟΓΙΟ,
                        ΔΗΜΟΣ_ΕΓΓΡΑΦΗΣ = d.ΔΗΜΟΣ_ΕΓΓΡΑΦΗΣ,
                        ΚΑΤΟΙΚΙΑ_ΔΝΣΗ = d.ΚΑΤΟΙΚΙΑ_ΔΝΣΗ,
                        ΚΑΤΟΙΚΙΑ_ΠΟΛΗ = d.ΚΑΤΟΙΚΙΑ_ΠΟΛΗ,
                        ΤΗΛΕΦΩΝΟ = d.ΤΗΛΕΦΩΝΟ,
                        ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ,
                        ΒΑΘΜΟΣ = d.ΒΑΘΜΟΣ,
                        ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ = d.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ,
                        ΤΡΙΤΕΚΝΟΣ = d.ΤΡΙΤΕΚΝΟΣ ?? false,
                        ΠΟΛΥΤΕΚΝΟΣ = d.ΠΟΛΥΤΕΚΝΟΣ ?? false,
                        ΜΟΝΟΓΟΝΕΙΚΟΣ = d.ΜΟΝΟΓΟΝΕΙΚΟΣ ?? false,
                        ΦΥΛΟ = d.ΦΥΛΟ,
                        ΙΕΚ1 = d.ΙΕΚ1,
                        TERM1 = d.TERM1,
                        TERM2 = d.TERM2,
                        TERM3 = d.TERM3,
                        TERM4 = d.TERM4,
                        TERM5 = d.TERM5,
                        ΕΙΔΙΚΟΤΗΤΑ1 = d.ΕΙΔΙΚΟΤΗΤΑ1,
                        ΕΙΔΙΚΟΤΗΤΑ2 = d.ΕΙΔΙΚΟΤΗΤΑ2,
                        ΕΙΔΙΚΟΤΗΤΑ3 = d.ΕΙΔΙΚΟΤΗΤΑ3,
                        ΙΕΚ2 = d.ΙΕΚ2,
                        ΕΙΔΙΚΟΤΗΤΑ4 = d.ΕΙΔΙΚΟΤΗΤΑ4,
                        ΕΙΔΙΚΟΤΗΤΑ5 = d.ΕΙΔΙΚΟΤΗΤΑ5,
                        ΔΙΑΒΑΤΗΡΙΟ = d.ΔΙΑΒΑΤΗΡΙΟ,
                        ΕΘΝΙΚΟΤΗΤΑ = d.ΕΘΝΙΚΟΤΗΤΑ,
                        ΕΜΠΕΙΡΙΑ_ΜΗΝΕΣ = d.ΕΜΠΕΙΡΙΑ_ΜΗΝΕΣ,
                        ΜΟΡΙΑ = d.ΜΟΡΙΑ,
                        ΜΟΡΙΑ_ΒΑΘΜΟΣ = d.ΜΟΡΙΑ_ΒΑΘΜΟΣ,
                        ΜΟΡΙΑ_ΑΠΟΦΟΙΤΗΣΗ = d.ΜΟΡΙΑ_ΑΠΟΦΟΙΤΗΣΗ,
                        ΜΟΡΙΑ_ΕΜΠΕΙΡΙΑ = d.ΜΟΡΙΑ_ΕΜΠΕΙΡΙΑ,
                        ΜΟΡΙΑ_ΠΟΛΥΤΕΚΝΟΣ = d.ΜΟΡΙΑ_ΠΟΛΥΤΕΚΝΟΣ,
                        ΜΟΡΙΑ_ΜΟΝΟΓΟΝΕΙΚΟΣ = d.ΜΟΡΙΑ_ΜΟΝΟΓΟΝΕΙΚΟΣ,
                        ΜΟΡΙΑ_ΤΡΙΤΕΚΝΟΣ = d.ΜΟΡΙΑ_ΤΡΙΤΕΚΝΟΣ,
                        ΑΠΟΤΕΛΕΣΜΑ = d.ΑΠΟΤΕΛΕΣΜΑ ?? 0,
                        ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                        ΠΑΡΑΤΗΡΗΣΕΙΣ = d.ΠΑΡΑΤΗΡΗΣΕΙΣ
                    }).FirstOrDefault();

            return (data);
        }

        public ActionResult CandidateAitisiMoriaPrint(int aitisiID = 0)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_STUDENTS");
            }
            else
            {
                loggedStudent = GetLoginStudent();
            }
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

        #endregion


        #region GETTERS

        public USER_STUDENTS GetLoginStudent()
        {
            loggedStudent = db.USER_STUDENTS.Where(m => m.USER_AFM == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            if (loggedStudent == null)
                return null;

            int egykliosId  = Common.GetOpenEgykliosID();
            ViewBag.loggedUser = loggedStudent.USERNAME;

            loggedStudentData = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ where d.ΑΦΜ == loggedStudent.USER_AFM && d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId select d).FirstOrDefault();

            if (loggedStudentData != null)
            {
                ViewBag.loggedStudent = loggedStudentData;

                if (!string.IsNullOrEmpty(loggedStudentData.ΟΝΟΜΑ) && !string.IsNullOrEmpty(loggedStudentData.ΕΠΩΝΥΜΟ))
                {
                    ViewBag.loggedUser = loggedStudentData.ΟΝΟΜΑ + " " + loggedStudentData.ΕΠΩΝΥΜΟ;
                }
            }
            return loggedStudent;
        }

        #endregion


        #region ERROR PAGES

        public ActionResult Error(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        public ActionResult ErrorData(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        #endregion

    }
}