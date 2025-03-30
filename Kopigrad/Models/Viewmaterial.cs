using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Viewmaterial
{
    public int IdView { get; set; }

    public string NameView { get; set; } = null!;

    public int Count { get; set; }

    public virtual ICollection<Viewcategory> Viewcategories { get; set; } = new List<Viewcategory>();
}
