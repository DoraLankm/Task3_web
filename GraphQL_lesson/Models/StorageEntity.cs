namespace GraphQL_lesson.Models
{
    public class StorageEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }
        public int Count { get; set; }
    }
}
