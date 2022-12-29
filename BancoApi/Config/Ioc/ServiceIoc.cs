using BancoApi.Interface.Service;
using BancoApi.Service;


namespace BancoApi.Config.Ioc;

public static class ServiceIoc
{
    public static void ConfigServiceIoc(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IContaService, ContaService>();
        services.AddScoped<ICartaoService, CartaoService>();
    }
}
