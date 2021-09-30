using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Http;


namespace OnlineEventBookingSystemAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.Filters.Add(new CustomException.CustomExceptionFilterApi());
            config.MapHttpAttributeRoutes();           
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}/{locationId}",
                defaults: new { id = RouteParameter.Optional , locationId = RouteParameter.Optional }
            );


        }
    }
}
