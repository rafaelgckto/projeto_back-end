using System.Collections.Generic;
using System.Threading.Tasks;
using ToBuy.Domains;

namespace ToBuy.Interfaces {
    interface IFabricante {
        Task<List<Fabricante>> Get ();
        Task<Fabricante> Get (int id);
        Task<Fabricante> Post (Fabricante fabricante);
        Task<Fabricante> Put (Fabricante fabricante);
        Task<Fabricante> Delete (Fabricante fabricanteRetornado);
    }
}