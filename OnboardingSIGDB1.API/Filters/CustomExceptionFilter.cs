using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using OnboardingSIGDB1.Core.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.API.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly NotificationContext _notificationContext;

        public CustomExceptionFilter(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public void OnException(ExceptionContext context)
        {
            if (_notificationContext.HasNotifications)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/json";

                var notifications = JsonConvert.SerializeObject(_notificationContext.Notifications);
                context.Result = new JsonResult(notifications);
            }
            else
            {
                var erro = new { Mensagem = context.Exception.GetBaseException().Message };

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/json";

                context.Result = new JsonResult(erro);
            }
        }
    }
}
