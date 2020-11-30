using System.Collections.Generic;
using System.Linq;
using DecoratorPatternExample.Models;
using Microsoft.Extensions.Logging;

namespace DecoratorPatternExample.DataAccessLayer.Imp
{
    public class DbProductRepository : IProductRepository
    {
        private readonly ProductDbContext _productDbContext;
        private readonly ILogger<DbProductRepository> _logger;

        public DbProductRepository(ProductDbContext productDbContext, ILogger<DbProductRepository> logger)
        {
            _productDbContext = productDbContext;
            _logger = logger;
        }

        public Product? GetProductById(string id)
        {
            _logger.LogInformation($"{nameof(DbProductRepository)}.{nameof(GetProductById)}('{id}') is executed");
            return _productDbContext.Products.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Product> GetProducts(int skip, int take)
        {
            List<Product> products = _productDbContext.Products
                                                      .Skip(skip)
                                                      .Take(take)
                                                      .ToList();

            return products;
        }
    }
}