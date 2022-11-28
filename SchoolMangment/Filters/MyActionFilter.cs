using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolMangment.Utils;
using SqlSugar;
using System.Reflection;

namespace SchoolMangment.Filters
{
    public class MyActionFilter : IAsyncActionFilter
    {
        private readonly ICache cache;

        public MyActionFilter(ICache cache)
        {
            this.cache = cache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionDescribe = context.ActionDescriptor as ControllerActionDescriptor;
            RequestLimitAttribute attribute = null;
            if (actionDescribe != null)
            {
              attribute = actionDescribe.MethodInfo.GetCustomAttribute<RequestLimitAttribute>();
            }
            if (attribute!=null)
            {  
                var remoteip = context.HttpContext.Connection.RemoteIpAddress;//获取请求的ip
                var cacheKey = $"lastrequest_{remoteip}";
                var keyvalue = cache.GetCache<string>(cacheKey);
                if (string.IsNullOrEmpty(keyvalue))
                {
                    cache.SetCache<string>(cacheKey, Guid.NewGuid().ToString(), DateTime.Now.AddMinutes(1));
                    await  next();
                }
                else
                {
                    context.Result = new ContentResult()
                    {
                        Content = "请求繁忙，请稍后再试",
                        StatusCode = 429
                    };
                }
            }
            else
            {
               await next();
            }
        }
    }
}