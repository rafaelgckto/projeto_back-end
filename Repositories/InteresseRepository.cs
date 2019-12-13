using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToBuy.Context;
using ToBuy.Domains;
using ToBuy.Interfaces;

namespace ToBuy.Repositories {
    public class InteresseRepository : IInteresse {
        ToBuyContext _context = new ToBuyContext ();

        public async Task<List<Interesse>> Listar () {
            return await _context.Interesse.ToListAsync ();
        }

        public async Task<Interesse> BuscarPorId (int id) {
            return await _context.Interesse.FirstOrDefaultAsync (x => x.IdInteresse == id);
        }

        public async Task<Interesse> Cadastrar (Interesse interesse) {
            await _context.Interesse.AddAsync (interesse);
            await _context.SaveChangesAsync ();

            return interesse;
        }

        public async Task<Interesse> Atualizar (Interesse interesse) {
            _context.Entry (interesse).State = EntityState.Modified;
            await _context.SaveChangesAsync ();

            return interesse;
        }

        public async Task<Interesse> Deletar (Interesse interesseRetornado) {
            _context.Interesse.Remove (interesseRetornado);
            await _context.SaveChangesAsync ();

            return interesseRetornado;
        }
    }
}