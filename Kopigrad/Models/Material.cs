using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Material
{
    public int IdMaterial { get; set; }

    public int IdMiniService { get; set; }

    public string NameMaterial { get; set; } = null!;

    public virtual Miniservice IdMiniServiceNavigation { get; set; } = null!;

    public virtual ICollection<Tableminiservice> Tableminiservices { get; set; } = new List<Tableminiservice>();
}
