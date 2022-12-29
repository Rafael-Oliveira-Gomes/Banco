using System.ComponentModel.DataAnnotations.Schema;

namespace BancoApi.Model
{
    public class Conta {
        public Conta() {
        }
        public int Id { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public decimal Saldo { get; set; }
        public decimal Salario { get; set; }
        public Cartao? Cartao { get; set; }
        [ForeignKey("Cartao")] public int? CartaoId { get; set; }
    }
}
