using EcommerceAspNet.Exception.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EcommerceAspNet.Api.Filters
{
    public class FilterException : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is BaseException baseException)
            {
                context.HttpContext.Response.StatusCode = (int)baseException.GetStatusCode();
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(baseException.GetErrorMessage()));
            }
        }
    }
}
