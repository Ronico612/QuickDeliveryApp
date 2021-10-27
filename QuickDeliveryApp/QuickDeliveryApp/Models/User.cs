using System;
using System.Collections.Generic;
using System.Text;

namespace QuickDeliveryApp.Models
{
    class User
    {
        public User()
        {
            Orders = new List<Order>();
        }

        public int UserId { get; set; }
        
        public string UserFname { get; set; }
        public string UserLname { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public DateTime? UserBirthDate { get; set; }
        public bool IsAdmin { get; set; }
        public bool? HasDiscount { get; set; }
        public string UserAdress { get; set; }
        public string UserCity { get; set; }
        public int? NumCreditCard { get; set; }
        public int? NumCode { get; set; }
        public int? ValidityCreditCard { get; set; }

        public virtual UserType TypeUser { get; set; }
        public virtual DelPerson DelPerson { get; set; }
        public virtual ShopManager ShopManager { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
