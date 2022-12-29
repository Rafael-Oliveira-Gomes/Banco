using BancoApi.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using BancoApi.Model;
using BancoApi.Config.Context;

namespace BancoApi.Repository {
    public class CartaoRepository : ICartaoRepository {

        private readonly MySqlContext _context;

        public CartaoRepository(MySqlContext context) {
            _context = context;
        }

        public async Task<Cartao> CreateAsync(Cartao Cartao) {
            var ret = await _context.Cartao.AddAsync(Cartao);

            await _context.SaveChangesAsync();

            ret.State = EntityState.Detached;

            return ret.Entity;
        }

        public async Task<bool> DeleteCartaoAsync(int Id) {
            var item = await _context.Cartao.FindAsync(Id);
            _context.Cartao.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Cartao> GetCartaoById(int Id) {
            Cartao Cartao = await _context.Cartao.Include(x => x.Conta!).FirstOrDefaultAsync((p => p.Id == Id));
            return Cartao;
        }

        public async Task<Cartao> GetCartaoByContaId(int contaId) {
            Cartao Cartao = await _context.Cartao.Include(x => x!).FirstOrDefaultAsync(p => p.ContaId == contaId);
            return Cartao;
        }

        public async Task<List<Cartao>> ListCartao() {
            List<Cartao> list = await _context.Cartao.OrderBy(p => p.Id).ToListAsync();
            return list;
        }

        public async Task<int> UpdateCartao(Cartao user) {
            _context.Entry(user).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }
    }
}
