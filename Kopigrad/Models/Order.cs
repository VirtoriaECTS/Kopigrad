using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int IdStatus { get; set; }

    public DateTime DataOrder { get; set; }

    public virtual Status IdStatusNavigation { get; set; } = null!;

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();
}
