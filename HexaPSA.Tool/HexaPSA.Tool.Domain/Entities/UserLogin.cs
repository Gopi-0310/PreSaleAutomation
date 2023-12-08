using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Domain.Entities
{
    public class UserLogin : EntityBase<Guid>
    {
        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public string? Email { get; set; }
        public bool? IsVerified { get; set; }
    }
}
