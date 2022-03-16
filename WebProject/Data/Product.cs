using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace WebProject.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public Covers Covers { get; set; }
        public Types Types { get; set; }
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public int PublishingYear { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int Amount { get; set; }
        public string Summary { get; set; }
        public string ImageURL { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
