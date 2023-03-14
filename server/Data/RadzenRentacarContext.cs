using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using CarTest.Models.RadzenRentacar;

namespace CarTest.Data
{
    public partial class RadzenRentacarContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IHttpContextAccessor httpAccessor;

        public RadzenRentacarContext(IHttpContextAccessor httpAccessor, DbContextOptions<RadzenRentacarContext> options):base(options)
        {
            this.httpAccessor = httpAccessor;
        }

        public RadzenRentacarContext(IHttpContextAccessor httpAccessor)
        {
            this.httpAccessor = httpAccessor;
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CarTest.Models.RadzenRentacar.Car>()
                  .HasOne(i => i.Category)
                  .WithMany(i => i.Cars)
                  .HasForeignKey(i => i.CategoryId)
                  .HasPrincipalKey(i => i.Id);
            builder.Entity<CarTest.Models.RadzenRentacar.Order>()
                  .HasOne(i => i.Car)
                  .WithMany(i => i.Orders)
                  .HasForeignKey(i => i.CarId)
                  .HasPrincipalKey(i => i.Id);

            this.OnModelBuilding(builder);
        }


        public DbSet<CarTest.Models.RadzenRentacar.AvailableCar> AvailableCars
        {
          get;
          set;
        }

        public DbSet<CarTest.Models.RadzenRentacar.Car> Cars
        {
          get;
          set;
        }

        public DbSet<CarTest.Models.RadzenRentacar.Category> Categories
        {
          get;
          set;
        }

        public DbSet<CarTest.Models.RadzenRentacar.Order> Orders
        {
          get;
          set;
        }
    }
}
