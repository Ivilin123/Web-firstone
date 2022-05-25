using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebProject.Models
{
    public class OrdersVM
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }
        public List<SelectListItem> Products { get; set; }

        public int AmountOrdered { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal PriceOrder { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Дата на закупуване: ")]
        public DateTime OrderedOn { get; set; }
    }
}
