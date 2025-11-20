using ia_learning.Models;
using Microsoft.EntityFrameworkCore;

namespace ia_learning.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Habilidade> Habilidades { get; set; }
        public DbSet<UsuarioHabilidade> UsuarioHabilidades { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Recomendacao> Recomendacoes { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<IA> IAs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("IA_USUARIO");
            modelBuilder.Entity<Habilidade>().ToTable("IA_HABILIDADE");
            modelBuilder.Entity<UsuarioHabilidade>().ToTable("IA_USUARIO_HABILIDADE");
            modelBuilder.Entity<Tarefa>().ToTable("IA_TAREFA");
            modelBuilder.Entity<Recomendacao>().ToTable("IA_RECOMENDACAO");
            modelBuilder.Entity<Avaliacao>().ToTable("IA_AVALIACAO");
            modelBuilder.Entity<IA>().ToTable("IA_MODELO");

            modelBuilder.Entity<UsuarioHabilidade>()
                .HasOne(uh => uh.Usuario)
                .WithMany(u => u.UsuarioHabilidades)
                .HasForeignKey(uh => uh.UsuarioId);

            modelBuilder.Entity<UsuarioHabilidade>()
                .HasOne(uh => uh.Habilidade)
                .WithMany(h => h.UsuarioHabilidades)
                .HasForeignKey(uh => uh.HabilidadeId);

            modelBuilder.Entity<Tarefa>()
                .HasOne(t => t.Usuario)
                .WithMany(u => u.Tarefas)
                .HasForeignKey(t => t.UsuarioId);

            modelBuilder.Entity<IA>()
                .HasMany(i => i.Tarefas)
                .WithOne(t => t.IA)
                .HasForeignKey(t => t.IAId);

            modelBuilder.Entity<Recomendacao>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Recomendacoes)
                .HasForeignKey(r => r.UsuarioId);

            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Avaliacoes)
                .HasForeignKey(a => a.UsuarioId);

            modelBuilder.Entity<IA>()
                .HasMany(i => i.Avaliacoes)
                .WithOne(a => a.IA)
                .HasForeignKey(a => a.IAId);
        }
    }
}
