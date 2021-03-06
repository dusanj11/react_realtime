﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotNetify;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LiveCharttt
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSignalR();
            services.AddDotNetify();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseWebSockets();
            app.UseSignalR(routes => routes.MapDotNetifyHub());
            app.UseDotNetify();

            app.Run(async (context) =>
            {
                using (var reader = new StreamReader(File.OpenRead("wwwroot/index.html")))
                    await context.Response.WriteAsync(reader.ReadToEnd());
            });
        }
    }
}
