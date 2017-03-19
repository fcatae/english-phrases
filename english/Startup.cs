using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using english.Models;
using english.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace english
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EnglishContext>(op => op.UseSqlite("Data Source=database.db"));
            services.AddTransient<IAnkiServices, AnkiServices>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IAnkiServices anki, EnglishContext edb)
        {
            Tests(edb);
            Tests(anki);

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseMvc();
        }

        public void Tests(EnglishContext edb)
        {
            edb.Database.EnsureCreated();

            var count = edb.Phrases.Count();

            if(count == 0)
            {
                edb.Phrases.Add(new Phrases() { Text = "Hello world" });
                edb.SaveChanges();
            }
        }

        public void Tests(IAnkiServices anki)
        {
            anki.StartSession("test",false);


        }
    }
}
