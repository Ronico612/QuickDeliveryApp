using System;
using System.Collections.Generic;
using System.Text;
using QuickDeliveryApp.Services;

namespace QuickDeliveryApp.Models
{
    public class Shop
    {
        public Shop()
        {
            Products = new HashSet<Product>();
        }

        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public string ShopStreet { get; set; }
        public int ShopHouseNum { get; set; }
        public string ShopCity { get; set; }
        public string ShopPhone { get; set; }
        public int? ShopManagerId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ShopManager ShopManager { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        //Added Properties only for client!!
        public string ImgSource
        { 
            get
            {
                QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
                Random r = new Random();
                string url = url = $"{proxy.GetBasePhotoUri()}ShopsPhotos/{ShopId}.png?{r.Next()}";
                return url;
            }
        }

        public string EmptyImgSource
        {
            get
            {
                QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
                Random r = new Random();
                string url = $"{proxy.GetBasePhotoUri()}ShopsPhotos/EmptyImg.png?{r.Next()}";
                return url;
            }
        }
    }
}
