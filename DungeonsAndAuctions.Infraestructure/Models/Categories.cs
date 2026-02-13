using System;
using System.Collections.Generic;

namespace D_A.Infraestructure.Models;

public partial class Categories
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Objects> Object { get; set; } = new List<Objects>();
}
