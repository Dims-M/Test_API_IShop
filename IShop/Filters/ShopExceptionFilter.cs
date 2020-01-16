using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace IShop.Filters
{
    public class ShopExceptionFilter : Attribute, IExceptionFilter
    {
        public bool AllowMultiple
        {
            get { return false; }
        }

        public async Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, 
            CancellationToken cancellationToken)
        {
            if (actionExecutedContext.Exception != null &&
actionExecutedContext.Exception is IndexOutOfRangeException)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                System.Net.HttpStatusCode.BadRequest, "Unknown exception");
            }
        }
    }
}