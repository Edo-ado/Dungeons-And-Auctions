using System;
using System.Collections.Generic;

namespace D_A.Infraestructure.Models;

public partial class Qualities
{
    public int Id { get; set; }

    public string? Quality { get; set; }

    public virtual ICollection<Objects> Objects { get; set; } = new List<Objects>();
}
