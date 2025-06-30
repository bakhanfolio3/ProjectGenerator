using ProjectName.Api;
using ProjectName.Api.Common;
using ProjectName.Api.Configurations;
//using ProjectName.Api.Endpoints;
using ProjectName.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

public class Program
{
    private static async Task Main(string[] args)
    {
        var app = CreateHostBuilder(args).Build();
        app.Configure();
        await app.RunAsync();
    }

    private static WebApplicationBuilder CreateHostBuilder(string[] args) => 
        WebApplication.CreateBuilder(args)
            
            .RegisterServices();
}