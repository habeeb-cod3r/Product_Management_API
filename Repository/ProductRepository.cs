using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Product_Management_API.Data;
using Product_Management_API.Models;
using System.Data;

namespace Product_Management_API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        private readonly IMapper _mapper;

        //ProductRepository Constructor
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
            var product = await _context.Products.FindAsync(productId);
            return _mapper.Map<ProductModel>(product);
        }
        public async Task<int> AddProductAsync(ProductModel productModel)
        {
            var product = new Product()
            {
                ProductName = productModel.ProductName,
                Price = productModel.Price,
                IsDisabled = productModel.IsDisabled,
                IsDeleted = productModel.IsDeleted,
                CreatedDate = DateTimeOffset.UtcNow
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task UpdateProductAsync(int productId, ProductModel productModel)
        {       
            var product = new Product()
            {
                Id = productId,
                ProductName = productModel.ProductName,
                Price = productModel.Price,
                IsDisabled = productModel.IsDisabled,
                IsDeleted = productModel.IsDeleted,
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

        public async Task<List<ProductModel>> GetAllDisabledProductsAsync()
        {
            var disabledRecords = await _context.Products
                .Where(e => e.IsDisabled == true)
                .OrderByDescending(x => x.CreatedDate).ToListAsync();
            return _mapper.Map<List<ProductModel>>(disabledRecords);
        }

        public async Task<decimal> GetSumOfProductsWithinOneWeekAsync()
        {
            var SumOfProductsWithinOneWeek = 0.0m;
            var ProductsWithinOneWeek = await _context.Products
                .Where(x => x.CreatedDate >= DateTimeOffset.Now.AddDays(-7)).ToListAsync();
            foreach (var product in ProductsWithinOneWeek)
            {
                SumOfProductsWithinOneWeek += product.Price;
            }
            return SumOfProductsWithinOneWeek;
        }
    }
}
