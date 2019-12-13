using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToBuy.Context;
using ToBuy.Domains;
using ToBuy.Interfaces;

namespace ToBuy.Repositories {
    public class TipoUsuarioRepository : ITipoUsuario {
        ToBuyContext _context = new ToBuyContext ();

        public async Task<List<TipoUsuario>> Listar () {
            return await _context.TipoUsuario.ToListAsync ();
        }

        public async Task<TipoUsuario> BuscarPorId (int id) {
            return await _context.TipoUsuario.FindAsync (id);
        }

        public async Task<TipoUsuario> Cadastrar (TipoUsuario tipo_usuario) {
            await _context.TipoUsuario.AddAsync (tipo_usuario);
            await _context.SaveChangesAsync ();

            return tipo_usuario;
        }

        public async Task<TipoUsuario> Atualizar (TipoUsuario tipo_usuario) {
            _context.Entry (tipo_usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync ();

            return tipo_usuario;
        }

        public async Task<TipoUsuario> Deletar (TipoUsuario tipo_usuarioRetornado) {
            _context.TipoUsuario.Remove (tipo_usuarioRetornado);
            await _context.SaveChangesAsync ();

            return tipo_usuarioRetornado;
        }
    }
}