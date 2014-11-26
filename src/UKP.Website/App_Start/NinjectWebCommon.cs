using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Infrastructure;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Mvc.FilterBindingSyntax;
using RestSharp.Extensions;
using UKP.Website.Extensions;
using UKP.Website.Service;
using UKP.Website.Application;


[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UKP.Web.App_Start.NinjectWebCommon), "PreStart")]
[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(UKP.Web.App_Start.NinjectWebCommon), "PostStart")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(UKP.Web.App_Start.NinjectWebCommon), "Stop")]

namespace UKP.Web.App_Start
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void PreStart() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }


        public static void PostStart()
        {
        }

        
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IConfiguration>().To<Website.Application.Configuration>();
            kernel.Bind<IRestClientWrapper>().To<RestClientWrapper>();
            kernel.Bind<IRestSharpLogger>().To<RestSharpLogger>();

            kernel.Bind<IVideoService>().To<VideoService>();
            kernel.Bind<IEventService>().To<EventService>();
            kernel.Bind<IRecessService>().To<RecessService>();
            kernel.Bind<ISearchService>().To<SearchService>();
            kernel.Bind<IMembersService>().To<MembersService>();

            kernel.BindFilter<BrowserFilterAttribute>(FilterScope.Global, 0);
        }        
    }
}

