﻿using AutoMapper;
using GraphQL_lesson.Abstraction;
using GraphQL_lesson.Models;
using GraphQL_lesson.Models.Dto;
using Microsoft.Extensions.Caching.Memory;

namespace GraphQL_lesson.Service
{
    public class ProductService : IProductServices
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        private readonly IMemoryCache _cache;

        public ProductService(AppDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }
        public int AddProduct(ProductDto product)
        {
            using (_context)
            {
                var entity = _mapper.Map<ProductEntity>(product);
                _context.Products.Add(entity);
                _context.SaveChanges();
                _cache.Remove("products");
                return entity.Id;
            }
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            using (_context)
            {
                if(_cache.TryGetValue("products", out List <ProductDto> products))
                {
                    return products;

                }

                products = _context.Products.Select(x => _mapper.Map<ProductDto>(x)).ToList();
                _cache.Set("products", products,TimeSpan.FromMinutes(30));
                return products;
            }
        }
    }
}
