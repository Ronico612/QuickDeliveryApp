using System;
using System.Collections.Generic;
using System.Text;

namespace QuickDeliveryApp.Models
{
    class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? ShopId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? AgeProductTypeId { get; set; }
        public int CountProductInShop { get; set; }
        public decimal ProductPrice { get; set; }

        public virtual AgeProductType AgeProductType { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
