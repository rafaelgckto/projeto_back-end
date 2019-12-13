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
    public class ProdutoController : ControllerBase {
        ProdutoRepository _produtoRepository = new ProdutoRepository ();

        /// <summary>
        /// Listagem de todos os produtos
        /// </summary>
        /// <returns> Retorna ao usuário todos os produtos </returns>
        [HttpGet ("tolist")]
        public async Task<ActionResult<List<Produto>>> ListProducts () {
            try {
                List<Produto> lstProduto = await _produtoRepository.Listar ();

                if (lstProduto == null) {
                    return NotFound ();
                }

                foreach (var item in lstProduto) {
                    item.FkIdFabricanteNavigation.Produto = null;
                    item.FkIdUsuarioNavigation.Produto = null;
                }

                return lstProduto;
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Lista de um produto específico
        /// </summary>
        /// <param name="id">Recebe o id de um produto informado</param>
        /// <returns>Retorna ao usuário as informações do produto informado</returns>
        [HttpGet ("search/{id}")]
        public async Task<ActionResult<Produto>> SearchProduct (int id) {
            try {
                Produto produto = await _produtoRepository.BuscarPorId (id);

                if (produto == null) {
                    return NotFound ();
                }

                return produto;
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Incusão de novo produto
        /// </summary>
        /// <param name="produto">Parâmetro recebe um novo produto </param>
        /// <returns> Retorna ao usuário os campos para criar um novo produto</returns>
        [HttpPost ("insert")]
        public async Task<ActionResult<Produto>> RegisterProduct (Produto produto) {
            try {
                return await _produtoRepository.Cadastrar (produto);
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Alteração de um produto específico
        /// </summary>
        /// <param name="id">Recebe o id específico do produto</param>
        /// <param name="produto">Recebe as informações que serão alteradas</param>
        /// <returns>Retorna ao usuário os campos para alteração de um produto</returns>
        [HttpPut ("update/{id}")]
        public async Task<ActionResult<Produto>> UpdateProduct (int id, Produto produto) {
            if (id != produto.IdProduto) {
                return BadRequest ();
            }

            try {
                return await _produtoRepository.Atualizar (produto);
            } catch (DbUpdateException ex) {
                var produtoValido = await _produtoRepository.BuscarPorId (id);

                if (produtoValido == null) {
                    return NotFound ();
                } else {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Deleta um produto
        /// </summary>
        /// <param name="id">Recebe o id do produto que será deletado</param>
        /// <returns>Retorna ao usuário a informação de exclusão</returns>
        [HttpDelete ("delete/{id}")]
        public async Task<ActionResult<Produto>> DeleteProduct (int id) {
            try {
                Produto produtoRetornado = await _produtoRepository.BuscarPorId (id);

                if (produtoRetornado == null) {
                    return NotFound ();
                }

                await _produtoRepository.Deletar (produtoRetornado);

                return produtoRetornado;
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}