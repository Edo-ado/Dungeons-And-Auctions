using System;
using System.Collections.Generic;

namespace D_A.Infraestructure.Models;

public partial class Users
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public int CountryId { get; set; }

    public int GenderId { get; set; }

    public string? AboutMe { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int RoleId { get; set; }

    public bool IsBlocked { get; set; }

    public DateOnly RegisterDate { get; set; }

    public DateOnly? LastLogin { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<AuctionBidHistory> AuctionBidHistory { get; set; } = new List<AuctionBidHistory>();


    public virtual ICollection<Auctions> Auctions { get; set; } = new List<Auctions>();

    public virtual Countries Country { get; set; } = null!;

    public virtual Genders Gender { get; set; } = null!;

    public virtual ICollection<Objects> Objects { get; set; } = new List<Objects>();

    public virtual ICollection<ProceduresHistory> ProceduresHistory { get; set; } = new List<ProceduresHistory>();

    public virtual Roles Role { get; set; } = null!;
}
