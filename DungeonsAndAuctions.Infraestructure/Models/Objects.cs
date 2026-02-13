using System;
using System.Collections.Generic;

namespace D_A.Infraestructure.Models;

public partial class Objects
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public int? Year { get; set; }

    public string? Description { get; set; }

    public DateOnly RegistrationDate { get; set; }

    public decimal MarketPrice { get; set; }

    public bool IsActive { get; set; }

    public int? IdCondition { get; set; }

    public int? IdState { get; set; }

    public int? Idimage { get; set; }

    public virtual ICollection<Auctions> Auctions { get; set; } = new List<Auctions>();

    public virtual Conditions? IdConditionNavigation { get; set; }

    public virtual Qualities? IdStateNavigation { get; set; }

    public virtual Images? IdimageNavigation { get; set; }

    public virtual Users User { get; set; } = null!;

    public virtual ICollection<Categories> Category { get; set; } = new List<Categories>();
}
