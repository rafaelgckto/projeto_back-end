using System.Collections.Generic;
using System.Threading.Tasks;
using ToBuy.Domains;

namespace ToBuy.Interfaces {
    interface IProduto {
        Task<List<Produto>> Listar ();
        Task<Produto> BuscarPorId (int id);
        Task<Produto> Cadastrar (Produto produto);
        Task<Produto> Atualizar (Produto produto);
        Task<Produto> Deletar (Produto produtoRetornado);
    }
}