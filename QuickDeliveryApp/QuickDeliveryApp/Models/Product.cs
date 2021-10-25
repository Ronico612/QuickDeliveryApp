using System;
using System.Collections.Generic;
using System.Text;

namespace QuickDeliveryApp.Models
{
    class Product
    {
        public Product()
        {
            AllTypesOfPrducts = new HashSet<AllTypesOfPrduct>();
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? ShopId { get; set; }
        public int CountProductInShop { get; set; }
        public int ProductPrice { get; set; }

        public virtual Shop Shop { get; set; }
        public virtual ICollection<AllTypesOfPrduct> AllTypesOfPrducts { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
