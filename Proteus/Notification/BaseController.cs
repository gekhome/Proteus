using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proteus.Notification
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Sets the information for the system notification.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="autoHideNotification">Determines whether the notification will stay visible or auto-hide.</param>
        /// <param name="notifyType">The type of notification to display to the user: Success, Error or Warning.</param>
        public void SetNotification(string message, NotificationEnumeration notifyType, bool autoHideNotification = true)
        {
            this.TempData["Notification"] = message;
            this.TempData["NotificationAutoHide"] = (autoHideNotification) ? "true" : "false";

            switch (notifyType)
            {
                case NotificationEnumeration.Success:
                    this.TempData["NotificationCSS"] = "notificationbox nb-success";
                    break;
                case NotificationEnumeration.Error:
                    this.TempData["NotificationCSS"] = "notificationbox nb-error";
                    break;
                case NotificationEnumeration.Warning:
                    this.TempData["NotificationCSS"] = "notificationbox nb-warning";
                    break;
                case NotificationEnumeration.Info:
                    this.TempData["NotificationCSS"] = "notificationbox nb-info";
                    break;
            }
        }

    }
}
