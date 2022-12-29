using BancoApi.Model;

namespace BancoApi.Interface.Service {
    public interface IContaService 
    {
        Task<bool> RemoverConta(int contaId);
        Task<Conta> GetConta(int contaId);
        Task<List<Conta>> ListarContas();
        Task<int> UpdateConta(Conta conta);
        Task<Conta> Depositar(int valor);
        Task<Conta> Sacar(int valor);
        Task<Conta> Transferir(int contaIdATransferir, int valor);
    }
}
