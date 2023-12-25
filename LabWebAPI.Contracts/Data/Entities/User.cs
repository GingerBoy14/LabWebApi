using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabWebAPI.Contracts.Data.Entities
{
    public class User : IdentityUser, IBaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string ImageAvatarUrl { get; set; }

        [ForeignKey("UserWhoCreatedId")]
        public ICollection<Product> Products { get;  } = new List<Product>();

        [ForeignKey("UserWhoCreatedId")]
        public ICollection<Comment> Comments { get; } = new List<Comment>();
    }
}