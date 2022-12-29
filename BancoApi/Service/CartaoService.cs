using BancoApi.Interface.Repository;
using BancoApi.Interface.Service;
using BancoApi.Model;
using System.Data.Entity.Core;

namespace BancoApi.Service {
    public class CartaoService : ICartaoService {
        private readonly ICartaoRepository _cartaoRepository;
        private readonly IContaService _contaService;
        private readonly IAuthService _authService;

        public CartaoService(ICartaoRepository cartaoRepository, IContaService contaService, IAuthService authService) {
            _cartaoRepository = cartaoRepository;
            _contaService = contaService;
            _authService = authService;
        }

        public async Task<Cartao> GetCartao(int cartaoId) {
            Cartao? conta = await _cartaoRepository.GetCartaoById(cartaoId);
            if (conta == null) throw new ObjectNotFoundException("Não encontrou");

            return conta;
        }

        public async Task<Cartao> ParcelarNoCredito(int valor, int mes) {
            var currentUser = await _authService.GetCurrentUser();
            var currentConta = await _contaService.GetConta((int)currentUser.ContaId);
            var currentCartao = await GetCartao((int)currentConta.CartaoId);

            if (valor > currentCartao.Limite - currentCartao.FaturaTotal) throw new ArgumentException("Não tem limite.");
            if (valor <= 0) throw new ArgumentException("Digite um valor válido.");
            if (mes <= 0 && mes > 12) throw new ArgumentException("Digite um mês válido");

            currentCartao.FaturaDoMes += valor / mes;
            currentCartao.FaturaTotal += valor;
            await _cartaoRepository.UpdateCartao(currentCartao);
            return currentCartao;
        }

        public async Task<Cartao> PagarFatura(decimal valor) {
            var currentUser = await _authService.GetCurrentUser();
            var currentConta = await _contaService.GetConta((int)currentUser.ContaId);
            var currentCartao = await GetCartao((int)currentConta.CartaoId);

            if (valor <= 0) throw new ArgumentException("Digite um valor válido.");
            if (valor > currentConta.Saldo) throw new ArgumentException("Não possui esse dinheiro em saldo.");

            currentCartao.FaturaDoMes -= valor;
            currentCartao.FaturaTotal -= valor;
            currentConta.Saldo -= valor;

            await _contaService.UpdateConta(currentConta);
            await _cartaoRepository.UpdateCartao(currentCartao);

            return currentCartao;
        }

        public async Task<Cartao> DataDeVencimento(int dia) {
            var currentUser = await _authService.GetCurrentUser();
            var currentConta = await _contaService.GetConta((int)currentUser.ContaId);
            var currentCartao = await GetCartao((int)currentConta.CartaoId);

            if (dia <= 0 && dia > 31) throw new ArgumentException("Digite uma data válida.");
            currentCartao.Ativo = true;

            return currentCartao;
        }
    }
}
