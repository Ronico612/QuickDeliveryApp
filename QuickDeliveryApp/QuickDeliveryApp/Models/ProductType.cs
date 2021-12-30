using System;
using System.Collections.Generic;
using System.Text;

namespace QuickDeliveryApp.Models
{
    public class ProductType
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
        }

        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
