using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phno { get; set; } = null!;

    public virtual ICollection<CollectRequest> CollectRequests { get; set; } = new List<CollectRequest>();
}
