using System;
using System.Collections.Generic;

namespace D_A.Infraestructure.Models;

public partial class AuctionHistory
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int AuctionId { get; set; }

    public bool IsActive { get; set; }

    public virtual Auctions Auction { get; set; } = null!;

    public virtual Users User { get; set; } = null!;
}
