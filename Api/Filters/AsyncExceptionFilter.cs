using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Application;
using System.Net;

namespace API
{
    public class AsyncExceptionFilter : IAsyncExceptionFilter
    {
        private readonly INotificationContext _notificationContext;
        public AsyncExceptionFilter(INotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {
            Exception e = context.Exception;
            while (e.InnerException != null) e = e.InnerException;
            
            _notificationContext.AddNotification("erro", e.Message);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.HttpContext.Response.ContentType = "application/json";
            var notifications = JsonConvert.SerializeObject(_notificationContext.Notifications);
            var output = JsonConvert.DeserializeObject(notifications);
            context.Result = new JsonResult(output);

            return Task.CompletedTask;
        }
    }
}
