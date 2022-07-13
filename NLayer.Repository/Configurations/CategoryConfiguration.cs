using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            // Id alanı PrimeryKey olarak belirlenir
            builder.HasKey(x => x.Id);

            // Id alanı 1,1 olarak otomatik artan şeklinde olması belirlenir
            builder.Property(x => x.Id).UseIdentityColumn();

            // Name alanı boş geçilemez ve maksimum 50 garekter olabilir şeklinde belirlenir
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            // Tablo ismi belirlenir.
            builder.ToTable("Categories");

        }
    }
}
