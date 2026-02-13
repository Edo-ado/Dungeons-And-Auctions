using System;
using System.Collections.Generic;

namespace D_A.Infraestructure.Models;

public partial class Genders
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Users> Users { get; set; } = new List<Users>();
}
