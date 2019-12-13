using System.Collections.Generic;
using System.Threading.Tasks;
using ToBuy.Domains;

namespace ToBuy.Interfaces {
    interface ITipoUsuario {
        Task<List<TipoUsuario>> Listar ();
        Task<TipoUsuario> BuscarPorId (int id);
        Task<TipoUsuario> Cadastrar (TipoUsuario tipo_usuario);
        Task<TipoUsuario> Atualizar (TipoUsuario tipo_usuario);
        Task<TipoUsuario> Deletar (TipoUsuario tipo_usuarioRetornado);
    }
}