[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CMD.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CMD.App_Start.NinjectWebCommon), "Stop")]

namespace CMD.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using System.Data.Entity;
    using Reusable;
    using CMDLogic.Logic;
    using Controllers;
    using BusinessSpecificLogic.Logic;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        public static IKernel CreateKernel()
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

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(DbContext)).To(typeof(MainContext)).InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
            kernel.Bind(typeof(BaseLogic<>)).ToSelf().InRequestScope();

            kernel.Bind<IDashboardLogic>().To<DashboardLogic>();
            kernel.Bind<IDepartmentLogic>().To<DepartmentLogic>();
            kernel.Bind<IMetricLogic>().To<MetricLogic>();
            kernel.Bind<IMetricYearLogic>().To<MetricYearLogic>();
            kernel.Bind<IMetricHistoryLogic>().To<MetricHistoryLogic>();
            kernel.Bind<IInitiativeLogic>().To<InitiativeLogic>();

            kernel.Bind<IUserLogic>().To<UserLogic>();
            kernel.Bind(typeof(BaseController<>)).ToSelf().InRequestScope();
        }
    }
}
