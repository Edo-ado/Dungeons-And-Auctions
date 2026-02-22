using System;
using System.Collections.Generic;

namespace D_A.Infraestructure.Models;

public partial class AuctionState
{
    public int Idstate { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Auctions> Auctions { get; set; } = new List<Auctions>();
}
