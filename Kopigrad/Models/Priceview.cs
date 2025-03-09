using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Priceview
{
    public int IdPriceCondition { get; set; }

    public int IdPrice { get; set; }

    public int IdView { get; set; }

    public virtual Price IdPriceNavigation { get; set; } = null!;

    public virtual Viewcategory IdViewNavigation { get; set; } = null!;
}
