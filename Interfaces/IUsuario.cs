using System.Collections.Generic;
using System.Threading.Tasks;
using ToBuy.Domains;

namespace ToBuy.Interfaces {
    interface IUsuario {
        // Usuario AutenticarLogin (UsuarioLogin login);
        Task<List<Usuario>> Listar ();
        Task<Usuario> BuscarPorId (int id);
        Task<Usuario> Cadastrar (Usuario usuario);
        Task<Usuario> VerificarEmail (Usuario login);
        Task<Usuario> Atualizar (Usuario usuario);
        Task<Usuario> Deletar (Usuario usuarioRetornado);
    }
}