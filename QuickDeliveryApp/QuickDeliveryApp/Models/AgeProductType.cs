using System;
using System.Collections.Generic;
using System.Text;

namespace QuickDeliveryApp.Models
{
    public class AgeProductType
    {
        public AgeProductType()
        {
            Products = new HashSet<Product>();
        }

        public int AgeProductTypeId { get; set; }
        public string AgeProductTypeName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
