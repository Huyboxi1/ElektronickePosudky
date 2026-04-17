using ElektronickePosudky.Application.Behaviors;
using ElektronickePosudky.Application.Mappings;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ElektronickePosudky.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);

            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}