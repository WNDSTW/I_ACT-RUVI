using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace I_ACT.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("default","", "~/Pages/Base.aspx");
            routes.MapPageRoute("byMenu", "{Menu}/{IdMenu}", "~/Pages/Base.aspx");
        }
    }
}