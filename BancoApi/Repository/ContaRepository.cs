using BancoApi.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using BancoApi.Model;
using BancoApi.Config.Context;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Hosting;

namespace BancoApi.Repository {
    public class ContaRepository : IContaRepository {

        private readonly MySqlContext _context;

        public ContaRepository(MySqlContext context) {
             _context = context;
        }

        public async Task<Conta> CreateAsync(Conta conta) {
            var ret = await _context.Conta.AddAsync(conta);

            await _context.SaveChangesAsync();

            ret.State = EntityState.Detached;

            return ret.Entity;
        }

        public async Task<bool> DeleteContaAsync(int Id) {
            var item = await _context.Conta.FindAsync(Id);
            _context.Conta.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Conta> GetContaById(int IdUser) {
            Conta Conta = await _context.Conta.Include(x => x.ApplicationUser!).FirstOrDefaultAsync((p => p.Id == IdUser));
            return Conta;
        }

        public async Task<Conta> GetContaByUserId(string userId) {
            Conta Conta = await _context.Conta.Include(x => x.ApplicationUser!).FirstOrDefaultAsync(p => p.ApplicationUserId == userId);
            return Conta;
        }

        public async Task<List<Conta>> ListDados() {
            List<Conta> list = await _context.Conta.OrderBy(p => p.Id).ToListAsync();
            return list;
        }

        public async Task<int> UpdateConta(Conta user) {
            _context.Entry(user).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }
    }


}
