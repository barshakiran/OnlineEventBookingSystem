using System.Web.Http;
using Unity;
using Unity.WebApi;
using OnlineEventBookingSystemBL;
using OnlineEventBookingSystemDAL;
using OnlineEventBookingSystemBL.Interface;
using OnlineEventBookingSystemDAL.Infrastructure;
using OnlineEventBookingSystemDAL.Infrastructure.Contract;
using OnlineEventBookingSystemAPI.Security;
namespace OnlineEventBookingSystemAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IUserBusiness, UserBusiness>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IUserServices, UserServices>();
            container.RegisterType<IEventDetailBusiness, EventDetailBusiness>();
            container.RegisterType<IUserDataHandler, UserDataHandler>();
            container.RegisterType<IEventDetailDataHandler,EventDetailDataHandler>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}