using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProServise.Models;

public partial class Work
{
    public int IdWork { get; set; }

    public string Name { get; set; } = null!;

    public float TotalPrice { get; set; }

    public float Discount { get; set; } = 0;
    [NotMapped] public bool SelectWork { get; set; } = false;

    public virtual ICollection<RepairsWork> RepairsWorks { get; set; } = new List<RepairsWork>();
}
