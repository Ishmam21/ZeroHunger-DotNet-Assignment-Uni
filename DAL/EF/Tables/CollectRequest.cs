using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class CollectRequest
{
    public int Id { get; set; }

    public int Resid { get; set; }

    public int? Empid { get; set; }

    public string Status { get; set; } = null!;

    public DateTime Maxtime { get; set; }

    public virtual Employee? Emp { get; set; }

    public virtual Restaurant Res { get; set; } = null!;
}
