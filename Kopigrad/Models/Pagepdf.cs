using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Pagepdf
{
    public int IdPage { get; set; }

    public int IdStory { get; set; }

    public string Size { get; set; } = null!;

    public double Price { get; set; }

    public virtual Storypdf IdStoryNavigation { get; set; } = null!;
}
