using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace IShop.Filters
{
    /// <summary>
    ///Класс  реализация фильтров аутифкация 
    ///Проверяет подлинность запроса
    /// </summary>
    public class ShopAuthenticationFilter : Attribute, IAuthenticationFilter 
    {

        public bool AllowMultiple
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// фильтр вызывается до работы метода(актион)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AuthenticateAsync(HttpAuthenticationContext context,
                            CancellationToken cancellationToken)
        {
            //проверка данных которые пришли в хедерах запроса от клиента
            context.Principal = null;
            AuthenticationHeaderValue authentication = context.Request.Headers.Authorization;

            if (authentication != null && authentication.Scheme == "Basic")
            {

                string[] authData = Encoding.ASCII.GetString(Convert.FromBase64String(authentication.Parameter)).Split(':');
                string[] roles = new string[] { "user" };
                string login = authData[0];
                context.Principal = new GenericPrincipal(new GenericIdentity(login), roles);
            }
            //если нет нужных(правельных) данных
            if (context.Principal == null)
            {
                context.ErrorResult
                = new UnauthorizedResult(new AuthenticationHeaderValue[] {
                    new AuthenticationHeaderValue("Basic") }, context.Request);
            }
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// фильтр вызывается после работы метода(астион)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context,
                                    CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }
    }
}