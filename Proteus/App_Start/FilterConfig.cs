using System.Web;
using System.Web.Mvc;
using Proteus.Filters;

namespace Proteus
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // Custom error handler
            filters.Add(new ErrorHandlerFilter());
        }
    }
}
