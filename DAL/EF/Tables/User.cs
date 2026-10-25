using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    // "Admin" | "Restaurant" | "Employee"
    public string Role { get; set; } = null!;

    // Links to Restaurant.Id or Employee.Id depending on role
    public int? LinkedId { get; set; }
}
