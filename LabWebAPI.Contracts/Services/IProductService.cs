﻿using LabWebAPI.Contracts.DTO.AdminPanel;
using LabWebAPI.Contracts.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Contracts.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(string id);
        Task<ProductDTO> EditProductAsync(ProductDTO productDTO);
        Task DeleteProductAsync(string id);
    }
}