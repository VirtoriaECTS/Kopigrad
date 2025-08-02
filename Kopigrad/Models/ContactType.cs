using System;
using System.Collections.Generic;

namespace Kopigrad.Models
{
    public partial class Contacttype
    {
        public Contacttype()
        {
            Orders = new HashSet<Order>();
        }

        public int ContactTypeId { get; set; }
        public string ContactTypeName { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
