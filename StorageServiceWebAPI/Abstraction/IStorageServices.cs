


using Shared.Dto;

namespace StorageServiceWebAPI.Abstraction
{
    public interface IStorageServices
    {
        IEnumerable<StorageDto> GetStorages();

        int AddStorage(StorageDto storage);
    }
}
