

using Shared.Dto;
using StorageServiceWebAPI.Abstraction;

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
