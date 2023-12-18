using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Contracts.Data.Entities
{
    public class Product : IBaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }    

        public string Description { get; set; } 
        
        public DateTime PublicationDate {  get; set; }

        public decimal Price {  get; set; }

        public string UserWhoCreatedId { get; set; }
        public User UserWhoCreated { get; set; } = null!;
    }
}
