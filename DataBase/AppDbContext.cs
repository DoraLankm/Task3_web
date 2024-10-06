using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    public class AppDbContext: DbContext
    {

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<StorageEntity> Storages { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<ProductEntity>(entity =>
            {
                // Указываем имя таблицы для сущности Product
                entity.ToTable("Products");

                // Настраиваем первичный ключ
                entity.HasKey(x => x.Id).HasName("Product_ID");

                // Создаём уникальный индекс на поле Name
                entity.HasIndex(x => x.Name).IsUnique();

                // Настройка для свойства Name
                entity.Property(e => e.Name)
                    .HasColumnName("ProductName")     // Имя колонки в базе данных
                    .HasMaxLength(255)                // Ограничение длины строки
                    .IsRequired();                    // Поле обязательно для заполнения

                // Настройка для свойства Description
                entity.Property(e => e.Description)
                    .HasColumnName("Description")     // Имя колонки в базе данных
                    .HasMaxLength(255)                // Ограничение длины строки
                    .IsRequired();                    // Поле обязательно для заполнения

                // Настройка для свойства Cost (цена)
                entity.Property(e => e.Cost)
                    .HasColumnName("Cost")            // Имя колонки в базе данных
                    .IsRequired();      // Поле обязательно для заполнения


                // Настройка связи с сущностью Category (многие к одному)
                entity.HasOne(x => x.Category)         // Один продукт связан с одной категорией
                    .WithMany(c => c.Products);         // Одна категория может содержать много продуктов


            });

            modelBuilder.Entity<CategoryEntity>(entity =>
            {
                // Указываем имя таблицы для сущности Category
                entity.ToTable("Categories");

                // Настраиваем первичный ключ
                entity.HasKey(x => x.Id).HasName("PK_Category");

                // Создаём уникальный индекс на поле Name
                entity.HasIndex(x => x.Name).IsUnique().HasDatabaseName("Category_Name");

                // Настройка для свойства Name
                entity.Property(e => e.Name)
                    .HasColumnName("CategoryName")     // Имя колонки в базе данных
                    .HasMaxLength(255)                 // Ограничение длины строки
                    .IsRequired();                     // Поле обязательно для заполнения

                // Настройка для свойства Description
                entity.Property(e => e.Description)
                    .HasColumnName("Description")      // Имя колонки в базе данных
                    .HasMaxLength(255)                 // Ограничение длины строки
                    .IsRequired();                     // Поле обязательно для заполнения
            });

            modelBuilder.Entity<StorageEntity>(entity =>
            {
                // Указываем имя таблицы для сущности Storage
                entity.ToTable("Storages");

                // Настраиваем первичный ключ
                entity.HasKey(x => x.Id).HasName("PK_Storage");

                // Настройка для свойства Name
                entity.Property(e => e.Name)
                    .HasColumnName("StorageName")      // Имя колонки в базе данных
                    .HasMaxLength(255)                 // Ограничение длины строки
                    .IsRequired();                     // Поле обязательно для заполнения

                // Настройка для свойства Description
                entity.Property(e => e.Description)
                    .HasColumnName("Description")      // Имя колонки в базе данных
                    .HasMaxLength(255)                 // Ограничение длины строки
                    .IsRequired();                     // Поле обязательно для заполнения

                // Связь "один к одному" с Product
                entity.HasOne(s => s.Product)
                    .WithMany(p => p.Storages);

            });
        }
    }
}
