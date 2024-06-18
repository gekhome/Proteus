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
    public class MoriaSchoolController : ControllerMoria
    {
        private readonly ProteusDBEntities db;

        private readonly IAitisiService aitisiService;
        private readonly IEgyklioiService egyklioiService;
        private readonly IEidikotitaEgykliosService eidikotitaEgykliosService;
        private readonly IExperienceService experienceService;
        private readonly IUploadService uploadService;

        public MoriaSchoolController(ProteusDBEntities entities, IAitisiService aitisiService,
            IEgyklioiService egyklioiService, IEidikotitaEgykliosService eidikotitaEgykliosService,
            IExperienceService experienceService, IUploadService uploadService) : base(entities)
        {
            db = entities;

            this.aitisiService = aitisiService;
            this.egyklioiService = egyklioiService;
            this.eidikotitaEgykliosService = eidikotitaEgykliosService;
            this.experienceService = experienceService;
            this.uploadService = uploadService;
        }

        public ActionResult MoriaMain(string notify = null)
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

            return View();
        }

        #region ΕΓΚΥΚΛΙΟΙ ΥΠΟΨΗΦΙΩΝ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult XmEgykliosList()
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
            PopulateSchoolYears();
            PopulateStatus();

            return View();
        }

        public ActionResult Egyklios_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<XmEgykliosViewModel> data = egyklioiService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΕΓΚΕΚΡΙΜΕΝΕΣ ΕΙΔΙΚΟΤΗΤΕΣ ΑΝΑ ΕΓΚΥΚΛΙΟ ΚΑΙ ΙΕΚ

        public ActionResult XmEidikotitesInEgyklios(string notify = null)
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

        public ActionResult XmEidikotitesEgykliosPrint()
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


        #region ΑΙΤΗΣΕΙΣ ΥΠΟΨΗΦΙΩΝ ΣΠΟΥΔΑΣΤΩΝ

        #region ΠΛΕΓΜΑ ΑΙΤΗΣΕΩΝ

        public ActionResult XmAitiseisList()
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

            if (Common.GetActiveEgykliosID() == 0)
            {
                string msg = "Δεν βρέθηκε ενεργή Προκήρυξη για προβολή υποψηφίων σπουδαστών.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            PopulateSchools();
            return View();
        }

        public ActionResult XmAitiseis_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            int egykliosId = Common.GetActiveEgykliosID();

            IEnumerable<XmAitisiGridViewModel> data = aitisiService.Read(egykliosId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult XmAitisi_Create([DataSourceRequest] DataSourceRequest request, XmAitisiGridViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            int egykliosId = Common.GetActiveEgykliosID();

            var newdata = new XmAitisiGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                aitisiService.Create(data, egykliosId, schoolId);
                newdata = aitisiService.Refresh(data.ΑΙΤΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult XmAitisi_Update([DataSourceRequest] DataSourceRequest request, XmAitisiGridViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            int egykliosId = Common.GetActiveEgykliosID();

            var newdata = new XmAitisiGridViewModel();

            if (data != null & ModelState.IsValid)
            {
                aitisiService.Update(data, egykliosId, schoolId);
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

        public bool CanDeleteXmAitisi(int aitisiID)
        {
            var data1 = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ_ΕΜΠΕΙΡΙΑ where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiID select d).Count();
            var data2 = (from d in db.ΧΜ_UPLOADS where d.AITISI_ID == aitisiID select d).Count();

            if (data1 > 0 || data2 > 0)
                return false;
            else
                return true;
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
            int egykliosId = Common.GetActiveEgykliosID();

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
            int egykliosId = Common.GetActiveEgykliosID();

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

        public ActionResult XmAitisiEdit(int aitisiID)
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
        public ActionResult XmAitisiEdit(XmAitisiViewModel data, int aitisiID)
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

            int egykliosID = Common.GetActiveEgykliosID();

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

        public ActionResult XmAitisiMoriaPrint(int aitisiID = 0)
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


        #region ΑΝΕΒΑΣΜΕΝΑ ΑΡΧΕΙΑ ΑΙΤΗΣΗΣ

        public ActionResult XmAitisiUploadedFiles(int aitisiId = 0)
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

        public ActionResult sDownloadData(string notify = null)
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

            int egykliosId = Common.GetActiveEgykliosID();
            if (egykliosId == 0)
            {
                string Msg = "Δεν βρέθηκε ενεργή Προκήρυξη.";
                return RedirectToAction("Index", "School", new { notify = Msg });
            }
            int schoolYearId = (int)Common.GetActiveEgyklios().ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            ViewData["egykliosProtocol"] = Common.GetActiveEgyklios().ΕΓΚΥΚΛΙΟΣ_ΑΠ;
            ViewData["schoolYearText"] = Common.GetSchoolYearText(schoolYearId);

            if (notify != null)
            {
                this.ShowMessage(MessageType.Warning, notify);
            }
            if (!AitisisSchoolExist(schoolId))
            {
                string msg = "Δεν βρέθηκαν αιτήσεις για το σχολείο αυτό. Η προβολή μεταφορτωμένων αρχείων είναι αδύνατη.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            PopulateSchoolYears();
            PopulateSchoolAitisis(schoolId);

            return View();
        }


        #region MASTER GRID CRUD FUNCTIONS

        public ActionResult Upload_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            int egykliosId = Common.GetActiveEgykliosID();

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


        #region ΜΟΡΙΟΔΟΤΗΣΗ ΑΙΤΗΣΕΩΝ

        public ActionResult BatchMoriodotisi()
        {
            int schoolId = (int)GetLoginSchool() .USER_SCHOOLID;

            string message = "";
            int egykliosId = Common.GetActiveEgykliosID();

            var aitiseis = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ
                            where d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId && d.ΙΕΚ1 == schoolId
                            orderby d.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ
                            select d).ToList();
            if (aitiseis.Count == 0)
            {
                message = "Δεν βρέθηκαν αιτήσεις του σχολείου αυτού για μοριοδότηση.";
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


        #endregion


        #region Ε-ΑΙΤΗΣΕΙΣ ΕΚΤΥΠΩΣΕΙΣ

        public ActionResult XmCandidatesDataPrint()
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

            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID(),
                SCHOOL_ID = schoolId
            };
            return View(xm_param);
        }

        public ActionResult sSchoolEidikotitaAitiseisPrint()
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
            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult sSchoolAitiseisPrint()
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
            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult sAitiseisDailyPrint()
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
            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult sAitiseisSpoudesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult sAitiseisGradesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult XmPinakasPrint()
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

            XmReportParameters XmParams = new XmReportParameters
            {
                SCHOOL_ID = schoolId,
                PROKIRIXI_ID = Common.GetAdminEgykliosID()
            };
            return View(XmParams);
        }

        public ActionResult XmPinakasIek1Print()
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

            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                SCHOOL_ID = schoolId,
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult XmPinakasIek2Print()
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

            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                SCHOOL_ID = schoolId,
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult XmPinakasPostIek1Print()
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

            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                SCHOOL_ID = schoolId,
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        public ActionResult XmPinakasPostIek2Print()
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

            XM_REPORT_PARAMETERS xm_param = new XM_REPORT_PARAMETERS
            {
                SCHOOL_ID = schoolId,
                EGYKLIOS_ID = Common.GetAdminEgykliosID()
            };
            return View(xm_param);
        }

        #endregion


        #region --- ΔΕΝ ΧΡΗΣΙΜΟΠΟΙΟΥΝΤΑΙ ---

        #region ΠΛΕΓΜΑ ΕΠΕΞΕΡΓΑΣΙΑΣ ΑΠΟΤΕΛΕΣΜΑΤΩΝ ΥΠΟΨΗΦΙΩΝ

        public ActionResult XmAitiseisResults()
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

            if (Common.GetAdminEgykliosID() == 0)
            {
                string msg = "Δεν βρέθηκε Προκήρυξη σε καθεστώς διαχείρισης για προβολή υποψηφίων σπουδαστών.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            PopulateEgykliosEidikotites(schoolId);
            PopulateXmApotelesma();
            return View();
        }

        public ActionResult XmAitisiResult_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = XmGetAitiseisResultsFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult XmAitisiResult_Update([DataSourceRequest] DataSourceRequest request, XmAitiseisResultsViewModel data)
        {
            var newdata = new XmAitiseisResultsViewModel();

            if (data != null & ModelState.IsValid)
            {
                ΧΜ_ΥΠΟΨΗΦΙΟΣ entity = db.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Find(data.ΑΙΤΗΣΗ_ΚΩΔ);
                entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ;
                entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ;
                entity.ΕΙΔΙΚΟΤΗΤΑ1 = data.ΕΙΔΙΚΟΤΗΤΑ1;
                entity.ΑΠΟΤΕΛΕΣΜΑ = data.ΑΠΟΤΕΛΕΣΜΑ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                newdata = XmRefreshAitiseisResultsFromDB(entity.ΑΙΤΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult XmAitisiResult_Destroy([DataSourceRequest] DataSourceRequest request, XmAitiseisResultsViewModel data)
        {
            if (data != null)
            {
                ΧΜ_ΥΠΟΨΗΦΙΟΣ entity = db.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Find(data.ΑΙΤΗΣΗ_ΚΩΔ);
                if (entity != null)
                {
                    if (CanDeleteXmAitisi(data.ΑΙΤΗΣΗ_ΚΩΔ))
                    {
                        db.Entry(entity).State = EntityState.Deleted;
                        db.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Remove(entity);
                        db.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί ο υποψήφιος διότι υπάρχουν εργασιακές εμπειρίες σε αυτόν.");
                    }
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public XmAitiseisResultsViewModel XmRefreshAitiseisResultsFromDB(int recordId)
        {
            var data = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ
                        where d.ΑΙΤΗΣΗ_ΚΩΔ == recordId
                        select new XmAitiseisResultsViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΕΙΔΙΚΟΤΗΤΑ1 = d.ΕΙΔΙΚΟΤΗΤΑ1 ?? 0,
                            ΜΟΡΙΑ = d.ΜΟΡΙΑ,
                            ΑΠΟΤΕΛΕΣΜΑ = d.ΑΠΟΤΕΛΕΣΜΑ ?? 0,
                            ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                        }).FirstOrDefault();
            return data;
        }

        public List<XmAitiseisResultsViewModel> XmGetAitiseisResultsFromDB()
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            int egykliosId = Common.GetAdminEgykliosID();

            var data = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ
                        join e in db.ΧΜ_sqlEIDIKOTITA_SELECTOR
                        on new { Egk = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ, Iek = d.ΙΕΚ1, Eid = d.ΕΙΔΙΚΟΤΗΤΑ1 }
                        equals new { Egk = (int?)e.EGYKLIOS_ID, Iek = (int?)e.SCHOOL_ID, Eid = (int?)e.EIDIKOTITA_ID }
                        where d.ΙΕΚ1 == schoolId && d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId
                        orderby e.EIDIKOTITA_TERM, d.ΜΟΡΙΑ descending, d.ΑΠΟΤΕΛΕΣΜΑ
                        select new XmAitiseisResultsViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΕΙΔΙΚΟΤΗΤΑ1 = d.ΕΙΔΙΚΟΤΗΤΑ1 ?? 0,
                            ΜΟΡΙΑ = d.ΜΟΡΙΑ,
                            ΑΠΟΤΕΛΕΣΜΑ = d.ΑΠΟΤΕΛΕΣΜΑ ?? 0,
                            ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                        }).ToList();
            return data;
        }

        #endregion


        #region ΚΑΤΑΧΩΡΗΣΗ ΑΜΚ ΕΠΙΤΥΧΟΝΤΩΝ ΚΑΙ ΜΕΤΑΦΟΡΑ ΣΤΟ ΜΗΤΡΩΟ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult XmAitiseisAmk()
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

            if (Common.GetActiveEgykliosID() == 0)
            {
                string msg = "Δεν βρέθηκε ανοικτή Προκήρυξη για προβολή υποψηφίων σπουδαστών.";
                return RedirectToAction("MoriaMain", "School", new { notify = msg });
            }
            PopulateXmIekEidikotites(schoolId);
            return View();
        }

        public ActionResult XmAitiseisAmk_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = XmGetAitiseisAmkFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult XmAitiseisAmk_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<XmAitiseisAmkViewModel> data)
        {
            string errmsg = "";

            if (data != null && ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    ΧΜ_ΥΠΟΨΗΦΙΟΣ entity = db.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Find(item.ΑΙΤΗΣΗ_ΚΩΔ);

                    entity.ΑΜΚ = item.ΑΜΚ;
                    entity.ΕΠΩΝΥΜΟ = item.ΕΠΩΝΥΜΟ;
                    entity.ΟΝΟΜΑ = item.ΟΝΟΜΑ;
                    entity.ΕΙΔΙΚΟΤΗΤΑ = item.ΕΙΔΙΚΟΤΗΤΑ;

                    db.Entry(entity).State = EntityState.Modified;
                    errmsg += ValidateAmk(entity);
                    if (string.IsNullOrEmpty(errmsg))
                    {
                        db.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("", errmsg);
                        return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult XmAitiseisAmk_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<XmAitiseisAmkViewModel> data)
        {
            if (data.Any())
            {
                foreach (var item in data)
                {
                    ΧΜ_ΥΠΟΨΗΦΙΟΣ entity = db.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Find(item.ΑΙΤΗΣΗ_ΚΩΔ);
                    if (entity != null)
                    {
                        if (CanDeleteXmAitisi(item.ΑΙΤΗΣΗ_ΚΩΔ))
                        {
                            db.Entry(entity).State = EntityState.Deleted;
                            db.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Remove(entity);
                            db.SaveChanges();
                        }
                        else ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί ο υποψήφιος διότι υπάρχουν επαγγελματικές εμπειρίες σε αυτόν.");
                    }
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public List<XmAitiseisAmkViewModel> XmGetAitiseisAmkFromDB()
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ
                        where d.ΙΕΚ1 == schoolId && d.ΑΠΟΤΕΛΕΣΜΑ == 1
                        orderby d.ΕΙΔΙΚΟΤΗΤΑ, d.ΕΠΩΝΥΜΟ, d.ΟΝΟΜΑ
                        select new XmAitiseisAmkViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ ?? 0,
                            ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                        }).ToList();

            return data;
        }

        public string ValidateAmk(ΧΜ_ΥΠΟΨΗΦΙΟΣ entity)
        {
            string errMsg = "";

            var data = (from d in db.ΜΑΘΗΤΕΣ where d.ΙΕΚ == entity.ΙΕΚ1 && d.ΑΜΚ == entity.ΑΜΚ select d).Count();
            if (data > 0) errMsg = "Βρέθηκαν αριθμοί μητρώου που είναι ήδη σε χρήση. Η αποθήκευση ακυρώθηκε.";

            return errMsg;
        }


        #region ΕΙΔΙΚΕΣ ΛΕΙΤΟΥΡΓΙΕΣ (ΜΕΤΑΦΟΡΑ ΣΤΟ ΜΗΤΡΩΟ ΣΠΟΥΔΑΣΤΩΝ)

        public ActionResult XmTransferStudents()
        {
            string msg = "Η μεταφορά των επιτυχόντων στο μητρώο σπουδαστών ολοκληρώθηκε επιτυχώς.";
            bool found = false;

            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            int egykliosID = Common.GetActiveEgykliosID();

            var source = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ where d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosID && d.ΙΕΚ1 == schoolId && d.ΑΠΟΤΕΛΕΣΜΑ == 1 select d).ToList();
            foreach (var item in source)
            {
                if (!(item.ΑΜΚ > 0) || (item.ΕΙΔΙΚΟΤΗΤΑ == null || item.ΕΙΔΙΚΟΤΗΤΑ == 0))
                {
                    found = true;
                    break;
                }
            }
            if (found == true)
            {
                msg = "Βρέθηκαν ΑΜΚ κενά ή ειδικότητες ένταξης κενές. Η μεταφορά ακυρώθηκε.";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

            // Insert the new students into table ΜΑΘΗΤΕΣ
            foreach (var item in source)
            {
                var student = (from d in db.ΜΑΘΗΤΕΣ where d.ΙΕΚ == schoolId && d.ΑΜΚ == item.ΑΜΚ && d.ΑΠΟ_ΠΡΟΚΗΡΥΞΗ == egykliosID select d).FirstOrDefault();
                if (student == null)
                {
                    ΜΑΘΗΤΕΣ entity = new ΜΑΘΗΤΕΣ()
                    {
                        ΙΕΚ = (int)item.ΙΕΚ1,
                        ΑΜΚ = (int)item.ΑΜΚ,
                        ΑΔΤ = item.ΑΔΤ,
                        ΑΜΚΑ = item.ΑΜΚΑ,
                        ΕΠΩΝΥΜΟ = item.ΕΠΩΝΥΜΟ,
                        ΟΝΟΜΑ = item.ΟΝΟΜΑ,
                        ΠΑΤΡΩΝΥΜΟ = item.ΠΑΤΡΩΝΥΜΟ,
                        ΜΗΤΡΩΝΥΜΟ = item.ΜΗΤΡΩΝΥΜΟ,
                        ΦΥΛΟ = item.ΦΥΛΟ,
                        ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ = item.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ,
                        ΕΙΔΙΚΟΤΗΤΑ = item.ΕΙΔΙΚΟΤΗΤΑ,
                        ΑΠΟ_ΠΡΟΚΗΡΥΞΗ = egykliosID
                    };
                    db.ΜΑΘΗΤΕΣ.Add(entity);
                    db.SaveChanges();
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoNumberAMK(int initial_amk = 0)
        {
            string msg = "Η αυτόματη αρίθμηση ΑΜΚ ολοκληρώθηκε.";

            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            int egykliosID = Common.GetActiveEgykliosID();

            if (!(initial_amk > 0))
            {
                msg = "Πρέπει να δωθεί μια αρχική τιμή > 0";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

            var source = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ
                          where d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosID && d.ΙΕΚ1 == schoolId && d.ΑΠΟΤΕΛΕΣΜΑ == 1
                          orderby d.ΕΙΔΙΚΟΤΗΤΑ, d.ΕΠΩΝΥΜΟ, d.ΟΝΟΜΑ
                          select d).ToList();
            if (source.Count > 0)
            {
                foreach (var item in source)
                {
                    ΧΜ_ΥΠΟΨΗΦΙΟΣ entity = db.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Find(item.ΑΙΤΗΣΗ_ΚΩΔ);
                    entity.ΑΜΚ = initial_amk;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    initial_amk++;
                }
            }
            else
            {
                msg = "Η λίστα με τους επιτυχόντες είναι κενή. Η αυτόματη αρίθμηση ακυρώθηκε.";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΕΠΕΞΕΡΓΑΣΙΑ ΚΑΙ ΕΓΓΡΑΦΕΣ ΝΕΩΝ ΣΠΟΥΔΑΣΤΩΝ

        public ActionResult XmStudentData(string notify = null)
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
                string msg = "Για επεξεργασία των νέων σπουδαστών πρέπει πρώτα να ορίσετε τις ειδικότητες και τα τμήματα του ΙΕΚ στις Ρυθμίσεις.";
                return RedirectToAction("MoriaMain", "School", new { notify = msg });
            }

            int egykliosID = Common.GetActiveEgykliosID();
            if (egykliosID == 0)
            {
                string msg = "Για επεξεργασία των νέων σπουδαστών πρέπει να είναι ανοικτή η τρέχουσα Προκήρυξη.";
                return RedirectToAction("MoriaMain", "School", new { notify = msg });
            }

            PopulateIekEidikotites(schoolId);
            PopulateIekTmimata(schoolId);
            PopulateRegisterTypes();
            PopulateFoitisi();
            return View();
        }

        public ActionResult XmStudent_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<StudentGridViewModel> data = XmGetStudentsFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<StudentGridViewModel> XmGetStudentsFromDB()
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            int egykliosID = Common.GetActiveEgykliosID();

            List<StudentGridViewModel> results = new List<StudentGridViewModel>();
            var data = (from s in db.ΜΑΘΗΤΕΣ
                        where s.ΙΕΚ == schoolId && s.ΑΠΟ_ΠΡΟΚΗΡΥΞΗ == egykliosID
                        orderby s.ΕΠΩΝΥΜΟ, s.ΟΝΟΜΑ
                        select new StudentGridViewModel
                        {
                            STUDENT_ID = s.STUDENT_ID,
                            ΑΜΚ = s.ΑΜΚ,
                            ΙΕΚ = s.ΙΕΚ,
                            ΕΠΩΝΥΜΟ = s.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = s.ΟΝΟΜΑ,
                            ΕΙΔΙΚΟΤΗΤΑ = s.ΕΙΔΙΚΟΤΗΤΑ ?? 1
                        }).ToList();
            return (data);
        }

        #endregion

        #endregion

        #endregion


    }
}