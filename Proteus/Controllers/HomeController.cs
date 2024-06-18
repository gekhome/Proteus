using Proteus.DAL;
using Proteus.Models;
using Proteus.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Proteus.Notification;

namespace Proteus.Controllers
{
    [ErrorHandlerFilter]
    public class HomeController : Controller
    {
        private readonly ProteusDBEntities db;

        public HomeController(ProteusDBEntities entities)
        {
            db = entities;
        }

        [AllowAnonymous]
        public ActionResult Index(string notify = null)
        {
            string userTxt = "(χωρίς σύνδεση)";
            try
            {
                bool AppStatusOn = GetApplicationStatus();
                if (AppStatusOn == false)
                {
                    return RedirectToAction("AppStatusOff", "Home");
                }
            }
            catch
            {
                return RedirectToAction("ErrorConnect", "Home");
            }

            if (notify != null)
                this.ShowMessage(MessageType.Warning, notify);

            if (IsApplicationLocal())
                ViewBag.appTest = true;

            ViewBag.loggedUser = userTxt;
            return View();
        }

        [AllowAnonymous]
        public ActionResult AppStatusOff()
        {
            string message = GetStatusMessage();

            if (string.IsNullOrEmpty(message))
                message = "Η εφαρμογή είναι προσωρινά απενεργοποιημένη για εργασίες συντήρησης και αναβάθμισης.";

            ViewData["message"] = message;
            return View();
        }

        public bool IsApplicationLocal()
        {
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();
            bool status = data.LOCAL_TEST ?? false;
            return status;
        }

        [AllowAnonymous]
        public ActionResult ErrorConnect()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Σύντομη περιγραφή της εφαρμογής.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Στοιχεία επικοινωνίας.";

            return View();
        }

        public string GetStatusMessage()
        {
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();

            return (data.STATUS_MESSAGE);
        }

        public bool GetApplicationStatus()
        {
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();
            bool status = data.STATUS_VALUE ?? false;
            return status;
        }

        [AllowAnonymous]
        public ActionResult PageInProgress()
        {
            return View();
        }


        #region States of Grids

        [ValidateInput(false)]
        public ActionResult Save(string data)
        {
            Session["data"] = data;

            string temp = data;
            //int stopper = 1;

            return new EmptyResult();
        }

        [AllowAnonymous]
        public ActionResult Load()
        {
            string data;

            if (Session["data"] != null)
            {
                data = Session["data"].ToString();
            }

            //int stopper = 1;

            return Json(Session["data"], JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult SaveRow(string data)
        {
            Session["row"] = data;

            //int temp = 1;

            return new EmptyResult();
        }

        [AllowAnonymous]
        public ActionResult LoadRow()
        {
            if (Session["row"] != null)
            {
                string data = Session["row"].ToString();
            }

            //int temp = 1;

            return Json(Session["row"], JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}