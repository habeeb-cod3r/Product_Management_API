﻿using AutoMapper;
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
                IsDisabled = productModel.isDisabled,
                IsDeleted = productModel.isDeleted,
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
                IsDisabled = productModel.isDisabled,
                IsDeleted = productModel.isDeleted,
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

        /*public IQueryable<ProductModel> GetDisableProduct()
        {
            return _context.Products.AsQueryable();
        }
        public async Task<List<ProductModel>> GetAllDisabledProductsAsync(bool isDisabled)
        {
            //var disabledRecords = new Product() { IsDisa/*bled = isDisabled };
            var disabledRecords =  GetDisableProduct().Where(e => e.IsDisabled == true).ToListAsync();
            return await disabledRecords; ;*/

            

        //}
    }
}
