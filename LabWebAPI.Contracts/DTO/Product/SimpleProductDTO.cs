using LabWebAPI.Contracts.DTO.AdminPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Contracts.DTO.Product
{
    public class SimpleProductDTO
    {

            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public DateTime PublicationDate { get; set; }
    }
}
