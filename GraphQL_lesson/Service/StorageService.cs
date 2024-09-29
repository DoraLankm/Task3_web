using AutoMapper;
using GraphQL_lesson.Abstraction;
using GraphQL_lesson.Models;
using GraphQL_lesson.Models.Dto;
using HotChocolate.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client.Extensions.Msal;

namespace GraphQL_lesson.Service
{
    public class StorageService: IStorageServices
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        private readonly IMemoryCache _cache;


        public StorageService(AppDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddStorage(StorageDto storage)
        {
            var entity = _mapper.Map<StorageEntity>(storage);
            _context.Storages.Add(entity);
            _context.SaveChanges();
            _cache.Remove("storages");
            return entity.Id;
        }

        public IEnumerable<StorageDto> GetStorages()
        {
            if(_cache.TryGetValue("storages", out List<StorageDto> storages))
                {
                return storages;

            }

            storages = _context.Storages.Select(x => _mapper.Map<StorageDto>(x)).ToList();
            _cache.Set("storages", storages, TimeSpan.FromMinutes(30));
            return storages;
        }
    }
}
