using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToBuy.Context;
using ToBuy.Domains;
using ToBuy.Interfaces;

namespace ToBuy.Repositories {
    public class AnuncioRepository : IAnuncio {
        ToBuyContext _context = new ToBuyContext ();

        public async Task<Anuncio> Delete (Anuncio ad) {
            _context.Anuncio.Remove (ad);
            await _context.SaveChangesAsync ();

            return ad;
        }

        public async Task<List<Anuncio>> PriceSearch (decimal price) {
            List<Anuncio> lstAd = await _context.Anuncio
                .Include (x => x.FkIdProdutoNavigation)
                .Where (a => a.PrecoAnuncio >= price)
                .ToListAsync ();

            return lstAd;
        }

        public async Task<Anuncio> Register (Anuncio ad) {
            await _context.Anuncio.AddAsync (ad);
            await _context.SaveChangesAsync ();

            return ad;
        }

        public async Task<List<Anuncio>> SearchBrandAndCondition (string manufacturer, string conservation) {
            List<Anuncio> lstAd = await _context.Anuncio
                .Include (x => x.FkIdProdutoNavigation.FkIdFabricanteNavigation)
                .Include (y => y.FkIdConservacaoNavigation)
                .Where (a => a.FkIdProdutoNavigation.FkIdFabricanteNavigation.NomeFabricante == manufacturer && a.FkIdConservacaoNavigation.EstadoConservacao == conservation)
                .ToListAsync ();

            return lstAd;
        }

        public async Task<List<Anuncio>> SearchByField (string desiredField) {
            List<Anuncio> lstAd = await _context.Anuncio
                .Include (x => x.FkIdProdutoNavigation)
                .Where (a => a.FkIdProdutoNavigation.NomeProduto.StartsWith (desiredField) ||
                    a.FkIdProdutoNavigation.ModeloProduto.StartsWith (desiredField))
                .OrderBy (a => a.PrecoAnuncio)
                .ToListAsync ();

            return lstAd;
        }

        public async Task<List<Anuncio>> SearchBySetPrice (decimal minPrice, decimal maxPrice) {
            List<Anuncio> lstAd = await _context.Anuncio
                .Include (a => a.FkIdProdutoNavigation)
                .Where (a => (a.PrecoAnuncio >= minPrice) && (a.PrecoAnuncio <= maxPrice))
                .ToListAsync ();

            return lstAd;
        }

        public async Task<Anuncio> SearchForId (int id) {
            return await _context.Anuncio
                .Include (x => x.FkIdConservacaoNavigation)
                .Include (y => y.FkIdProdutoNavigation)
                .FirstOrDefaultAsync (a => a.IdAnuncio == id);
        }

        public async Task<List<Anuncio>> SortByDate () {
            List<Anuncio> lstAd = await _context.Anuncio
                .Include (x => x.FkIdProdutoNavigation)
                .OrderByDescending (a => a.FkIdProdutoNavigation.DtLancProduto)
                .ToListAsync ();

            return lstAd;
        }

        public async Task<List<Anuncio>> ToList () {
            return await _context.Anuncio
                .Include (x => x.FkIdConservacaoNavigation)
                .Include (y => y.FkIdProdutoNavigation)
                .ToListAsync ();
        }

        public async Task<Anuncio> Update (Anuncio ad) {
            _context.Entry (ad).State = EntityState.Modified;
            await _context.SaveChangesAsync ();

            return ad;
        }
    }
}