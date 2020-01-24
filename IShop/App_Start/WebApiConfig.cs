using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing.Constraints;
using IShop.Filters;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace IShop
{
    public static class WebApiConfig
    {
        /// <summary>
        /// Файл конфигурации приложения
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            //Для работы с форматом Json. Нужно добавть 2 JsonFormatter
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(
                                                new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(
                                            new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream"));

            // Web API configuration and services
            // Настроить веб-API, чтобы использовать только носителем маркера проверки подлинности.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes Маршрутизация шаблон
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "SearchRoute",
                routeTemplate: "api/{controller}/{action}",
                defaults: new {},
                constraints: new
                {
                    action = new AlphaRouteConstraint()
                });

        }
    }
}
