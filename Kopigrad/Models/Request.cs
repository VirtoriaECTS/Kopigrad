using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Request
{
    public int IdRequest { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int IdStatus { get; set; }

    public DateTime DataRequst { get; set; }

    public virtual Status IdStatusNavigation { get; set; } = null!;

    public virtual ICollection<Requstсontent> Requstсontents { get; set; } = new List<Requstсontent>();
}
