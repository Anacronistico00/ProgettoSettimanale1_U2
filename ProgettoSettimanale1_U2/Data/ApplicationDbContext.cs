using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProgettoSettimanale1_U2.Models;

namespace ProgettoSettimanale1_U2.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Anagrafiche> Anagrafiches { get; set; }

    public virtual DbSet<TipiViolazione> TipiViolaziones { get; set; }

    public virtual DbSet<Verbali> Verbalis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ANACRONISTICO\\SQLEXPRESS;Database=Progetto1_U2;User Id=sa;Password=sa;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anagrafiche>(entity =>
        {
            entity.HasKey(e => e.IdAnagrafica).HasName("PK__Anagrafi__11B12B19F35A7E21");

            entity.Property(e => e.IdAnagrafica).ValueGeneratedNever();
        });

        modelBuilder.Entity<TipiViolazione>(entity =>
        {
            entity.HasKey(e => e.IdViolazione).HasName("PK__Tipi Vio__30BEFB3BB9616BBE");

            entity.Property(e => e.IdViolazione).ValueGeneratedNever();
        });

        modelBuilder.Entity<Verbali>(entity =>
        {
            entity.HasKey(e => new { e.IdVerbale, e.IdAnagrafica, e.IdViolazione }).HasName("PK_IdVerbaliComposto");

            entity.HasOne(d => d.IdAnagraficaNavigation).WithMany(p => p.Verbalis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Verbale_Anagrafica");

            entity.HasOne(d => d.IdViolazioneNavigation).WithMany(p => p.Verbalis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Verbale_Violazione");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
