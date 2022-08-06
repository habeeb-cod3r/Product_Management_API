using Microsoft.AspNetCore.JsonPatch;
using Product_Management_API.Models;

namespace Product_Management_API.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllProductsAsync();
        Task<ProductModel> GetProductByIdAsync(int ProductId);
        Task<int> AddProductAsync(ProductModel productModel);
        Task UpdateProductAsync(int productId, ProductModel productModel);
        Task UpdateProductPatchAsync(int productId, JsonPatchDocument productModel);
        Task DeleteProductAsync(int productId);
        Task<List<ProductModel>> GetAllDisabledProductsAsync();
        Task<decimal> GetSumOfProductsWithinOneWeekAsync();
    }
}
