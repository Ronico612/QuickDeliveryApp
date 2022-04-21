using QuickDeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickDeliveryApp.Models
{
    public class Product
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
        public bool IsDeleted { get; set; }

        public virtual AgeProductType AgeProductType { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        //Added Properties only for client!!
        public string ImgSource
        {
            get
            {
                QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
                Random r = new Random();
                string url = $"{proxy.GetBasePhotoUri()}ProductPhotos/{ProductId}.jpg?{r.Next()}";
                return url;
            }
        }
    }
}
