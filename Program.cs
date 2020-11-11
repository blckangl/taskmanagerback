using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using taskmanagerback.Models;

namespace taskmanagerback
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateDb();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void CreateDb()
        {
            using(var context = new TaskManagerContext())
            {
                context.Database.EnsureCreated();

                context.SaveChanges();
            }
        }
    }
}
