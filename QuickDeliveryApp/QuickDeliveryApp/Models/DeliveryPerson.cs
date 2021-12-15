using System;
using System.Collections.Generic;
using System.Text;

namespace QuickDeliveryApp.Models
{
    class DeliveryPerson
    {
        public DeliveryPerson()
        {
            Orders = new HashSet<Order>();
        }

        public int DeliveryPersonId { get; set; }

        public virtual User DeliveryPersonNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
