using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FiadorHYCM032024.Models
{
    public partial class FiadorHYCM032024Context : DbContext
    {
        public FiadorHYCM032024Context()
        {
        }

        public FiadorHYCM032024Context(DbContextOptions<FiadorHYCM032024Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Fiador> Fiadors { get; set; } = null!;
        public virtual DbSet<ReferenciasFamiliare> ReferenciasFamiliares { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=FiadorHYCM032024;Integrated Security=True;Encrypt=False");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fiador>(entity =>
            {
                entity.ToTable("Fiador");

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.IngresoMensual).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ocupacion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(9)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ReferenciasFamiliare>(entity =>
            {
                entity.Property(e => e.Direccion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Relacion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(9)
                    .IsFixedLength();

                entity.HasOne(d => d.IdFiadorNavigation)
                    .WithMany(p => p.ReferenciasFamiliare)
                    .HasForeignKey(d => d.IdFiador)
                    .HasConstraintName("FK__Referenci__IdFia__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
