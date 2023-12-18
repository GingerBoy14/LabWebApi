using AutoMapper;
using LabWebAPI.Contracts.Data;
using LabWebAPI.Contracts.Data.Entities;
using LabWebAPI.Contracts.DTO.AdminPanel;
using LabWebAPI.Contracts.DTO.Product;
using LabWebAPI.Contracts.Exceptions;
using LabWebAPI.Contracts.Roles;
using LabWebAPI.Contracts.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Services.Services
{
    internal class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _productRepository;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ProductService(IMapper mapper,
        IRepository<Product> productRepository,
        RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _roleManager = roleManager;
        }
    //    public async Task<bool> CanUserDeleteProductAsync(string userId, int productId)
    //    {
     //       var product = await _productRepository.GetByKeyAsync(productId);
    //        return product != null && (product.UserWhoCreated.Id == userId || IsAdmin(userId));
//}

        public async Task DeleteProductAsync(string id)
        {
            var product = await _productRepository.GetByKeyAsync(id) ?? throw new ProductNotFoundException("Product not found!");
            await _productRepository.DeleteAsync(product);
            await _productRepository.SaveChangesAsync();
        }
        public async Task<ProductDTO> GetProductByIdAsync(string id)
        {
           
            var product = await _productRepository.GetByKeyAsync(id) ?? throw new ProductNotFoundException("Product not found!");
            var model = new ProductDTO();
            _mapper.Map(product, model);
          
            return model;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productsInfo = products.Select(product =>
            {

                return new ProductDTO()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,

                    Price = product.Price,
                    PublicationDate = product.PublicationDate
                    
                };
            })
            .ToList();
            return productsInfo;
        }

        public async Task<ProductDTO> EditProductAsync(ProductDTO model) {

            await _productRepository.SaveChangesAsync();

            return model;
        }

    }
 
   
}
