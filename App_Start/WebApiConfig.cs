using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BackEnd
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Config the AutoMappper
            //BackEnd.App_Start.MappingConfig.Config();

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // make the json properties first letter lowercase, whatever the
            // .net property's shape is
            var json = config.Formatters.JsonFormatter;
#if !DEBUG
            json.Indent = false;
#else
            json.Indent = true;
#endif
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            json.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;

            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
