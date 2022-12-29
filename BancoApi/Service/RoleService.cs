using BancoApi.Interface.Repository;
using BancoApi.Interface.Service;
using BancoApi.Model;

namespace BancoApi.Service
{
    public class RoleService : IRoleService {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository) {
            _roleRepository = roleRepository;
        }

        public async Task<ApplicationRole> CriarRole(string nome) {
            var role = new ApplicationRole() {
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
                Name = nome,
                NormalizedName = nome.ToUpper(),
            };
            var result = await _roleRepository.CreateAsync(role);

            return result;
        }
    }
}
