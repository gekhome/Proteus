using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.DAL.Security;
using Proteus.Models;
using Proteus.Notification;
using Proteus.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Proteus.Controllers.UserControllers
{
    [ErrorHandlerFilter]
    public class USER_STUDENTSController : Controller
    {
        private readonly ProteusDBEntities db;

        public USER_STUDENTSController(ProteusDBEntities entities)
        {
            db = entities;
        }

        #region NEW USER-REGISTER USING TAXISnet

        // GET: RegisterUser <- accepts a random integer as Id of ΑΦΜ
        public ActionResult RegisterUser(int rnd_numberID = 0, string Afm = null)
        {
            ViewBag.loggedUser = "(χωρίς σύνδεση)";
            string userAFM = "";

            if (!String.IsNullOrEmpty(Afm))
                userAFM = Afm;
            else
                userAFM = GetTaxisnetAfm(rnd_numberID);

            if (String.IsNullOrEmpty(userAFM))
            {
                string msg = "Δεν βρέθηκαν στοιχεία εισόδου (ΑΦΜ) από το TAXISnet";
                return RedirectToAction("ErrorUser", "USER_STUDENTS", new { notify = msg });
            }
            UserStudentViewModel model = GetUserStudentFromDB(userAFM);
            if (model == null)
            {
                UserStudentViewModel newmodel = new UserStudentViewModel()
                {
                    USER_AFM = userAFM,
                    USERNAME = userAFM,
                    PASSWORD = "XXXXXXXXXX",
                    CREATEDATE = DateTime.Now
                };
                return View(newmodel);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterUser(UserStudentViewModel UserStudent, int rnd_numberID = 0, string Afm = null)
        {
            string userAFM = "";

            if (!String.IsNullOrEmpty(Afm))
                userAFM = Afm;
            else
                userAFM = GetTaxisnetAfm(rnd_numberID);

            var user = GetUserStudentFromDB(userAFM);
            if (user != null)
            {
                WriteUserCookie(user);
                DeleteTaxisRecord(rnd_numberID);

                return RedirectToAction("Index", "Candidates");
            }

            // User does not exist, so create one
            if (ModelState.IsValid)
            {
                USER_STUDENTS newUserStudent = new USER_STUDENTS()
                {
                    USER_AFM = userAFM,
                    USERNAME = userAFM,
                    PASSWORD = "XXXXXXXXXX",
                    CREATEDATE = DateTime.Now
                };
                db.USER_STUDENTS.Add(newUserStudent);
                db.SaveChanges();

                UserStudentViewModel data = GetUserStudentFromDB(userAFM);
                WriteUserCookie(data);
                DeleteTaxisRecord(rnd_numberID);

                return RedirectToAction("Index", "Candidates");
            }
            UserStudent.USER_AFM = userAFM;
            UserStudent.USERNAME = userAFM;
            UserStudent.PASSWORD = "XXXXXXXXXX";
            return View(UserStudent);
        }

        public void WriteUserCookie(UserStudentViewModel user)
        {
            if (user != null)
            {
                CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                serializeModel.UserId = user.USER_ID;
                serializeModel.Username = user.USERNAME;
                serializeModel.Afm = user.USER_AFM;

                string userData = JsonConvert.SerializeObject(serializeModel);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.USER_AFM, DateTime.Now, DateTime.Now.AddMinutes(Kerberos.TICKET_TIMEOUT_MINUTES), false, userData);
                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);
            }
        }

        public void DeleteTaxisRecord(int rnd_numberID = 0)
        {
            var data = (from d in db.TAXISNET where d.RANDOM_NUMBER == rnd_numberID select d).FirstOrDefault();
            if (data == null)
                return;

            TAXISNET entity = db.TAXISNET.Find(data.TAXISNET_ID);
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.TAXISNET.Remove(entity);
                db.SaveChanges();
            };
        }

        public UserStudentViewModel GetUserStudentFromDB(string Afm)
        {
            var data = (from d in db.USER_STUDENTS
                        where d.USER_AFM == Afm
                        select new UserStudentViewModel
                        {
                            USER_ID = d.USER_ID,
                            USER_AFM = d.USER_AFM,
                            USERNAME = d.USERNAME,
                            PASSWORD = d.PASSWORD,
                            CREATEDATE = d.CREATEDATE
                        }).FirstOrDefault();
            return (data);
        }

        #endregion


        #region REDIRECTION TO EXTERNAL TAXISnet URL AND ERROR PAGES

        /// <summary>
        /// Redirect to TAXISnet Login Application. TODO
        /// </summary>
        /// <returns></returns>
        public ActionResult TaxisNetLogin()
        {
            // TEST: Link to Google maps
            //string address = "Laodikis 31";
            //string Area = "Glyfada";
            //string city = "Athens";
            //string zipCode = "16674";

            //var segment = string.Join(" ", address, Area, city, zipCode);
            //var escapedSegment = Uri.EscapeDataString(segment);

            //var baseFormat = "https://www.google.co.za/maps/search/{0}/";
            //var url = string.Format(baseFormat, escapedSegment);
            //return new RedirectResult(url);

            var data = (from d in db.APP_STATUS select d).FirstOrDefault();
            if (data.STUDENT_ENABLE == false)
                return RedirectToAction("AppClosed", "USER_STUDENTS");

            // ---- This is the actual Url and it works ------------
            string url = "http://auth.oaed.gr/Default.aspx?spiek=true";
            //string url = "http://auth.oaed.gr/OAuth2.aspx?spiek=true";   
            return new RedirectResult(url);
        }

        public ActionResult Error(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        public ActionResult ErrorUser(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        public ActionResult AppClosed(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            ViewData["message"] = "Η εφαρμογή είναι κλειστή για υποψήφιους σπουδαστές διότι δεν υπάρχει τρέχουσα ανοικτή προκήρυξη.";

            return View();
        }

        #endregion


        #region USER-STUDENT SELECTOR FOR TESTING

        public ActionResult UserStudentsList()
        {
            string userTxt = "(χωρίς σύνδεση)";
            ViewBag.loggedUser = userTxt;

            List<sqlUserStudentViewModel> data = GetUserStudentListFromDB();

            return View(data);
        }

        public ActionResult UserStudent_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetUserStudentListFromDB();

            var result = new JsonResult();
            result.Data = data.ToDataSourceResult(request);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        public List<sqlUserStudentViewModel> GetUserStudentListFromDB()
        {
            var users = (from a in db.xmUSER_STUDENT_SELECT
                         orderby a.STUDENT_FULLNAME
                         select new sqlUserStudentViewModel
                         {
                             USER_ID = a.USER_ID,
                             USERNAME = a.USERNAME,
                             PASSWORD = a.PASSWORD,
                             USER_AFM = a.USER_AFM,
                             CREATEDATE = a.CREATEDATE,
                             STUDENT_FULLNAME = a.STUDENT_FULLNAME,
                         }).ToList();

            return users;
        }

        #endregion


        #region GETTERS NEW (08/05/2020)

        // Remember to exclude school_id=999 which is IEK DEMO
        public JsonResult GetSchools()
        {
            var data = (from d in db.SYS_SCHOOLS
                        orderby d.SCHOOL_NAME
                        where d.SCHOOL_ID != 999
                        select new SYS_SCHOOLSViewModel
                        {
                            SCHOOL_ID = d.SCHOOL_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public string GetTaxisnetAfm(int rnd_numberID = 0)
        {
            string safm = "";
            var data = (from d in db.TAXISNET where d.RANDOM_NUMBER == rnd_numberID select d).FirstOrDefault();
            if (data != null)
            {
                safm = data.TAXISNET_AFM;
            }
            return safm;
        }

        public bool AccountExists(string safm)
        {
            var user = db.USER_STUDENTS.Where(u => u.USER_AFM == safm).FirstOrDefault();

            if (user != null) return true;
            else return false;

        }

        public bool GetApplicationStatus()
        {
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();
            bool status = data.STATUS_VALUE ?? false;
            return status;
        }

        public bool isApplicationLocal()
        {
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();
            bool status = data.LOCAL_TEST ?? false;
            return status;
        }


        [AllowAnonymous]
        public ActionResult LogOut([Bind(Include = "ISACTIVE")] USER_STUDENTS userParent)
        {
            var user = db.USER_STUDENTS.Where(u => u.USERNAME == userParent.USERNAME && u.PASSWORD == userParent.PASSWORD).FirstOrDefault();

            FormsAuthentication.SignOut();

            // maybe we need to add that the profile is empty
            // so when the home page is displayed, it shows "No Connection"
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}