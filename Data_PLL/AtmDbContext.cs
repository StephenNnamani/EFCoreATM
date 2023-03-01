using Data_PLL.Entities;
using Microsoft.EntityFrameworkCore;


namespace Data_PLL
{
    public class AtmDbContext : DbContext
    {
        public AtmDbContext(DbContextOptions<AtmDbContext> options)
            : base(options)
        {

        }
        
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(p =>
            {
                
                p.Property(p => p.FirstName)
                .IsRequired();

                p.Property(p => p.LastName)
                .IsRequired();

                p.Property(p => p.AccountName)
                .HasComputedColumnSql("[FirstName] + ' ' + [LastName] + ' '", stored: true);

                p.Property(p => p.AccountNumber)
                .IsRequired()
                .HasMaxLength(10);

                p.Property(p => p.Balance)
                .IsRequired()
                .HasPrecision(18, 2);

                p.Property(p => p.Pin)
                .IsRequired()
                .HasMaxLength(4);
                

            });

            modelBuilder.Entity<TransactionHistory>(p =>
            {
                p.Property(p => p.Remark)
                .IsRequired(false);

                p.Property(p => p.Balance)
                .IsRequired()
                .HasPrecision(18, 2);
            });

            modelBuilder.Entity<Admin>(p =>
            {
                p.Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(15);

                p.Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(50);
            });


            base.OnModelCreating(modelBuilder);
        }



    }
}
