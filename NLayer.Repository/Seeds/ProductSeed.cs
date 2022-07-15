using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1, Name= "Faber-Castell 1425 0,7 Mm İğne Uç Tükenmez Mavi 10'lu Kutu", CategoryId = 1, Price = 90, Stock = 100, CreateDate = DateTime.Now},
                new Product { Id = 2, Name = "Rotring 1904508 Versatil Kalem Tikky 0.7 mm Mavi", CategoryId = 1, Price = 50, Stock = 100, CreateDate = DateTime.Now },
                new Product { Id = 3, Name = "Gece Yarısı Kütüphanesi - Matt Haig", CategoryId = 2, Price = 30, Stock = 100, CreateDate = DateTime.Now },
                new Product { Id = 4, Name = "Mynote Flex Neon PP Kapak A4 80 Yaprak 4'lü Defter Seti - 2 Kareli, 2 Çizgili", CategoryId = 3, Price = 80, Stock = 100, CreateDate = DateTime.Now }
                );
        }
    }
}
