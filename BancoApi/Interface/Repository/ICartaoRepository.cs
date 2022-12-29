using BancoApi.Model;

namespace BancoApi.Interface.Repository
{
    public interface ICartaoRepository
    {
        Task<Cartao> CreateAsync(Cartao Cartao);
        Task<bool> DeleteCartaoAsync(int Id);
        Task<Cartao> GetCartaoByContaId(int contaId);
        Task<Cartao> GetCartaoById(int Id);
        Task<List<Cartao>> ListCartao();
        Task<int> UpdateCartao(Cartao user);
    }
}