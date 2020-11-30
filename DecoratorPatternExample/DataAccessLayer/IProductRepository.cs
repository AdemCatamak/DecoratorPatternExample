using System.Collections.Generic;
using DecoratorPatternExample.Models;

namespace DecoratorPatternExample.DataAccessLayer
{
    public interface IProductRepository
    {
        Product? GetProductById(string id);
        IEnumerable<Product> GetProducts(int skip, int take);
    }
}