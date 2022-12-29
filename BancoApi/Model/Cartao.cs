using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BancoApi.Model {
    public class Cartao {
        public int Id { get; set; }
        public decimal FaturaDoMes { get; set; }
        public decimal FaturaTotal { get; set; }
        public decimal Limite { get; set; }
        [JsonIgnore] public Conta? Conta { get; set; }
        [ForeignKey("Conta")] public int? ContaId { get; set; }

        public bool Ativo { get; set; }
        public DateTime? DataDeVencimento { get; set; }

    }
}
