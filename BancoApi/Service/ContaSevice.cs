using BancoApi.Interface.Service;
using BancoApi.Interface.Repository;
using BancoApi.Model;
using System.Data.Entity.Core;

namespace BancoApi.Service {

    public class ContaService : IContaService {

        private readonly IContaRepository _contaRepository;
        private readonly IAuthService _authService;

        public ContaService(IContaRepository contaRepository, IAuthService authService) {
            _contaRepository = contaRepository;
            _authService = authService;
        }

        public async Task<Conta> Depositar(int valor) {
            var currentUser = await _authService.GetCurrentUser();
            var currentConta = await GetContaByUserId(currentUser.Id);

            if (valor <= 0) throw new ArgumentException("Digite um valor válido.");

            currentConta.Saldo += valor;
            await _contaRepository.UpdateConta(currentConta);

            return currentConta;
        }

        public async Task<Conta> Transferir(int contaIdATransferir, int valor) {
            var currentUser = await _authService.GetCurrentUser();
            var currentConta = await GetContaByUserId(currentUser.Id);
            var contaAReceber = await GetConta(contaIdATransferir);
            
            if (valor <=0) throw new ArgumentException("Digite um valor válido.");
            if (valor > currentConta.Saldo) throw new ArgumentException("Saldo indisponível.");
            if (contaIdATransferir == currentConta.Id) throw new ArgumentException("Não é possível transferir para a própria conta.");

            currentConta.Saldo -= valor;
            contaAReceber.Saldo += valor;
            await _contaRepository.UpdateConta(contaAReceber);
            await _contaRepository.UpdateConta(currentConta);

            return currentConta;
        }

        public async Task<Conta> Sacar(int valor) {
            var currentUser = await _authService.GetCurrentUser();
            var currentConta = await GetContaByUserId(currentUser.Id);

            if (valor > currentConta.Saldo) throw new ArgumentException("Saldo indisponível.");
            if (valor <= 0) throw new ArgumentException("Digite um valor válido.");

            currentConta.Saldo -= valor;
            await _contaRepository.UpdateConta(currentConta);
            return currentConta;
        }
   
        public async Task<Conta> GetContaByUserId(string userId) {
            Conta? conta = await _contaRepository.GetContaByUserId(userId);
            if (conta == null) throw new ObjectNotFoundException("Não encontrou");

            return conta;
        }

        public async Task<List<Conta>> ListarContas() {
            var contas = await _contaRepository.ListDados();

            return contas;
        }

        public async Task<bool> RemoverConta(int contaId) {

            await _contaRepository.DeleteContaAsync(contaId);
            return true;
        }

        public async Task<int> UpdateConta(Conta conta) {

            ApplicationUser currentUser = await _authService.GetCurrentUser();

            return await _contaRepository.UpdateConta(conta);
        }

    public async Task<Conta> GetConta(int contaId) {
        Conta? conta = await _contaRepository.GetContaById(contaId);
            if (conta == null) throw new ObjectNotFoundException("Não encontrou");

            return conta;
        }
    }
}
