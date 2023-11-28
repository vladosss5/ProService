using System;
using System.Collections.Generic;

namespace ProServise.Models;

public partial class Work
{
    public int IdWork { get; set; }

    public string Name { get; set; } = null!;

    public float TotalPrice { get; set; }

    public float? Discount { get; set; }

    public virtual ICollection<RepairsWork> RepairsWorks { get; set; } = new List<RepairsWork>();
}
