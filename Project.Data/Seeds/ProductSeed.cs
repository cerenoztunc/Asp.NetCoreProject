using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Data.Seeds
{
    public class ProductSeed:IEntityTypeConfiguration<Product>
    {
        private readonly int[] _ids;
        public ProductSeed(int[] ids)
        {
            _ids = ids;
        }

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "Pilot Kalem",
                    Price = 12.50m,
                    Stock = 100,
                    CategoryId = _ids[0]
                },
                new Product
                {
                    Id = 2,
                    Name = "Kurşun Kalem",
                    Price = 10.50m,
                    Stock = 100,
                    CategoryId = _ids[0]
                },
                new Product
                {
                    Id = 3,
                    Name = "Tükenmez Kalem",
                    Price = 15.50m,
                    Stock = 100,
                    CategoryId = _ids[0]
                },
                new Product
                {
                    Id = 4,
                    Name = "Pilot Kalem",
                    Price = 12.50m,
                    Stock = 100,
                    CategoryId = _ids[0]
                },
                new Product
                {
                    Id = 5,
                    Name = "Küçük Boy Defter",
                    Price = 10.50m,
                    Stock = 100,
                    CategoryId = _ids[1]
                },
                 new Product
                 {
                     Id = 6,
                     Name = "Orta Boy Defter",
                     Price = 12.50m,
                     Stock = 100,
                     CategoryId = _ids[1]
                 },
                new Product
                {
                    Id = 7,
                    Name = "Büyük Boy Defter",
                    Price = 15.50m,
                    Stock = 100,
                    CategoryId = _ids[1]
                }
                );
        }
    }
}
