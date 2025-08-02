using System;
using System.Collections.Generic;

namespace Kopigrad.Models
{
    public partial class Columnname
    {
        public Columnname()
        {
            Tableminiservices = new HashSet<Tableminiservice>();
        }

        public int IdColumnNames { get; set; }
        public int IdMiniService { get; set; }
        public string NameColumn { get; set; } = null!;

        public virtual Miniservice IdMiniServiceNavigation { get; set; } = null!;
        public virtual ICollection<Tableminiservice> Tableminiservices { get; set; }
    }
}
