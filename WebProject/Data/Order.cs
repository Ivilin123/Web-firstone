using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Data
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int AmountOrdered { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PriceOrder { get; set; }
        public DateTime OrderedOn { get; set; }
    }
}
