using System;
using System.Collections.Generic;

namespace ProServise.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string Fname { get; set; } = null!;

    public string Sname { get; set; } = null!;

    public string? Lname { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Repair> Repairs { get; set; } = new List<Repair>();
}
