using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebProject.Models
{
    public class PublishersVM
    {
        public int Id { get; set; }
        public string NamePublisher { get; set; }

        public virtual ICollection<SelectListItem> Products { get; set; }
    }
}
