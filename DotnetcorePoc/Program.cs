using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DotnetcorePoc
{
    public class Program
    {
        // Entry point of the dotnet app
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /* CreateDefaultBuilder 
         *  sets up webserver
         *  loads app config info from config sources
         *  configs logging
         *  Detali-link: https://github.com/aspnet/AspNetCore/blob/master/src/DefaultBuilder/src/WebHost.cs
         */
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
