using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickDeliveryApp.Models
{
    public class OrderDetails
    {
        public string ShopName { get; set; }
        public string ShopCity { get; set; }
        public string ShopStreet { get; set; }
        public int? ShopHouseNum { get; set; }
        public string ShopPhone { get; set; }
        public decimal? TotalPrice { get; set; }
        public string OrderDate { get; set; }
        public int OrderId { get; set; }
        public int? OrderStatusId { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public string OrderStreet { get; set; }
        public int? OrderHouseNum { get; set; }
        public string OrderCity { get; set; }
        public User User { get; set; }

        public OrderDetails(Order o)
        {
            this.TotalPrice = o.TotalPrice;
            this.OrderDate = o.OrderDate.ToString("dd/MM/yyyy HH:mm");
            this.OrderId = o.OrderId;
            this.OrderStatusId = o.StatusOrderId;

            if (o.OrderProducts.Count > 0)
            {
                this.ShopName = o.OrderProducts.ToList()[0].Product.Shop.ShopName;
                this.ShopCity = o.OrderProducts.ToList()[0].Product.Shop.ShopCity;
                this.ShopStreet = o.OrderProducts.ToList()[0].Product.Shop.ShopStreet;
                this.ShopHouseNum = o.OrderProducts.ToList()[0].Product.Shop.ShopHouseNum;
                this.ShopPhone = o.OrderProducts.ToList()[0].Product.Shop.ShopPhone;
                this.OrderProducts = new List<OrderProduct>(o.OrderProducts);
                this.OrderStreet = o.OrderStreet;
                this.OrderHouseNum = o.OrderHouseNum;
                this.OrderCity = o.OrderCity;
                this.User = o.User;
            }
        }
    }
}
