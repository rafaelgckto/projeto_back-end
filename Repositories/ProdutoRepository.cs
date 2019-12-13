using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToBuy.Context;
using ToBuy.Domains;
using ToBuy.Interfaces;

namespace ToBuy.Repositories {
    public class ProdutoRepository : IProduto {
        ToBuyContext _context = new ToBuyContext ();

        public async Task<List<Produto>> Listar () {
            return await _context.Produto
                .Include (c => c.FkIdFabricanteNavigation)
                .Include (u => u.FkIdUsuarioNavigation)
                .ToListAsync ();
        }

        public async Task<Produto> BuscarPorId (int id) {
            return await _context.Produto
                .Include (c => c.FkIdFabricanteNavigation)
                .Include (u => u.FkIdUsuarioNavigation)
                .FirstOrDefaultAsync (x => x.IdProduto == id);
        }

        public async Task<Produto> Cadastrar (Produto produto) {
            await _context.Produto.AddAsync (produto);
            await _context.SaveChangesAsync ();

            return produto;
        }

        public async Task<Produto> Atualizar (Produto produto) {
            _context.Entry (produto).State = EntityState.Modified;
            await _context.SaveChangesAsync ();

            return produto;
        }

        public async Task<Produto> Deletar (Produto produtoRetornado) {
            _context.Produto.Remove (produtoRetornado);
            await _context.SaveChangesAsync ();

            return produtoRetornado;
        }
    }
}