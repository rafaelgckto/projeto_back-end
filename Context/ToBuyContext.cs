using Microsoft.EntityFrameworkCore;
using ToBuy.Domains;

namespace ToBuy.Context {
    public partial class ToBuyContext : DbContext {
        public ToBuyContext () { }

        public ToBuyContext (DbContextOptions<ToBuyContext> options) : base (options) { }

        public virtual DbSet<Anuncio> Anuncio { get; set; }
        public virtual DbSet<Conservacao> Conservacao { get; set; }
        public virtual DbSet<Fabricante> Fabricante { get; set; }
        public virtual DbSet<Imagem> Imagem { get; set; }
        public virtual DbSet<Interesse> Interesse { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer ("Server=.\\SQLEXPRESS;Database=ToBuy;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<Anuncio> (entity => {
                entity.HasKey (e => e.IdAnuncio)
                    .HasName ("PK__Anuncio__0BC1EC3EA99E336B");

                entity.Property (e => e.DescAnuncio).IsUnicode (false);

                entity.HasOne (d => d.FkIdConservacaoNavigation)
                    .WithMany (p => p.Anuncio)
                    .HasForeignKey (d => d.FkIdConservacao)
                    .HasConstraintName ("FK__Anuncio__FK_idCo__49C3F6B7");

                entity.HasOne (d => d.FkIdProdutoNavigation)
                    .WithMany (p => p.Anuncio)
                    .HasForeignKey (d => d.FkIdProduto)
                    .HasConstraintName ("FK__Anuncio__FK_idPr__48CFD27E");
            });

            modelBuilder.Entity<Conservacao> (entity => {
                entity.HasKey (e => e.IdConservacao)
                    .HasName ("PK__Conserva__B3B6B0D3BA89E26F");

                entity.Property (e => e.EstadoConservacao).IsUnicode (false);
            });

            modelBuilder.Entity<Fabricante> (entity => {
                entity.HasKey (e => e.IdFabricante)
                    .HasName ("PK__Fabrican__6E91D59965250377");

                entity.Property (e => e.NomeFabricante).IsUnicode (false);
            });

            modelBuilder.Entity<Imagem> (entity => {
                entity.HasKey (e => e.IdImagem)
                    .HasName ("PK__Imagem__B42D8F156B478586");

                entity.Property (e => e.Imagem1).IsUnicode (false);

                entity.HasOne (d => d.FkIdAnuncioNavigation)
                    .WithMany (p => p.Imagem)
                    .HasForeignKey (d => d.FkIdAnuncio)
                    .HasConstraintName ("FK__Imagem__FK_idAnu__4D94879B");
            });

            modelBuilder.Entity<Interesse> (entity => {
                entity.HasKey (e => e.IdInteresse)
                    .HasName ("PK__Interess__7E48DA568962D664");

                entity.HasOne (d => d.FkIdAnuncioNavigation)
                    .WithMany (p => p.Interesse)
                    .HasForeignKey (d => d.FkIdAnuncio)
                    .HasConstraintName ("FK__Interesse__FK_id__5165187F");

                entity.HasOne (d => d.FkIdUsuarioNavigation)
                    .WithMany (p => p.Interesse)
                    .HasForeignKey (d => d.FkIdUsuario)
                    .HasConstraintName ("FK__Interesse__FK_id__5070F446");
            });

            modelBuilder.Entity<Produto> (entity => {
                entity.HasKey (e => e.IdProduto)
                    .HasName ("PK__Produto__5EEDF7C3ECE527B5");

                entity.Property (e => e.ModeloProduto).IsUnicode (false);

                entity.Property (e => e.NomeProduto).IsUnicode (false);

                entity.HasOne (d => d.FkIdFabricanteNavigation)
                    .WithMany (p => p.Produto)
                    .HasForeignKey (d => d.FkIdFabricante)
                    .HasConstraintName ("FK__Produto__FK_idFa__4316F928");

                entity.HasOne (d => d.FkIdUsuarioNavigation)
                    .WithMany (p => p.Produto)
                    .HasForeignKey (d => d.FkIdUsuario)
                    .HasConstraintName ("FK__Produto__FK_idUs__440B1D61");
            });

            modelBuilder.Entity<TipoUsuario> (entity => {
                entity.HasKey (e => e.IdTipoUsuario)
                    .HasName ("PK__TipoUsua__03006BFF9C0F2EA9");

                entity.HasIndex (e => e.PermissaoTipoUsuario)
                    .HasName ("UQ__TipoUsua__8F44324B4609DBBB")
                    .IsUnique ();

                entity.Property (e => e.PermissaoTipoUsuario)
                    .IsUnicode (false)
                    .HasDefaultValueSql ("('Funcionário')");
            });

            modelBuilder.Entity<Usuario> (entity => {
                entity.HasKey (e => e.IdUsuario)
                    .HasName ("PK__Usuario__645723A619CA653D");

                entity.HasIndex (e => e.EmailUsuario)
                    .HasName ("UQ__Usuario__ACC1DD99B0C09CE4")
                    .IsUnique ();

                entity.HasIndex (e => e.NomeUsuario)
                    .HasName ("UQ__Usuario__8C9D1DE51641FA0C")
                    .IsUnique ();

                entity.Property (e => e.EmailUsuario).IsUnicode (false);

                entity.Property (e => e.NomeUsuario).IsUnicode (false);

                entity.Property (e => e.SenhaUsuario).IsUnicode (false);

                entity.Property (e => e.TelefoneUsuario).IsUnicode (false);

                entity.HasOne (d => d.FkIdTipoUsuarioNavigation)
                    .WithMany (p => p.Usuario)
                    .HasForeignKey (d => d.FkIdTipoUsuario)
                    .HasConstraintName ("FK__Usuario__FK_idTi__3E52440B");
            });

            OnModelCreatingPartial (modelBuilder);
        }

        partial void OnModelCreatingPartial (ModelBuilder modelBuilder);
    }
}