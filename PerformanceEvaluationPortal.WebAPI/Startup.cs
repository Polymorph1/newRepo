using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using PerformanceEvaluationPortal.WebAPI.Notifications;

[assembly: OwinStartup(typeof(PerformanceEvaluationPortal.WebAPI.Startup))]

namespace PerformanceEvaluationPortal.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new SignalRContractResolver();
            var serializer = JsonSerializer.Create(settings);
            GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => serializer);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);            
            app.MapSignalR();

           
        }
    }
}
