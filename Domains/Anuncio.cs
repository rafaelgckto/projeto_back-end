using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToBuy.Domains {
    public partial class Anuncio {
        public Anuncio () {
            Imagem = new HashSet<Imagem> ();
            Interesse = new HashSet<Interesse> ();
        }

        [Key]
        [Column ("idAnuncio")]
        public int IdAnuncio { get; set; }

        [Column ("FK_idProduto")]
        public int? FkIdProduto { get; set; }

        [Column ("FK_idConservacao")]
        public int? FkIdConservacao { get; set; }

        [Column ("precoAnuncio", TypeName = "decimal(7, 2)")]
        public decimal PrecoAnuncio { get; set; }

        [Column ("dt_finalAnuncio", TypeName = "date")]
        public DateTime DtFinalAnuncio { get; set; }

        [Column ("descAnuncio")]
        [StringLength (200)]
        public string DescAnuncio { get; set; }

        [Column ("statusAnuncio")]
        public bool StatusAnuncio { get; set; }

        [ForeignKey (nameof (FkIdConservacao))]
        [InverseProperty (nameof (Conservacao.Anuncio))]
        public virtual Conservacao FkIdConservacaoNavigation { get; set; }

        [ForeignKey (nameof (FkIdProduto))]
        [InverseProperty (nameof (Produto.Anuncio))]
        public virtual Produto FkIdProdutoNavigation { get; set; }

        [InverseProperty ("FkIdAnuncioNavigation")]
        public virtual ICollection<Imagem> Imagem { get; set; }

        [InverseProperty ("FkIdAnuncioNavigation")]
        public virtual ICollection<Interesse> Interesse { get; set; }
    }
}