using DecoratorPatternExample.Models;

namespace DecoratorPatternExample.DataAccessLayer
{
    public interface IAdminProductRepository : IProductRepository
    {
        string CreateProduct(Product product);
    }
}