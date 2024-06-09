using Application.Features.Create;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using Application.Features.DeleteAnime;
using Application.Features.UpdateAnime;
using Application.Features.GetAnime;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssemblyContaining<GetAnimeValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateAnimeValidator>();
        services.AddValidatorsFromAssemblyContaining<DeleteAnimeValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateAnimeValidator>();

        return services;
    }
}
