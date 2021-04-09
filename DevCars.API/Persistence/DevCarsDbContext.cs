using DevCars.API.Entities;
using DevCars.API.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DevCars.API.Persistence
{
    public class DevCarsDbContext :DbContext
    {

        public DevCarsDbContext(DbContextOptions<DevCarsDbContext> options) :  base(options)
        {            
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ExtraOrderItem> ExtraOrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            /*modelBuilder.ApplyConfiguration(new CarDbConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerDbConfiguration());
            modelBuilder.ApplyConfiguration(new ExtraOrderDbConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDbConfiguration());*/

            /*modelBuilder.Entity<Car>()
                  .Property(c => c.Brand)
                  .HasColumnName("Marca");

            modelBuilder.Entity<Customer>()
                   .HasKey(c => c.Id);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                 .WithOne(o => o.Customer)
                 .HasForeignKey(o => o.IdCustomer)                
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.ExtraItems)
                .WithOne()
                .HasForeignKey(e => e.IdOrder)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Car)
                .WithOne()
                .HasForeignKey<Order>(o => o.IdCar)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExtraOrderItem>()
                .HasKey(e => e.Id); */
        }
    }
}
