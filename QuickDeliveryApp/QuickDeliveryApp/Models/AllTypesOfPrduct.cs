using System;
using System.Collections.Generic;
using System.Text;

namespace QuickDeliveryApp.Models
{
    class AllTypesOfPrduct
    {
        public int AllTypesOfPrductId { get; set; }
        public int? ProductId { get; set; }
        public int? ProductTypeId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
