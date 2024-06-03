using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSatisProjesi.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Price { get; set; }
        public bool IsInUse { get; set; }
    }
}
