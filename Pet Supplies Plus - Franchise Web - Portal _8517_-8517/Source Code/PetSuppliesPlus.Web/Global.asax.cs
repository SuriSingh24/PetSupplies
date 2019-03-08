using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PetSuppliesPlus
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    Resources.Resource.Culture = new System.Globalization.CultureInfo(PetSuppliesPlus.Framework.SessionHelper.Culture);
        //}

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            if (Context.Items["IsSessionExpired"] is bool)
            {
                //Context.Response.StatusCode = 401;
                //Context.Response.End();
            }
        }
    }
}