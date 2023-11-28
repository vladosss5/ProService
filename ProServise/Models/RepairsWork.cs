using System;
using System.Collections.Generic;

namespace ProServise.Models;

public partial class RepairsWork
{
    public int IdList { get; set; }

    public int IdRepair { get; set; }

    public int IdWork { get; set; }

    public virtual Repair IdRepairNavigation { get; set; } = null!;

    public virtual Work IdWorkNavigation { get; set; } = null!;
}
