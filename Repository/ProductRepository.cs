using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Product_Management_API.Data;
using Product_Management_API.Models;

namespace Product_Management_API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ProductDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            var records = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductModel>>(records);
        }

        public async Task<ProductModel> GetProductByIdAsync(int productId)
        {
            /*var record = await _context.Products.Where(x => x.Id == ProductId).Select(x => new ProductModel()
            {
                Id = x.Id,
                ProductName = x.ProductName,
                Price = x.Price,
                isDisabled = x.isDisabled,
                isDeleted = x.isDeleted,
                CreatedDate = x.CreatedDate
            }).FirstOrDefaultAsync();

            return record;*/

            var product = await _context.Products.FindAsync(productId);
            return _mapper.Map<ProductModel>(product);
        }
        public async Task<int> AddProductAsync(ProductModel productModel)
        {
            var product = new Product()
            {
                ProductName = productModel.ProductName,
                Price = productModel.Price,
                isDisabled = productModel.isDisabled,
                isDeleted = productModel.isDeleted,
                CreatedDate = DateTimeOffset.UtcNow
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task UpdateProductAsync(int productId, ProductModel productModel)
        {
            /*var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.ProductName = productModel.ProductName;
                product.Price = productModel.Price;
                product.isDeleted = productModel.isDeleted;
                product.isDisabled = productModel.isDisabled;

                await _context.SaveChangesAsync();
            }*/
            var product = new Product()
            {
                Id = productId,
                ProductName = productModel.ProductName,
                Price = productModel.Price,
                isDisabled = productModel.isDisabled,
                isDeleted = productModel.isDeleted,
            };
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductPatchAsync(int productId, JsonPatchDocument productModel)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                productModel.ApplyTo(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = new Product() { Id = productId };
           
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

           
        }
    }
}
