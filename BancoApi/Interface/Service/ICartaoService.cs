using BancoApi.Model;
using BancoApi.Model.Dto;
using System.Security.Cryptography;

namespace BancoApi.Interface.Service {
    public interface ICartaoService {
        Task<Cartao> ParcelarNoCredito(int valor, int mes);
        Task<Cartao> PagarFatura(decimal valor);
        Task<Cartao> DataDeVencimento(int dia);

    }
}
