using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Price
{
    public int IdPrice { get; set; }

    public int IdView { get; set; }

    public double Price1 { get; set; }

    public virtual Viewcategory IdViewNavigation { get; set; } = null!;
}
