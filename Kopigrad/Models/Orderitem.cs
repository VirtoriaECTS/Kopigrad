using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Orderitem
{
    public int IdOrderItems { get; set; }

    public int IdOrder { get; set; }

    public int IdTableMiniService { get; set; }

    public string FilePath { get; set; } = null!;

    public decimal Price { get; set; }

    public int Count { get; set; }

    public virtual Order IdOrderNavigation { get; set; } = null!;

    public virtual Tableminiservice IdTableMiniServiceNavigation { get; set; } = null!;
}
