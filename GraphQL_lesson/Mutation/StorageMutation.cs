using GraphQL_lesson.Abstraction;
using GraphQL_lesson.Models.Dto;

namespace GraphQL_lesson.Mutation
{
    public class StorageMutation
    {
        public int AddStorage(StorageDto storageDto, [Service] IStorageServices service)
        {
            return service.AddStorage(storageDto); 
        }
    }
}
