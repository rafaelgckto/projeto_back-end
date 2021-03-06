﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToBuy.Domains
{
    public partial class Conservacao
    {
        public Conservacao()
        {
            Anuncio = new HashSet<Anuncio>();
        }

        [Key]
        [Column("idConservacao")]
        public int IdConservacao { get; set; }
        [Required]
        [Column("estadoConservacao")]
        [StringLength(10)]
        public string EstadoConservacao { get; set; }

        [InverseProperty("FkIdConservacaoNavigation")]
        public virtual ICollection<Anuncio> Anuncio { get; set; }
    }
}
