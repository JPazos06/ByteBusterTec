using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BBT_EstablecimientosDeSalud.Models.DB;

public partial class BbtEstablecimientosDeSaludContext : DbContext
{
    public BbtEstablecimientosDeSaludContext()
    {
    }

    public BbtEstablecimientosDeSaludContext(DbContextOptions<BbtEstablecimientosDeSaludContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Busquedum> Busqueda { get; set; }

    public virtual DbSet<Ep> Eps { get; set; }

    public virtual DbSet<EpsEstablecimientoDeSalud> EpsEstablecimientoDeSaluds { get; set; }

    public virtual DbSet<EstablecimientoDeSalud> EstablecimientoDeSaluds { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Valoracion> Valoracions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database=BBT_EstablecimientosDeSalud; Trusted_Connection=True; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Busquedum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Busqueda__3213E83F61F3A4F9");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.TerminoBusqueda)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("termino_busqueda");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Busqueda)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Busqueda__usuari__412EB0B6");
        });

        modelBuilder.Entity<Ep>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EPS__3213E83FCE3714EB");

            entity.ToTable("EPS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<EpsEstablecimientoDeSalud>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EPS_Esta__3213E83F49B687B2");

            entity.ToTable("EPS_EstablecimientoDeSalud");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EpsId).HasColumnName("eps_id");
            entity.Property(e => e.EstablecimientoId).HasColumnName("establecimiento_id");

            entity.HasOne(d => d.Eps).WithMany(p => p.EpsEstablecimientoDeSaluds)
                .HasForeignKey(d => d.EpsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EPS_Estab__eps_i__4222D4EF");

            entity.HasOne(d => d.Establecimiento).WithMany(p => p.EpsEstablecimientoDeSaluds)
                .HasForeignKey(d => d.EstablecimientoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EPS_Estab__estab__4316F928");
        });

        modelBuilder.Entity<EstablecimientoDeSalud>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Establec__3213E83FB0928A6C");

            entity.ToTable("EstablecimientoDeSalud");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imagen");
            entity.Property(e => e.Latitud)
                .HasColumnType("decimal(12, 8)")
                .HasColumnName("latitud");
            entity.Property(e => e.Longitud)
                .HasColumnType("decimal(12, 8)")
                .HasColumnName("longitud");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3213E83F0EABE080");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contrasena");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imagen");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Valoracion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Valoraci__3213E83F698EA89B");

            entity.ToTable("Valoracion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comentario)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("comentario");
            entity.Property(e => e.EstablecimientoId).HasColumnName("establecimiento_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.Valoracion1).HasColumnName("valoracion");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Valoracions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Valoracio__usuar__44FF419A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
