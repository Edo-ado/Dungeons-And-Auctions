using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D_A.Infraestructure.Models;

public partial class AuctionWinner
{
    public int Actionid { get; set; }

    public int Winnerid { get; set; }

    public decimal Finalprice { get; set; }

    public DateTime Closeddate { get; set; }

    public int Bidwinningid { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Idauctionwinner { get; set; }

    public virtual Auctions Action { get; set; } = null!;

    public virtual AuctionBidHistory Bidwinning { get; set; } = null!;

    public virtual ICollection<Payment> Payment { get; set; } = new List<Payment>();
}