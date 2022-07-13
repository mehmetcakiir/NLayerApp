using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {

        // Veri tabanı yolunun startup dosyasından verilebilmesi için gerekli kod parçacığı
        public AppDbContext(DbContextOptions<DbContext> options) : base(options)
        {
            
        }

        // Herbir Entity için DbSet oluşturulur.

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductFeature> ProductFeatures { get; set; }


        // Model oluşurken çalışacak olan method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Çalışmış olduğum Assemby i tara
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }


    }
}
