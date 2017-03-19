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
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace english
{
    public class Startup
    {
        IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            _env = env;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAnkiServices, AnkiServices>();

            services.AddMvc();

            // Development
            if (_env.IsDevelopment())
            {
                services.AddDbContext<EnglishContext>(op => op.UseSqlServer("Data Source=.;Database=DBE01;Integrated Security=SSPI"));
            }
            else
            {
                services.AddDbContext<EnglishContext>(op => op.UseSqlite("Data Source=database.db"));
            }

            // Test Environment
            if (_env.IsEnvironment("Test"))
            {
                services.AddTransient<IAnkiServices, Services.Tests.TestAnkiServices>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IAnkiServices anki, EnglishContext edb)
        {
            Tests(edb);
            Tests(anki);

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDefaultFiles();


            // Test Environment
            if(env.IsEnvironment("Test"))
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\tests")),
                        RequestPath = new PathString("")
                });
            }

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

            int questionId = anki.GetRandomQuestion(user: "test");

            string questionText = anki.GetQuestion(questionId);

            string answerText = anki.GetAnswer(questionId);

            anki.RateQuestion(questionId, rating: 100);
        }
    }
}
