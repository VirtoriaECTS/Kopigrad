using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Categoryservice
{
    public int IdCategoryService { get; set; }

    public int IdServie { get; set; }

    public int IdCategory { get; set; }

    public virtual Category IdCategoryNavigation { get; set; } = null!;

    public virtual Service IdServieNavigation { get; set; } = null!;
}
