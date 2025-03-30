using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public int IdMiniService { get; set; }

    public string NameCategory { get; set; } = null!;

    public virtual Miniservice IdMiniServiceNavigation { get; set; } = null!;

    public virtual Viewcategory? Viewcategory { get; set; }
}
