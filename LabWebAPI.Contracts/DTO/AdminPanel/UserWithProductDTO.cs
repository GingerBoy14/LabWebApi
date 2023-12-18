using LabWebAPI.Contracts.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Contracts.DTO.AdminPanel
{
    public class UserWithProductsDTO : UserInfoDTO
    {
        public List<ProductDTO> Products { get; set; }
    }
}
