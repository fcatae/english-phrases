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
using Swashbuckle.AspNetCore.Swagger;

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
            
            services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "EnglishPhrases", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IAnkiServices anki, EnglishContext db)
        {
            SetupDatabase(db);

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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnglishPhrases v1");
            });

            app.UseMvc();
        }

        public static void SetupDatabase(EnglishContext db)
        {
            db.Database.EnsureCreated();

            if (db.Users.Count() == 0)
            {
                db.Users.Add(new Users() { Name = "user01" });
            }

            var q = new[] { 1,2,3};

            var qaList = new[] {
                new { Q = "Hello World", A = "Olá Mundo" },
                new { Q = "How old are you?", A = "Quantos anos você tem?" },
                new { Q = "What is your name?", A = "Qual é o seu nome?" } };

            if( db.Phrases.Count() == 0 )
            {
                foreach(var qa in qaList)
                {
                    var phrase = new Phrases() { Text = qa.Q };
                    var translation = new Translations() { Text = qa.A, Phrase = phrase };

                    db.Translations.Add(translation);
                    db.SaveChanges();
                }
                
            }

            db.SaveChanges();
        }
    }
}
