using System;
using System.Collections.Generic;

namespace ProServise.Models;

public partial class RepairHistory
{
    public int IdRepairHistory { get; set; }

    public DateTime? DateTime { get; set; }

    public int? IdRepairs { get; set; }

    public string? Status { get; set; }

    public virtual Repair? IdRepairsNavigation { get; set; }
}
