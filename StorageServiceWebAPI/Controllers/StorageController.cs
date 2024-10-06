
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;
using StorageServiceWebAPI.Abstraction;


namespace StorageServiceWebAPI.Controllers
{
    [ApiController]
    [Route("storage")]
    public class StorageController : ControllerBase
    {
        private readonly IStorageServices _storageService;

        public StorageController(IStorageServices storageService)
        {
            _storageService = storageService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StorageDto>> GetStorages()
        {
            var storages = _storageService.GetStorages();
            return Ok(storages);
        }

        [HttpPost]
        public ActionResult<int> AddStorage([FromBody] StorageDto storageDto)
        {
            var id = _storageService.AddStorage(storageDto);
            return Ok(id);
        }

        
    }
}
