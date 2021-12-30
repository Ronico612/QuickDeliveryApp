using System;
using System.Collections.Generic;
using System.Text;

namespace QuickDeliveryApp.Models
{
    public class StatusOrder
    {
        public StatusOrder()
        {
            AllStatusOfOrders = new HashSet<AllStatusOfOrder>();
            Orders = new HashSet<Order>();
        }

        public int StatusOrderId { get; set; }
        public string TypeStatus { get; set; }

        public virtual ICollection<AllStatusOfOrder> AllStatusOfOrders { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
