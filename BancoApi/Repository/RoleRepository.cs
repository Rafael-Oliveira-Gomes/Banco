using BancoApi.Config.Context;
using BancoApi.Interface.Repository;
using BancoApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BancoApi.Repository
{
    public class RoleRepository : IRoleRepository {


        private readonly MySqlContext _context;

        public RoleRepository(MySqlContext context) {
            _context = context;
        }

        public async Task<ApplicationRole> CreateAsync(ApplicationRole role) {
            var ret = await _context.Role.AddAsync(role);

            await _context.SaveChangesAsync();

            ret.State = EntityState.Detached;

            return ret.Entity;
        }
    }
}
