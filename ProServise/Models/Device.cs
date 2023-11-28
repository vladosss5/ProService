using System;
using System.Collections.Generic;

namespace ProServise.Models;

public partial class Device
{
    public int IdDevice { get; set; }

    public string Manufacturer { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string? SerialNumber { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Repair> Repairs { get; set; } = new List<Repair>();
}
