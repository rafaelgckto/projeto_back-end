using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToBuy.Domains
{
    public partial class Imagem
    {
        [Key]
        public int IdImagem { get; set; }
        [Column("imagem")]
        [StringLength(300)]
        public string Imagem1 { get; set; }
        [Column("FK_idAnuncio")]
        public int? FkIdAnuncio { get; set; }

        [ForeignKey(nameof(FkIdAnuncio))]
        [InverseProperty(nameof(Anuncio.Imagem))]
        public virtual Anuncio FkIdAnuncioNavigation { get; set; }
    }
}
