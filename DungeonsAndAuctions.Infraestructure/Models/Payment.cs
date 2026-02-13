using System;
using System.Collections.Generic;

namespace D_A.Infraestructure.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? AuctionId { get; set; }

    public int? WinnerUserId { get; set; }

    public decimal? Amount { get; set; }

    public int? PaymentStatusId { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? Dateconfirmed { get; set; }

    public virtual Auctions? Auction { get; set; }

    public virtual Paymentstatus? PaymentStatus { get; set; }

    public virtual AuctionWinner? WinnerUser { get; set; }
}
