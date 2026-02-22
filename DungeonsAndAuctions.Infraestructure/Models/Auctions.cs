using System;
using System.Collections.Generic;

namespace D_A.Infraestructure.Models;

public partial class Auctions
{
    public int Id { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public bool IsActive { get; set; }

    public decimal BasePrice { get; set; }

    public decimal IncrementoMinimo { get; set; }

    public int Idstate { get; set; }

    public int Idusercreator { get; set; }

    public int Idobject { get; set; }

    public virtual ICollection<AuctionBidHistory> AuctionBidHistory { get; set; } = new List<AuctionBidHistory>();

    public virtual ICollection<AuctionWinner> AuctionWinner { get; set; } = new List<AuctionWinner>();

    public virtual Objects IdobjectNavigation { get; set; } = null!;

    public virtual AuctionState IdstateNavigation { get; set; } = null!;

    public virtual Users IdusercreatorNavigation { get; set; } = null!;

    public virtual ICollection<Payment> Payment { get; set; } = new List<Payment>();
}
