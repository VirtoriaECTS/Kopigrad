using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Price
{
    public int IdPrice { get; set; }

    public double Price1 { get; set; }

    public virtual ICollection<Priceview> Priceviews { get; set; } = new List<Priceview>();
}
