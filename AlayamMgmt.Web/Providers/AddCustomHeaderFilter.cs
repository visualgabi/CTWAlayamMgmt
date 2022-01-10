using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;


namespace AlayamMgmt.Web.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);

            //if (actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            //{
            //    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            //}
            //else
            //{
            //    base.HandleUnauthorizedRequest(actionContext);
            //}
        }
    }

    public class CustomAccessTokenProvider : AuthenticationTokenProvider
    {
        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
            var expired = context.Ticket.Properties.ExpiresUtc < DateTime.UtcNow;
            if (expired)
            {
                //If current token is expired, set a custom response header
                context.Response.Headers.Add("X-AccessTokenExpired", new string[] { "1" });                
            }

            base.Receive(context);
        }
    }

    //public class CustomHeaderAttribute : FilterAttribute, IActionFilter, IExceptionFilter
    //{
    //    private static string HEADER_KEY { get { return "X-CustomHeader"; } }
    //    private static string HEADER_VALUE { get { return "Custom header value"; } }

    //    public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
    //    {
    //        return (new CustomHeaderAction() as System.Web.Http.Filters.IActionFilter).ExecuteActionFilterAsync(actionContext, cancellationToken, continuation);
    //    }

    //    public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
    //    {
    //        return (new CustomHeaderException() as IExceptionFilter).ExecuteExceptionFilterAsync(actionExecutedContext, cancellationToken);
    //    }

    //    private class CustomHeaderAction : ActionFilterAttribute
    //    {
    //        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
    //        {
    //            if (actionExecutedContext.Response != null && actionExecutedContext.Response.Content != null)
    //            {
    //                actionExecutedContext.Response.Content.Headers.Add(HEADER_KEY, HEADER_VALUE);
    //            }
    //        }
    //    }

    //    private class CustomHeaderException : ExceptionFilterAttribute
    //    {
    //        public override void OnException(HttpActionExecutedContext context)
    //        {
    //            if (context.Response == null)
    //            {
    //                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, context.Exception);
    //            }

    //            context.Response.Content.Headers.Add(HEADER_KEY, HEADER_VALUE);
    //        }
    //    }
    //}
}