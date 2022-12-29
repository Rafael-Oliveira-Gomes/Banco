using BancoApi.Interface.Service;
using BancoApi.Interface.Repository;
using BancoApi.Repository;
using BancoApi.Service;


namespace BancoApi.Config.Ioc;

public static class RepositoryIoc {
    public static void ConfigRepositoryIoc(this IServiceCollection services) {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IContaRepository, ContaRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ICartaoRepository, CartaoRepository>();
    }
}
