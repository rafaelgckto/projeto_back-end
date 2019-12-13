using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToBuy.Domains;
using ToBuy.Repositories;

namespace ToBuy.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    [Produces ("application/json")]
    public class InteresseController : ControllerBase {
        InteresseRepository _interesseRepository = new InteresseRepository ();
        ProdutoRepository _produtoRepository = new ProdutoRepository ();
        AnuncioRepository _anuncioRepository = new AnuncioRepository ();
        UsuarioRepository _usuarioRepository = new UsuarioRepository ();
        TipoUsuarioRepository _tipoRepository = new TipoUsuarioRepository ();
        // EnvioEmail _email = new EnvioEmail();

        /// <summary>
        /// Listagem de todos os interesses
        /// </summary>
        /// <returns>Retorna ao usuário todos os interesses</returns>
        [HttpGet ("tolist")]
        public async Task<ActionResult<List<Interesse>>> ListInterests () {
            try {
                List<Interesse> lstInteresse = await _interesseRepository.Listar ();

                if (lstInteresse == null) {
                    return NotFound ();
                }

                return lstInteresse;
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Lista de um interesse específico
        /// </summary>
        /// <param name="id">Recebe o id do interesse informado</param>
        /// <returns>Retorna ao usuário as informações de interesse informado</returns>
        [HttpGet ("search/{id}")]
        public async Task<ActionResult<Interesse>> SearchInterest (int id) {
            try {
                Interesse interesse = await _interesseRepository.BuscarPorId (id);

                if (interesse == null) {
                    return NotFound ();
                }

                return interesse;
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Incusão de um novo interesse
        /// </summary>
        /// <param name="interesse">Parâmetro recebe um novo interesse</param>
        /// <returns>Retorna ao usuário os campos para criar um novo interesse</returns>
        [HttpPost ("insert")]
        public async Task<ActionResult<Interesse>> RegisterInterest (Interesse interesse) {
            try {
                return await _interesseRepository.Cadastrar (interesse);
            } catch (Exception ex) {
                throw ex;
            }
        }

        // /// <summary>
        // /// Alteração de um interesse específico
        // /// </summary>
        // /// <param name="id">Recebe o id específico do interesse</param>
        // /// <param name="interesse">Recebe as informações que serão alteradas</param>
        // /// <returns>Retorna ao usuário os campos para alteração de um interesse</returns>
        // [HttpPut ("update/{id}")]
        // public async Task<ActionResult<Interesse>> UpdateInterest (int id, Interesse interesse) {
        //     if (id != interesse.IdInteresse) {
        //         return BadRequest ();
        //     }

        //     try {
        //         Interesse dados = await _interesseRepository.Atualizar (interesse);

        //         List<Interesse> lstInteresse = await _interesseRepository.Listar ();

        //         foreach (var item in lstInteresse) {
        //             #region Busca do nome do usuário e nome do produto
        //             int usuarioID = item.FkIdUsuario;
        //             Usuario usuarioBuscado = await _usuarioRepository.BuscarPorId (usuarioID);

        //             string nomeUsuario = usuarioBuscado.NomeUsuario;

        //             int anuncioID = item.FkIdAnuncio;
        //             Anuncio anuncioBuscado = await _anuncioRepository.BuscaPorId (anuncioID);

        //             int produtoID = anuncioBuscado.FkIdProduto;
        //             Produto produtoBuscado = await _produtoRepository.BuscarPorId (produtoID);

        //             string nomeProduto = produtoBuscado.NomeProduto;
        //             #endregion

        //             string titulo = $"Parabéns {nomeUsuario} você foi selecionado - Você acaba de adquirir {nomeProduto}";

        //             //Construct the alternate body as HTML
        //             string corpo = System.IO.File.ReadAllText (path: @"EdiçãoClassificadosEmail.html");

        //             int tipo_userID = usuarioBuscado.FkIdTipoUsuario;
        //             TipoUsuario tipo_userBuscado = await _tipoRepository.BuscarPorId (tipo_userID);

        //             string tipo_user = tipo_userBuscado.PermissaoTipoUsuario;

        //             if (item.CompradorInteresse == false) {
        //                 if (tipo_user == "Funcionario") {
        //                     _email.ValidarEnvio (
        //                         email: usuarioBuscado.EmailUsuario,
        //                         titulo: titulo,
        //                         corpo: corpo
        //                     );
        //                 } else {
        //                     Debug.WriteLine ("ENVIOU O E-MAIL");
        //                 }
        //             } else {
        //                 Debug.WriteLine ("ENVIOU <ganhador> E-MAIL");
        //             }
        //         }

        //         return dados;
        //     } catch (DbUpdateException ex) {
        //         var interesseValido = await _interesseRepository.BuscarPorId (id);

        //         if (interesseValido == null) {
        //             return NotFound ();
        //         } else {
        //             throw ex;
        //         }

        //     }
        // }

        /// <summary>
        /// Deleta um interesse
        /// </summary>
        /// <param name="id">Recebe o id de interesse que será deletado</param>
        /// <returns>Retorna ao usuário a informação de exclusão</returns>
        [HttpDelete ("delete/{id}")]
        public async Task<ActionResult<Interesse>> DeleteInterest (int id) {
            try {
                Interesse interesseRetornado = await _interesseRepository.BuscarPorId (id);

                if (interesseRetornado == null) {
                    return NotFound ();
                }

                await _interesseRepository.Deletar (interesseRetornado);

                return interesseRetornado;
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}