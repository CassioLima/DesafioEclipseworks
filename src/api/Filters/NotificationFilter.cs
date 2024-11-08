using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Application;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace API
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly INotificationContext _notificationContext;

        public NotificationFilter(INotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_notificationContext.HasNotifications)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/json";

                var notifications = JsonConvert.SerializeObject(_notificationContext.Notifications);

                var retorno = new
                {
                    success = false,
                    message = JsonConvert.SerializeObject(_notificationContext.Notifications),
                    context = ""
                };

                await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(retorno));

                return;
            }

            await next();
        }
    }
}
