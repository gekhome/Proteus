using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Telerik.Reporting.Cache.Interfaces;
using Telerik.Reporting.Services.Engine;
using Telerik.Reporting.Services.WebApi;

namespace Proteus.Controllers
{
    public class ReportsController : ReportsControllerBase
    {
        protected override IReportResolver CreateReportResolver()
        {
            var appPath = HttpContext.Current.Server.MapPath("~/");
            var reportsPath = Path.Combine(appPath, @"..\..\..\Report Designer\Examples");

            return new ReportFileResolver(reportsPath)
                .AddFallbackResolver(new ReportTypeResolver());
        }

        protected override ICache CreateCache()
        {
            return Telerik.Reporting.Services.Engine.CacheFactory.CreateFileCache();
        }
    }
    //using System.IO;
    //using System.Web;
    //using System.Web.Mvc;
    //using Telerik.Reporting.Cache.Interfaces;
    //using Telerik.Reporting.Services.Engine;
    //using Telerik.Reporting.Services.WebApi;


    //public class ReportsController : ReportsControllerBase
    //{
    //    protected override IReportResolver CreateReportResolver()
    //    {
    //        var appPath = HttpContext.Current.Server.MapPath("~/Reports");

    //        return new ReportFileResolver(appPath)
    //            .AddFallbackResolver(new ReportTypeResolver());
    //    }

    //    protected override ICache CreateCache()
    //    {
    //        return Telerik.Reporting.Services.Engine.CacheFactory.CreateFileCache();
    //    }
    //}
}