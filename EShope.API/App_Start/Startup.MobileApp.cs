using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using EShope.API.DataObjects;
using EShope.API.Models;
using Owin;
using System.Linq;
using Microsoft.Azure.Mobile.Server.Tables.Config;
using System.Data.Entity.Migrations;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;
using EShope.API.Repository.Base;
using Unity.Lifetime;
using EShope.API.Migrations;

namespace EShope.API
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            //var container = new UnityContainer();
            //container.RegisterType<DbContext, EShopeMobileServiceContext>(new HierarchicalLifetimeManager());
            //container.RegisterType<IRepository<Order>, EFRepository<Order>>(new HierarchicalLifetimeManager());

            HttpConfiguration config = new HttpConfiguration();
            //config.DependencyResolver = new UnityResolver(container);
            //config.Formatters.JsonFormatter.SupportedMediaTypes
            //config.EnableSystemDiagnosticsTracing();

            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional });

            //config.Routes.MapHttpRoute(
            //name: "ActionApi",
            //routeTemplate: "api/{controller}/{action}/{id}",
            //defaults: new { id = RouteParameter.Optional });

            //config.Services
            //app.Use
            // Swagger
            //SwaggerConfig.Register(config);

            var mobileAppConfiguration = new MobileAppConfiguration();

            mobileAppConfiguration.UseDefaultConfiguration();
                //.AddMobileAppHomeController()             // from the Home package
                //.MapApiControllers()
                //.AddTables(                               // from the Tables package
                //    new MobileAppTableConfiguration()
                //        .MapTableControllers()
                //        .AddEntityFramework()             // from the Entity package
                //    )
                ////.AddPushNotifications()                   // from the Notifications package
                //.MapLegacyCrossDomainController()         // from the CrossDomain package

            //.AddTablesWithEntityFramework()
            //mobileAppConfiguration.MapApiControllers();
            mobileAppConfiguration.ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            //Database.SetInitializer(new MobileServiceInitializer());

            //var migrator = new DbMigrator(new Migrations.Configuration());
            //migrator.Update();

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    // This middleware is intended to be used locally for debugging. By default, HostName will
                    // only have a value when running in an App Service application.
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }

            app.UseWebApi(config);
        }
    }

    public class MobileServiceInitializer : CreateDatabaseIfNotExists<EShopeMobileServiceContext>
    {
        protected override void Seed(EShopeMobileServiceContext context)
        {
            EShopeSeedData.AddToContext(context);
            base.Seed(context);
        }
    }

    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException ex)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException ex)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            container.Dispose();
        }
    }
}

