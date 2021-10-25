using System;
using System.Collections.Generic;
using System.Text;

namespace QuickDeliveryApp.Models
{
    class AllStatusOfOrder
    {
        public int AllStatusOfOrderId { get; set; }
        public int? OrderId { get; set; }
        public int? StatusOrderId { get; set; }
        public DateTime? StatusTime { get; set; }

        public virtual Order Order { get; set; }
        public virtual StatusOrder StatusOrder { get; set; }
    }
}
