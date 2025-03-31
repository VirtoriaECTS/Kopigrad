using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Tableminiservice
{
    public int IdTableMiniService { get; set; }

    public int IdMiniService { get; set; }

    public int IdMaterial { get; set; }

    public int IdColumnNames { get; set; }

    public decimal Price { get; set; }

    public virtual Columnname IdColumnNamesNavigation { get; set; } = null!;

    public virtual Material IdMaterialNavigation { get; set; } = null!;

    public virtual Miniservice IdMiniServiceNavigation { get; set; } = null!;

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();
}
