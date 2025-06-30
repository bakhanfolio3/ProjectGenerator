using ProjectName.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectName.Api.Configurations;

public static class MediatRSetup
{
    public static IServiceCollection AddMediatRSetup(this IServiceCollection services)
    {
        //services.AddMediatR(cf => cf.RegisterServicesFromAssembly(ProjectName.Application.AssemblyReference.Assembly));

        services.AddMediatR((config) =>
        {
            config.RegisterServicesFromAssemblyContaining(typeof(ProjectName.Application.AssemblyReference));
            config.AddOpenBehavior(typeof(ValidationResultPipelineBehavior<,>));
        });
        


        return services;
    }
}