using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Contracts.DTO.Comment
{
    public class SimpleCommentDTO
    {

        public string Text { get; set; }
        public string ProductId { get; set; }
        public DateTime PublicationDate { get; set; }
    }
    public class SimpleCommentWithUserIdDTO
    {

        public string Text { get; set; }
        public string ProductId { get; set; }
        public string UserWhoCreatedId { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}
