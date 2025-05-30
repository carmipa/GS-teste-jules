// File: gsApi/data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using SeuProjetoNET.Models; // Ajuste para o namespace dos seus modelos (ex: gsApi.model)
using System.Collections.Generic;

namespace SeuProjetoNET.Data // Ajuste para o namespace desejado (ex: gsApi.data)
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<EonetEvent> EonetEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração para usar as sequences do Oracle
            modelBuilder.Entity<Cliente>()
                .Property(c => c.IdCliente)
                .HasDefaultValueSql("tb_cliente3_id_cliente_seq.NEXTVAL");

            modelBuilder.Entity<Contato>()
                .Property(c => c.IdContato)
                .HasDefaultValueSql("tb_contato3_id_contato_seq.NEXTVAL");

            modelBuilder.Entity<Endereco>()
                .Property(e => e.IdEndereco)
                .HasDefaultValueSql("tb_endereco3_id_endereco_seq.NEXTVAL");

            modelBuilder.Entity<EonetEvent>()
                .Property(e => e.IdEonet)
                .HasDefaultValueSql("tb_eonet3_id_eonet_seq.NEXTVAL");

            // Relações Many-to-Many
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Contatos)
                .WithMany(t => t.Clientes)
                .UsingEntity<Dictionary<string, object>>(
                    "tb_clientecontato3",
                    r => r.HasOne<Contato>().WithMany().HasForeignKey("tb_contato3_id_contato").HasConstraintName("tb_clientecontato3_tb_contato3_fk"),
                    l => l.HasOne<Cliente>().WithMany().HasForeignKey("tb_cliente3_id_cliente").HasConstraintName("tb_clientecontato3_tb_cliente3_fk"),
                    j => { j.HasKey("tb_cliente3_id_cliente", "tb_contato3_id_contato"); j.ToTable("tb_clientecontato3"); });

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Enderecos)
                .WithMany(e => e.Clientes)
                .UsingEntity<Dictionary<string, object>>(
                    "tb_clienteendereco3",
                    r => r.HasOne<Endereco>().WithMany().HasForeignKey("tb_endereco3_id_endereco").HasConstraintName("tb_clienteendereco3_tb_endereco3_fk"),
                    l => l.HasOne<Cliente>().WithMany().HasForeignKey("tb_cliente3_id_cliente").HasConstraintName("tb_clienteendereco3_tb_cliente3_fk"),
                    j => { j.HasKey("tb_cliente3_id_cliente", "tb_endereco3_id_endereco"); j.ToTable("tb_clienteendereco3"); });

            modelBuilder.Entity<Endereco>()
                .HasMany(e => e.EventosEonet)
                .WithMany(ev => ev.Enderecos)
                .UsingEntity<Dictionary<string, object>>(
                    "tb_enderecoeventos3",
                    r => r.HasOne<EonetEvent>().WithMany().HasForeignKey("tb_eonet3_id_eonet").HasConstraintName("tb_enderecoeventos3_tb_eonet3_fk"),
                    l => l.HasOne<Endereco>().WithMany().HasForeignKey("tb_endereco3_id_endereco").HasConstraintName("tb_enderecoeventos3_tb_endereco3_fk"),
                    j => { j.HasKey("tb_endereco3_id_endereco", "tb_eonet3_id_eonet"); j.ToTable("tb_enderecoeventos3"); });
        }
    }
}