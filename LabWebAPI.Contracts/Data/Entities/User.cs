using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Contracts.Data.Entities
{
    public class User : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string ImageAvatarUrl { get; set; }
    }
}
