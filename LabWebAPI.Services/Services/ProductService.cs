using AutoMapper;
using LabWebAPI.Contracts.Data;
using LabWebAPI.Contracts.Data.Entities;
using LabWebAPI.Contracts.DTO.AdminPanel;
using LabWebAPI.Contracts.DTO.Product;
using LabWebAPI.Contracts.Exceptions;
using LabWebAPI.Contracts.Roles;
using LabWebAPI.Contracts.Services;
using LabWebAPI.Services.Validators;
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
        private readonly UserManager<User> _userManager;

        public ProductService(IMapper mapper,
        IRepository<Product> productRepository,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task DeleteProductAsync(string id, string userId)
        {
            var product = await _productRepository.GetByKeyAsync(id) ?? throw new ProductNotFoundException("Product not found!");
            var _isAdmin = await Validator.IsAdmin(_userManager, userId);
            if (_isAdmin || product.UserWhoCreatedId == userId)
            {
                await _productRepository.DeleteAsync(product);
                await _productRepository.SaveChangesAsync();
            }
            else
            {
                throw new UnauthorizedToProductChangeException("Unauthorized To Product Delete");
            }
        
        }
        public async Task<ProductDTO> GetProductByIdAsync(string id)
        {

            var product = await _productRepository.Query()
       .Include(p => p.UserWhoCreated)
       .FirstOrDefaultAsync(p => p.Id == id)
       ?? throw new ProductNotFoundException("Product not found!");

            var model = new ProductDTO();
           _mapper.Map(product, model);

            return model;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var products = await _productRepository.Query().Include(p => p.UserWhoCreated).ToListAsync();
            var productsInfo = products.Select(product =>
            {

                return new ProductDTO()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,

                    Price = product.Price,
                    PublicationDate = product.PublicationDate,
                      UserWhoCreated = new SimpleUserInfoDTO
                      {
                          Id = product.UserWhoCreated.Id,
                          Name = product.UserWhoCreated.Name,
                          Email = product.UserWhoCreated.Email, 
                          Surname = product.UserWhoCreated.Surname
                      }
                };
            })
            .ToList();
            return productsInfo;
        }

        public async Task<ProductDTO> EditProductAsync(SimpleProductDTO model) {

            var product = await _productRepository.Query()
       .Include(p => p.UserWhoCreated)
       .FirstOrDefaultAsync(p => p.Id == model.Id) ?? throw new ProductNotFoundException("Product not found!");

            _mapper.Map(model, product);

            await _productRepository.UpdateAsync(product);

            await _productRepository.SaveChangesAsync();
            var formattedModel = new ProductDTO();
            _mapper.Map(product, formattedModel);

            return formattedModel;
        }

        public async Task<bool> AddProduct(Product model)
        {

            try
            {

                // Додати продукт до репозиторію
                await _productRepository.AddAsync(model);

                // Зберегти зміни в базі даних
                await _productRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
 
   
}
