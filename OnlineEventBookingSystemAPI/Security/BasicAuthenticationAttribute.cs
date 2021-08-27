using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Security.Principal;

namespace OnlineEventBookingSystemAPI.Security
{
    public class BasicAuthenticationAttribute:AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var provider = actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(IUserServices)) as IUserServices;
           if(actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, "No username and password supplied");
            }
           else
            {
                string authInfo = actionContext.Request.Headers.Authorization.Parameter;
                string decodeAuthInfo = Encoding.UTF8.GetString(Convert.FromBase64String(authInfo));
                string[] authInfoArray = decodeAuthInfo.Split(':');
                string username = authInfoArray[0];
                string password = authInfoArray[1];
                if(provider != null)
                {
                    if(provider.Authenticate(username,password))
                    {
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid username or password");
                    }
                }
            }
            base.OnAuthorization(actionContext);
        }
    }
}