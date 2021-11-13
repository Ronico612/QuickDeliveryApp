using System;
using System.Collections.Generic;
using System.Text;
using QuickDeliveryApp.Services;

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

        //Added Properties only for client!!
        public string ImgSource
        { 
            get
            {
                QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
                return $"{proxy.GetBasePhotoUri()}ShopsPhotos/{ShopId}.png";
            }
        }
    }
}
