using Microsoft.AspNetCore.Http;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.API.Middlewares
{
    public class CommitMiddleware
    {
        private readonly RequestDelegate _next;

        public CommitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next.Invoke(httpContext);

            var verboRequest = httpContext.Request.Method.ToUpper();
            var verbos = new string[] { HttpMethod.Post.Method.ToUpper(), HttpMethod.Put.Method.ToUpper(), HttpMethod.Delete.Method.ToUpper() };

            if (verbos.Contains(verboRequest))
            {
                var notificationContext = (NotificationContext)httpContext.RequestServices.GetService(typeof(NotificationContext));

                if (!notificationContext.HasNotifications)
                {
                    try
                    {
                        var uow = (IUnitOfWork)httpContext.RequestServices.GetService(typeof(IUnitOfWork));
                        await uow.Commit();
                    }
                    catch (Exception ex )
                    {

                    }
                }
            }
        }
    }
}
