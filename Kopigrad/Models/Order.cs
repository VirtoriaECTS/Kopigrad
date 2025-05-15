using System;
using System.Collections.Generic;

namespace Kopigrad.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public string Contact { get; set; } = null!;

    public string NameUser { get; set; } = null!;

    public int IdStatus { get; set; }

    public DateTime DataOrder { get; set; }

    public int ContactTypeId { get; set; }

    public int IdTableMiniService { get; set; }

    public int Count { get; set; }

    public decimal Price { get; set; }

    public virtual ContactType ContactType { get; set; } = null!;

    public virtual Status IdStatusNavigation { get; set; } = null!;

    public virtual Tableminiservice IdTableMiniServiceNavigation { get; set; } = null!;

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();
}
