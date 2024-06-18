using Kendo.Mvc.Extensions;
using Newtonsoft.Json;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.DAL.Security;
using Proteus.Models;
using Proteus.Filters;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace Proteus.Controllers.UserControllers
{
    [ErrorHandlerFilter]
    public class USER_SCHOOLController : Controller
    {
        private USER_SCHOOLS loggedSchool;
        private readonly ProteusDBEntities db;

        public USER_SCHOOLController(ProteusDBEntities entities)
        {
            db = entities;
        }

        public ActionResult Login()
        {
            bool AppStatusOn = true;
            try
            {
                AppStatusOn = GetApplicationStatus();
                if (AppStatusOn == false)
                {
                    return RedirectToAction("AppStatusOff", "Home");
                }
            }
            catch
            {
                return RedirectToAction("ErrorConnect", "Home");
            }

            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
            }
            else
            {
                loggedSchool = db.USER_SCHOOLS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
                if (loggedSchool != null)
                {
                    ViewBag.loggedUser = GetLoginSchool();
                    return RedirectToAction("Index", "School");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "USERNAME,PASSWORD")]  UserSchoolViewModel model)
        {
            var user = db.USER_SCHOOLS.Where(u => u.USERNAME == model.USERNAME && u.PASSWORD == model.PASSWORD).FirstOrDefault();

            if (GetSchoolEnableStatus() == false)
            {
                string msg = GetSchoolEnableMessage();
                return RedirectToAction("Index", "Home", new { notify = msg });
            }

            if (user != null)
            {
                WriteUserCookie(model);
                SetLoginStatus(user, true);
                LoginRecord(user.USERNAME);

                return RedirectToAction("Index", "School");
            }
            ModelState.AddModelError("", "Το όνομα χρήστη ή/και ο κωδ.πρόσβασης δεν είναι σωστά");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOut([Bind(Include = "ISACTIVE")] USER_SCHOOLS userSchool)
        {
            var user = db.USER_SCHOOLS.Where(u => u.USERNAME == userSchool.USERNAME && u.PASSWORD == userSchool.PASSWORD).FirstOrDefault();

            FormsAuthentication.SignOut();
            SetLoginStatus(user, false);

            return RedirectToAction("Index", "Home");
        }

        public void WriteUserCookie(UserSchoolViewModel user)
        {
            SchoolPrincipalSerializeModel serializeModel = new SchoolPrincipalSerializeModel();
            serializeModel.UserId = user.USER_ID;
            serializeModel.Username = user.USERNAME;
            serializeModel.SchoolId = user.USER_SCHOOLID ?? 0;

            string userData = JsonConvert.SerializeObject(serializeModel);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1, user.USERNAME, DateTime.Now, DateTime.Now.AddMinutes(Kerberos.TICKET_TIMEOUT_MINUTES), false, userData);
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
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

        public void SetLoginStatus(USER_SCHOOLS user, bool value)
        {
            db.Entry(user).State = EntityState.Modified;
            user.ISACTIVE = value;
            db.SaveChanges();
        }

        /// <summary>
        /// Records most current school login timestamp
        /// </summary>
        /// <param name="username"></param>
        public void LoginRecord(string username)
        {
            var loginData = (from d in db.SYS_LOGINS where d.LOGIN_USERNAME == username select d).FirstOrDefault();

            if (loginData == null)
            {
                SYS_LOGINS entity = new SYS_LOGINS()
                {
                    LOGIN_USERNAME = username,
                    LOGIN_DATETIME = DateTime.Now
                };
                db.SYS_LOGINS.Add(entity);
                db.SaveChanges();
            }
            else
            {
                SYS_LOGINS entity = db.SYS_LOGINS.Find(loginData.LOGIN_ID);
                entity.LOGIN_DATETIME = DateTime.Now;
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public bool GetApplicationStatus()
        {
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();
            bool status = data.STATUS_VALUE ?? false;
            return status;
        }

        public bool GetSchoolEnableStatus()
        {
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();
            bool status = data.SCHOOL_ENABLE ?? false;
            return status;
        }

        public string GetSchoolEnableMessage()
        {
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();
            string msg = data.SCHOOL_MESSAGE;
            return msg;
        }
    }
}
