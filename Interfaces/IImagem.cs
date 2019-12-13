using System.Collections.Generic;
using System.Threading.Tasks;
using ToBuy.Domains;

namespace ToBuy.Interfaces {
    interface IImagem {
        Task<List<Imagem>> Get ();
        Task<Imagem> Get (int id);
        Task<Imagem> Post (Imagem imagem);
        Task<Imagem> Put (Imagem imagem);
        Task<Imagem> Delete (Imagem imagemRetornada);
    }
}