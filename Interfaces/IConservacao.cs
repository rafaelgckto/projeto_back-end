using System.Collections.Generic;
using System.Threading.Tasks;
using ToBuy.Domains;

namespace ToBuy.Interfaces {
    interface IConservacao {
        Task<List<Conservacao>> Get ();
        Task<Conservacao> Get (int id);
        Task<Conservacao> Post (Conservacao conservacao);
        Task<Conservacao> Put (Conservacao conservacao);
        Task<Conservacao> Delete (Conservacao conservacaoRetornada);
    }
}