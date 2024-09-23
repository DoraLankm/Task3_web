using GraphQL_lesson.Models.Dto;

namespace GraphQL_lesson.Abstraction
{
    public interface IStorageServices
    {
        IEnumerable<StorageDto> GetStorages();

        int AddStorage(StorageDto storage);
    }
}
