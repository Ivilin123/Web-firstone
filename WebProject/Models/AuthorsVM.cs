using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebProject.Models
{
    public class AuthorsVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<SelectListItem> Products { get; set; }
    }
}
