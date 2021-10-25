using System;
using System.Collections.Generic;
using System.Text;

namespace QuickDeliveryApp.Models
{
    class UserType
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        public int TypeUserId { get; set; }
        public string TypeUser { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
