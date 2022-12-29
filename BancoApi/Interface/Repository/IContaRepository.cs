using BancoApi.Model;

namespace BancoApi.Interface.Repository {
    public interface IContaRepository {
        Task<List<Conta>> ListDados();
        Task<Conta> GetContaById(int IdUser);
        Task<Conta> GetContaByUserId(string userId);
        Task<int> UpdateConta(Conta user);
        Task<bool> DeleteContaAsync(int Id);
        Task<Conta> CreateAsync(Conta conta);
    }
}
