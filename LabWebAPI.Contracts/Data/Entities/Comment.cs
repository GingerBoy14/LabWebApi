using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Contracts.Data.Entities
{
    public class Comment : IBaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Text { get; set; }


        public DateTime PublicationDate { get; set; }

        public string? ProductId { get; set; }
        public Product? Product { get; set; }

        public string? UserWhoCreatedId { get; set; }
        public User? UserWhoCreated { get; set; }
    }
}
