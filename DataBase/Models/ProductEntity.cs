using System.Data.SqlTypes;

namespace DataBase.Models
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }

        public int? CategoryId { get; set; }

        public virtual CategoryEntity? Category { get; set; }

        public virtual List<StorageEntity> Storages { get; set; } = new List<StorageEntity>();
    }
}
