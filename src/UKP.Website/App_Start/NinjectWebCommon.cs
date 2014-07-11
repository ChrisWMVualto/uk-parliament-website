using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;


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
        }        
    }
}

