using System;
using System.Collections.Generic;
using System.Text;

namespace QuickDeliveryApp.Models
{
    class Shop
    {
        public Shop()
        {
            Products = new HashSet<Product>();
        }

        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public string ShopAdress { get; set; }
        public int? ShopManagerId { get; set; }
        public string ShopCity { get; set; }
        public string ShopPhone { get; set; }

        public virtual ShopManager ShopManager { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
