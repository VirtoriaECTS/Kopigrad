using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Viewcategory
{
    public int IdViewCategory { get; set; }

    public int IdCategory { get; set; }

    public int IdView { get; set; }

    public string NameStobets { get; set; } = null!;

    public virtual Category IdViewCategoryNavigation { get; set; } = null!;

    public virtual Viewmaterial IdViewNavigation { get; set; } = null!;

    public virtual ICollection<Price> Prices { get; set; } = new List<Price>();

    public virtual ICollection<Requstсontent> Requstсontents { get; set; } = new List<Requstсontent>();
}
