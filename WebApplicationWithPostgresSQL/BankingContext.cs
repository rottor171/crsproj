using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplicationWithPostgresSQL
{
    public partial class BankingContext : DbContext
    {
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Deposit> Deposit { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?//LinkId=723263 for guidance on storing connection strings.
        //    optionsBuilder.UseNpgsql(@"Host=localhost;Database=sharpPrg;Username=jenya;Password=123");
        //"Host=localhost;Database=lol;Username=postgres;Password=7355608"
        //}
        public BankingContext(DbContextOptions<BankingContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(e => e.BranchNo)
                    .HasName("PK_Branch");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnType("bpchar")
                    .HasMaxLength(30);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnType("bpchar")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClientNo)
                    .HasName("PK_Client");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("FName")
                    .HasColumnType("bpchar")
                    .HasMaxLength(30);

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasColumnName("LName")
                    .HasColumnType("bpchar")
                    .HasMaxLength(30);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnType("bpchar")
                    .HasMaxLength(14);

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasColumnType("bpchar")
                    .HasMaxLength(1);
            });

            modelBuilder.Entity<Deposit>(entity =>
            {
                entity.HasKey(e => e.DepositNo)
                    .HasName("PK_Deposit");

                entity.Property(e => e.Commentary)
                    .IsRequired()
                    .HasColumnType("bpchar")
                    .HasMaxLength(128);

                entity.Property(e => e.DateOfBegin).HasColumnType("date");

                entity.Property(e => e.DateOfEnd).HasColumnType("date");

                entity.HasOne(d => d.DepClientNoNavigation)
                    .WithMany(p => p.Deposit)
                    .HasForeignKey(d => d.DepClientNo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Deposit_DepClientNo_fkey");

                entity.HasOne(d => d.DepStaffNoNavigation)
                    .WithMany(p => p.Deposit)
                    .HasForeignKey(d => d.DepStaffNo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Deposit_DepStaffNo_fkey");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(e => e.StaffNo)
                    .HasName("PK_Staff");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("FName")
                    .HasColumnType("bpchar")
                    .HasMaxLength(30);

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasColumnName("LName")
                    .HasColumnType("bpchar")
                    .HasMaxLength(30);

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasColumnType("bpchar")
                    .HasMaxLength(1);

                entity.HasOne(d => d.StaffBranchNoNavigation)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.StaffBranchNo)
                    .HasConstraintName("Staff_StaffBranchNo_fkey");
            });
        }
    }
}