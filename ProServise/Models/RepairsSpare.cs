using System;
using System.Collections.Generic;

namespace ProServise.Models;

public partial class RepairsSpare
{
    public int IdList { get; set; }

    public int IdRepair { get; set; }

    public int IdSpare { get; set; }

    public virtual Repair IdRepairNavigation { get; set; } = null!;

    public virtual Spare IdSpareNavigation { get; set; } = null!;
}
