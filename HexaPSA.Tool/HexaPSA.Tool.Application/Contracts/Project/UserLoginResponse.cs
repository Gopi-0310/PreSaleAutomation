using HexaPSA.Tool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Contracts.Project
{
    public class UserLoginResponse
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string EMail { get; set; }
        public bool IsVerified { get; set; }
    }
}
