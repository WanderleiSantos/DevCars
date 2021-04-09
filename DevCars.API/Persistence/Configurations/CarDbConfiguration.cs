using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Persistence.Configurations
{
    public class CarDbConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Car> builder)
        {
            builder
                .HasKey(c => c.Id);
        }
    }
}
