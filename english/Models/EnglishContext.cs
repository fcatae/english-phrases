using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace english.Models
{
    public partial class EnglishContext : DbContext
    {
        public virtual DbSet<Phrases> Phrases { get; set; }
        public virtual DbSet<Translations> Translations { get; set; }
        public virtual DbSet<UserQuestions> UserQuestions { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phrases>(entity =>
            {
                entity.HasKey(e => e.PhraseId)
                    .HasName("PK__Phrases__0DBA0E8200AC3393");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<Translations>(entity =>
            {
                entity.HasKey(e => e.TranslationId)
                    .HasName("PK__Translat__663DA04C39EE93DE");

                entity.HasIndex(e => e.PhraseId)
                    .HasName("idxPhraseId");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.HasOne(d => d.Phrase)
                    .WithMany(p => p.Translations)
                    .HasForeignKey(d => d.PhraseId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Translati__Phras__35BCFE0A");
            });

            modelBuilder.Entity<UserQuestions>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PhraseId })
                    .HasName("PK__UserQues__27536CA4AEB1655D");

                entity.Property(e => e.Difficulty).HasDefaultValueSql("200");

                entity.HasOne(d => d.Phrase)
                    .WithMany(p => p.UserQuestions)
                    .HasForeignKey(d => d.PhraseId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__UserQuest__Phras__403A8C7D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserQuestions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__UserQuest__UserI__3F466844");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CC4C5FA26EAB");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });
        }
    }
}