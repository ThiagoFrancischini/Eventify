using Eventify.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Infrastructure.Data
{
    public class EventifyDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Ingresso> Ingressos { get; set; }
        public DbSet<CategoriaIngresso> CategoriasIngresso { get; set; }

        public EventifyDbContext(DbContextOptions<EventifyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Nome).IsRequired().HasMaxLength(150);
                entity.Property(u => u.Cpf).IsRequired().HasMaxLength(11);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);

                entity.HasOne(u => u.Endereco)
                      .WithOne()
                      .HasForeignKey<Usuario>(u => u.EnderecoId)
                      .IsRequired();
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Rua).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Bairro).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Numero).HasMaxLength(20);
                entity.Property(e => e.Cep).IsRequired().HasMaxLength(8);

                entity.HasOne(e => e.Cidade)
                      .WithMany()
                      .HasForeignKey(e => e.CidadeId)
                      .IsRequired();
            });

            modelBuilder.Entity<Cidade>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Nome).IsRequired().HasMaxLength(100);

                entity.HasOne(c => c.Estado)
                      .WithMany()
                      .HasForeignKey(c => c.EstadoId)
                      .IsRequired();
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome).IsRequired().HasMaxLength(75);
                entity.Property(e => e.Sigla).IsRequired().HasMaxLength(2);
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Descricao).HasMaxLength(1000);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Categoria).HasMaxLength(100);

                entity.HasMany(e => e.CategoriasIngressos)
                      .WithOne(c => c.Evento)
                      .HasForeignKey(c => c.EventoId);

                entity.HasOne(i => i.UsuarioCriacao)
                      .WithMany()
                      .HasForeignKey(i => i.UsuarioCriacaoId)
                      .IsRequired();
            });

            modelBuilder.Entity<CategoriaIngresso>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Titulo).IsRequired().HasMaxLength(150);
                entity.Property(c => c.Descricao).HasMaxLength(500);
                entity.Property(c => c.Valor).IsRequired().HasColumnType("decimal(10,2)");

                entity.HasMany(c => c.Ingressos)
                      .WithOne(i => i.CategoriaIngresso)
                      .HasForeignKey(i => i.CategoriaIngressoId);
            });

            modelBuilder.Entity<Ingresso>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.Property(i => i.Codigo).IsRequired().HasMaxLength(100);

                entity.HasOne(i => i.UsuarioCompra)
                      .WithMany()
                      .HasForeignKey(i => i.UsuarioCompraId)
                      .IsRequired();

                entity.HasOne(i => i.Evento)
                      .WithMany()
                      .HasForeignKey(i => i.EventoId)
                      .IsRequired();
            });
        }
    }
}
