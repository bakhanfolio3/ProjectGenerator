using ProjectName.Application.Common;
using EntityFramework.Exceptions.MySQL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using Respawn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Testcontainers.PostgreSql;
using EntityFramework.Exceptions.MySQL.Pomelo;
using ProjectName.Domain;
using ProjectName.Application.Abstraction.Contexts;
using ProjectName.Infrastructure.DbContext;

namespace ProjectName.Api.IntegrationTests.Common;

public class CustomWebApplicationFactory : WebApplicationFactory<IAssemblyMarker>, IAsyncLifetime
{
    
    // Db connection
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithUsername("app_user")
        .WithPassword("myHardCoreTestDb123")
        .WithDatabase("HeroDb")
        .WithName($"integration-tests-{Guid.NewGuid()}")
        .Build();
    private string _connString = default!;
    private MySqlConnection _dbConnection = default!;
    private Respawner _respawner = default!;
    
    public HttpClient Client { get; private set; } = default!;
    
    public CustomWebApplicationFactory()
    {
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            var serviceTypes = new List<Type>
            {
                typeof(DbContextOptions<WriteDbContext>),
            };
            var contextsDescriptor = services.Where(d => serviceTypes.Contains(d.ServiceType)).ToList();
            foreach (var descriptor in contextsDescriptor)
                services.Remove(descriptor);

            services.AddSingleton(_ => new DbContextOptionsBuilder<WriteDbContext>()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .UseExceptionProcessor()
                .UseMySql(_connString, ServerVersion.AutoDetect(_connString))
                .Options);
        }).ConfigureLogging(o => o.AddFilter(loglevel => loglevel >= LogLevel.Error));
        base.ConfigureWebHost(builder);
    }
    
    public IApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<WriteDbContext>()
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .UseExceptionProcessor()
            .UseMySql(_connString, ServerVersion.AutoDetect(_connString))
            .Options;
        return new WriteDbContext(options);
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        _connString = _dbContainer.GetConnectionString();
        _dbConnection = new MySqlConnection(_connString);
        Client = CreateClient();
        await using var context = CreateContext();
        await SetupRespawnerAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }

    private async Task SetupRespawnerAsync()
    {
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
        });
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _dbConnection.DisposeAsync();
        await _dbContainer.DisposeAsync();
    }
}