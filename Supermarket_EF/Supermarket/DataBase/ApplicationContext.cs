using Microsoft.EntityFrameworkCore;
using Supermarket_EF.Supermarket;
using Microsoft.Extensions.Configuration;

namespace Supermarket_EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SpecificProduct> SpacificProducts { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                 .AddJsonFile($"appsettings.json", true, true).Build();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.ToTable("Category");
                entity.Property(e => e.Name).HasMaxLength(25);
                entity.Property(e => e.Title).HasMaxLength(25);
            });
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.ToTable("Person");
                entity.Property(e => e.Name).HasMaxLength(30);
                entity.Property(e => e.Soname).HasMaxLength(30);
                // Пiдхiд TPH - одна таблиця на всi нащадки
                // Колонка Discriminator визначає тип нащадка
                entity.ToTable("Person").HasDiscriminator<string>("Discriminator").HasValue<Person>("Person");
                entity.ToTable("Person").HasDiscriminator<string>("Discriminator").HasValue<Employee>("Employee");
                entity.ToTable("Person").HasDiscriminator<string>("Discriminator").HasValue<Customer>("Customer");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("date");
                entity.Property(e => e.JoinDate).HasColumnType("date");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.ToTable("Department");
                entity.Property(e => e.Name).HasMaxLength(15);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.PaymentPerHour).HasColumnType("money");
                entity.Property(e => e.StartDate).HasColumnType("date");
                entity.Property(e => e.EndDate).HasColumnType("date");
                entity.HasOne(p => p.Department).WithMany(p => p.Employees)
                    .HasForeignKey(e => e.Department_Id)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.City).HasMaxLength(15);
                entity.Property(e => e.Country).HasMaxLength(25);
                entity.Property(e => e.BranchName).HasMaxLength(6);
                entity.Property(e => e.Address).HasMaxLength(25);
                entity.ToTable("Location");
                entity.HasOne(d => d.Department).WithOne(p => p.Location)
                    .HasForeignKey<Department>(d => d.Location_Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.ToTable("Payment");
                entity.Property(e => e.Type).HasMaxLength(20);
                entity.Property(e => e.Brand).HasMaxLength(20);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.ToTable("Product");
                entity.Property(e => e.Name).HasMaxLength(25);
                entity.Property(e => e.Price).HasColumnType("money");
                entity.Property(e => e.Brand).HasMaxLength(20);
                entity.Property(e => e.OtherDetails).HasMaxLength(500);
                entity.Property(e => e.SKU).HasMaxLength(11);
                entity.HasOne(e => e.Category).WithMany(e => e.Products)
                    .HasForeignKey(e => e.Category_Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.ToTable("Sales");
            });

            modelBuilder.Entity<SpecificProduct>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.ToTable("SpecificProduct");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.ToTable("Ticket");
                entity.HasOne(e => e.Employee).WithMany(e => e.Tickets)
                    .HasForeignKey(p => p.Employee_ID)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}