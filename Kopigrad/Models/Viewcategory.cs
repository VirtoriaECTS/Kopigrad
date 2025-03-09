using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Viewcategory
{
    public int IdViewCategory { get; set; }

    public int IdCategory { get; set; }

    public string NameView { get; set; } = null!;

    public virtual Category IdViewCategoryNavigation { get; set; } = null!;

    public virtual ICollection<Priceview> Priceviews { get; set; } = new List<Priceview>();
}
