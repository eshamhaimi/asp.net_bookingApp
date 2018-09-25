using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using System.Web.Http.OData.Builder;
using BookingApp.Models.AppModel;
using System.Web.Http.OData.Extensions;

namespace BookingApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Accommodation>("Accommodations1");
            builder.EntitySet<AccommodationType>("AccommodationTypes");
            builder.EntitySet<AppUser>("AppUsers");
            builder.EntitySet<Comment>("Comments");
            builder.EntitySet<Place>("Places");
            builder.EntitySet<Room>("Rooms");
            builder.EntitySet<Region>("Regions");
            builder.EntitySet<Country>("Countries");
            builder.EntitySet<RoomReservations>("RoomReservations");
            //builder.EntitySet<Product>("Products");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

           // var cors = new EnableCorsAttribute("http://localhost:4200", "*", "*");
           // config.EnableCors(cors);
			
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
			
			config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
