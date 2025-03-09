using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Service
{
    public int IdService { get; set; }

    public string NameService { get; set; } = null!;

    public string? ConditionText { get; set; }

    public string? Time { get; set; }

    public virtual ICollection<Categoryservice> Categoryservices { get; set; } = new List<Categoryservice>();
}
