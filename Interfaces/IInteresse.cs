using System.Collections.Generic;
using System.Threading.Tasks;
using ToBuy.Domains;

namespace ToBuy.Interfaces {
    interface IInteresse {
        Task<List<Interesse>> Listar ();
        Task<Interesse> BuscarPorId (int id);
        Task<Interesse> Cadastrar (Interesse interesse);
        Task<Interesse> Atualizar (Interesse interesse);
        Task<Interesse> Deletar (Interesse interesseRetornado);
    }
}