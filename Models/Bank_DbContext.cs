using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EntityFramewor_MVC.Models
{
    public partial class Bank_DbContext : DbContext
    {
        public Bank_DbContext()
        {
        }

        public Bank_DbContext(DbContextOptions<Bank_DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<LoginCredential> LoginCredentials { get; set; }
        public virtual DbSet<NewTransaction> NewTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=Bank_Db;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountNo)
                    .HasName("PK__Account__349D9DFD3EBEE965");

                entity.ToTable("Account");

                entity.Property(e => e.AccountHolderName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Account_HolderName");

                entity.Property(e => e.AccountType)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Account_Type")
                    .HasDefaultValueSql("('Savings')");

                entity.Property(e => e.BalanceAmount).HasColumnName("Balance_Amount");

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("DOC")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<LoginCredential>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.PassWord)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Pass_Word");

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NewTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__NewTrans__55433A6B540AB409");

                entity.ToTable("NewTransaction");

                entity.Property(e => e.TransactionAccountNum).HasColumnName("Transaction_AccountNum");

                entity.Property(e => e.TransactionAmount).HasColumnName("Transaction_Amount");

                entity.Property(e => e.TransactionTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Transaction_Time")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.TransactionAccountNumNavigation)
                    .WithMany(p => p.NewTransactions)
                    .HasForeignKey(d => d.TransactionAccountNum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NewTransa__Trans__31EC6D26");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
