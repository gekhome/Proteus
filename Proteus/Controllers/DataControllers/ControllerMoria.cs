using Kendo.Mvc.Extensions;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Proteus.Controllers.DataControllers
{
    public class ControllerMoria : Controller
    {
        public USER_ADMINS loggedAdmin;
        public USER_SCHOOLS loggedSchool;
        public const string UPLOAD_PATH = "~/Uploads/";

        private readonly ProteusDBEntities db;

        public ControllerMoria(ProteusDBEntities entities)
        {
            db = entities;
        }


        #region MORIODOTISI GETTERS

        public JsonResult GetSchools()
        {
            var data = (from d in db.SYS_SCHOOLS
                        orderby d.SCHOOL_NAME
                        where d.SCHOOL_ID != 999
                        select new SYS_SCHOOLSViewModel
                        {
                            SCHOOL_ID = d.SCHOOL_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            SCHOOL_PERIFERIAKI_ID = d.SCHOOL_PERIFERIAKI_ID
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEgykliosList()
        {
            var data = db.ΧΜ_ΕΓΚΥΚΛΙΟΣ.Select(d => new XmEgykliosViewModel
            {
                ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                ΕΓΚΥΚΛΙΟΣ_ΑΠ = d.ΕΓΚΥΚΛΙΟΣ_ΑΠ
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIekList()
        {
            var data = db.SYS_SCHOOLS.Select(d => new SYS_SCHOOLSViewModel
            {
                SCHOOL_ID = d.SCHOOL_ID,
                SCHOOL_NAME = d.SCHOOL_NAME
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XmGetEidikotitesIek()
        {
            var data = (from d in db.SYS_EIDIKOTITES_IEK
                        orderby d.EIDIKOTITA_TEXT
                        where d.APPROVED == true
                        orderby d.EIDIKOTITA_TEXT
                        select new SYS_EIDIKOTITES_IEKViewModel
                        {
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XmGetIekEidikotites()
        {
            var data = db.viewIEK_EIDIKOTITES;

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XmGetResult()
        {
            var data = (from d in db.ΧΜ_ΑΠΟΤΕΛΕΣΜΑ
                        select new XmResultViewModel
                        {
                            ΑΠΟΤΕΛΕΣΜΑ_ΚΩΔ = d.ΑΠΟΤΕΛΕΣΜΑ_ΚΩΔ,
                            ΑΠΟΤΕΛΕΣΜΑ = d.ΑΠΟΤΕΛΕΣΜΑ
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XmGetNationalities()
        {
            var data = (from d in db.ΧΜ_ΕΘΝΙΚΟΤΗΤΑ
                        select new XmNationalityViewModel
                        {
                            NATIONALITY_ID = d.NATIONALITY_ID,
                            NATIONALITY_TEXT = d.NATIONALITY_TEXT
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XmGetEidikotitesIek1(int iek1 = 0)
        {
            int egykliosId = Common.GetAdminEgykliosID();

            var data = (from d in db.ΧΜ_sqlEIDIKOTITA_SELECTOR
                        where d.EGYKLIOS_ID == egykliosId && d.SCHOOL_ID == iek1
                        orderby d.EIDIKOTITA_TERM
                        select new XmEidikotitaSelectorViewModel
                        {
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            EIDIKOTITA_TERM = d.EIDIKOTITA_TERM
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XmGetEidikotitesIek2(int iek2 = 0)
        {
            int egykliosId = Common.GetAdminEgykliosID();

            var data = (from d in db.ΧΜ_sqlEIDIKOTITA_SELECTOR
                        where d.EGYKLIOS_ID == egykliosId && d.SCHOOL_ID == iek2
                        orderby d.EIDIKOTITA_TERM
                        select new XmEidikotitaSelectorViewModel
                        {
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            EIDIKOTITA_TERM = d.EIDIKOTITA_TERM
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XmGetEidikotites()
        {
            var data = (from d in db.ΧΜ_ΕΙΔΙΚΟΤΗΤΕΣ
                        where d.APPROVED == true
                        orderby d.EIDIKOTITA_TEXT
                        select new XmEidikotitesViewModel
                        {
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XmGetFilteredTerms(int iek = 0, int eidikotitaId = 0)
        {
            int egykliosId = Common.GetAdminEgykliosID();

            var data = (from d in db.ΧΜ_sqlEIDIKOTITA_SELECTOR
                        where d.EGYKLIOS_ID == egykliosId && d.SCHOOL_ID == iek && d.EIDIKOTITA_ID == eidikotitaId
                        orderby d.TERM_ID
                        select new XmEidikotitaSelectorViewModel
                        {
                            TERM_ID = d.TERM_ID,
                            TERM = d.TERM,
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XmGetSchools()
        {
            var data = (from d in db.SYS_SCHOOLS
                        where d.SCHOOL_ID != 999
                        orderby d.SCHOOL_NAME
                        select new SYS_SCHOOLSViewModel
                        {
                            SCHOOL_ID = d.SCHOOL_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XmGetGenders()
        {
            var genders = db.SYS_GENDERS.Select(p => new SYS_GENDERSViewModel
            {
                GENDER = p.GENDER,
                GENDER_ID = p.GENDER_ID
            });

            return Json(genders, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XmGetTerms()
        {
            var data = (from d in db.SYS_TERM
                        where d.TERM_ID < 4
                        select new SYS_TERMViewModel
                        {
                            TERM_ID = d.TERM_ID,
                            TERM = d.TERM
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XmGetApolytiria()
        {
            var data = db.ΑΠΟΛΥΤΗΡΙΑ.Select(p => new ApolytiriaViewModel
            {
                ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔΙΚΟΣ = p.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔΙΚΟΣ,
                ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ = p.ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ
            }).OrderBy(d => d.ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region POPULATORS

        public void PopulateAitiseis()
        {
            var data = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ select d).ToList();
            ViewData["aitiseis"] = data;
        }

        public void PopulateXmEidikotites()
        {
            var data = (from d in db.ΧΜ_ΕΙΔΙΚΟΤΗΤΕΣ
                        where d.APPROVED == true
                        orderby d.EIDIKOTITA_TEXT
                        select d).ToList();

            ViewData["eidikotites"] = data;
            ViewData["defaultEidikotita"] = data.First().EIDIKOTITA_ID;
        }

        public void PopulateStatus()
        {
            var data = (from d in db.ΧΜ_ΕΓΚΥΚΛΙΟΣ_ΚΑΘΕΣΤΩΣ select d).ToList();
            ViewData["Status"] = data;
        }

        public void PopulateSchoolYears()
        {
            var schoolyears = (from p in db.SYS_SCHOOLYEARS orderby p.SY_TEXT select p).ToList();
            ViewData["schoolyears"] = schoolyears;
            ViewData["defaultSchoolYear"] = schoolyears.First().SY_ID;
        }

        public void PopulateSchools()
        {
            var data = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID != 999 orderby d.SCHOOL_NAME select d).ToList();
            ViewData["schools"] = data;
            ViewData["defaultSchool"] = data.First().SCHOOL_ID;
        }

        public void PopulateTerms()
        {
            var terms = (from t in db.SYS_TERM select t).ToList();
            ViewData["terms"] = terms;
        }

        public void PopulateIekEidikotites(int schoolId)
        {
            var iedata = (from d in db.viewIEK_EIDIKOTITES where d.IEK_ID == schoolId orderby d.EIDIKOTITA_ID select d).ToList();

            ViewData["eidikotitesIek"] = iedata;
            ViewData["defaultEidikotita"] = iedata.First().EIDIKOTITA_ID;
        }

        public void PopulateIekTmimata(int schoolId)
        {
            var tmimata = (from d in db.ΤΜΗΜΑ where d.ΙΕΚ == schoolId orderby d.ΠΕΡΙΟΔΟΣ_ΚΩΔ, d.ΤΜΗΜΑ_ΟΝΟΜΑ select d).ToList();

            ViewData["tmimata"] = tmimata;
            ViewData["defaultTmima"] = tmimata.First().ΤΜΗΜΑ_ΚΩΔ;
        }

        public void PopulateRegisterTypes()
        {
            var regtypes = (from d in db.ΕΓΓΡΑΦΗ_ΕΙΔΗ select d).ToList();
            ViewData["registerTypes"] = regtypes;
        }

        public void PopulateFoitisi()
        {
            var foitisi = (from d in db.ΦΟΙΤΗΣΗ select d).ToList();
            ViewData["foitisi"] = foitisi;
        }

        public void PopulateEgykliosEidikotites(int schoolId)
        {
            int egykliosId = Common.GetAdminEgykliosID();

            var data = (from d in db.ΧΜ_sqlEIDIKOTITA_SELECTOR
                        where d.EGYKLIOS_ID == egykliosId && d.SCHOOL_ID == schoolId
                        select d).ToList();

            ViewData["eidikotites"] = data;
            ViewData["defaultEidikotita"] = data.First().EIDIKOTITA_ID;
        }

        public void PopulateSchoolAitisis(int schoolId)
        {
            int egykliosId = Common.GetActiveEgykliosID();

            var aitiseis = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ
                            where d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId && d.ΙΕΚ1 == schoolId
                            select d).ToList();

            ViewData["aitiseis"] = aitiseis;
            ViewData["defaultAitisi"] = aitiseis.First().ΑΙΤΗΣΗ_ΚΩΔ;
        }

        public void PopulateXmApotelesma()
        {
            var data = (from d in db.ΧΜ_ΑΠΟΤΕΛΕΣΜΑ select d).ToList();
            ViewData["results"] = data;
            ViewData["defaultResult"] = data.First().ΑΠΟΤΕΛΕΣΜΑ_ΚΩΔ;
        }

        public void PopulateXmIekEidikotites(int schoolId)
        {
            var data = (from d in db.viewIEK_EIDIKOTITES where d.IEK_ID == schoolId orderby d.EIDIKOTITA_TEXT select d).ToList();
            ViewData["IekEidikotites"] = data;
            ViewData["defaultEidikotita"] = data.First().EIDIKOTITA_ID;
        }

        public void PopulateXmTerms()
        {
            var terms = (from t in db.SYS_TERM where t.TERM_ID < 4 select t).ToList();
            ViewData["terms"] = terms;
        }

        #endregion


        #region MISC FUNCTIONS

        public bool AitisεisExist()
        {
            int egykliosId = Common.GetAdminEgykliosID();

            var aitiseis = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ where d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId select d).Count();
            if (aitiseis > 0)
                return true;

            return false;
        }

        public bool AitisisSchoolExist(int schoolId)
        {
            int egykliosId = Common.GetActiveEgykliosID();

            var aitiseis = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ
                            where d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId && d.ΙΕΚ1 == schoolId
                            select d).ToList();
            if (aitiseis.Count > 0)
                return true;
            return false;
        }

        public bool IekEidikotitesExist(int schoolId)
        {
            var eidikotites = (from d in db.IEK_EIDIKOTITES where d.IEK_ID == schoolId select d).Count();

            if (eidikotites > 0) return true;
            else return false;
        }

        public bool IekTmimataExist(int schoolId)
        {
            var tmimata = (from d in db.ΤΜΗΜΑ where d.ΙΕΚ == schoolId select d).Count();

            if (tmimata > 0) return true;
            else return false;
        }

        public USER_SCHOOLS GetLoginSchool()
        {
            loggedSchool = db.USER_SCHOOLS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            int SchoolID = loggedSchool.USER_SCHOOLID ?? 0;
            var _school = (from s in db.sqlUSER_SCHOOL
                           where s.USER_SCHOOLID == SchoolID
                           select new { s.SCHOOL_NAME }).FirstOrDefault();

            ViewBag.loggedUser = _school.SCHOOL_NAME;
            return loggedSchool;
        }

        public USER_ADMINS GetLoginAdmin()
        {
            loggedAdmin = db.USER_ADMINS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            ViewBag.loggedAdmin = loggedAdmin;
            ViewBag.loggedUser = loggedAdmin.FULLNAME;

            return loggedAdmin;
        }

        #endregion
    }
}