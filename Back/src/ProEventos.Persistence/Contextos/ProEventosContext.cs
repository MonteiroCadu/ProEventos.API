using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contextos
{
    public class ProEventosContext : DbContext
    {
        
        public ProEventosContext(DbContextOptions<ProEventosContext> options)
            : base(options)
        {
        }

        public  DbSet<Evento> Eventos { get; set; }
        public  DbSet<Lote> Lotes { get; set; }
        public  DbSet<Palestrante> Palestrantes { get; set; }
        public  DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public  DbSet<RedeSocial> RedesSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>()
            .HasKey(PE => new{PE.EventoId,PE.PalestrateId}); 

             modelBuilder.Entity<Evento>()
                    .HasMany(e => e.RedesSociais)  
                    .WithOne(r => r.Evento)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Palestrante>()
                    .HasMany(p => p.RedesSociais)  
                    .WithOne(r => r.Palestrante)
                    .OnDelete(DeleteBehavior.Cascade);   
            
        }
    }
}