﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToBuy.Domains {
    public partial class Usuario {
        public Usuario () {
            Interesse = new HashSet<Interesse> ();
            Produto = new HashSet<Produto> ();
        }

        [Key]
        [Column ("idUsuario")]
        public int IdUsuario { get; set; }

        [Required]
        [Column ("nomeUsuario")]
        [StringLength (50)]
        public string NomeUsuario { get; set; }

        [Column ("telefoneUsuario")]
        [StringLength (14)]
        public string TelefoneUsuario { get; set; }

        [Required(ErrorMessage = "Informe o e-mail")]
        [Column ("emailUsuario")]
        [StringLength (50)]
        public string EmailUsuario { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [Column ("senhaUsuario")]
        [StringLength (300)]
        public string SenhaUsuario { get; set; }

        [Column ("statusUsuario")]
        public bool StatusUsuario { get; set; }

        [Column ("FK_idTipoUsuario")]
        public int? FkIdTipoUsuario { get; set; }

        [ForeignKey (nameof (FkIdTipoUsuario))]
        [InverseProperty (nameof (TipoUsuario.Usuario))]
        public virtual TipoUsuario FkIdTipoUsuarioNavigation { get; set; }

        [InverseProperty ("FkIdUsuarioNavigation")]
        public virtual ICollection<Interesse> Interesse { get; set; }

        [InverseProperty ("FkIdUsuarioNavigation")]
        public virtual ICollection<Produto> Produto { get; set; }
    }
}