using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToBuy.Context;
using ToBuy.Domains;
using ToBuy.Interfaces;

namespace ToBuy.Repositories {
    public class ConservacaoRepository : IConservacao {
        ToBuyContext _context = new ToBuyContext ();

        public async Task<List<Conservacao>> Get () {
            return await _context.Conservacao.ToListAsync ();
        }

        public async Task<Conservacao> Get (int id) {
            return await _context.Conservacao.FindAsync (id);
        }

        public async Task<Conservacao> Post (Conservacao conservacao) {
            await _context.Conservacao.AddAsync (conservacao);
            await _context.SaveChangesAsync ();

            return conservacao;
        }

        public async Task<Conservacao> Put (Conservacao conservacao) {
            _context.Entry (conservacao).State = EntityState.Modified;
            await _context.SaveChangesAsync ();

            return conservacao;
        }

        public async Task<Conservacao> Delete (Conservacao conservacaoRetornada) {
            _context.Conservacao.Remove (conservacaoRetornada);
            await _context.SaveChangesAsync ();

            return conservacaoRetornada;
        }
    }
}