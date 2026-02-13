using System;
using System.Collections.Generic;

namespace D_A.Infraestructure.Models;

public partial class Paymentstatus
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Payment> Payment { get; set; } = new List<Payment>();
}
