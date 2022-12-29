using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BancoApi.Interface.Repository;
using BancoApi.Interface.Service;
using BancoApi.Model;

namespace BancoApi.Controllers {

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly ILogger<ContaController> _logger;
        private readonly IContaService _contaService;
        public ContaController(ILogger<ContaController> logger, IContaService contaService) {
            _logger = logger;
            _contaService = contaService;
        }

        [HttpPost(template: "Depositar")]
        public async Task<IActionResult> Depositar([FromBody] int valor) {
            try {
                var result = await _contaService.Depositar(valor);
                return Ok(result);
            } catch (Exception ex) {
                _logger.LogError(1, $"Erro: {nameof(ContaController)} - {ex.Message}" +
                    $"\n {ex.InnerException}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(template: "Sacar")]
        public async Task<IActionResult> Sacar([FromBody] int valor) {
            try {
                var result = await _contaService.Sacar(valor);
                return Ok(result);
            } catch (Exception ex) {
                _logger.LogError(1, $"Erro: {nameof(ContaController)} - {ex.Message}" +
                    $"\n {ex.InnerException}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(template: "Transferir")]
        public async Task<IActionResult> Transferir(int contaIdATransferir, int valor) {
            try {
                var result = await _contaService.Transferir(contaIdATransferir, valor);
                return Ok(result);
            } catch (Exception ex) {
                _logger.LogError(1, $"Erro: {nameof(ContaController)} - {ex.Message}" +
                    $"\n {ex.InnerException}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(template: "Atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] Conta conta) {
            try {
                var result = await _contaService.UpdateConta(conta);
                return Ok(result);
            } catch (Exception ex) {
                _logger.LogError(1, $"Erro: {nameof(ContaController)} - {ex.Message}" +
                    $"\n {ex.InnerException}");
                return NotFound(ex.Message);
            }
        }
    }
}

