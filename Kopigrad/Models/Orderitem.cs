using System;
using System.Collections.Generic;

namespace Kopigrad.Models
{
    public partial class Orderitem
    {
        public int IdOrderItems { get; set; }
        public int IdOrder { get; set; }
        public string FilePath { get; set; } = null!;

        public virtual Order IdOrderNavigation { get; set; } = null!;
    }
}
