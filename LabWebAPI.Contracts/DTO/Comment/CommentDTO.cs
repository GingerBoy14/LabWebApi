using LabWebAPI.Contracts.DTO.AdminPanel;
using LabWebAPI.Contracts.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Contracts.DTO.Comment
{
    public class CommentDTO
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string ProductId { get; set; }
        public string UserWhoCreatedId { get; set; }
        public DateTime PublicationDate { get; set; }
        public SimpleUserInfoDTO UserWhoCreated { get; set; }
        public SimpleProductWithIdDTO Product { get; set; }
    }
}
