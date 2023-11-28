using System;
using System.Collections.Generic;

namespace ProServise.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Fname { get; set; } = null!;

    public string Sname { get; set; } = null!;

    public string? Lname { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Repair> Repairs { get; set; } = new List<Repair>();
}
