using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSatisProjesi.Models
{
    public class Invoice
    {
        public int ID { get; set; }
        
        [ForeignKey("Customer")]
        public int CustomerId { get; set; } //Customer.cs deki id ile joinlenmesi lazım

        public Customer Customer { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; } //Product.cs deki id ile joinlenmesi lazım

        public Product Product { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public bool IsInUse { get; set; }
    }
}
