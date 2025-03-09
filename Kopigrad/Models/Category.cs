using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string NameCategory { get; set; } = null!;

    public virtual ICollection<Categoryservice> Categoryservices { get; set; } = new List<Categoryservice>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual Viewcategory? Viewcategory { get; set; }
}
