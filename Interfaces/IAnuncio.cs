using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToBuy.Domains;

namespace ToBuy.Interfaces {
    interface IAnuncio {
        Task<List<Anuncio>> ToList ();
        Task<Anuncio> SearchForId (int id);
        Task<List<Anuncio>> PriceSearch (decimal price);
        Task<List<Anuncio>> SearchBySetPrice (decimal minPrice, decimal maxPrice);
        Task<List<Anuncio>> SearchBrandAndCondition (string manufacturer, string conservation);
        Task<List<Anuncio>> SortByDate ();
        Task<List<Anuncio>> SearchByField (string desiredField);
        Task<Anuncio> Register (Anuncio ad);
        Task<Anuncio> Update (Anuncio ad);
        Task<Anuncio> Delete (Anuncio ad);
    }
}