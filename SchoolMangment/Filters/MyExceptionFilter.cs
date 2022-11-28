using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SchoolMangment.Filters
{
    public class MyExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;
            context.Result = new ObjectResult(new
            {
                message="系统出现异常"
            });
            
           return Task.CompletedTask;
        }
    }
}
