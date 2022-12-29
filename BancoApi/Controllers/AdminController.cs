using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BancoApi.Interface.Service;

namespace BancoApi.Controllers {
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]

    public class AdminController : ControllerBase {

        private readonly ILogger<ContaController> _logger;
        private readonly IContaService _contaService;
        private readonly IRoleService _roleService;

        public AdminController(ILogger<ContaController> logger, IContaService contaService, IRoleService roleService) {
            _logger = logger;
            _contaService = contaService;
            _roleService = roleService;
        }

        [AllowAnonymous]
        [HttpPost(template: "CriarRole")]
        public async Task<IActionResult> CriarRole([FromBody] string nome) {
            try {
                var result = await _roleService.CriarRole(nome);
                return Ok(result);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(template: "Remover")]
        public async Task<IActionResult> RemoverConta([FromBody] int contaId) {
            try {
                if (contaId <= 0) { return BadRequest("Id do usuário não deve ser menor que 1"); }

                var result = await _contaService.RemoverConta(contaId);
                return Ok(result);
            } catch (Exception ex) {
                _logger.LogError(1, $"Erro: {nameof(ContaController)} - {ex.Message}" +
                    $"\n {ex.InnerException}");
                return NotFound(ex.Message);
            }
        }

        [HttpGet(template: "Listar")]
        public async Task<IActionResult> ListarContas() {
            try {
                var result = await _contaService.ListarContas();
                return Ok(result);
            } catch (Exception ex) {
                _logger.LogError(1, $"Erro: {nameof(ContaController)} - {ex.Message}" +
                    $"\n {ex.InnerException}");
                return NotFound(ex.Message);
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetConta([FromQuery] int contaId) {
            if (contaId <= 0) { return BadRequest("Id do Conta não deve ser menor que 1"); }
            try {
                var result = await _contaService.GetConta(contaId);
                return Ok(result);
            } catch (Exception ex) {
                _logger.LogError(1, $"Erro: {nameof(ContaController)} - {ex.Message}" +
                    $"\n {ex.InnerException}");
                return NotFound(ex.Message);
            }
        }
    }
}
