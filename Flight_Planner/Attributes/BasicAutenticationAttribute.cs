using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;
using System.Threading; 

namespace Flight_Planner.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.GenericParameter)]
    public class BasicAutenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)   
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                var authToken = actionContext.Request.Headers
                    .Authorization.Parameter;

                // decoding authToken we get decode value in 'Username:Password' format  
                var decodeauthToken = System.Text.Encoding.UTF8.GetString(
                    Convert.FromBase64String(authToken));

                // spliting decodeauthToken using ':'   
                var userNameAndPwd = decodeauthToken.Split(':');

                // at 0th postion of array we get username and at 1st we get password  
                if (userNameAndPwd[0] == "codelex-admin" && userNameAndPwd[1] == "Password123")
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(
                        new GenericIdentity(userNameAndPwd[0]), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                actionContext.Response = actionContext.Request
                 .CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
    }
}
