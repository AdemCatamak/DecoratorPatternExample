using System;
using System.Collections.Generic;
using System.Linq;
using DecoratorPatternExample.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace DecoratorPatternExample.DataAccessLayer.Imp
{
    public class CacheProductRepository : IProductRepository
    {
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _cacheTimeLimit = TimeSpan.FromSeconds(15);
        private readonly ILogger<CacheProductRepository> _logger;

        public CacheProductRepository(IProductRepository productRepository, IMemoryCache memoryCache, ILogger<CacheProductRepository> logger)
        {
            _productRepository = productRepository;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public Product? GetProductById(string id)
        {
            _logger.LogInformation($"{nameof(CacheProductRepository)}.{nameof(GetProductById)}('{id}') is executed");
            _memoryCache.TryGetValue(id, out Product? product);

            if (product != null)
            {
                _logger.LogInformation($"{nameof(CacheProductRepository)}.{nameof(GetProductById)}('{id}') is returning response. There is no need to call {nameof(DbProductRepository)}");
                return product;
            }

            product = _productRepository.GetProductById(id);

            if (product != null)
                _memoryCache.Set(product.Id, product, _cacheTimeLimit);

            return product;
        }

        public IEnumerable<Product> GetProducts(int skip, int take)
        {
            List<Product> products = _productRepository.GetProducts(skip, take)
                                                       .ToList();

            foreach (var product in products)
            {
                _memoryCache.Set(product.Id, product, _cacheTimeLimit);
            }

            return products;
        }
    }
}