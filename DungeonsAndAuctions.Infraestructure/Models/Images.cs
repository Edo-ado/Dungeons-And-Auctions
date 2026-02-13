using System;
using System.Collections.Generic;

namespace D_A.Infraestructure.Models;

public partial class Images
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public byte[] ImageData { get; set; } = null!;

    public virtual ICollection<Objects> Objects { get; set; } = new List<Objects>();
}
