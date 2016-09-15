using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SamMusicStoreWebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Data.Entity.Database.SetInitializer(new SamMusicStoreWebSite.Models.SampleData());
           // System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SamMusicStoreWebSite.Models.SamMusicStoreEntities>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SamMusicStoreEntities>());
        }
    }
}
