using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Requstсontent
{
    public int IdRequstContent { get; set; }

    public int IdRequst { get; set; }

    public int IdViewCategory { get; set; }

    public string Description { get; set; } = null!;

    public int Price { get; set; }

    public int Count { get; set; }

    public virtual Request IdRequstNavigation { get; set; } = null!;

    public virtual Viewcategory IdViewCategoryNavigation { get; set; } = null!;
}
