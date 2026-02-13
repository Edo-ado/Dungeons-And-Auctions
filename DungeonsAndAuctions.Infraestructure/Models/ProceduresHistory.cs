using System;
using System.Collections.Generic;

namespace D_A.Infraestructure.Models;

public partial class ProceduresHistory
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProcedureType { get; set; }

    public decimal Amount { get; set; }

    public DateOnly CompletionDate { get; set; }

    public virtual Users User { get; set; } = null!;
}
