using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebProject.Data
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        
        public Roles Roles { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
