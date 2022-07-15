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
    internal class ProductFeatureSeed : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasData(
                new ProductFeature { Id = 1, Color = "Kırmızı", Width = 100, Height = 100, ProductId = 1 },
                new ProductFeature { Id = 2, Color = "Mavi", Width = 10, Height = 30, ProductId = 2 },
                new ProductFeature { Id = 3, Color = "Turuncu", Width = 50, Height = 90, ProductId = 3 },
                new ProductFeature { Id = 4, Color = "Mor", Width = 14, Height = 57, ProductId = 4 }
                );
        }
    }
}
