using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Miniservice
{
    public int IdMiniService { get; set; }

    public int IdService { get; set; }

    public string NameMunuSercise { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual Service IdServiceNavigation { get; set; } = null!;
}
