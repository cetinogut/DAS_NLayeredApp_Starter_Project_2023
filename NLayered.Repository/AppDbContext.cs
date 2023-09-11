using Microsoft.EntityFrameworkCore;
using NLayered.Core;
using NLayered.Repository.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayered.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; } // ProductFeature mutlaka bir Product'a bağlı olacağından bestpractice olarak burası kapatılabilir ve bir Product create edildiğinde onun ProductFeature'u da doldurulabilir.


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // bu satır ile ilave yazdığımız configuration dosyalrını okur. Bu satır olmazsa congf classlarına yazdığımız modelBuilderler okunmaz ve EF Core'un default değerleri devreye girer.

            // modelBuilder.ApplyConfiguration(new ProductConfiguration()); //yukarıdaki satır ile bassemblydeki bütün conf classları çalıştırılırken, bu satır ile de istediğimiz conf class'ı apply edebiliirz.

            //modelBuilder.Entity<Category>().HasKey(c => c.Id); // burada Category'nin Id columnunun Key olduğunu belirtebiliriz. Aslında EF Core  Id isimli bir propery'i zaten otomatik olarak Key alır. Ama eğer biz farklı bir isimle property belirtti isek, mesela Category_Id diyelim, bunun bu satırda CAtegory_Id Key'dir diyebilirz. Ama bu kirlilik demek, bunları entity bazında conf class ile yapmak daha doğrudur, best practice..


            //Seeds klasöründe CategorySeed ve ProductSeed oluşturmuştuk, Burada da aşağıdaki gibi ProductFeature'u seed ediyoruz
            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature()
            {
                Id = 1,
                Color = "Kırmızı",
                Height = 100,
                Width = 200,
                ProductId = 1


            },
            new ProductFeature()
            {
                Id = 2,
                Color = "Mavi",
                Height = 300,
                Width = 500,
                ProductId = 2


            }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
