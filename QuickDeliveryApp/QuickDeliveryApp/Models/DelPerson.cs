using System;
using System.Collections.Generic;
using System.Text;

namespace QuickDeliveryApp.Models
{
    class DelPerson
    {
        public DelPerson()
        {
            Orders = new HashSet<Order>();
        }

        public int DelPersonId { get; set; }

        public virtual User DelPersonNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
