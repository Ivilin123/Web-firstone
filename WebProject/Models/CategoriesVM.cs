using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebProject.Models
{
    public class CategoriesVM
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<SelectListItem> Products { get; set; }
    }
}
