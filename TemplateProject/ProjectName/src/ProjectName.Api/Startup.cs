using Asp.Versioning;
using ProjectName.Api.Common;
using ProjectName.Api.Configurations;
using ProjectName.Infrastructure;
using ProjectName.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;


namespace ProjectName.Api;

public static class Startup
{
    public static IConfigurationRoot? Configuration { get; private set; }

    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        var configuration = BuildConfiguration(builder.Environment);

        // Controllers
        builder.Services.AddControllers();
        builder.AddValidationSetup();
        //Cache
        //builder.Services.AddMemoryCache();
        builder.Services.AddDistributedMemoryCache();

        //builder.Services.AddAuthorization();

        // Swagger
        builder.Services.AddSwaggerSetup();

        // Persistence
        builder.Services.AddPersistenceSetup(builder.Configuration);

        
        //Cache
        //builder.Services.AddMemoryCache();
        // Application layer setup
        builder.Services.AddApplicationSetup();

        builder.Services.AddSharedInfrastructure(configuration);
        builder.Services.AddAuthenticationSetting(configuration);

        builder.Services.AddRepositories();
        //builder.Services.Scan(scan => scan
        //        .FromAssemblies(
        //            Application.AssemblyReference.Assembly,
        //            Infrastructure.AssemblyReference.Assembly)
        //        .AddClasses(false)
        //        .AsImplementedInterfaces()
        //        .WithScopedLifetime());

        // Add identity stuff
        //builder.Services        
        //    .AddEntityFrameworkStores<Infrastructure.DbContext.WriteDbContext>();
            //.AddIdentityApiEndpoints<ApplicationUser>()

        // Request response compression
        builder.Services.AddCompressionSetup();

        // HttpContextAcessor
        builder.Services.AddHttpContextAccessor();

        // Mediator
        builder.Services.AddMediatRSetup();

        // Exception handler
        builder.Services.AddExceptionHandler<ExceptionHandler>();

        //Api versioning
        //builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        builder.Logging.ClearProviders();

        // Add serilog
        if (builder.Environment.EnvironmentName != "Testing")
        {
            builder.Host.UseLoggingSetup(builder.Configuration);

            // Add opentelemetry
            builder.AddOpenTemeletrySetup();
        }


        //builder.Services.AddDbContext()



        return builder;
    }

    private static IConfiguration BuildConfiguration(IHostEnvironment env)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("./appsettings.json", optional: false, reloadOnChange: true)
            //.AddJsonFile("./Configuration/appsettings.json", optional: false, reloadOnChange: true)
            //.AddJsonFile("./Configuration/appsettings.other.json", optional: false, reloadOnChange: true)
            //.AddJsonFile($"./Configuration/appsettings.{env.EnvironmentName}.json", optional: true)
            //.AddJsonFile($"./Configuration/appsettings.other.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

        Configuration = configurationBuilder.Build();
        return Configuration;
    }

    public static WebApplication Configure(this WebApplication app)
    {
        app.UseResponseCompression();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseSwaggerSetup();
        //app.UseHsts();
        if (!app.Environment.IsDevelopment())
        {
            //app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseResponseCompression();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        //app.MapHeroEndpoints();
        //app.MapGroup("api/identity")
        //    .WithTags("Identity")
        //    .MapIdentityApi<ApplicationUser>();

        return app;
    }
}
