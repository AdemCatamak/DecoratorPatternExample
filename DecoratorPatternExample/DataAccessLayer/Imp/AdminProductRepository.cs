using System.Collections.Generic;
using DecoratorPatternExample.Models;

namespace DecoratorPatternExample.DataAccessLayer.Imp
{
    public class AdminProductRepository : IAdminProductRepository
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductDbContext _productDbContext;

        public AdminProductRepository(IProductRepository productRepository, ProductDbContext productDbContext)
        {
            _productRepository = productRepository;
            _productDbContext = productDbContext;
        }

        public Product? GetProductById(string id)
        {
            return _productRepository.GetProductById(id);
        }

        public IEnumerable<Product> GetProducts(int skip, int take)
        {
            return _productRepository.GetProducts(skip, take);
        }

        public string CreateProduct(Product product)
        {
            _productDbContext.Products.Add(product);
            _productDbContext.SaveChanges();
            return product.Id;
        }
    }
}