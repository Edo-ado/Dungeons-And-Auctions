using System;
using System.Collections.Generic;

namespace D_A.Infraestructure.Models;

public partial class AuctionBidHistory
{
    public int UserId { get; set; }

    public int AuctionId { get; set; }

    public DateTime BidDate { get; set; }

    public decimal Amount { get; set; }

    public bool IsLastBid { get; set; }

    public string Id { get; set; } = null!;

    public virtual Auctions Auction { get; set; } = null!;

    public virtual ICollection<AuctionWinner> AuctionWinner { get; set; } = new List<AuctionWinner>();

    public virtual Users User { get; set; } = null!;
}
