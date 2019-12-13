using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToBuy.Context;
using ToBuy.Domains;
using ToBuy.Interfaces;

namespace ToBuy.Repositories {
    public class FabricanteRepository : IFabricante {
        ToBuyContext _context = new ToBuyContext ();

        public async Task<List<Fabricante>> Get () {
            return await _context.Fabricante.ToListAsync ();
        }

        public async Task<Fabricante> Get (int id) {
            return await _context.Fabricante.FindAsync (id);
        }

        public async Task<Fabricante> Post (Fabricante fabricante) {
            await _context.Fabricante.AddAsync (fabricante);
            await _context.SaveChangesAsync ();

            return fabricante;
        }

        public async Task<Fabricante> Put (Fabricante fabricante) {
            _context.Entry (fabricante).State = EntityState.Modified;
            await _context.SaveChangesAsync ();

            return fabricante;
        }

        public async Task<Fabricante> Delete (Fabricante fabricanteRetornado) {
            _context.Fabricante.Remove (fabricanteRetornado);
            await _context.SaveChangesAsync ();

            return fabricanteRetornado;
        }
    }
}