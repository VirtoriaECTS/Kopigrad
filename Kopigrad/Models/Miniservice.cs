using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Miniservice
{
    public int IdMiniService { get; set; }

    public int IdService { get; set; }

    public string NameMiniServise { get; set; } = null!;

    public string TopName { get; set; } = null!;

    public string BottomName { get; set; } = null!;

    public virtual ICollection<Columnname> Columnnames { get; set; } = new List<Columnname>();

    public virtual Service IdServiceNavigation { get; set; } = null!;

    public virtual ICollection<Tableminiservice> Tableminiservices { get; set; } = new List<Tableminiservice>();
}
