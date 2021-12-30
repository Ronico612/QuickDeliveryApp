using System;
using System.Collections.Generic;
using System.Text;

namespace QuickDeliveryApp.Models
{
    public class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string UserFname { get; set; }
        public string UserLname { get; set; }
        public string UserPassword { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public DateTime? UserBirthDate { get; set; }
        public bool IsAdmin { get; set; }
        public bool? HasDiscount { get; set; }
        public string UserAddress { get; set; }
        public string UserCity { get; set; }
        public string NumCreditCard { get; set; }
        public string NumCode { get; set; }
        public DateTime? ValidityCreditCard { get; set; }

        public virtual DeliveryPerson DeliveryPerson { get; set; }
        public virtual ShopManager ShopManager { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
