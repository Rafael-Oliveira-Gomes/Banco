using BancoApi.Model;

namespace BancoApi.Interface.Repository
{
    public interface IRoleRepository
    {
        Task<ApplicationRole> CreateAsync(ApplicationRole role);
    }
}