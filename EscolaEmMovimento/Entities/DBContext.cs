using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebService.Entities
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        #region Tabelas
        public virtual DbSet<Aluno> Aluno { get; set; }
        public virtual DbSet<Responsavel> Responsavel { get; set; }
        public virtual DbSet<AlunoResponsavel> AlunoResponsavel { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tabela Aluno
            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.ToTable("aluno");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DataNascimento)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Segmento)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Foto)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
            #endregion

            #region Tabela Responsavel
            modelBuilder.Entity<Responsavel>(entity =>
            {
                entity.ToTable("responsavel");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DataNascimento)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
            #endregion

            #region Tabela Aluno x Responsavel
            modelBuilder.Entity<AlunoResponsavel>(entity =>
            {
                entity.ToTable("aluno_responsavel").HasNoKey();

                entity.Property(e => e.IdAluno)
                    .IsRequired()
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdResponsavel).HasColumnType("int(11)");

                entity.Property(e => e.Parentesco)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });
            #endregion

            OnModelCreatingPartial(modelBuilder);
        }
    }
}
