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
    public class CartaoController : ControllerBase
        {
        private readonly ILogger<CartaoController> _logger;
        private readonly ICartaoService _cartaoService;
        public CartaoController(ILogger<CartaoController> logger, ICartaoService cartaoService) {
            _logger = logger;
            _cartaoService = cartaoService;
        }

        [HttpPost(template: "ParcelarNoCredito")]
        public async Task<IActionResult> ParcelarNoCredito(int valor, int mes) {
            try {
                var result = await _cartaoService.ParcelarNoCredito(valor, mes);
                return Ok(result);
            } catch (Exception ex) {
                _logger.LogError(1, $"Erro: {nameof(CartaoController)} - {ex.Message}" +
                    $"\n {ex.InnerException}");
                return BadRequest(ex.Message);
            }
        }
        [HttpPost(template: "PagarFatura")]
        public async Task<IActionResult> PagarFatura(decimal valor) {
            try {
                var result = await _cartaoService.PagarFatura(valor);
                return Ok(result);
            } catch (Exception ex) {
                _logger.LogError(1, $"Erro: {nameof(CartaoController)} - {ex.Message}" +
                    $"\n {ex.InnerException}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(template: "AtivarCartao")]
        public async Task<IActionResult> DataDeVencimento(int dia) {
            try {
                var result = await _cartaoService.DataDeVencimento(dia);
                return Ok(result);
            } catch (Exception ex) {
                _logger.LogError(1, $"Erro: {nameof(CartaoController)} - {ex.Message}" +
                    $"\n {ex.InnerException}");
                return BadRequest(ex.Message);
            }
        }
    }
}
