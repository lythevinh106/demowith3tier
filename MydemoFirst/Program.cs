using DTOShared.Enums;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using MydemoFirst.Authorization.HandlerAuthorizations;
using MydemoFirst.Authorization.requirements;
using MydemoFirst.Authorization.Requirements;
using MydemoFirst.Congigruations;
using MydemoFirst.DataAccess.CacheService;
using MydemoFirst.DataAccess.Infrastructure;
using MydemoFirst.DataAccess.Repositories;
using MydemoFirst.Middleware.MiddlewareExrensions;
using MydemoFirst.Services.Application;
using MydemoFirst.Services.Blob;
using MydemoFirst.Services.Contracts;
using MydemoFirst.Services.ServiceBus;
using MydemoFirst.Signal;
using MydemoFirst.Signal.DemoAppChat;
using MydemoFirst.Signal.FilterHub;
using MydemoFirst.Signal.Stream;
using Serilog;

namespace MydemoFirst
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.ConfigueController();

            //cache response s
            builder.Services.AddResponseCaching();


            builder.Services.AddMemoryCache();
            //implement context

            builder.Services.AddDbContext<MydemoFirst.Models.MyDemoFirstWith3TierContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });

            //cors
            builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
             policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            //implement Di

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<ICacheService, CacheService>();

            //implement automapper

            // builder.Services.AddAutoMapper(typeof(MydemoFirst.Services.Mapping.MappingProfile).Assembly);
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //  builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //builder.Services.AddSingleton<Configuration>();

            builder.Services.AddScoped<MailKit.Net.Smtp.SmtpClient>();

            builder.Services.AddScoped<IMail, Services.Email.MailService>();

            //implement Identity
            builder.Services.ConfigureIdentity();

            //implement Authentication
            builder.Services.ConfigureAuthentication(builder.Configuration);

            builder.Services.AddScoped<IAccountRepository, AccountResponsitory>();
            builder.Services.AddSingleton<IUserIdProvider, EmailBaseUserIdProvider>();

            //implement  Serilog
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
            builder.Host.UseSerilog();

            //implement MediatR

            //builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
            builder.Services.AddMediatR(cfg =>

            {
                cfg.RegisterServicesFromAssemblyContaining<MydemoFirst.Services.Application.BaseHandler>();

                cfg.AddOpenBehavior(typeof(CacheBehavior<,>));
                cfg.AddOpenBehavior(typeof(UpdateCacheBehavior<,>));
            }



            );
            // builder.Services.AddSingleton(typeof(IPipelineBehavior<,>, typeof(CacheBehavior<,>))

            //implement HangFire
            builder.Services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

            /// implement hangFire server
            builder.Services.AddHangfireServer();

            //AzureService
            ////add azure service bus :
            builder.Services.AddAzureClients((clientBuilder) =>
            {
                var conectionString = builder.Configuration.GetSection("Azure")["AzureNameSpace"];

                clientBuilder.AddServiceBusClient(conectionString).WithName("NameSpace");

                //var conectionString2 = "Endpoint=sb://demotopicsubnamespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=EBioRTP0KmzU8ACz13l7W46zAgmAz+cWf+ASbKrspYA=";

                //clientBuilder.AddServiceBusClient(conectionString2).WithName("client2");

                var conectionStringBlob = builder.Configuration.GetSection("Azure")["AzureBlob"];

                clientBuilder.AddBlobServiceClient(conectionStringBlob).WithName("blob1");
            });

            builder.Services.AddScoped<IBlob, BLobService>();
            builder.Services.AddScoped(typeof(IServiceBus<>), typeof(ServiceBus<>));

            //implement Policy

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy($"IsFemale", policy =>
                {
                    policy.Requirements.Add(new SexRequirement(Sex.Female));
                });

                options.AddPolicy($"IsAdmin", policy =>
                {
                    policy.Requirements.Add(new RoleRequirement("Admin"));
                });

                options.AddPolicy($"IsCustomer", policy =>
                {
                    policy.Requirements.Add(new RoleRequirement("Customer"));
                });



                //CRUD policy

                foreach (var m in CRUDTableTypes.AllTypes)
                {
                    options.AddPolicy($"Create{m}Policy",
                   policy =>
                   {
                       policy.Requirements.Add(new CRUDRequirement(m, AppRoleClaim
                       .Create.ToString()

                    ));
                   });
                    options.AddPolicy($"Read{m}Policy",
                       policy => policy.Requirements.Add(new CRUDRequirement(m, AppRoleClaim
                   .Read.ToString())));

                    options.AddPolicy($"Update{m}Policy",
                      policy => policy.Requirements.Add(new CRUDRequirement(m, AppRoleClaim
                   .Update.ToString())));

                    options.AddPolicy($"Delete{m}Policy",
                     policy => policy.Requirements.Add(new CRUDRequirement(m, AppRoleClaim
                   .Delete.ToString()))

                     );
                }
            });

            builder.Services.AddSingleton<IAuthorizationHandler, SexHandleAuthorization>();

            builder.Services.AddScoped<IAuthorizationHandler, CRUDHandleAuthorization>();

            builder.Services.AddScoped<IAuthorizationHandler, RoleHandleAuthorization>();





            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            ///add Razor Page
            builder.Services.AddRazorPages();

            //Realtime

            builder.Services.AddSignalR(options =>
            {
                options.AddFilter<MainHubFilter>();
            });




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseRouting();


            app.UseCors();
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();


            app.UseResponseCaching();

            //implement filters cacche global = filter trong configs
            //app.Use(async (context, next) =>
            //{
            //    context.Response.GetTypedHeaders().CacheControl =
            //        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
            //        {
            //            Public = true,
            //            MaxAge = TimeSpan.FromSeconds(10)


            //        };
            //    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
            //        new string[] { "Accept-Encoding" };

            //    await next();
            //});





            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                 name: "SignalR",
                 pattern: "SignalR/{action=Demo1}",
                 defaults: new { controller = "SignalR" });


                endpoints.MapControllerRoute(
                name: "AppChat",
                pattern: "AppChat/{action=Demo1}",
                defaults: new { controller = "AppChat" });
            });


            //implement Middlewares
            app.UseHandleExceptionsMiddleware();

            app.MapControllers();

            app.MapRazorPages();

            ///hangfire
            app.UseHangfireDashboard();
            app.MapHangfireDashboard("/hangfire");

            //realtime
            app.MapHub<UserHub>("/hub/userHub");
            app.MapHub<MeatHub>("/hub/meatHub");
            app.MapHub<GroupSample>("/hub/groupSample");
            app.MapHub<AppChat>("/hub/AppChat");
            app.MapHub<StreamingHub>("/hub/streamingHub");

            app.Run();
        }
    }
}