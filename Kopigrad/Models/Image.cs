using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Image
{
    public int IdImage { get; set; }

    public int IdCategory { get; set; }

    public string Image1 { get; set; } = null!;

    public virtual Category IdCategoryNavigation { get; set; } = null!;
}
