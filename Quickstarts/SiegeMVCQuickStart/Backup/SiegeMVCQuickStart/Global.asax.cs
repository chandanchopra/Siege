using System.Web.Mvc;
using System.Web.Routing;
using Siege.ServiceLocation;
using Siege.ServiceLocation.HttpIntegration;
using Siege.ServiceLocation.NinjectAdapter;
using SiegeMVCQuickStart.SampleClasses;

namespace SiegeMVCQuickStart
{
    public class MvcApplication : SiegeHttpApplication
    {
        protected override IServiceLocatorAdapter GetServiceLocatorAdapter()
        {
            return new NinjectAdapter();
        }

        public override void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
        }

        protected override void OnApplicationStarted()
        {
            ServiceLocator
                .WithControllers()
                .WithServices()
                .WithFinders();

            base.OnApplicationStarted();
        }
    }
}