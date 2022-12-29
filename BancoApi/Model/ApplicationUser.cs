using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text.Json.Serialization;

namespace BancoApi.Model {
    public class ApplicationUser : IdentityUser {
        [JsonIgnore] public Conta? Conta { get; set; }
        [ForeignKey("Conta")] public int? ContaId { get; set; }
    }
} // para cada conta é um usuario
