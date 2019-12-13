using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToBuy.Domains;
using ToBuy.Repositories;

namespace ToBuy.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    [Produces ("application/json")]
    public class AnuncioController : ControllerBase {
        AnuncioRepository _adRepository = new AnuncioRepository ();

        /// <summary>
        /// Listagem de todos os anúncios
        /// </summary>
        /// <returns>Retorna ao usuário uma lista com todos anúncios</returns>
        [HttpGet ("tolist")]
        public async Task<ActionResult<List<Anuncio>>> ListAds () {
            try {
                List<Anuncio> lstAd = await _adRepository.ToList ();

                if (lstAd == null) {
                    return NotFound ();
                }

                foreach (var item in lstAd) {
                    item.FkIdProdutoNavigation.Anuncio = null;
                    item.FkIdConservacaoNavigation.Anuncio = null;
                }

                return lstAd;
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Lista de anúncio específico
        /// </summary>
        /// <param name="id">Recebe o id do anúncio informada</param>
        /// <returns>Retorna ao usuário as informações do anúncio informado</returns>
        [HttpGet ("search/{id}")]
        public async Task<ActionResult<Anuncio>> SearchId (int id) {
            try {
                Anuncio ad = await _adRepository.SearchForId (id);

                if (ad == null) {
                    return NotFound ();
                }

                return ad;
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Filtro de preço
        /// </summary>
        /// <param name="price">Parâmetro recebe o valor de preço</param>
        /// <returns>Retorna ao usuário lista de anúncios com o preço informado</returns>
        [HttpGet ("search/filter/price/{price}")]
        public async Task<ActionResult<List<Anuncio>>> ListByPrice (decimal price) {
            try {
                List<Anuncio> lstAnuncio = await _adRepository.PriceSearch (price);

                if (lstAnuncio == null) {
                    return NotFound ();
                }

                return lstAnuncio;
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Filtro de preço pré setado
        /// </summary>
        /// <param name="minPrice">Parâmetro recebe o valor de preço mínimo</param>
        /// <param name="maxPrice">Parâmetro recebe o valor de preço máximo</param>
        /// <returns>Retorna ao usuário lista de anúncios entre os preços informados</returns>
        [HttpGet ("search/filter/price/{minPrice}/{maxPrice}")]
        public async Task<ActionResult<List<Anuncio>>> ListBySetPrice (decimal minPrice, decimal maxPrice) {
            try {
                List<Anuncio> lstAd = await _adRepository.SearchBySetPrice (minPrice, maxPrice);

                if (lstAd == null) {
                    return NotFound ();
                }

                return lstAd;
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Filtro de conservação do produto e fabricante
        /// </summary>
        /// <param name="manufacturer">Recebe o fabricante do produto informado</param>
        /// <param name="condition">Recebe o estado de conservação do produto informado</param>
        /// <returns>Retorna ao usuário os anúncios com as condições de produto e estado de conservação informados</returns>
        [HttpGet ("search/filter/brand-condition/{manufacturer}/{condition}")]
        public async Task<ActionResult<List<Anuncio>>> ListBrandAndCondition (string manufacturer, string condition) {
            try {
                List<Anuncio> lstAd = await _adRepository.SearchBrandAndCondition (manufacturer, condition);

                if (lstAd == null) {
                    return NotFound ();
                }

                foreach (var item in lstAd) {
                    item.FkIdConservacaoNavigation.Anuncio = null;
                    item.FkIdProdutoNavigation.Anuncio = null;
                    item.FkIdProdutoNavigation.FkIdFabricanteNavigation.Produto = null;
                }

                return lstAd;
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Filtro de Data
        /// </summary>
        /// <returns>Retorna ao usuário os anúncios ordenado pela data</returns>
        [HttpGet ("search/filter/date")]
        public async Task<ActionResult<List<Anuncio>>> ListByDate () {
            try {
                List<Anuncio> lstAd = await _adRepository.SortByDate ();

                if (lstAd == null) {
                    return NotFound ();
                }

                return lstAd;
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Filtro de busca
        /// </summary>
        /// <param name="desiredField">Recebe uma informação como titulo ou palavras que contenham na descrição</param>
        /// <returns>Retorna ao usuário os anúncios que possuem aquela palavra ou texto</returns>
        [HttpGet ("search/filter/field/{desiredField}")]
        public async Task<ActionResult<List<Anuncio>>> SearchField (string desiredField) {
            try {
                List<Anuncio> lstAd = await _adRepository.SearchByField (desiredField);

                if (lstAd == null) {
                    return NotFound ();
                } else if (lstAd.Count == 0) {
                    return BadRequest ();
                }

                return lstAd;
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Incusão de novo anúncio
        /// </summary>
        /// <param name="ad">Parâmetro recebe um novo anúncio</param>
        /// <returns>Retorna ao usuário os campos para criar um novo anúncio</returns>
        [HttpPost ("insert")]
        public async Task<ActionResult<Anuncio>> RegisterAd (Anuncio ad) {
            try {
                return await _adRepository.Register (ad);
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Alteração de anúncio específico
        /// </summary>
        /// <param name="id">Recebe o id específico do anúncio</param>
        /// <param name="ad">Recebe as informações que serão alteradas</param>
        /// <returns>Retorna ao usuário os campos para alteração de um anúncio</returns>
        [HttpPut ("update/{id}")]
        public async Task<ActionResult<Anuncio>> UpdateAd (int id, Anuncio ad) {
            if (id != ad.IdAnuncio) {
                return BadRequest ();
            }

            try {
                return await _adRepository.Update (ad);
            } catch (DbUpdateException ex) {
                var validAd = _adRepository.SearchForId (id);

                if (validAd == null) {
                    return NotFound ();
                } else {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Deleta um anúncio
        /// </summary>
        /// <param name="id">Recebe o id do anúncio que será deletado</param>
        /// <returns>Retorna ao usuário a informação de exclusão</returns>
        [HttpDelete ("delete/{id}")]
        public async Task<ActionResult<Anuncio>> DeleteAd (int id) {
            try {
                Anuncio adReturned = await _adRepository.SearchForId (id);

                if (adReturned == null) {
                    return NotFound ();
                }

                await _adRepository.Delete (adReturned);

                return adReturned;
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}