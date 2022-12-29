using BancoApi.Model;

namespace BancoApi.Interface.Service
{
    public interface IRoleService
    {
        Task<ApplicationRole> CriarRole(string nome);
    }
}