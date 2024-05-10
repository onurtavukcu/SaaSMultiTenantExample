using WithMultiTenant.Models;

namespace WithMultiTenant.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product CreateProduct(CreateProductRequest request);
        bool DeleteProduct(int id);
    }
}
