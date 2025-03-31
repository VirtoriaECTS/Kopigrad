using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Status
{
    public int IdStatus { get; set; }

    public string NameStatus { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
