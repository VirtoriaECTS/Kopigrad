using System;
using System.Collections.Generic;

namespace Kopigrad.Models
{
    public partial class Miniservice
    {
        public Miniservice()
        {
            Columnnames = new HashSet<Columnname>();
            Materials = new HashSet<Material>();
            Tableminiservices = new HashSet<Tableminiservice>();
        }

        public int IdMiniService { get; set; }
        public int IdService { get; set; }
        public string NameMiniServise { get; set; } = null!;
        public string TopName { get; set; } = null!;
        public string BottomName { get; set; } = null!;

        public virtual Service IdServiceNavigation { get; set; } = null!;
        public virtual ICollection<Columnname> Columnnames { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
        public virtual ICollection<Tableminiservice> Tableminiservices { get; set; }
    }
}
