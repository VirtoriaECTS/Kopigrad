using System;
using System.Collections.Generic;

namespace Kopigrad.Models
{
    public partial class Storypdf
    {
        public Storypdf()
        {
            Pagepdfs = new HashSet<Pagepdf>();
        }

        public int IdStory { get; set; }
        public string NameFile { get; set; } = null!;
        public double AllPrice { get; set; }

        public virtual ICollection<Pagepdf> Pagepdfs { get; set; }
    }
}
